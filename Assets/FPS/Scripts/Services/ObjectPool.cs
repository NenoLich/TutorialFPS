using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Interfaces;
using UnityEngine;

namespace TutorialFPS.Services
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private KeyValuePair<IPoolable, int>[] Poolables;

        private List<TypedStack> _poolableStacks;

        private void Awake()
        {
            _poolableStacks = new List<TypedStack>(Poolables.Length);

            for (int i = 0; i < Poolables.Length; i++)
            {
                _poolableStacks[i]=new TypedStack();

                for (int j = 0; j < Poolables[i].Value; j++)
                {
                    IPoolable poolable = Instantiate(Poolables[i].Key.GetInstance()).GetComponent<IPoolable>();
                    AddInPoolByIndex(poolable, i);
                }
            }
        }

        private void AddInPool(IPoolable poolable)
        {
            int i = 0;
            while (poolable.GetType() != _poolableStacks[i].TypeOfpoolable)
            {
                i++;
            }

            AddInPoolByIndex(poolable, i);
        }

        private void AddInPoolByIndex(IPoolable poolable,int stackIndex)
        {
            poolable.GetInstance().transform.parent = gameObject.transform;
            _poolableStacks[stackIndex].Push(poolable);
        }

        public void ReleasePoolable(IPoolable poolable)
        {
            AddInPool(poolable);
        }

        public T AcquirePoolable<T>() where T:IPoolable
        {
            int i = 0;
            while (typeof(T) != _poolableStacks[i].TypeOfpoolable)
            {
                i++;
            }

            IPoolable poolable = _poolableStacks[i].Count == 0 ? Instantiate(Poolables[i].Key.GetInstance()).GetComponent<IPoolable>() : _poolableStacks[i].Pop();
            return (T)poolable;
        }
    }
}

