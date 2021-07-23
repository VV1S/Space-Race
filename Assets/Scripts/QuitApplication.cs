using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    void Update()
    {
        Quit();
    }
    void Quit()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Application is over.");
            Application.Quit();
        }
    }


}
