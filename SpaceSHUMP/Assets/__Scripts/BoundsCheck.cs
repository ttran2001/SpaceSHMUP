using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    [Header("Inscribed")]
    public float radius = 4f;
    public bool keepOnScreen = true; 

    [Header("Dynamic")]
    public float camWidth; 
    public float camHeight;

    void Awake(){
        camHeight = Camera.main.orthographicSize; 
        camWidth = camHeight*Camera.main.aspect; 
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = transform.position; 

        if(pos.x + radius > camWidth)
        {
            pos.x = camWidth-radius;
        }
        if(pos.x-radius < -camWidth)
        {
            pos.x = -camWidth+radius;
        }
        
        if(keepOnScreen)
        {
            if(pos.y+radius > camHeight)
            {
            pos.y = camHeight-radius;
            }
            if(pos.y-radius < -camHeight)
            {
            pos.y = -camHeight+radius;
            }
        }
        
        transform.position = pos;
    }
}
