using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
            NavMeshPath path=new NavMeshPath();
            aiModel.navMeshAgent.CalculatePath(aiModel.waypoints[aiModel.nextWayPoint].position, path);
            aiModel.navMeshAgent.SetPath(path);

            aiModel.SetAgentActive(true);

            if (aiModel.navMeshAgent.remainingDistance <= aiModel.navMeshAgent.stoppingDistance && !aiModel.navMeshAgent.pathPending)
            {
                aiModel.nextWayPoint = (aiModel.nextWayPoint + 1) % aiModel.waypoints.Length;
            }

        }
    }

}


