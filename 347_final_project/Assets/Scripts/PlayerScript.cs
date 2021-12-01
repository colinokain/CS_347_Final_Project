using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [Header("Inspector-set Values:")]
    // public Text taskUI;
    // public GameObject canvas;  // for pausing
    CharacterController controller;

    public float speed;

    private string task;
    private int currentTaskNum;
    private string[] taskList = new string[] { "radio" };




    void Start()
    {
        currentTaskNum = 0;
        task = "radio";
        // taskUI.text = "Current Task: " + task;
        speed = 4.5f;
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {

        // Getting keyboard from input and building a velocity vector
        float vertical = speed * Input.GetAxis("Vertical");
        float horizontal = speed * Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);

        // Transforming built velocity vector to match the direction the player is currently facing
        movement = transform.TransformDirection(movement);
        controller.SimpleMove(movement);    // Moving character controller

        // Rotating player character based off mouse input
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 2f);
        transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y") * 2f);

        // Setting character's z rotation to 0 to avoid awkward camera angles
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.z = 0;
        transform.eulerAngles = currentRotation;

        print(lookingAt());
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
        // taskUI.text = "Current Task: " + taskList[currentTaskNum];
        currentTaskNum++;
        task = taskList[currentTaskNum];
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

}
