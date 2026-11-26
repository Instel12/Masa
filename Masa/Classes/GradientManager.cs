using System.Collections.Generic;
using Masa.Mods;
using UnityEngine;

namespace Masa.Classes
{
    class GradientManager
    {
        public static List<Color> Colors = new List<Color>()
        {
            new Color(0, 0, 0),
            new Color(0.5f, 0, 0),
            new Color(0, 0, 0),
        };
        public static float Speed = 0.5f;
        public static Color DisabledColor = Color.black;
        public static Color EnbaledColor = Color.red;

        public static Color GetColor()
        {
            if (Settings.CurrentTheme.IsRainbow) return Color.HSVToRGB((Time.time / 5) % 1, 1, 1);
            if (Settings.CurrentTheme.IsPastel) return Color.HSVToRGB((Time.time / 5) % 1, 0.4f, 1);
            
            if (Colors.Count == 1) return Colors[0];

            float time = Mathf.Repeat(Time.time * Speed, Colors.Count - 1);
            int index = Mathf.FloorToInt(time);
            float t = time - index;
            
            return Color.Lerp(Colors[index], Colors[index + 1], t);
        }
    }
}
