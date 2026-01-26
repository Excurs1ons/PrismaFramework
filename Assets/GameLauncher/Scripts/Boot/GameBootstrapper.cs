using Cysharp.Threading.Tasks;
using VContainer.Unity;
using Microsoft.Extensions.Logging;
using UnityEngine.SceneManagement;
using ZLogger.Unity.Runtime;
using ZLogger.Unity;
using ZLogger;
namespace GameLauncher.Boot
{
    public class GameBootstrapper: IAsyncStartable
    {
        private readonly ILogger _logger;
        // private readonly IAssetProvider _assetProvider;

        // VContainer 会自动注入这些依赖
        // public GameBootstrapper(ILogger<GameBootstrapper> logger, IAssetProvider assetProvider)
        // {
        //     _logger = logger;
        //     _assetProvider = assetProvider;
        // }
        public GameBootstrapper(ILogger<GameBootstrapper> logger)
        {
            _logger = logger;
        }
        public async UniTask StartAsync(System.Threading.CancellationToken cancellation)
        {
            _logger.LogInformation("Game Framework Starting...");

            // 1. 初始化资源系统 (YooAsset)
            // await _assetProvider.InitializeAsync();
        
            // 2. (如果是HybridCLR) 可以在这里下载 HotUpdate.dll 并 LoadMetadataForAOTAssembly
            // await DownloadAndLoadHotfixAssemblies();

            // 3. 加载配置表 (Luban)
            // await LoadConfigsAsync();

            // 4. 进入游戏主场景
            _logger.LogInformation("All Systems Go. Loading Main Menu...");
            // 注意：这里使用 YooAsset 加载场景，而不是 SceneManager
            // await _assetProvider.LoadSceneAsync("MainMenu");
            await SceneManager.LoadSceneAsync("MainMenu").ToUniTask(cancellationToken: cancellation);
        }
    }
}