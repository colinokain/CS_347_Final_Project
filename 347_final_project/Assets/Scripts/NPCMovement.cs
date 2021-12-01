using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Vector3 goal;
    private float rotateSpeed = 3.0f;       // speed NPC rotates during survey state
    private float sightDistance = 17.0f;    // distance NPC can see player from
    private float senseDistance = 5.0f;     // distance NPC can "sense" the player from regardless of orientation or line of sight
    private GameObject[] locations;
    public string state;
    private float startingOrientation;

    private Vector3 lastKnownPos;
    private float wanderSpeed = 3.5f;       // speed NPC walks at during the wander state
    private float persueSpeed = 4.5f;       // speed NPC walks at during the persue state

    void Start()
    {
        locations = GameObject.FindGameObjectsWithTag("WanderLocation");
        state = "wander";
    }


    void Update()
    {
        GameObject player;
        Vector3 pos;
        UnityEngine.AI.NavMeshAgent agent;

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        if (playerInSight())
        {
            state = "persue";
            agent.speed = persueSpeed;
            lastKnownPos = GameObject.FindWithTag("Player").transform.position;
            agent.destination = lastKnownPos;
        }
        if (state == "survey")
        {
            Vector3 currentRotation = transform.eulerAngles;
            currentRotation.y += rotateSpeed;
            if (Mathf.Abs(currentRotation.y - startingOrientation) < rotateSpeed)
            {
                state = "wander";
                agent.speed = wanderSpeed;
                setRandomTarget();
            }
            transform.eulerAngles = currentRotation;
        }
        else if (agent.remainingDistance < 0.1f)
        {
            state = "survey";
            startingOrientation = transform.eulerAngles.y - (rotateSpeed * 2.0f);
        }
    }

    void setRandomTarget()
    {
        int index;
        UnityEngine.AI.NavMeshAgent agent;

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        index = Random.Range(0, locations.Length);
        goal = locations[index].transform.position;

        agent.destination = goal;
    }

    bool playerInSight()
    {
        GameObject player;
        float orientation;
        float x1, z1, x2, z2;
        Vector3 position, pos1, pos2;

        player = GameObject.FindWithTag("Player");
        position = transform.position;

        if (Vector3.Distance(player.transform.position, position) > sightDistance)
        {
            return false;
        }

        if (Vector3.Distance(player.transform.position, position) < senseDistance)
        {
            return true;
        }

        orientation = transform.eulerAngles.y * Mathf.Deg2Rad;

        x1 = position.x + (sightDistance * Mathf.Cos(orientation - 1.0f));
        z1 = position.z - (sightDistance * Mathf.Sin(orientation - 1.0f));
        pos1 = new Vector3(x1, 0.0f, z1);

        x2 = position.x - (sightDistance * Mathf.Cos(orientation + 1.0f));
        z2 = position.z + (sightDistance * Mathf.Sin(orientation + 1.0f));
        pos2 = new Vector3(x2, 0.0f, z2);


        if (!toLeft(transform.position, pos1, player.transform.position) && toLeft(transform.position, pos2, player.transform.position))
        {
            if (hasLineOfSight())
            {
                return true;
            }
        }

        return false;
    }

    bool toLeft(Vector3 pos1, Vector3 pos2, Vector3 q)
    {
        float det;
        det = (q.x - pos1.x) * (pos2.z - pos1.z) - (q.z - pos1.z) * (pos2.x - pos1.x);
        return (det > 0);
    }

    bool hasLineOfSight()
    {
        RaycastHit hit1, hit2, hit3;
        Vector3 position, playerPosition;

        position = transform.position;
        position.y += 1.0f;
        playerPosition = GameObject.FindWithTag("Player").transform.position;

        if (!Physics.Raycast(position, playerPosition - position, out hit1))
        {
            return false;
        }

        playerPosition.y += 1.8f;
        if (!Physics.Raycast(position, playerPosition - position, out hit2))
        {
            return false;
        }

        playerPosition.y -= 3.0f;
        if (!Physics.Raycast(position, playerPosition - position, out hit3))
        {
            return false;
        }

        return (hit1.collider.tag == "Player" || hit2.collider.tag == "Player" || hit3.collider.tag == "Player");
    }
}
