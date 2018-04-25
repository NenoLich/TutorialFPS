using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Interfaces
{
    public interface IInteractable
    {
        string InteractionText { get; }

        void Interact();
    }
}

