using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDetection : MonoBehaviour
{
    [SerializeField] GameObject WindowKeyboard;
    [SerializeField] GameObject WindowGamepad;
    void Start()
    {
        if(!GManager.instance.isGamepad)
        {
        WindowKeyboard.SetActive(true);
        }
        else
        {
        WindowGamepad.SetActive(true);
        }

    }
}
