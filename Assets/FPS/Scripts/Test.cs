using System.Collections;
using System.Collections.Generic;
using TutorialFPS;
using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform wayp;

	void Start ()
	{
	    agent = GetComponent<NavMeshAgent>();

	}
	
	// Update is called once per frame
	void Update ()
	{
	    transform.LookAt(Main.Player.transform);

	}
}
