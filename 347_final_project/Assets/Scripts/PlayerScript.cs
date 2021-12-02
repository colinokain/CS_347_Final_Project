using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [Header("Inspector-set Values:")]
    public Text taskUI;
    public Text narrativeTextUI;
    // public GameObject canvas;  // for pausing
    private CharacterController controller;

    public float speed;
    public float sensitivity = 2.0f;

    private bool paused = false;
    private string task;
    private int currentTaskNum;
    private string[] taskList = new string[] { "radio", "weapon", "blackmail", "voodoo", "harddrive", "key", "leave the house" };
    private string[] taskNarrations = new string[] {"I think I hear him coming... I need to find a radio to call for help.",
                                                    "Shoot... This radio has no batteries. I know my gun is around here. I need to find it.",
                                                    "Dang, I forgot I have no ammo because of the ammo shortage. I should go ahead and find the evidence on the killer.",
                                                    "There's the evidence I was looking for. I should see if the voodoo doll I have works. Worth a shot.",
                                                    "Nope. The doll doesn't work. The kid would probably like this as a gift though. Now I need to destroy my hard-drive.",
                                                    "Hard drive gone. Now he can't find my sensitive information such as my social security number. I should find the key and get out of here.",
                                                    "Got the key. Now it's time to leave."};
    Vector3 movement;
    ArrayList inventory = new ArrayList();




    void Start()
    {
        currentTaskNum = 0;
        task = "radio";
        taskUI.text = "Current Task: " + task;
        narrativeTextUI.text = taskNarrations[0];
        speed = 4.5f;
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Invoke("ClearNarrative", 5f);
    }

    private void Update()
    {

        Vector3 currentRotation;
        float horizontal;
        float vertical;
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
        vertical = speed * Input.GetAxis("Vertical");
        horizontal = speed * Input.GetAxis("Horizontal");
        movement = new Vector3(horizontal, 0.0f, vertical);

        controller = GetComponent<CharacterController>();
        // Transforming built velocity vector to match the direction the player is currently facing
        movement = transform.TransformDirection(movement);
        controller.SimpleMove(movement);    // Moving character controller


        // Rotating player character based off mouse input
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * sensitivity);
        transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y") * sensitivity);

        // Setting character's z rotation to 0 to avoid awkward camera angles
        currentRotation = transform.eulerAngles;
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
        narrativeTextUI.text = taskNarrations[currentTaskNum];
        try
        {
            task = taskList[currentTaskNum];
        }
        catch
        {
            print("No more tasks available");
        };
        Invoke("ClearNarrative", 5f);
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
                if (taskList[currentTaskNum] == task && task == "blackmail")
                {
                    Destroy(item);
                    inventory.Add("Blackmail");
                    currentTaskNum++;
                    UpdateTask();
                }
            }
            else if(item.tag== "VoodooDoll"){
                if (taskList[currentTaskNum] == task && task == "voodoo")
                {
                    Destroy(item);
                    inventory.Add("VoodooDoll");
                    currentTaskNum++;
                    UpdateTask();
                }
            }
            else if (item.tag == "HardDrive")
            {
                if (taskList[currentTaskNum] == task && task == "harddrive")
                {
                    Destroy(item);
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

    void ClearNarrative()
    {
        narrativeTextUI.text = "";
    }

    void WinGame()
    {
        narrativeTextUI.text = "I'm out! Now I can bring this information and get him taken away forever.";
        // win the game :D
    }
}
