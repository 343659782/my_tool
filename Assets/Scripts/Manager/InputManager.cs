using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : Singleton<InputManager>
{
    Ray ray;
    RaycastHit hit;
    GameObject obj;

    private void Awake()
    {
        _instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                obj = hit.collider.gameObject;
                EventManager.DispatchEvent<GameObject>(EventManager.OnMouse0Up, obj);
            }
            else
            {
                WarManager.Instance.ResetMouse0Up();
                WarManager.Instance.ResetMouse1Up();
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                obj = hit.collider.gameObject;
                EventManager.DispatchEvent<GameObject>(EventManager.OnMouse1Up, obj);
                if (hit.collider.gameObject.tag != "Player")
                {
                    WarManager.Instance.ResetMouse1Up();
                }
            }
            else
            {
                WarManager.Instance.ResetMouse1Up();
                WarManager.Instance.ResetMouse0Up();
            }
        }
    }

}
