using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjCreator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            PoolManager.Instance.Get_Obj("Cube",Vector3.zero,Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            PoolManager.Instance.Get_Obj("Spheres/Sphere",Vector3.one,Quaternion.identity);
        }
    }
}
