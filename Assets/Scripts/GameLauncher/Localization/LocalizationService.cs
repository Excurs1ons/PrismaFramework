using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace PrismaFramework.GameLauncher.Localization
{
    public class LocalizationService : ILocalizationService
    {
        public ReadOnlyReactiveProperty<int> Revision => _revision;
        private readonly ReactiveProperty<int> _revision = new(0);

        public string GetText(string key, params object[] args)
        {
            throw new NotImplementedException();
        }

        public string GetText(LocalizationKey key, params object[] args)
        {
            throw new NotImplementedException();
        }

        public string GetText(LocalizedData data)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            
        }

        public void Dispose()
        {
            
        }

        public UniTask StartAsync(CancellationToken cancellation)
        {
            return UniTask.CompletedTask;
        }
    }
}