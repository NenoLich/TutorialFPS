using System;
using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Interfaces;
using UnityEngine;

namespace TutorialFPS.Services
{
    [DefaultExecutionOrder(-1)]
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private Poolable[] Poolables;

        private List<TypedStack> _poolableStacks;

        private void Awake()
        {
            _poolableStacks = new List<TypedStack>(Poolables.Length);

            for (int i = 0; i < Poolables.Length; i++)
            {
                IPoolable poolable = Poolables[i].gameObject.GetComponent<IPoolable>();
                if (poolable == null)
                {
                    _poolableStacks.Add(new TypedStack());
                    continue;
                }

                _poolableStacks.Add(new TypedStack(poolable.GetType()));

                for (int j = 0; j < Poolables[i].quantity; j++)
                {
                    poolable = Instantiate(Poolables[i].gameObject).GetComponent<IPoolable>();
                    AddInPoolByIndex(poolable, i);
                }
            }
        }

        private void AddInPool(IPoolable poolable)
        {
            for (int i = 0; i < _poolableStacks.Count; i++)
            {
                if (poolable.GetType() == _poolableStacks[i].TypeOfpoolable)
                {
                    AddInPoolByIndex(poolable, i);
                    return;
                }
            }
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

        public IPoolable AcquirePoolable(Type iPoolable)
        {
            for (int i = 0; i < _poolableStacks.Count; i++)
            {
                if (iPoolable == _poolableStacks[i].TypeOfpoolable)
                {
                    return _poolableStacks[i].Count == 0 ? Instantiate(Poolables[i].gameObject).GetComponent<IPoolable>() : _poolableStacks[i].Pop();
                }
            }

            return null;
        }
    }
}

