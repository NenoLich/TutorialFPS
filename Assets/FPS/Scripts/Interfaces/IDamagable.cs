using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Interfaces
{
    public interface IDamagable
    {
        //void GetDamage(float damage);
        void GetDamage(float damage,Vector3 source);
    }
}
