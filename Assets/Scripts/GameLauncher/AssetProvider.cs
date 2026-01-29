using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using PrismaFramework.GameLauncher.Boot;
using UnityEngine.AddressableAssets;

namespace PrismaFramework.GameLauncher
{
    public class AssetProvider : IAssetProvider
    {
        // 缓存字典：Key -> SharedHandle
        private readonly Dictionary<string, SharedAssetHandle> _loadedAssets = new();

        public async UniTask<T> LoadAssetAsync<T>(string key)
        {
            // 1. 如果已经加载过，直接引用计数 +1
            if (_loadedAssets.TryGetValue(key, out var sharedHandle))
            {
                sharedHandle.Retain();
                return sharedHandle.Get<T>();
            }

            // 2. 没加载过，走 Addressables 加载
            var handle = Addressables.LoadAssetAsync<T>(key);
            await handle.ToUniTask();

            // 3. 封装并存入缓存
            var newShared = new SharedAssetHandle(key, handle);
            _loadedAssets.Add(key, newShared);
        
            return newShared.Get<T>();
        }

        // 卸载接口
        public void Unload(string key)
        {
            if (_loadedAssets.TryGetValue(key, out var sharedHandle))
            {
                // 引用归零时，才从字典移除
                if (sharedHandle.Release())
                {
                    _loadedAssets.Remove(key);
                }
            }
        }
    }
}