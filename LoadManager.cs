using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager :SingletonBase<LoadManager>
{
    private bool _isLoadScene=false;


    /// <summary>
    /// 判断异步加载场景是否已经进行。
    /// </summary>
    public  bool IsLoadScene
    {
        get
        {
            return _isLoadScene;
        }
    }

    private Slider loadSlider=null;
    private Image loadBackground=null;



    /// <summary>
    /// 同步加载场景
    /// </summary>
    /// <param name="sceneName">要加载的场景名称</param>
    /// <param name="action">回调函数，在执行完加载任务后要执行的方法</param>
    public void Load_Scene(string sceneName,UnityAction action)
    {
        SceneManager.LoadScene(sceneName);
        action();
    }

    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <param name="sceneName">要加载的场景名称</param>
    /// <param name="action">回调函数，在执行完加载任务后要执行的方法</param>
    /// <param name="loadCanvas">已实例化的Canvas对象</param>
    /// <param name="isActivation"></param>
    public void Load_SceneAsync(string sceneName, UnityAction action=null, GameObject loadCanvas=null,bool isActivation=false)
    {
        if(loadCanvas!=null)
        {
            AddLoadCanvas(loadCanvas);
        }
        MonoManager.Instance.StartCoroutine(SceneAsync(sceneName,action, isActivation));
    }


    IEnumerator SceneAsync(string sceneName, UnityAction action = null, bool isActivation = false)
    {
        _isLoadScene = true;
        float progress;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = isActivation;
        while (!operation.isDone)
        {
            if (isActivation)
            {
                progress = operation.progress;
            }
            else
            {
                
                progress = operation.progress <0.89f ? operation.progress:1;
               
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    operation.allowSceneActivation = true;
                }
            }
            EventManager2.Instance.EventTrigger("Load", "LoadSlider", progress);
            yield return null;
        }
        if (action != null)
        {
            action();
        }
        _isLoadScene = false;
    }


    /// <summary>
    /// 进度条变化
    /// </summary>
    /// <param name="progress"></param>
    void LoadSlider(object progress)
    {
        loadSlider.value = (float)progress;
    }



    /// <summary>
    /// 要使用可变的背景时可以将其在AddLoadCanvas中加入EventManager2里的事件再由其他脚本调用
    /// </summary>
    /// <param name="sprite">要显示的背景图</param>
    void LoadImage(object sprite)
    {
        loadBackground.sprite = (Sprite)sprite;
    }



    /// <summary>
    /// canvas包含其子对象中只能有一个slider控件
    /// </summary>
    /// <param name="loadCanvas">Canvas对象</param>
    void AddLoadCanvas(GameObject loadCanvas)
    {
        loadSlider = loadCanvas.GetComponentInChildren<Slider>();
        EventManager2.Instance.AddEvent("Load", "LoadSlider", new UnityAction<object>(LoadSlider));
        EventManager2.Instance.AddEvent("Load", "LoadImage", new UnityAction<object>(LoadImage));
    }
}
