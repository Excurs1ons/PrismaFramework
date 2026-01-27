namespace PrismaFramework.GameLauncher.Localization
{
    public struct LocalizedData
    {
        public string Key; // 本地化 Key，例如 "DOWNLOAD_PROGRESS"
        public object[] Args; // 动态参数，例如 ["3.5MB", "10MB"]

        public static LocalizedData Create(string key, params object[] args)
        {
            return new LocalizedData { Key = key, Args = args };
        }

        // 使用枚举的实现
        public static LocalizedData Create(LocalizationKey key, params object[] args)
        {
            return new LocalizedData { Key = key.ToString(), Args = args };
        }

        public void Set(string key,params object[] args)
        {
            Key = key;
            Args = args;
        }

        public void Set(LocalizationKey resourceDownloaded,params object[] args)
        {
            Key = resourceDownloaded.ToString();
            Args = args;
        }
    }
}