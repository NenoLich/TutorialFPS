using System;
using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Interfaces;
using UnityEngine;

namespace TutorialFPS.Services
{
    public class TypedStack : Stack<IPoolable>
    {
        private Type _typeOfpoolable;

        public Type TypeOfpoolable
        {
            get
            {
                if (_typeOfpoolable==null)
                {
                    _typeOfpoolable = Peek().GetType();
                }

                return _typeOfpoolable;
            }

            set { _typeOfpoolable = value; }
        }

        public TypedStack()
        {
        }

        public TypedStack(Type typeOfpoolable)
        {
            TypeOfpoolable = typeOfpoolable;
        }
    }
}

