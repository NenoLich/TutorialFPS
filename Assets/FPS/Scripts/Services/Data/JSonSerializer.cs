using System.Collections;
using System.Collections.Generic;
using System.IO;
using TutorialFPS.Interfaces;
using UnityEngine;

namespace TutorialFPS.Services.Data
{
    public class JSonSerializer : ISerialize
    {
        public static string DefaultDirectory = Path.Combine(Application.dataPath, @"/Saves/");

        public Data Load(string path)
        {
            string str = File.ReadAllText(Path.Combine(DefaultDirectory, path));
            return JsonUtility.FromJson<Data>(str);
        }

        public void Save(Data data, string path)
        {
            string str = JsonUtility.ToJson(Path.Combine(DefaultDirectory, path));
            File.WriteAllText(path, str);
        }
    }
}

