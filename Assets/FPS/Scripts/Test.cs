using System.Collections;
using System.Collections.Generic;
using TutorialFPS;
using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    public Transform wayp;

	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	    transform.LookAt(Main.Player.transform);

	}
}
