using Masa.Menu;
using TMPro;
using UnityEngine;

namespace Masa.Classes
{
    class TMPHelper : MonoBehaviour
    {
        public TextMeshPro TMP;

        public TMP_FontAsset BackupFont;
        public Material BackupMaterial; // JUST IN CASE sombody is using the most useless mod on the menu 😭
        public float BackupSpacing;
        public FontStyles BackupStyle;

        void Update()
        {
            TMP.font = Main.MenuFont;

            if (Main.RedactText) TMP.fontMaterial.mainTexture = null;
            else TMP.fontMaterial.mainTexture = Main.MenuFont.material.mainTexture;

            TMP.fontStyle = Main.FontStyle;
        }
    }
}
