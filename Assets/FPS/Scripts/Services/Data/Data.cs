using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Services.Data
{
    public struct Data
    {
        public string Name;
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;
        public float HitPoints;
        public int CurrentWeaponID;
        public int[] WeaponsMagazine;
        public bool IsVisible;
        public int NextWayPoint;
        public int CurrentAiBehaviour;
    }
}

