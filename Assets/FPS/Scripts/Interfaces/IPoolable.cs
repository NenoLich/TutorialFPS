using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Interfaces
{
    public interface IPoolable
    {
        GameObject GetInstance();
        void Release();
        void SetDefaults();
    }
}

