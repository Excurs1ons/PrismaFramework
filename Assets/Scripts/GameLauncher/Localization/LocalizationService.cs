using System;
using PrismaFramework.GameLauncher.Localization;

namespace PrismaFramework.GameLauncher.UI
{
    public class LocalizationService : ILocalizationService
    {
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
    }
}