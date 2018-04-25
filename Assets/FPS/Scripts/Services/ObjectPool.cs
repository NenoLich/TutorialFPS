using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Services
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject Bullet;
        [SerializeField] private int BulletPoolSize;

        private Stack<Bullet> _bulletPool;

        private void Awake()
        {
            for (int i = 0; i < BulletPoolSize; i++)
            {
                Bullet bullet = Instantiate(Bullet).GetComponent<Bullet>();
                AddInPool(bullet);
            }
        }

        private void AddInPool(Bullet bullet)
        {
            bullet.IsVisible = false;
            _bulletPool.Push(bullet);
        }

        public void ReleaseBullet(Bullet bullet)
        {
            AddInPool(bullet);
        }

        public Bullet AcquireBullet()
        {
            return _bulletPool.Peek() == null ? Instantiate(Bullet).GetComponent<Bullet>() : _bulletPool.Pop();
        }
    }
}

