using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Services.Data;
using UnityEngine;

namespace TutorialFPS.Interfaces
{
    public interface ISerialize
    {
        void Save(Data[] data, string path,string password);
        Data[] Load(string path, string password);
    }
}

