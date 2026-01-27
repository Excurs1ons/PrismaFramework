using System.Threading;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using MessagePipe;
using Microsoft.Extensions.Logging;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace PrismaFramework.GameLauncher.Boot
{
    // 演示事件定义
    public class GameEvent { }
    public class PlayerEvent { public int Id; public string Name; }

    [UsedImplicitly]
    public class GameBootstrapper : IAsyncStartable
    {
        private readonly ILogger _logger;

        private readonly LifetimeScope _rootScope;
        private readonly IAssetProvider _assetProvider;
        
        private readonly ISubscriber<GameEvent> _gameEventSub;
        private readonly ISubscriber<PlayerEvent> _playerEventSub;
        private readonly IPublisher<GameEvent> _gameEventPub;
        private readonly IPublisher<PlayerEvent> _playerEventPub;
        private readonly DisposableBagBuilder _disposables;

        // === VContainer: 依赖注入 ===
        // 解耦组件依赖，便于测试和维护
        public GameBootstrapper(LifetimeScope scope,
            IAssetProvider assetProvider,
            ILoggerFactory loggerFactory,
            ISubscriber<GameEvent> gameEventSub,
            ISubscriber<PlayerEvent> playerEventSub,
            IPublisher<GameEvent> gameEventPub,
            IPublisher<PlayerEvent> playerEventPub)
        {
            
            _logger = loggerFactory.CreateLogger<GameBootstrapper>();
            
            _rootScope = scope;
            _assetProvider = assetProvider;
            
            _gameEventSub = gameEventSub;
            _playerEventSub = playerEventSub;
            _gameEventPub = gameEventPub;
            _playerEventPub = playerEventPub;
            _disposables = DisposableBag.CreateBuilder();

            // === MessagePipe: 订阅事件 ===
            // 类型安全的事件总线，替代 C# 委托/事件，支持过滤和异步处理
            _gameEventSub.Subscribe(e => _logger.LogInformation("收到 GameEvent")).AddTo(_disposables);
            _playerEventSub.Subscribe(e => _logger.LogInformation("收到 PlayerEvent: Id={Id}, Name={Name}", e.Id, e.Name)).AddTo(_disposables);
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            // === ZLogger: 结构化日志 ===
            // 零分配、高性能，支持结构化日志输出
            _logger.LogInformation("=== PrismaFramework ===");

            // === UniTask: 异步操作 ===
            // 为什么: 替代 Coroutine，提供真正的 async/await 体验，性能更好
            await DemoUniTask(cancellation);

            // === MessagePipe: 发布事件 ===
            _gameEventPub.Publish(new GameEvent());
            _playerEventPub.Publish(new PlayerEvent { Id = 1, Name = "Player1" });

            // === ZString: 高性能字符串 ===
            // 为什么: 零分配字符串拼接，避免 GC 压力
            using (var sb = ZString.CreateStringBuilder())
            {
                sb.AppendFormat("玩家 {0} 进入游戏，等级 {1}", "Player1", 10);
                _logger.LogInformation(sb.ToString());
            }

            // === ULID: 唯一标识符 ===
            // 为什么: 有序且唯一的 ID，替代 GUID，更适合分布式系统
            var playerId = System.Ulid.NewUlid();
            _logger.LogInformation("生成玩家 ULID: {Ulid}", playerId.ToString());

            // === UniTask: 倒数 3 秒 ===
            _logger.LogInformation("所有系统初始化完毕，3 秒后进入游戏主场景...");
            for (int i = 3; i > 0; i--)
            {
                _logger.LogInformation("{i}...", i);
                await UniTask.Delay(1000, cancellationToken: cancellation);
            }


#if UNITY_EDITOR
            // --- 编辑器模式：直接调用 ---
            // 此时不需要加载 DLL，因为 GameMain 已经在当前的内存域里了
            await SceneManager.LoadSceneAsync(1).ToUniTask(cancellationToken: cancellation);
            GameMain.Entry.Start(_rootScope);
#else
            // --- 运行时模式：加载 DLL ---
#endif
        }

        private async UniTask DemoUniTask(CancellationToken cancellationToken)
        {
            // UniTask 支持标准的 await 模式
            await UniTask.Delay(100, cancellationToken: cancellationToken);

            // 支持基于帧的延迟
            await UniTask.DelayFrame(10, cancellationToken: cancellationToken);

            // Yield 到下一帧
            await UniTask.Yield();
        }
    }
}