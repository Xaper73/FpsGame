using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform cam;
    
    void LateUpdate()
    {
//        transform.LookAt(cam);
        transform.LookAt(new Vector3(cam.position.x, transform.position.y, cam.position.z));
    }
}
