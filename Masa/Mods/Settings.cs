using System.Collections.Generic;
using Masa.Classes;
using Masa.Menu;
using TMPro;
using UnityEngine;

namespace Masa.Mods
{
    class Settings
    {
        public class Theme
        {
            public List<Color> Colors;
            public Color Enabled = Color.red;
            public Color Disabled = Color.black;

            public Color TitleColor = Color.white;
            public Color ButtonColor = Color.white;

            public bool FlushButtons = false;
        }

        public static Theme CurrentTheme;
        public static void ShuffleTheme(object value)
        {
            GradientManager.Colors = ((Theme)value).Colors;
            CurrentTheme = (Theme)value;
            Main.FlushButtons = ((Theme)value).FlushButtons;
        }

        public static void ShuffleFont(object value) => Main.MenuFont = (TMP_FontAsset)value;
        public static void ShuffleFontType(object value) => Main.FontStyle = (FontStyles)value;
        public static void ShufflePageType(object value) => Main.PageType = (int)value;
        public static void ShuffleSafetyType(object value) => Safety.CurrentSafetyAction = (int) value;
    }
}
