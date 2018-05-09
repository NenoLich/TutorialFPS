using System.Collections;
using System.Collections.Generic;
using TutorialFPS.Models.AI;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Death")]
public class DeathAction : Action
{
    public override void Act(AIModel aiModel)
    {
        Death(aiModel);
    }

    private void Death(AIModel aiModel)
    {
        if (aiModel.isDead)
        {
            return;
        }

        aiModel.StopAllCoroutines();
        aiModel.isDead = true;
        aiModel.navMeshAgent.enabled = false;
        aiModel.Animator.SetTrigger("Death");
        Destroy(aiModel.InstanceObject, 4f);
    }
}
