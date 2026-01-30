using System.Threading;
using Cysharp.Threading.Tasks;
using PrismaFramework.GameLauncher.Infrastructure.Interfaces;
using UnityEngine;

namespace PrismaFramework.GameMain
{
    public class MainEntry : IGameEntry
    {
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            // 在场景中动态创建一个物体，并挂载热更层的 Scope
            var go = new GameObject("MainContext");
            var mainScope = go.AddComponent<MainLifetimeScope>();
            Debug.Log("<color=cyan>[PrismaFramework.GameMain]</color> 已进入热更层");
        }
    }
}