using System.Collections;
using UnityEngine;

namespace TutorialFPS.Models.AI
{
    [CreateAssetMenu(menuName = "AI/Actions/Attack")]
    public class AttackAction : Action
    {
        public override void Act(AIModel aiModel)
        {
            if (!aiModel.CheckIfCountDownElapsed(aiModel.timeBetweenAttack) || aiModel.weapon._reload|| !DetectEnemy(aiModel))
            {
                return;
            }

            aiModel.Coroutine(() => Attack(aiModel));
            aiModel.ResetCountDown();
        }

        private IEnumerator Attack(AIModel aiModel)
        {
            aiModel.SetAgentActive(false);

            while (Quaternion.Angle(aiModel.Rotation,
                       Quaternion.LookRotation(aiModel.target - aiModel.eyes.position) * Quaternion.Euler(0, 45, 0)) > 5f)
            {
                aiModel.Rotation = Quaternion.Slerp(aiModel.Rotation,
                    Quaternion.LookRotation(new Vector3(aiModel.target.x - aiModel.eyes.position.x,
                        aiModel.target.y - aiModel.eyes.position.y,
                        aiModel.target.z - aiModel.eyes.position.z)) * Quaternion.Euler(0, 45, 0),
                    aiModel.attackTurnSpeed * 0.1f);

                yield return new WaitForSeconds(0.1f);
            }

            float elapsedTime = 0f;
            while (elapsedTime < aiModel.attackTime)
            {
                aiModel.Rotation = Quaternion.LookRotation(aiModel.target - aiModel.eyes.position) * Quaternion.Euler(0, 45, 0);

                aiModel.weapon.Fire();
                if (aiModel.weapon._reload)
                {
                    aiModel.Animator.SetBool("Reload",true);
                }
                else
                {
                    aiModel.Animator.SetTrigger("Fire");
                }

                yield return new WaitForSeconds(aiModel.weapon.FireRate);
                elapsedTime += aiModel.weapon.FireRate;
            }

            aiModel.ResetCountDown();
            aiModel.SetAgentActive(true);
        }

        private bool DetectEnemy(AIModel aiModel)
        {
            Vector3 enemyCenter = aiModel.enemy.transform.TransformPoint(aiModel.enemy.center);
            RaycastHit hit;

            Debug.DrawRay(aiModel.eyes.position, (enemyCenter - aiModel.eyes.position));
            if (Physics.SphereCast(aiModel.eyes.position, aiModel.enemy.radius, (enemyCenter - aiModel.eyes.position).normalized,
                    out hit, (enemyCenter - aiModel.eyes.position).magnitude)
                && hit.collider.CompareTag("Player"))
            {
                aiModel.UpdateTarget(hit.point);
                return true;
            }

            return false;
        }
    }
}
