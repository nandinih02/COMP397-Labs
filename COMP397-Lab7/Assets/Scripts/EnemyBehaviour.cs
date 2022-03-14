using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerBehaviour>().gameObject.transform;
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating(nameof(FindPlayer), 0.2f, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void FindPlayer()
    {
        agent.destination = player.position;
    }
}
