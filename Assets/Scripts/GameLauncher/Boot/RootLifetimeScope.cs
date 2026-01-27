using Microsoft.Extensions.Logging;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using ZLogger.Unity;
using MessagePipe;
using MessagePipe.VContainer;
namespace GameLauncher.Boot
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameConfig settings;

        protected override void Configure(IContainerBuilder builder)
        {
            // === ZLogger: 日志工厂 ===
            // 为什么: 零分配结构化日志，支持 LogLevel 过滤
            builder.RegisterInstance(CreateLogFactory());

            // === MessagePipe: 事件总线 ===
            // 为什么: 类型安全的发布订阅，解耦组件间通信
            var options = builder.RegisterMessagePipe();
            builder.RegisterMessageBroker<GameEvent>(options);
            builder.RegisterMessageBroker<PlayerEvent>(options);

            // === 游戏启动入口 ===
            builder.RegisterEntryPoint<GameBootstrapper>();
        }

        private static ILoggerFactory CreateLogFactory()
        {
            return LoggerFactory.Create(logging =>
            {
                logging.SetMinimumLevel(LogLevel.Trace);
                logging.AddZLoggerUnityDebug();
            });
        }
    }
}