using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToggle : MonoBehaviour
{
    public GameObject Camera1;
    public GameObject Camera2;
    // Start is called before the first frame update
    void Start()
    {
        
            Camera2.SetActive(false);
            Camera1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C key was pressed.");

            Debug.Log("hi");
            Camera1.SetActive(false);
            Camera2.SetActive(true);
        }
        
        if (Input.GetKeyUp(KeyCode.C))
        {
            Debug.Log("C key was released.");
            Camera2.SetActive(false);
            Camera1.SetActive(true);
        }
    }
}
