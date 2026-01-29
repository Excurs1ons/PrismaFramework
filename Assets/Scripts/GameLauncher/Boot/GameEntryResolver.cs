using System.Threading;
using Cysharp.Threading.Tasks;
using PrismaFramework.GameLauncher.Infrastructure.Interfaces;

namespace PrismaFramework.GameLauncher.Boot
{
    public class GameEntryResolver : IGameEntry
    {
        public UniTask StartAsync(CancellationToken cancellation)
        {
            
        }

        public static IGameEntry Resolve()
        {
#if UNITY_EDITOR
            return new GameMain.Entry();
#else
            HybridCLRLoader.LoadGameEntry();
#endif
        }
    }
}