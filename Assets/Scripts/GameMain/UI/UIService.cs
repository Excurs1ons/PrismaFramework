using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrismaFramework.GameMain.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace PrismaFramework.GameLauncher.UI
{
    public class UIService : IUIService
    {
        //虽然我们叫它“栈”（Stack），但在游戏 UI 管理中，它并不总是遵循 LIFO (Last In, First Out) 原则。
        // 【新增】UI 栈，记录打开顺序
        private readonly LinkedList<UIWindow> _windowStack = new();
        private readonly Dictionary<int, UIWindow> _allWindows = new();

        public async UniTask<T> OpenAsync<T>(object args = null) where T : UIWindow
        {
            string key = typeof(T).Name;

            // 1. 加载资源并获取句柄
            // 注意：这里 LoadAssetAsync 返回的是 handle
            // 如果用 _assetProvider 封装过，需要确保它能返回 handle 或者提供 Release 方法
            var handle = Addressables.LoadAssetAsync<GameObject>(key);
            GameObject prefab = await handle.ToUniTask();

            // ... 实例化 ...
            var instance = Object.Instantiate(prefab);
            var window = instance.GetComponent<T>();
            // 2. 把句柄塞给窗口
            window.AssetHandle = handle;

            // ... 后续逻辑 ...
            return window;
        }

        public void Close(int instanceId)
        {
            if (_allWindows.TryGetValue(instanceId, out var window))
            {
                // 1. 销毁 GameObject
                Object.Destroy(window.gameObject);

                // 2. 【核心】释放 Addressables 引用计数
                if (window.AssetHandle.IsValid())
                {
                    Addressables.Release(window.AssetHandle);
                }

                _allWindows.Remove(instanceId);
            }
        }

        public UniTask StartAsync(CancellationToken cancellation)
        {
            return UniTask.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}