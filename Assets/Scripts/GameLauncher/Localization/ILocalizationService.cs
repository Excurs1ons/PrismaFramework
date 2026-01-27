using PrismaFramework.GameLauncher.Localization;

namespace PrismaFramework.GameLauncher.UI
{
    public interface ILocalizationService
    {
        string GetText(string key, params object[] args);
        string GetText(LocalizationKey key, params object[] args);
        string GetText(LocalizedData data);
    }
}