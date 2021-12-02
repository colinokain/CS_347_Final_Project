using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Vector3 goal;
    private float rotateSpeed = 3.0f;       // speed NPC rotates during survey state
    private float sightDistance = 17.0f;    // distance NPC can see player from
    private float senseDistance = 5.0f;     // distance NPC can "sense" the player from regardless of orientation or line of sight
    private float fov = 0.5f;
    private GameObject[] locations;
    public string state;
    private int surveyFrames;

    private Vector3 lastKnownPos;
    private float wanderSpeed = 3.5f;       // speed NPC walks at during the wander state
    private float persueSpeed = 4.5f;       // speed NPC walks at during the persue state

    void Start()
    {
        locations = GameObject.FindGameObjectsWithTag("WanderLocation");
        state = "wander";       // Start NPC in wander state
    }


    void Update()
    {
        UnityEngine.AI.NavMeshAgent agent;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();    // geting navmesh agent

        GameObject player = GameObject.FindWithTag("Player");
        Vector3 position = transform.position;
        print(Vector3.Distance(player.transform.position, position));
        if (Vector3.Distance(player.transform.position, position) < 3f)
        {
            Application.Quit();

            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;                
            #endif
        }


        if (playerInSight())        // if npc can see the player
        {
            state = "persue";       // change state to persue
            agent.speed = persueSpeed;  // change npc speed to be slightly faster
            // setting destination for npc navmesh to the player's location
            lastKnownPos = GameObject.FindWithTag("Player").transform.position;
            agent.destination = lastKnownPos;
        }
        if (state == "survey")  // if state is survey, spin in a circle and survey the area
        {
            Vector3 currentRotation = transform.eulerAngles;
            currentRotation.y += rotateSpeed;
            transform.eulerAngles = currentRotation;
            surveyFrames++;

            if (surveyFrames > (360.0f / rotateSpeed))
            {
                state = "wander";
                agent.speed = wanderSpeed;
                setRandomTarget();
            }
        }
        else if (agent.remainingDistance < 0.1f)    // else if the state is wander or persue and the npc is close to its target, change state to survey
        {
            state = "survey";
            surveyFrames = 0;
        }
    }

    void setRandomTarget()  // randomly selects one of the location spheres locations to be its next target
    {
        int index;
        UnityEngine.AI.NavMeshAgent agent;

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        index = Random.Range(0, locations.Length);
        goal = locations[index].transform.position;

        agent.destination = goal;
    }

    bool playerInSight()    // returns true if the npc can see the player
    {
        GameObject player;
        float orientation;
        float x1, z1, x2, z2;
        Vector3 position, pos1, pos2;

        player = GameObject.FindWithTag("Player");
        position = transform.position;

        // immediately returns false if player is too far away
        if (Vector3.Distance(player.transform.position, position) > sightDistance)
        {
            return false;
        }

        // returns true if player is withing "senseDistance" without any further checks
        if (Vector3.Distance(player.transform.position, position) < senseDistance)
        {
            return true;
        }

        // calculating the two edge points of the NPC's FOV
        orientation = transform.eulerAngles.y * Mathf.Deg2Rad;

        x1 = position.x + (sightDistance * Mathf.Cos(orientation - fov));
        z1 = position.z - (sightDistance * Mathf.Sin(orientation - fov));
        pos1 = new Vector3(x1, 0.0f, z1);

        x2 = position.x - (sightDistance * Mathf.Cos(orientation + fov));
        z2 = position.z + (sightDistance * Mathf.Sin(orientation + fov));
        pos2 = new Vector3(x2, 0.0f, z2);

        // if the player is to the right of the left FOV edge and to the left of the right FOV edge, the player is within the NPC's FOV
        if (!toLeft(transform.position, pos1, player.transform.position) && toLeft(transform.position, pos2, player.transform.position))
        {
            if (hasLineOfSight())   // if there are no obstacles obstructing the player, return true
            {
                return true;
            }
        }

        return false;
    }

    // checks if point q is to the "left" of a line defined by two points, pos1 and pos2
    bool toLeft(Vector3 pos1, Vector3 pos2, Vector3 q)
    {
        float det;
        det = (q.x - pos1.x) * (pos2.z - pos1.z) - (q.z - pos1.z) * (pos2.x - pos1.x);
        return (det > 0);
    }

    // sends out 3 raycasts to determine if there are any obstacles obstructing the view of the NPC
    bool hasLineOfSight()
    {
        RaycastHit hit1, hit2, hit3;
        Vector3 position, playerPosition;

        position = transform.position;
        position.y += 1.0f;
        playerPosition = GameObject.FindWithTag("Player").transform.position;   // getting players position

        if (!Physics.Raycast(position, playerPosition - position, out hit1))    // sending a raycast to the middle of the player's body
        {
            return false;
        }

        playerPosition.y += 1.8f;
        if (!Physics.Raycast(position, playerPosition - position, out hit2))    // sending a raycast to the top of the player's body
        {
            return false;
        }

        playerPosition.y -= 3.0f;
        if (!Physics.Raycast(position, playerPosition - position, out hit3))    // sending a raycast to the bottom of the player's body
        {
            return false;
        }

        return (hit1.collider.tag == "Player" || hit2.collider.tag == "Player" || hit3.collider.tag == "Player");   // returning true if ANY of the rays hit the player
    }
}
