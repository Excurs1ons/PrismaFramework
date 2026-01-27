using System.Collections.Generic;
using System.Reflection;

namespace PrismaFramework.GameLauncher.Localization
{
    public static class LocalizationExtensions
    {
        // 缓存字典：Enum -> 文本
        private static readonly Dictionary<LocalizationKey, string> Cache = new();

        public static string ToText(this LocalizationKey key, params object[] args)
        {
            // 1. 尝试从缓存拿
            if (!Cache.TryGetValue(key, out string text))
            {
                // 2. 缓存没有，执行反射 (只在第一次发生)
                var field = key.GetType().GetField(key.ToString());
                var attr = field.GetCustomAttribute<LocalizationTextAttribute>();

                // 3. 获取文本 (如果没有 Attribute，就降级显示 Enum 名字)
                text = attr != null ? attr.Text : key.ToString();
                
                // 4. 存入缓存
                Cache[key] = text;
            }

            // 5. 如果有参数，进行格式化
            if (args != null && args.Length > 0)
            {
                return string.Format(text, args);
            }

            return text;
        }
    }
}