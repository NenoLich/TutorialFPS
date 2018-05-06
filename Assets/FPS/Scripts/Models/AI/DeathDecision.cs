using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Models.AI
{
    [CreateAssetMenu(menuName = "AI/Decision/DeathDecision")]
    public class DeathDecision : Decision
    {
        public override bool Decide(AIModel aiModel)
        {
            bool isDead = Death(aiModel);
            return isDead;
        }

        private bool Death(AIModel aiModel)
        {
            return aiModel.Health == 0;
        }
    }
}
