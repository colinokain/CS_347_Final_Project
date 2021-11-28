using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour {
    public Transform goal;

    //public Vector3 last_known_pos
    // this is for if the player leaves the NPCs line of sight. it will still check the last known position before going back to patrol mode
    // It is also important to keep in mind with the pauseing the game.


    void Start(){}


    void Update() {
        GameObject player;
        Vector3 pos;
        UnityEngine.AI.NavMeshAgent agent;

        // NPC will now only move if the game is not paused
        if (PauseMenu.isPaused) {
            pos = this.transform.position;
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            agent.destination = pos;
        }
        else {
            // Currently just targets the player's position at all times
            // Later (once the map is done) this will be running on a state machine

            player = GameObject.FindWithTag("Player");
            pos = player.transform.position;
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            agent.destination = pos;
        }
    }
}
