using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrismaFramework.GameLauncher.Infrastructure.Interfaces;

namespace PrismaFramework.GameLauncher.Boot
{
    public static class GameEntryResolver
    {
        public static IGameEntry Resolve()
        {
            var asm = LoadHotfixAssembly();
            var entryType = asm.GetType("GameMain.Entry", throwOnError: true);

            return (IGameEntry)Activator.CreateInstance(entryType);
        }

        private static Assembly LoadHotfixAssembly()
        {
#if UNITY_EDITOR
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .First(a => a.GetName().Name == "Hotfix.Gameplay");
#else
        var bytes = File.ReadAllBytes(GetHotfixDllPath());
        return Assembly.Load(bytes);
#endif
        }
    }
}