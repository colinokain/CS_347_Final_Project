using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    public static bool isPaused;


    void Start() {
        isPaused = false;
    }


    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            isPaused = false;
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            isPaused = true;
        }
    }
}
