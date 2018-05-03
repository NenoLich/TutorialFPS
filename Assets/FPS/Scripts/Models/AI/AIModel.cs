using System.Collections;
using System.Collections.Generic;
using TutorialFPS;
using UnityEngine;

namespace TutorialFPS.Models.AI
{
    public class AIModel : BaseGameObject
    {
        public AIBehaviour currentBehaviour;
        public AIBehaviour remainBehaviour;
        [HideInInspector] public float timeElapsed;

        private Transform[] _waypoints;

        protected override void Awake()
        {
            base.Awake();
            _waypoints = Transform.Find("Waypoints").GetComponentsInChildren<Transform>();
        }

        private void Update()
        {
            currentBehaviour.UpdateBehaviour(this);
        }

        public void TransitionToBehaviour(AIBehaviour nextBehaviour)
        {
            if (nextBehaviour != remainBehaviour)
            {
                currentBehaviour = nextBehaviour;
                timeElapsed = 0;
            }
        }

        public bool CheckIfCountDownElapsed(float duration)
        {
            timeElapsed += Time.deltaTime;
            return (timeElapsed >= duration);
        }
    }
}

