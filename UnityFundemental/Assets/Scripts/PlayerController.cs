using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {

    private Animator anim;
    private NavMeshAgent agent;
    private Transform target;

	// Use this for initialization
	void Awake ()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("target").transform;
    }

    void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);

        if (target != null && Vector3.Distance(transform.position, target.position) < 3f)
        {
            anim.SetFloat("Attack", 0);
        }
    }
}
