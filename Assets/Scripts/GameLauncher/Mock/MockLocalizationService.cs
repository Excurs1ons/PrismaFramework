using System;
using System.Collections.Generic;
using PrismaFramework.GameLauncher.Localization;
using PrismaFramework.GameLauncher.UI;
using R3;

namespace PrismaFramework.GameLauncher.Mock
{
    public class MockLocalizationService : ILocalizationService
    {
        private readonly Dictionary<string, string> _localization = new Dictionary<string, string>()
        {
            { "DOWNLOAD_PROGRESS", "正在下载资源包... {0}%" },
            { "CONNECTING_SERVER", "正在连接资源服务器..." },
            { "INITIALIZING", "正在初始化..." },
            { "RESOURCE_DOWNLOADED", "资源已下载" },
            { "DL_ING", "正在下载资源... {0}%" },
            { "DL_DONE", "下载完成！" },
        };
        

        // 模拟切换语言
        public void SwitchLanguage()
        {
            _revision.Value++; // 版本号 +1，所有 UI 组件会感知到这个数字变了
        }
        public ReadOnlyReactiveProperty<int> Revision => _revision;
        private readonly ReactiveProperty<int> _revision = new(0);

        public string GetText(string key, params object[] args)
        {
            _localization.TryGetValue(key, out var format);

            if (string.IsNullOrEmpty(format))
            {
                //尝试枚举
                if (Enum.TryParse(typeof(LocalizationKey), key, out var enumKey))
                {
                    format = ((LocalizationKey)enumKey).ToText();
                }
            }
            if (string.IsNullOrEmpty(format))
            {
                format = key;
            }
            return string.Format(format, args);
        }

        public string GetText(LocalizationKey key, params object[] args)
        {
            return GetText(key.ToString(), args);
        }

        public string GetText(LocalizedData data)
        {
            return GetText(data.Key, data.Args);
        }
    }
}