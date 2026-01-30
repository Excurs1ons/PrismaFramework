using GameLauncher.Boot;
using MessagePipe;
using Microsoft.Extensions.Logging;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;
using ZLogger.Unity;

namespace PrismaFramework.GameLauncher.Boot
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] private AssetReferenceT<GameConfig> settings;

        protected override void Configure(IContainerBuilder builder)
        {
            // === MessagePipe: 事件总线 ===
            // 类型安全的发布订阅，解耦组件间通信
            var options = builder.RegisterMessagePipe();
            builder.RegisterMessageBroker<GameEvent>(options);
            builder.RegisterMessageBroker<PlayerEvent>(options);
            // 1. 注册 Logger<> 类型，并指定单例
            // 2. 使用 .As() 告诉容器它实现了 ILogger<> 接口
            // builder.Register(typeof(Logger<>), Lifetime.Singleton).As(typeof(ILogger<>));

            builder.Register<IAssetProvider, AddressablesProvider>(Lifetime.Singleton);
            // === 游戏启动入口 ===
            builder.RegisterEntryPoint<GameBootstrapper>();
        }
    }
}