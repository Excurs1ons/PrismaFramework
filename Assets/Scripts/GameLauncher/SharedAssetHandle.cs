using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PrismaFramework.GameLauncher
{
    public class SharedAssetHandle
    {
        private AsyncOperationHandle _handle;
        private int _refCount;
        public string Key { get; private set; }

        public SharedAssetHandle(string key, AsyncOperationHandle handle)
        {
            Key = key;
            _handle = handle;
            _refCount = 1; // 创建时默认计数为 1
        }

        public T Get<T>() => (T)_handle.Result;

        // 增加引用
        public void Retain()
        {
            _refCount++;
            Debug.Log($"[Res] Retain {Key}: {_refCount}");
        }

        // 释放引用 (返回是否真的需要卸载)
        public bool Release()
        {
            _refCount--;
            Debug.Log($"[Res] Release {Key}: {_refCount}");
            
            if (_refCount <= 0)
            {
                if (_handle.IsValid())
                {
                    Addressables.Release(_handle);
                }
                return true; // 告诉 Manager 把它从缓存里删掉
            }
            return false;
        }
    }
}