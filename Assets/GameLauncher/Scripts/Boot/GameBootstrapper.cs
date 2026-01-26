using Cysharp.Threading.Tasks;
using VContainer.Unity;
using Microsoft.Extensions.Logging;
using UnityEngine.SceneManagement;
namespace GameLauncher.Boot
{
    public class GameBootstrapper: IAsyncStartable
    {
        private readonly ILogger _logger;
        // private readonly IAssetProvider _assetProvider;

        // VContainer自动注入依赖
        public GameBootstrapper(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GameBootstrapper>();
        }
        public async UniTask StartAsync(System.Threading.CancellationToken cancellation)
        {
            _logger.LogInformation("开始启动游戏...");

            // 1. 初始化资源系统 (YooAsset)
            // await _assetProvider.InitializeAsync();
        
            // 2. (如果是HybridCLR) 可以在这里下载 HotUpdate.dll 并 LoadMetadataForAOTAssembly
            // await DownloadAndLoadHotfixAssemblies();

            // 3. 加载配置表 (Luban)
            // await LoadConfigsAsync();

            // 4. 进入游戏主场景
            _logger.LogInformation("所有系统初始化完毕，进入游戏主场景");
            // 注意：这里使用 YooAsset 加载场景，而不是 SceneManager
            // await _assetProvider.LoadSceneAsync("MainMenu");
            await SceneManager.LoadSceneAsync(1).ToUniTask(cancellationToken: cancellation);
        }
    }
}