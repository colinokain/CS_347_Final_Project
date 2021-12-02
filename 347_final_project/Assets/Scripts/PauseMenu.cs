//Colin O'Kain, Joshua Payne, Christopher Day

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    void Start()
    {
        GetComponent<Canvas>().enabled = false;
    }

    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;                
        #endif
    }

    public void UpdateSensitivity()
    {
        float sens = GameObject.Find("Sensitivity Slider").GetComponent<Slider>().value;
        GameObject.FindWithTag("Player").GetComponent<PlayerScript>().sensitivity = sens;
        GameObject.Find("Sensitivity Display").GetComponent<Text>().text = sens.ToString("F2");

    }
}
