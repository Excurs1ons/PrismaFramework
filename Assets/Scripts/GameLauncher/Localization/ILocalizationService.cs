using R3;

namespace PrismaFramework.GameLauncher.Localization
{
    public interface ILocalizationService
    {
        // 全局语言版本号 (0, 1, 2...)
        // 这是一个极轻量的“心跳”，View 只要监听这个 int 变化
        ReadOnlyReactiveProperty<int> Revision { get; }
        string GetText(string key, params object[] args);
        string GetText(LocalizationKey key, params object[] args);
        string GetText(LocalizedData data);
    }
}