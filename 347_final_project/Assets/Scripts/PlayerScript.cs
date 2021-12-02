using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [Header("Inspector-set Values:")]
    public Text taskUI;
    // public GameObject canvas;  // for pausing
    private CharacterController controller;

    public float speed;
    public float sensitivity = 2.0f;

    private bool paused = false;
    private string task;
    private int currentTaskNum;
    private string[] taskList = new string[] { "radio", "weapon", "key", "leave the house", "blackmail" };
    Vector3 movement;
    ArrayList inventory = new ArrayList();




    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                GameObject.FindWithTag("PauseMenu").GetComponent<Canvas>().enabled = false;
                paused = false;
                Time.timeScale = 1;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                GameObject.FindWithTag("PauseMenu").GetComponent<Canvas>().enabled = true;
                paused = true;
                Time.timeScale = 0;
            }
        }

        if (paused)
        {
            return;
        }

        // Getting keyboard from input and building a velocity vector
        float vertical = speed * Input.GetAxis("Vertical");
        float horizontal = speed * Input.GetAxis("Horizontal");
        movement = new Vector3(horizontal, 0.0f, vertical);

        controller = GetComponent<CharacterController>();
        // Transforming built velocity vector to match the direction the player is currently facing
        movement = transform.TransformDirection(movement);
        controller.SimpleMove(movement);    // Moving character controller


        // Rotating player character based off mouse input
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * sensitivity);
        transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y") * sensitivity);

        // Setting character's z rotation to 0 to avoid awkward camera angles
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.z = 0;
        transform.eulerAngles = currentRotation;

        if (Input.GetKeyDown(KeyCode.E))
        {
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
            else if (item.tag == "Door")
            {
                if (inventory.Contains("KeyHouse1"))
                {
                    WinGame();
                }
            }
        }
        catch
        {
        };
    }

    void WinGame()
    {
        // win the game :D
    }
}
