using System;

namespace PrismaFramework.GameLauncher.Localization
{
    public class LocalizationTextAttribute : Attribute
    {
        public readonly string text;

        public LocalizationTextAttribute(string text)
        {
            this.text = text;
        }
    }
}