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
            return Time.time - aiModel.lastTimeTargetUpdated > aiModel.fightingOutOfVisibleTime;
        }
    }
}
