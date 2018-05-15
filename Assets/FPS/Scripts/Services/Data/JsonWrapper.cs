using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Services.Data
{
    [Serializable]
    public class JsonWrapper<T>
    {
        public T[] Items;

        public JsonWrapper()
        {
            
        }

        public JsonWrapper(T[] items)
        {
            Items = items;
        }

        public static implicit operator T[] (JsonWrapper<T> value)
        {
            return value.Items;
        }
        public static implicit operator JsonWrapper<T>(T[] value)
        {
            return new JsonWrapper<T>(value);
        }

        public T this[int index]
        {
            get { return Items[index]; }
            set { Items[index] = value; }
        }

        public static U[] FromJson<U>(string json)
        {
            JsonWrapper<U> wrapper = JsonUtility.FromJson<JsonWrapper<U>>(json);
            return wrapper.Items;
        }

        public static string ToJson<U>(U[] array)
        {
            JsonWrapper<U> wrapper = new JsonWrapper<U>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }
    }

}

