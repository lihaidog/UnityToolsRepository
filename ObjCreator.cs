using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjCreator : MonoBehaviour
{
    string m_Name;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            PoolManager.Instance.Get_Obj("Cube");
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            m_Name = "Spheres/Sphere";
            PoolManager.Instance.Get_ObjAsync(m_Name,(obj)=> { obj.name = m_Name; });
        }
    }


}
