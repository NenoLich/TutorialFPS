using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Models.AI
{
    [CreateAssetMenu(menuName="AI/AIBehaviour")]
    public class AIBehaviour : ScriptableObject
    {
        public Action[] actions;
        public Transition[] transitions;

        public void UpdateBehaviour(AIModel aiModel)
        {
            DoActions(aiModel);
            CheckTransitions(aiModel);
        }
        public void DoActions(AIModel aiModel)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].Act(aiModel);
            }

        }
        private void CheckTransitions(AIModel aiModel)
        {
            for (int i = 0; i < transitions.Length; i++)
            {
                bool decisionSucceded = transitions[i].decision.Decide(aiModel);
                if (decisionSucceded)
                {
                    aiModel.TransitionToBehaviour(transitions[i].trueBehaviour);
                }
                else
                    aiModel.TransitionToBehaviour(transitions[i].falseBehaviour);
            }
        }
    }
}
