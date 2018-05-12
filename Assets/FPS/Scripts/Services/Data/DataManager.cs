using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Interfaces;
using UnityEngine;

namespace TutorialFPS.Services.Data
{
    public class DataManager
    {
        private ISerialize _serializer;

        public void SetData<T>() where T : ISerialize, new()
        {
            _serializer = new T();
        }

        public void Save(Data[] data,string path)
        {
            if (_serializer==null)
            {
                return;
            }

            _serializer.Save(data,path);
        }

        public Data[] Load(string path)
        {
            if (_serializer == null)
            {
                return null;
            }

            return _serializer.Load(path);
        }
    }
}