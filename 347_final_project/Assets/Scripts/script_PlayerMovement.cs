using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class script_PlayerMovement : MonoBehaviour {
    public GameObject canvas;  // for pauseing

    public float speed = 100;


    void Start() {}


    void Update() {
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
