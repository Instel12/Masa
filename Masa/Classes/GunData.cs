using System;
using UnityEngine;

namespace Masa.Classes
{
    [Serializable]
    class GunData
    {
        public Vector3 Position;
        public Vector3 Origin;
        public VRRig Rig;

        public bool Triggering;
        public bool Deployed;
    }
}
