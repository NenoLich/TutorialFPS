using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Services
{
    public class ObjectManager : MonoBehaviour
    {
        [SerializeField]
        private Weapon[] _weapons;

        public Weapon[] Weapons
        {
            get { return _weapons; }
        }
    }
}
