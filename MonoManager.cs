using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using System.ComponentModel;

public class MonoManager : SingletonBase<MonoManager>
{
    private MonoPublisher MonoPublisher;
    /*public class MonoAPI : MonoBehaviour
    {
        private MonoAPI() { }
    }
    public MonoAPI MonoApi =(MonoAPI) Activator.CreateInstance(typeof(MonoAPI), true);*///可以通过继承mono的空类来使其他脚本使用mono中的方法，实现简单但封闭性不好
    private MonoManager()
    {
        GameObject gameObject = new GameObject("MonoPublisher");
        MonoPublisher = gameObject.AddComponent<MonoPublisher>();
    }
    public void ToUpdate(UnityAction method)
    {
        
        MonoPublisher.AddMonoSubscriber(method);
    }
    public void ToUpdate(UnityAction<List<object>> method,object obj)
    {
        MonoPublisher.AddMonoSubscriber(method,obj);
    }
    public void ExitUpdate(UnityAction<List<object>> method,object obj)
    {
        MonoPublisher.RemoveMonoSubscriber(method,obj);
    }
    public void ExitUpdate(UnityAction method)
    {
        MonoPublisher.RemoveMonoSubscriber(method);
    }
    #region
    public void ParamNext()
    {
        MonoPublisher.ParamNext();
    }
    public void ParamClear()
    {
        MonoPublisher.ParamClear();
    }
    public int ParamGet()
    {
        return MonoPublisher.ParamGet();
    }
    #endregion //param operation

    public Coroutine StartCoroutine(string methodName)
    {
        return MonoPublisher.StartCoroutine(methodName);
    }//创建返回mono中的协程函数的函数，可以使不继承mono的脚本使用这些协程方法,相比使用一个MonoApi调用起来更方便，但需要更多样板代码
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return MonoPublisher.StartCoroutine(routine);
    }
    public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
    {
        return MonoPublisher.StartCoroutine(methodName,value);
    }
    public Coroutine StartCoroutine_Auto(IEnumerator routine)
    {
        return MonoPublisher.StartCoroutine(routine);
    }
    public void StopAllCoroutines()
    {
        MonoPublisher.StopAllCoroutines();
    }
    public void StopCoroutine(IEnumerator routine)
    {
        MonoPublisher.StopCoroutine(routine);
    }
    public void StopCoroutine(Coroutine routine)
    {
        MonoPublisher.StopCoroutine(routine);
    }
    public void StopCoroutine(string methodName)
    {
        MonoPublisher.StopCoroutine(methodName);
    }
}
