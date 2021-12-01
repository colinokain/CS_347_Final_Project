using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [Header("Inspector-set Values:")]
    public Text taskUI;
    // public GameObject canvas;  // for pausing
    CharacterController controller;

    public float speed;

    private bool canMove;
    private string task;
    private int currentTaskNum;
    private string[] taskList = new string[] { "radio", "weapon", "key", "leave the house", "blackmail" };
    Vector3 movement;
    ArrayList inventory = new ArrayList();




    void Start()
    {
        canMove = true;
        currentTaskNum = 0;
        task = "radio";
        taskUI.text = "Current Task: " + task;
        speed = 4.5f;
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {


        if (canMove)
        {

            // Getting keyboard from input and building a velocity vector
            float vertical = speed * Input.GetAxis("Vertical");
            float horizontal = speed * Input.GetAxis("Horizontal");
            movement = new Vector3(horizontal, 0.0f, vertical);

            // Transforming built velocity vector to match the direction the player is currently facing
            movement = transform.TransformDirection(movement);
            controller.SimpleMove(movement);    // Moving character controller

        }


        // Rotating player character based off mouse input
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 2f);
        transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y") * 2f);

        // Setting character's z rotation to 0 to avoid awkward camera angles
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.z = 0;
        transform.eulerAngles = currentRotation;

        print(lookingAt());

        if (Input.GetKeyDown(KeyCode.E))
        {
            canMove = false;
            Interact();
        }
    }

    void FixedUpdate()
    {
        if (taskList[currentTaskNum] != task)
        {
            UpdateTask();
        }
    }

    void UpdateTask()
    {
        taskUI.text = "Current Task: " + taskList[currentTaskNum];
        try
        {
            task = taskList[currentTaskNum];
        }
        catch
        {
            print("No more tasks available");
        };
    }

    GameObject lookingAt()
    {
        RaycastHit hit;
        Camera camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out hit))
        {
            return null;
        }

        return hit.transform.gameObject;
    }

    void Interact()
    {
        GameObject item = lookingAt();
        try { 
            if (item.tag == "Radio")
            {
                if (taskList[currentTaskNum] == task && task == "radio")
                {
                    currentTaskNum++;
                    UpdateTask();
                }
            }
            else if (item.tag == "Gun")
            {
                if (taskList[currentTaskNum] == task && task == "weapon")
                {
                    currentTaskNum++;
                    UpdateTask();
                }
            }
            else if (item.tag == "Key")
            {
                if (taskList[currentTaskNum] == task && task == "key")
                {
                    Destroy(item);
                    inventory.Add("KeyHouse1");
                    currentTaskNum++;
                    UpdateTask();
                }
            }
            else if (item.tag == "Door")
            {
                if (inventory.Contains("KeyHouse1"))
                {
                    TeleportToHouse2();
                    if (task != "blackmail")
                    {
                        currentTaskNum++;
                        UpdateTask();
                    }
                }
            }
            else if (item.tag == "Blackmail")
            {
                print(task);

                if (taskList[currentTaskNum] == task && task == "blackmail")
                {
                    Destroy(item);
                    inventory.Add("Blackmail");
                    currentTaskNum++;
                    UpdateTask();
                }
            }
        }
        catch
        {

        };
        canMove = true;
    }

    void TeleportToHouse2()
    {
        Vector3 test = new Vector3(-112.0118f, 5.546f, -135.415f);

        this.gameObject.transform.position = test;
    }
}
