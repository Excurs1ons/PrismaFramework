using PrismaFramework.GameLauncher.Localization;
using PrismaFramework.GameLauncher.UI;

namespace PrismaFramework.GameLauncher.Mock
{
    public class MockLocalizationService: ILocalizationService
    {
        public string GetText(string key, params object[] args)
        {
            throw new System.NotImplementedException();
        }

        public string GetText(LocalizationKey key, params object[] args)
        {
            throw new System.NotImplementedException();
        }

        public string GetText(LocalizedData data)
        {
            throw new System.NotImplementedException();
        }
    }
}