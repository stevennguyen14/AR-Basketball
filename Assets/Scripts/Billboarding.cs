using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Billboarding : MonoBehaviour
{
    public GameObject canvas;
    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        Vector3 camPos = cam.transform.position;

        //canvas.transform.LookAt(new Vector3(-camPos.x, transform.position.y, -camPos.z));
        canvas.transform.LookAt(2*canvas.transform.position - camPos);
    }
}
