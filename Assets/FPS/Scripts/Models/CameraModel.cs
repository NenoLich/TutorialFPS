using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Interfaces;
using TutorialFPS.Services.Data;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace TutorialFPS.Models
{
    public class CameraModel : BaseGameObject, ISavable
    {
        public Data Data
        {
            get
            {
                return new Data
                {
                    Name = Name,
                    Position = Position,
                    Rotation = Rotation,
                    Scale = Scale,
                    IsVisible = IsVisible
                };
            }
            set
            {
                Position = value.Position;
                Rotation = value.Rotation;
                Scale = value.Scale;
                IsVisible = value.IsVisible;
            }
        }
    }
}

