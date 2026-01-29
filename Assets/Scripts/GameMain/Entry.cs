using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace PrismaFramework.GameMain
{
    public static class Entry
    {
        public static void Start(LifetimeScope parentResolver)
        {
            // 1. 转换传过来的 AOT 容器
            var parent = parentResolver;

            // 2. 在场景中动态创建一个物体，并挂载热更层的 Scope
            var go = new GameObject("MainContext");
            var mainScope = go.AddComponent<MainLifetimeScope>();

            // 3. 关键：建立父子继承关系
            // 这样 MainLifetimeScope 里的类就能直接注入 Root 里的 IAssetProvider 了
            using (LifetimeScope.EnqueueParent(parent))
            {
                mainScope.Build();
            }
            
            Debug.Log("<color=cyan>[Prisma.Main]</color> VContainer 业务容器已下钻完成。");
        }
    }
}