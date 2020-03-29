using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoPublisher : MonoBehaviour
{
    private event UnityAction UpdateHandler;
    private event UnityAction<List<object>> UpdateHandlerS;
    private List<object> handlerParam;
    private int paramIndex;
    void Awake()
    {
        paramIndex = 0;
        handlerParam = new List<object>();
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(UpdateHandler!=null)
        {
            UpdateHandler();
        }
        if(UpdateHandlerS!=null&&handlerParam.Count>0)
        {
            UpdateHandlerS(handlerParam);
            ParamClear();
        }
    }
    public void AddMonoSubscriber(UnityAction action)
    {
        UpdateHandler += action;
    }
    public void AddMonoSubscriber(UnityAction<List<object>> action,object actionParam)//这种复杂的做法其实完全没有意义，在需要update的方法确定方法要调用的object后，都可以用无参函数实现
    {
        handlerParam.Add("bbb");
        UpdateHandlerS += action; 
        
    }
    public void RemoveMonoSubscriber(UnityAction action)
    {
        UpdateHandler -= action;
    }
    public void RemoveMonoSubscriber(UnityAction<List<object>> action,object actionParam)
    {
        UpdateHandlerS -= action;
        handlerParam.Remove(actionParam);
    }
    public int ParamGet()
    {
       return paramIndex;
    }
    public void ParamNext()
    {
        if (paramIndex < handlerParam.Count-1)
        {
            paramIndex++;
        }
    }
    public void ParamClear()
    {
        paramIndex = 0;
    }
}
