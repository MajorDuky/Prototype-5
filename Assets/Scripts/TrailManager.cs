using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {       

        if (Input.GetAxis("Fire1") != 0)
        {
            Vector3 cursorPos = Input.mousePosition;
            cursorPos.z = Camera.main.nearClipPlane;
            transform.position = Camera.main.ScreenToWorldPoint(cursorPos);
        }
    }
}
