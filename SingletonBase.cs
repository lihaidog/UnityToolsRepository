using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 单例基类，要实现单例功能可继承该类
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonBase<T> where T : class
{
    /*private static T _Instance;
    public static T Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = new T();
            return _Instance;
        }
    }*/ //由于需要子类有公共构造函数，并不是真正意义上的单例
    private static readonly T _Instance = (T)Activator.CreateInstance(typeof(T), true);
    public static T Instance
    {
        get
        {
            return _Instance;
        }
    }
}
