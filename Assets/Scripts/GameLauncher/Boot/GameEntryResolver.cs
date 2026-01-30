using System;
#if !UNITY_EDITOR
using System.IO;
#endif

using ZLinq;
using System.Reflection;
using System.Threading;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PrismaFramework.GameLauncher.Infrastructure.Interfaces;
using ZLogger;

namespace PrismaFramework.GameLauncher.Boot
{
    public static class GameEntryResolver
    {
        public static IGameEntry Resolve()
        {
            var asm = LoadHotfixAssembly();
            if (asm == null)
            {
                GameBootstrapper.Logger.LogInformation("Failed to load hotfix assembly");
                return null;
            }

            var entryType = asm.GetType("PrismaFramework.GameMain.MainEntry", throwOnError: true);

            return (IGameEntry)Activator.CreateInstance(entryType);
        }

        private static Assembly LoadHotfixAssembly()
        {
#if UNITY_EDITOR
            //输出全部程序集
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            using var sb = ZString.CreateStringBuilder();

            sb.AppendLine("========== [Assembly Dump Start] ==========");
            sb.AppendFormat("Total Assemblies: {0}", assemblies.Length);
            sb.AppendLine();
            foreach (var asm in assemblies)
            {
                sb.AppendLine(asm.GetName().Name);
            }

            GameBootstrapper.Logger.LogInformation(sb.ToString());
            return assemblies.AsValueEnumerable()
                .First(a => a.GetName().Name.Contains("GameMain", StringComparison.OrdinalIgnoreCase));

#else
            var bytes = File.ReadAllBytes(GetHotfixDllPath());
            var asm = Assembly.Load(bytes);
            if(asm == null)
            { 
                GameBootstrapper.Logger.LogInformation("Failed to load hotfix assembly");
            }
            return asm;
#endif
        }

        private static string GetHotfixDllPath()
        {
            return "";
        }
    }
}