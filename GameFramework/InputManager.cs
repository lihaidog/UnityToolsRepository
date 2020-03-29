using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager :SingletonBase<InputManager>
{
    private InputManager() { ToMonoUp(); }
    private bool isInputActive = false;

    public void SetInputActive(bool status)
    {
        isInputActive = status;
    }
    void KeyCheck(KeyCode k) 
    {
        if (Input.GetKeyDown(k)) 
        {  
            EventManager2.Instance.EventTrigger<KeyCode>("InputKey", "KeyDown", k);
        }
        if (Input.GetKeyUp(k))
        {
            EventManager2.Instance.EventTrigger<KeyCode>("InputKey", "KeyUp", k);
        }
        if (Input.GetKey(k))
        {
            EventManager2.Instance.EventTrigger<KeyCode>("InputKey", "KeyIn", k);
        }
    }

    void KeyCheck(string k)
    {
        if (Input.GetKeyDown(k))
        {
            EventManager2.Instance.EventTrigger<string>("InputKey", "KeyDown", k);
        }
        if (Input.GetKeyUp(k))
        {
            EventManager2.Instance.EventTrigger<string>("InputKey", "KeyUp", k);
        }
        if (Input.GetKey(k))
        {
            EventManager2.Instance.EventTrigger<string>("InputKey", "KeyIn", k);
        }
    }

    void AxisCheck()
    {
        if (Input.GetAxis("Horizontal")!=0)
        {
            EventManager2.Instance.EventTrigger<string>("InputAxis", "Axis", "Horizontal");
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            EventManager2.Instance.EventTrigger<string>("InputAxis", "Axis", "Vertical");
        }
    }

    void CheckMoving()
    {
        if (isInputActive)
        {
            KeyCheck(KeyCode.Q);
            KeyCheck(KeyCode.F);
            KeyCheck(KeyCode.T);
            KeyCheck(KeyCode.E);
            AxisCheck();

        }
    }

    public void ToMonoUp()
    {
        MonoManager.Instance.ToUpdate(new UnityAction(CheckMoving));
    }
}
