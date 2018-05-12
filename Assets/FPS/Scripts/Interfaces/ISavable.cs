using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Services.Data;
using UnityEngine;

namespace TutorialFPS.Interfaces
{
    public interface ISavable
    {
        Data Data { get; set; }
    }
}
