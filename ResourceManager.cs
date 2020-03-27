using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceManager : SingletonBase<ResourceManager>
{
    public T LoadResource<T>(string sourceName) where T: Object
    {
        T resourceLoaded = Resources.Load<T>(sourceName);
        if(resourceLoaded is GameObject)
        {
            GameObject.Instantiate(resourceLoaded);
        }
        return resourceLoaded;
    }

    public void LoadResourceAsync<T>(string sourceName,UnityAction<T> action) where T:Object
    {
        MonoManager.Instance.StartCoroutine(LoadAsync<T>(sourceName,action));
    }

    IEnumerator LoadAsync<T>(string sourceName,UnityAction<T> action) where T:Object
    {
        ResourceRequest resourceLoading = Resources.LoadAsync<T>(sourceName);
        yield return resourceLoading;
        if(resourceLoading.asset is GameObject)
        {
            action(GameObject.Instantiate(resourceLoading.asset) as T);
        }
        else
        {
            action(resourceLoading.asset as T);
        }
    }


}
