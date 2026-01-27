namespace PrismaFramework.GameLauncher.Localization
{
    public interface ILocalizationService
    {
        string GetText(string key, params object[] args);
        string GetText(LocalizationKey key, params object[] args);
        string GetText(LocalizedData data);
    }
}