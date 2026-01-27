using Cysharp.Threading.Tasks;
using R3;
namespace PrismaFramework.GameLauncher.Infrastructure.Interfaces
{
    public interface IDownloadService
    {
        // 0.0 ~ 1.0 的进度流 (UI 只需要订阅这个)
        ReadOnlyReactiveProperty<float> Progress { get; }
        
        // 当前下载状态描述 (比如 "正在下载资源 3/10MB...")
        ReadOnlyReactiveProperty<string> StateDescription { get; }

        // 检查是否有更新
        UniTask<bool> CheckUpdateAsync();

        // 开始下载
        UniTask StartDownloadAsync();
    }
}