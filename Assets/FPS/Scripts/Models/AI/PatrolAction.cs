using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Models.AI
{
    [CreateAssetMenu(menuName = "AI/Actions/Patrol")]
    public class PatrolAction : Action
    {
        public override void Act(AIModel aiModel)
        {
            Patrol(aiModel);
        }
        private void Patrol(AIModel aiModel)
        {
            aiModel.navMeshAgent.destination = aiModel.waypoints[aiModel.nextWayPoint].position;
            aiModel.navMeshAgent.isStopped = false;

            if (aiModel.navMeshAgent.remainingDistance <= aiModel.navMeshAgent.stoppingDistance && !aiModel.navMeshAgent.pathPending)
            {
                aiModel.nextWayPoint = (aiModel.nextWayPoint + 1) % aiModel.waypoints.Length;
            }

        }
    }

}


