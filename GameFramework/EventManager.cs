using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : SingletonBase<EventManager>
{
    private Dictionary<string, UnityAction<object>> eventDict = new Dictionary<string, UnityAction<object>>();
    public void AddEvent(string name, UnityAction<object> _event)
    {
        if (eventDict.ContainsKey(name))
        {
            eventDict[name] += _event;
        }
        else
        {
            eventDict.Add(name, _event);
        }
    }
    public void GetEvent(string name, object obj)
    {
        if (eventDict.ContainsKey(name))
        {
            eventDict[name](obj);
        }

    }
    public void RemoveEvent(string name, UnityAction<object> _event)
    {
        if (eventDict.ContainsKey(name))
        {
            eventDict[name] -= _event;
        }

    }
    public void Clear()
    {
        eventDict.Clear();
    }
}