using System;
using System.Collections.Generic;
using Masa.Menu;

namespace Masa.Classes
{
    [Serializable]
    class Button
    {
        public Buttons.Categories Category;

        public string Title;
        public string Note;

        public bool OneShot;
        public bool Enabled;
        public bool Favorite;

        public Action EnableMethod;
        public Action DisableMethod;
        public Action Method;
        public Action PrefixMethod;

        public int ShuffleValue;
        public Action<object> ShuffleAction;
        public Dictionary<string, Func<object>> ShuffleValues;
    }
}
