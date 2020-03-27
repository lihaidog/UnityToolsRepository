using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolManager :SingletonBase<PoolManager>
{
    Dictionary<string, List<GameObject>> poolDict = new Dictionary<string, List<GameObject>>();//key=objName,value=objInPoolList
    Dictionary<string,GameObject>poolIndex=new Dictionary<string, GameObject>();//key=objName,value=poolObj
    private GameObject poolManager;
    private PoolManager() { poolManager = new GameObject("PoolManager"); }
    /// <summary>
    /// used to push gameObject into pool
    /// </summary>
    /// <param name="name">is name of the prefab</param>
    /// <param name="obj">is the prefab instance</param>
    public void Push_Obj(string name,GameObject obj)
    {
        if(poolDict.ContainsKey(name))
        {
            poolDict[name].Add(obj);
            obj.transform.SetParent(poolIndex[name].transform);
            obj.SetActive(false);

        }
        else
        {
            List<GameObject> objList = new List<GameObject>();
            poolDict.Add(name, objList);
            objList.Add(obj);
            GameObject pool_Name = new GameObject(name + "_Pool");
            pool_Name.transform.SetParent(poolManager.transform);
            poolIndex.Add(name, pool_Name);
            obj.transform.SetParent(pool_Name.transform);
            
            
        }
        obj.gameObject.SetActive(false);
    }
    /// <summary>
    /// used to get gameObject from pool(Load from Resources while the pool is empty)
    /// </summary>
    /// <param name="name">is name of gameObject</param>
    /// <param name="place">place of instance </param>
    /// <param name="direction">direction of instance</param>
    /// /// <param name="direction"></param>
    /// <returns></returns>
    public GameObject Get_Obj(string name,Vector3 place,Quaternion direction)
    {
        GameObject obj;
        GameObject gmObj;
        if (poolDict.ContainsKey(name))
        {
            if (poolDict[name].Count > 0)
            {
                gmObj = poolDict[name][0];
                gmObj.SetActive(true);
                poolDict[name].RemoveAt(0);
                return gmObj;
            }
        }
            obj= Resources.Load(name) as GameObject;
            gmObj= GameObject.Instantiate(obj,place,direction) as GameObject;
            gmObj.name = name;
        return gmObj;
    }
}
