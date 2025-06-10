using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] bool EnableCursor;
    // Start is called before the first frame update
    void Start()
    {
        if (!EnableCursor)
        
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        
        else
        
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        
        UnityEngine.Cursor.visible = EnableCursor;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
