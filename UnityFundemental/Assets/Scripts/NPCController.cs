using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour {

    private Animator anim;
    private NavMeshAgent agent;

    public float PatrolTime = 10f;
    public float aggroRange = 10f;
    public Transform[] waypoints;

    private int index;
    private float speed, agentSpeed;
    private Transform player;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        if(agent != null) { agentSpeed = agent.speed; }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        index = Random.Range(0, waypoints.Length);

        InvokeRepeating("Tick", 0, 0.5f);

        if(waypoints.Length > 0)
        {
            InvokeRepeating("Patrol", 0, PatrolTime);
        }
    }

    void Patrol()
    {
        index = index == waypoints.Length - 1 ? 0 : index + 1;
    }

    void Tick()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
        agent.destination = waypoints[index].position;

        if(player != null && Vector3.Distance(transform.position, player.position) < aggroRange)
        {
            agent.destination = player.position;
            agent.speed = agentSpeed / 2;
        }
    }
}
