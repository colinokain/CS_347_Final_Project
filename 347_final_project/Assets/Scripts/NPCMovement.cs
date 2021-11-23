using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform goal;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Currently just targets the player's position at all times
        // Later (once the map is done) this will be running on a state machine 
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 pos = player.transform.position;

        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = pos;
    }
}
