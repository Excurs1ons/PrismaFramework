using Microsoft.Extensions.Logging;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using ZLogger.Unity;
namespace GameLauncher.Boot
{
    public class RootLifetimeScope : LifetimeScope
    {
        // 一个ScriptableObject配置，包含一些其他的ScriptableObject配置，每一个都需要支持热更
        [SerializeField] private GameConfig settings;
        
        protected override void Configure(IContainerBuilder builder)
        {
            // 1. 注册日志 (ZLogger)
            builder.RegisterInstance(CreateLogFactory()); 

            // 2. 注册资源管理 (YooAsset 包装器)
            //builder.Register<IAssetProvider, YooAssetProvider>(Lifetime.Singleton);

            // 3. 注册网络/配置服务
            // builder.Register<INetworkService, KcpNetworkService>(Lifetime.Singleton);
            // builder.Register<IConfigService, LubanConfigService>(Lifetime.Singleton);

            // 4. 注册游戏流程入口 (EntryPoint)
            // 这是一个纯 C# 类，继承 IAsyncStartable，是游戏的"Main函数"
            builder.RegisterEntryPoint<GameBootstrapper>();
        }
        private static ILoggerFactory CreateLogFactory()
        {
            // 配置 ZLogger，使用 ZString 格式化
            return LoggerFactory.Create(logging =>
            {
                logging.SetMinimumLevel(LogLevel.Trace);
                logging.AddZLoggerUnityDebug(); // 输出到 Unity Console
            });
        }
    }
}