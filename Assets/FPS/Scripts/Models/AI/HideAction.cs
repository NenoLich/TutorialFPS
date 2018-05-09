using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TutorialFPS.Models.AI
{
    [CreateAssetMenu(menuName = "AI/Actions/Hide")]
    public class HideAction : Action
    {
        public override void Act(AIModel aiModel)
        {
            Hide(aiModel);
        }
        private void Hide(AIModel aiModel)
        {
            if (aiModel.navMeshAgent.isStopped|| aiModel.navMeshAgent.remainingDistance > aiModel.navMeshAgent.stoppingDistance)
            {
                return;
            }
            
            float distance =
                aiModel.maxHideDistance * aiModel.maxHideDistance >
                (aiModel.Position - Main.Player.transform.position).sqrMagnitude
                    ? aiModel.maxHideDistance
                    : (aiModel.Position - Main.Player.transform.position).magnitude;
            float desiredDistance = 0;

            Vector3 prevVector = Main.Player.transform.position;
            Vector3 newVector = Vector3.zero;
            Vector3 closestCoverVector = Main.Player.transform.position;

            float targetXAngle = Mathf.Deg2Rad * Vector3.SignedAngle(aiModel.Position - Main.Player.transform.position, Vector3.forward, Vector3.up);
            float angle = 0;
            for (int i = 0; i < aiModel.maxHideRays; i++)
            {
                float x = Mathf.Sin(angle + Mathf.PI - targetXAngle);
                float z = Mathf.Cos(angle + Mathf.PI - targetXAngle);
                angle += 2 * Mathf.PI / aiModel.maxHideRays;

                Vector3 direction = new Vector3(aiModel.Position.x + x * distance, aiModel.Position.y,
                    aiModel.Position.z + z * distance);
                RaycastHit hit;

                Debug.DrawLine(aiModel.Position, direction, Color.red);

                newVector =
                    Physics.Linecast(aiModel.Position, direction, out hit)
                        ? hit.point
                        : direction;

                if (angle < Mathf.PI)
                {
                    if (CheckDistance((prevVector - aiModel.Position).sqrMagnitude, (newVector - aiModel.Position).sqrMagnitude, aiModel.navMeshAgent.radius))
                    {
                        desiredDistance = (prevVector - aiModel.Position).magnitude + aiModel.navMeshAgent.radius * 2;

                        closestCoverVector = (closestCoverVector - aiModel.Position).magnitude < desiredDistance
                            ? closestCoverVector
                            : aiModel.Position + (newVector - aiModel.Position).normalized * desiredDistance;
                    }
                }
                else if (CheckDistance((newVector - aiModel.Position).sqrMagnitude, (prevVector - aiModel.Position).sqrMagnitude, aiModel.navMeshAgent.radius))
                {
                    desiredDistance = (newVector - aiModel.Position).magnitude + aiModel.navMeshAgent.radius * 2;

                    closestCoverVector = (closestCoverVector - aiModel.Position).magnitude < desiredDistance
                        ? closestCoverVector
                        : aiModel.Position + (prevVector - aiModel.Position).normalized * desiredDistance;
                }

                prevVector = newVector;
            }

            NavMeshHit navMeshHit;
            NavMeshPath path = new NavMeshPath();

            if (NavMesh.SamplePosition(closestCoverVector, out navMeshHit, 1f, NavMesh.AllAreas) && 
                NavMesh.CalculatePath(aiModel.Position, navMeshHit.position, NavMesh.AllAreas, path))
            {
                aiModel.navMeshAgent.SetPath(path);
            }
        }
        private bool CheckDistance(float prevDistance, float newDistance, float agentRadius)
        {
            return newDistance + agentRadius * 4 > prevDistance;
        }
    }
}
