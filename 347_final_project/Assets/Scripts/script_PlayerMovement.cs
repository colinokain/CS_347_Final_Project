using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_PlayerMovement : MonoBehaviour {
    public GameObject canvas;  // for pauseing
    CharacterController controller;

    public float speed;


    void Start() {
        speed = 4.5f;
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {

        float vertical = speed * Input.GetAxis("Vertical");
        float horizontal = speed * Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);
        movement = transform.TransformDirection(movement);
        controller.SimpleMove(movement);

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 2f);
        float mouse = Input.GetAxis("Mouse Y");
        transform.Rotate(new Vector3(-mouse * 1f, 0, 0));

        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.z = 0;
        transform.eulerAngles = currentRotation;

        // currentRotation.x = Mathf.Clamp(currentRotation.x, -45, 100);
        // transform.localRotation = Quaternion.Euler(currentRotation);
    }

    void FixedUpdate() {
        return;  // stop from running while I work on different movement code 
        Vector3 pos;
        Vector3 v;

        Vector2 mouseDelta;
        Quaternion rotation;
        Quaternion horiz;
        Quaternion vert;


        if(PauseMenu.isPaused) {}
        else {
            // rotation
            mouseDelta = new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
            rotation = transform.rotation;
            horiz = Quaternion.AngleAxis(mouseDelta.x, Vector3.up);
            vert = Quaternion.AngleAxis(mouseDelta.y, Vector3.right);
            transform.rotation = horiz * rotation * vert;


            // position
            v = new Vector3(0f, 0f, 0f);
            if (Input.GetKey(KeyCode.W)) {
                v.z += 1f;
            }
            if (Input.GetKey(KeyCode.S)) {
                v.z -= 1f;
            }
            if (Input.GetKey(KeyCode.D)) {
                v.x += 1f;
            }
            if (Input.GetKey(KeyCode.A)) {
                v.x -= 1f;
            }

            v = v * speed * Time.deltaTime;

            transform.Translate(v, Space.Self);

            pos = this.transform.position;
            pos.y = 3.5f;

            transform.position = pos;
        }
    }
}
