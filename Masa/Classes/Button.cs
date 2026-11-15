using System;
using System.Collections.Generic;
using Masa.Menu;

namespace Masa.Classes
{
    [Serializable]
    class Button
    {
        public Buttons.Categories Category;

        public string Title = "Untitled";
        public string Note = null;
        public string Description = "No description.";

        public bool OneShot = false;
        public bool Enabled = false;
        public bool Favorite = false;
        public bool Master = false;

        public Action EnableMethod = null;
        public Action DisableMethod = null;
        public Action Method = null;
        public Action PrefixMethod = null;

        public int ShuffleValue = 0;
        public Action<object> ShuffleAction = null;
        public Dictionary<string, Func<object>> ShuffleValues = null;
    }
}
