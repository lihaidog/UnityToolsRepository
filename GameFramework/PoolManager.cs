using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolManager : SingletonBase<PoolManager>
{
    Dictionary<string, PoolData> poolDict = new Dictionary<string, PoolData>();//key=objName,value=objInPoolList
    //Dictionary<string, GameObject> poolIndex = new Dictionary<string, GameObject>();//key=objName,value=poolObj
    private GameObject poolManager;
    private PoolManager() { poolManager = new GameObject("PoolManager"); }
    protected class PoolData
    {
        private GameObject parentObject;
        private List<GameObject> objectList=new List<GameObject>();
        private string dataName;
        public PoolData(string objName,GameObject poolManager)
        {
            dataName = objName;
            parentObject = new GameObject(objName + "_Pool");
            parentObject.transform.SetParent(poolManager.transform);
        }
        public void PushData(GameObject obj)
        {
            objectList.Add(obj);
            obj.transform.SetParent(parentObject.transform);
            obj.SetActive(false);
        }
        public GameObject GetData()
        {
            GameObject obj;
            if (objectList.Count > 0)
            {
                obj = objectList[0];
                objectList.RemoveAt(0);
                obj.transform.parent = null;
                obj.SetActive(true);
                return obj;
            }
            obj= ResourceManager.Instance.LoadResource<GameObject>(dataName);
            obj.name = dataName;
            Debug.Log(obj.name);
            return obj;
            
        }
        public void GetDataAsync(UnityAction<GameObject> callback=null)
        {
            if (objectList.Count > 0)
            {
                Debug.Log(objectList.Count);
                GameObject obj = objectList[0];
                objectList.RemoveAt(0);
                obj.transform.parent = null;
                obj.SetActive(true);
                if (callback != null)
                {
                    callback(obj);
                }
            }
            else
            {
                ResourceManager.Instance.LoadResourceAsync<GameObject>(dataName, callback);
            }

        }
    }

    /// <summary>
    /// used to push gameObject into pool
    /// </summary>
    /// <param name="name">is name of the prefab</param>
    /// <param name="obj">is the prefab instance</param>
    public void Push_Obj(string name,GameObject obj)
    {
        if (poolDict.ContainsKey(name))
        {
            poolDict[name].PushData(obj);

        }
        else
        {

            PoolData poolData = new PoolData(name, poolManager);
            poolData.PushData(obj);
            
            
        }
    }
    /// <summary>
    /// used to get gameObject from pool(Load from Resources while the pool is empty)
    /// </summary>
    /// <param name="name">is name of gameObject</param>
    /// <param name="place">place of instance </param>
    /// <param name="direction">direction of instance</param>
    /// /// <param name="direction"></param>
    /// <returns></returns>
    public GameObject Get_Obj(string name,UnityAction<object> callback=null)
    {
        GameObject obj;
        if (poolDict.ContainsKey(name))
        {
            obj= poolDict[name].GetData();
            if(callback!=null)
            {
                callback(obj);
            }
            return obj;
        }
        else
        {
            PoolData poolData = new PoolData(name, poolManager);
            poolDict.Add(name, poolData);
            obj= poolData.GetData();
            if (callback != null)
            {
                callback(obj);
            }
            return obj;
        }
    }
    /// <summary>
    /// get object from pool by async method
    /// </summary>
    /// <param name="name">object to load</param>
    /// <param name="callback">used to do some operation of object geted</param>
    public void Get_ObjAsync(string name, UnityAction<GameObject> callback = null)
    {

        if (poolDict.ContainsKey(name))
        {
            poolDict[name].GetDataAsync(callback);
        }
        else
        {
            PoolData poolData = new PoolData(name, poolManager);
            poolDict.Add(name, poolData);
            poolData.GetDataAsync(callback);
        }
    }
}
