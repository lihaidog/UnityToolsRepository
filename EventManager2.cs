using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager2 : SingletonBase<EventManager2>
{
    /// <summary>
    /// 对象和包含事件的字典，key是对象名称，value是事件字典。事件字典中，key是事件名称，value是泛型方法，用于多播所有调用此方法的事件
    /// </summary>
    Dictionary<string, Dictionary<string, UnityAction<object>>> objDict = new Dictionary<string, Dictionary<string, UnityAction<object>>>();

    /// <summary>
    /// 添加对象及对应的事件名、事件的字典。
    /// </summary>
    /// <param name="objName">对象的名称</param>
    /// <param name="eventName">事件的名称</param>
    /// <param name="_event">事件方法</param>
    public void AddEvent(string objName,string eventName,UnityAction<object> _event)
    {
        if(objDict.ContainsKey(objName))
        {
            if(objDict[objName].ContainsKey(eventName))
            {
                objDict[objName][eventName] +=  _event;
            }
            else
            {
                objDict[objName].Add(eventName, _event);
            }
        }
        else
        {
            objDict.Add(objName, new Dictionary<string, UnityAction<object>>());
            objDict[objName].Add(eventName, _event);
        }
    }
    
    public void RemoveEvent(string objName, string eventName, UnityAction<object> _event)
    {
        if (objDict.ContainsKey(objName))
        {
            if (objDict[objName].ContainsKey(eventName))
            {
                objDict[objName][eventName] -= _event;
                if(objDict[objName][eventName]==null)
                {
                    objDict[objName].Remove(eventName);
                }
            }
            if(objDict[objName].Count==0)
            {
                objDict.Remove(objName);
            }
        }
    }
    public void Clean()
    {
        
        objDict.Clear();
    }
    
    public void EventTrigger(string objName,string eventName,object obj)
    {
        if(objDict.ContainsKey(objName))
        {
            if(objDict[objName].ContainsKey(eventName))
            {
                objDict[objName][eventName](obj);
            }
        }
    }
}
