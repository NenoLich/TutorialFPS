using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Models.AI
{
    [CreateAssetMenu(menuName = "AI/Decision/StopFightingDecision")]
    public class StopFightingDecision : Decision
    {
        public override bool Decide(AIModel aiModel)
        {
            bool targetInisible = LoseTarget(aiModel);
            return targetInisible;
        }

        private bool LoseTarget(AIModel aiModel)
        {
            if (Time.time - aiModel.lastTimeTargetUpdated > aiModel.fightingOutOfVisibleTime)
            {
                aiModel.navMeshAgent.speed = aiModel.patrolSpeed;
                aiModel.Animator.SetBool("Fight", false);
                return true;
            }

            return false;
        }
    }
}
