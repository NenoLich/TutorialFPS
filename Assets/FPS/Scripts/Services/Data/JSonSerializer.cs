using System.Collections;
using System.Collections.Generic;
using System.IO;
using TutorialFPS.Interfaces;
using UnityEngine;

namespace TutorialFPS.Services.Data
{
    public class JSonSerializer : ISerialize
    {
        public static string DefaultDirectory = Application.dataPath;

        public Data[] Load(string path, string password)
        {
            string str = File.ReadAllText(Path.Combine(DefaultDirectory, path));
            return JsonWrapper<Data>.FromJson<Data>(str);

            //string str = File.ReadAllText(Path.Combine(DefaultDirectory, path));
            //return JsonWrapper<Data>.FromJson<Data>(AES.Decrypt(str, password));
        }

        public void Save(Data[] data, string path,string password)
        {
            string str = JsonWrapper<Data>.ToJson(data);
            File.WriteAllText(Path.Combine(DefaultDirectory, path), str);

            //string str = AES.Encrypt(JsonWrapper<Data>.ToJson(data), password);
            //File.WriteAllText(Path.Combine(DefaultDirectory, path), str);
        }
    }
}

