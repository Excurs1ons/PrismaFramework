using System;

namespace PrismaFramework.GameLauncher.Localization
{
    public class LocalizationTextAttribute : Attribute
    {
        public string Text { get; }
        public LocalizationTextAttribute(string text) => Text = text;
    }
}