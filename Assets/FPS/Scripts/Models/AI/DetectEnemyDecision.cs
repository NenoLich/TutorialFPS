﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialFPS.Models.AI
{
    [CreateAssetMenu(menuName = "AI/Decision/DetectEnemyDecision")]
    public class DetectEnemyDecision : Decision
    {
        public override bool Decide(AIModel aiModel)
        {
            bool targetVisible = DetectEnemy(aiModel);
            return targetVisible;
        }
        private bool DetectEnemy(AIModel aiModel)
        {
            Vector3 enemyCenter = aiModel.enemy.transform.TransformPoint(aiModel.enemy.center);
            RaycastHit hit;

            Debug.DrawRay(aiModel.eyes.position, (enemyCenter - aiModel.eyes.position));
            //if (Physics.SphereCast(aiModel.eyes.position, aiModel.enemy.radius, (enemyCenter - aiModel.eyes.position).normalized, 
            //        out hit, (enemyCenter - aiModel.eyes.position).magnitude)
            //        && hit.collider.CompareTag("Player"))
            //{
            //    aiModel.UpdateTarget(hit.point);
            //    aiModel.navMeshAgent.speed = aiModel.fightSpeed;
            //    aiModel.Animator.SetBool("Fight",true);
            //    return true;
            //}

            if ((Physics.Linecast(aiModel.eyes.position, Camera.main.transform.position, out hit)
                && hit.collider.CompareTag("Player"))||aiModel.isDamaged)
            {
                aiModel.UpdateTarget(Camera.main.transform.position);
                aiModel.navMeshAgent.speed = aiModel.fightSpeed;
                aiModel.Animator.SetBool("Fight", true);
                return true;
            }

            return false;
        }
    }

}

