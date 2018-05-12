using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Controllers;
using UnityEngine;

namespace TutorialFPS.Models.AI
{
    [CreateAssetMenu(menuName="AI/AIBehaviour")]
    public class AIBehaviour : ScriptableObject
    {
        public Action[] actions;
        public Transition[] transitions;

        public void UpdateBehaviour(AIModel aiModel,AIController aiController)
        {
            DoActions(aiModel);
            CheckTransitions(aiModel, aiController);
        }
        public void DoActions(AIModel aiModel)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].Act(aiModel);
            }

        }
        private void CheckTransitions(AIModel aiModel, AIController aiController)
        {
            for (int i = 0; i < transitions.Length; i++)
            {
                bool decisionSucceded = transitions[i].decision.Decide(aiModel);
                if (decisionSucceded)
                {
                    aiController.TransitionToBehaviour(transitions[i].trueBehaviour, aiModel);
                }
                else
                    aiController.TransitionToBehaviour(transitions[i].falseBehaviour, aiModel);
            }
        }
    }
}
