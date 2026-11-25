using System;
using System.Collections.Generic;
using GorillaTag.Cosmetics;
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
            public List<Color> Colors = new List<Color> { Color.black };
            public Color Enabled = Color.red;
            public Color Disabled = Color.black;

            public Color TitleColor = Color.white;
            public Color ButtonColor = Color.white;

            public bool FlushButtons = false;

            public bool IsBread = false;
            public bool IsRainbow = false;
            public bool IsPastel = false;
        }

        public static Theme CurrentTheme;
        public static void ShuffleTheme(object value)
        {
            GradientManager.Colors = ((Theme)value).Colors;
            CurrentTheme = (Theme)value;
            Main.FlushButtons = ((Theme)value).FlushButtons;
        }

        public static Dictionary<string, Func<object>> Themes = new Dictionary<string, Func<object>>
        {
            {
                "Masa", () => new Theme
                {
                    Colors = new List<Color>
                    {
                        Color.black,
                        new Color(0.5f, 0, 0),
                        Color.black
                    }
                }
            },
            {
                "Hope Reborn", () => new Theme
                {
                    Colors = new List<Color>
                    {
                        Color.black,
                        Color.midnightBlue,
                        Color.black
                    }
                }
            },
            {
                "GRAND", () => new Theme // i know alot about this menu cause it was my first one
                {
                    Colors = new List<Color> { Color.yellow },
                    FlushButtons = true
                }
            },
            {
                "Hudbud", () => new Theme
                {
                    Colors = new List<Color> { Color.cyan }
                }
            },
            {
                "Dark", () => new Theme
                {
                    Colors = new List<Color> { Color.black },
                    Disabled = new Color(0.1f, 0.1f, 0.1f),
                }
            },
            {
                "Super Dark", () => new Theme
                {
                    Colors = new List<Color> { Color.black }
                }
            },
            {
                "ii's Stupid", () => new Theme
                {
                    Colors = new List<Color>
                    {
                        new Color32(255, 128, 0, 128),
                        new Color32(255, 102, 0, 128),
                        new Color32(255, 128, 0, 128)
                    },
                    Disabled = new Color32(226, 74, 44, 255),
                    Enabled = new Color32(255, 102, 0, 128)
                }
            },
            {
                "Shiba Gold", () => new Theme
                {
                    Colors = new List<Color>
                    {
                        Color.black,
                        Color.yellow,
                        Color.black
                    },
                    Enabled = Color.magenta,
                    Disabled = Color.yellow,
                    ButtonColor = Color.black
                }
            },
            {
                "Seralyth", () => new Theme
                {
                    Colors = new List<Color> { new Color32(22, 22, 22, 128) },
                    Disabled = new Color32(118, 6, 252, 128),
                    Enabled = new Color32(98, 0, 232, 128)
                }
            },
            {
                "Mango's Mods", () => new Theme
                {
                    Colors = new List<Color> { Color.black },
                    Disabled = Color.blue,
                    Enabled = Color.magenta
                }
            },
            {
                "Aspect Menu", () => new Theme
                {
                    Colors = new List<Color> { Color.black },
                    Disabled = Color.black,
                    Enabled = Color.grey,
                    TitleColor = Color.red
                }
            },
            {
                "Rainbow", () => new Theme
                {
                    IsRainbow = true,
                }
            },
            {
                "Pastel Rainbow", () => new Theme
                {
                    IsPastel = true,
                }
            },
            {
                "Bread", () => new Theme
                {
                    IsBread = true,
                }
            },
            {
                "Fade Red", () => new Theme
                {
                    Colors = new List<Color>
                    {
                        Color.black,
                        Color.red,
                        Color.black
                    },
                    Enabled = Color.red
                }
            },
            {
                "Fade Orange", () => new Theme
                {
                    Colors = new List<Color>
                    {
                        Color.black,
                        Color.orange,
                        Color.black
                    },
                    Enabled = Color.orange
                }
            },
            {
                "Fade Yellow", () => new Theme
                {
                    Colors = new List<Color>
                    {
                        Color.black,
                        Color.yellow,
                        Color.black
                    },
                    Enabled = Color.yellow
                }
            },
            {
                "Fade Green", () => new Theme
                {
                    Colors = new List<Color>
                    {
                        Color.black,
                        Color.green,
                        Color.black
                    },
                    Enabled = Color.green
                }
            },
            {
                "Fade Blue", () => new Theme
                {
                    Colors = new List<Color>
                    {
                        Color.black,
                        Color.blue,
                        Color.black
                    },
                    Enabled = Color.blue
                }
            },
            {
                "Fade Purple", () => new Theme
                {
                    Colors = new List<Color>
                    {
                        Color.black,
                        Color.purple,
                        Color.black
                    },
                    Enabled = Color.purple
                }
            },
            {
                "lol", () => new Theme // im so fucking immature, what the fuck am i even doing
                {
                    Colors = new List<Color>
                    {
                        Color.black
                    },
                    Enabled = Color.orange,
                    Disabled = Color.black,
                    TitleColor = Color.orange
                }
            },
            {
                "Dark Inverted", () => new Theme
                {
                    Colors = new List<Color>
                    {
                        Color.white
                    },
                    Enabled = Color.white,
                    Disabled = Color.red,
                    TitleColor = Color.black,
                    ButtonColor = Color.black
                }
            },
            {
                "Ugly", () => new Theme
                {
                    Colors = new List<Color>
                    {
                        Color.darkGreen
                    },
                    Enabled = Color.magenta,
                    Disabled = Color.blue,
                    TitleColor = Color.red,
                    ButtonColor = Color.limeGreen
                }
            },
        };

        public static void ShuffleFont(object value) => Main.MenuFont = (TMP_FontAsset)value;
        public static void ShuffleFontType(object value) => Main.FontStyle = (FontStyles)value;
        public static void ShufflePageType(object value) => Main.PageType = (int)value;
        public static void ShuffleSafetyType(object value) => Safety.CurrentSafetyAction = (int)value;
        public static void ShuffleMenuSize(object value) => Main.MenuScale = (float)value;
        public static void ShufflePullStrength(object value) => Movement.PullStrength = (float)value;
    }
}
