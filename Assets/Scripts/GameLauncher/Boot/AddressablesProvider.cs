using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PrismaFramework.GameLauncher.Boot
{
    [UsedImplicitly]
    public class AddressablesProvider : IAssetProvider
    {
        private readonly ILogger<AddressablesProvider> _logger;

        // 简单的 Handle 缓存，防止句柄丢失导致的内存泄露
        // Key: 资源Key, Value: Handle
        private readonly Dictionary<string, AsyncOperationHandle> _handles = new();

        public async UniTask InitializeAsync()
        {
            _logger.LogInformation("Initializing Addressables...");
            await Addressables.InitializeAsync().ToUniTask();
        }

        public async UniTask<T> LoadAssetAsync<T>(string key) where T : Object
        {
            if (_handles.TryGetValue(key, out var existingHandle))
            {
                if (existingHandle.IsValid()) return existingHandle.Result as T;
            }

            var handle = Addressables.LoadAssetAsync<T>(key);
            _handles[key] = handle; // 缓存 Handle

            try
            {
                return await handle.ToUniTask();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Failed to load asset: {0}", key);
                throw;
            }
        }

        public async UniTask<GameObject> InstantiateAsync(string key, Transform parent = null)
        {
            // Addressables.InstantiateAsync 自动管理引用计数，通常不需要手动缓存 Handle
            // 但为了统一管理，建议根据项目需求决定是否封装
            return await Addressables.InstantiateAsync(key, parent).ToUniTask();
        }

        public void Release(object handle)
        {
            if (handle is AsyncOperationHandle opHandle)
            {
                Addressables.Release(opHandle);
            }
        }
    }
}