using Cysharp.Threading.Tasks;
using PrismaFramework.GameLauncher.Infrastructure.Interfaces;
using PrismaFramework.GameLauncher.Localization;
using R3;
using UnityEngine;

namespace PrismaFramework.GameLauncher.Mock
{
    public class MockDownloadService : IDownloadService
    {
        // R3 的 ReactiveProperty 用于驱动数据变化
        private readonly ReactiveProperty<float> _progress = new(0f);
        // private readonly ReactiveProperty<string> _stateDescription = new("初始化中...");
        private readonly ReactiveProperty<LocalizedData> _stateDescription =
            new(LocalizedData.Create(LocalizationKey.Initializing));

        public ReadOnlyReactiveProperty<float> Progress => _progress;
        // public ReadOnlyReactiveProperty<string> StateDescription => _stateDescription;
        public ReadOnlyReactiveProperty<LocalizedData> StateDescription => _stateDescription;

        public async UniTask<bool> CheckUpdateAsync()
        {
            // _stateDescription.Value = "正在连接资源服务器...";
            _stateDescription.Value.Set(LocalizationKey.ConnectingServer);
            
            // 假装网络延迟 1秒
            await UniTask.Delay(1000);
            return true; // 假装永远有更新
        }

        public async UniTask StartDownloadAsync()
        {
            _progress.Value = 0f;

            // 模拟下载过程：3秒内跑完进度条
            float duration = 3.0f;
            float time = 0f;

            while (time < duration)
            {
                time += Time.deltaTime;
                float p = Mathf.Clamp01(time / duration);

                // 更新数据
                _progress.Value = p;
                // _stateDescription.Value = $"正在下载资源包... {(p * 100):F1}%";
                _stateDescription.Value = LocalizedData.Create("DL_ING", (p * 100).ToString("F1"));
                // 等待下一帧
                await UniTask.Yield();
            }

            _progress.Value = 1f;
            // _stateDescription.Value = "资源解压完成";
            _stateDescription.Value.Set(LocalizationKey.ResourceDownloaded);
            // 再停顿一下让玩家看清 100%
            await UniTask.Delay(500);
        }
    }
}