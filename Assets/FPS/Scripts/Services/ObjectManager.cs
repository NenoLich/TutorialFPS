﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Services
{
    public class ObjectManager : MonoBehaviour
    {
        [SerializeField]
        private Weapon[] _weapons;
        [SerializeField]
        private Ammunition[] _ammunitions;

        public Weapon[] Weapons
        {
            get { return _weapons; }
        }

        public Ammunition[] Ammunitions
        {
            get { return _ammunitions; }
        }
    }
}
