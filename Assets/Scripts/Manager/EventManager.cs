using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static string OnMouse0Up = "OnMouse0Up";
    public static string OnMouse1Up = "OnMouse1Up";

    /// <summary>
    /// 带返回参数的回调列表,参数类型为T，支持一对多
    /// </summary>
    public static Dictionary<string, List<Delegate>> events = new Dictionary<string, List<Delegate>>();

    /// <summary>
    /// 通用注册事件方法
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="callback"></param>
    private static void CommonAdd(string eventName, Delegate callback)
    {
        List<Delegate> actions = null;

        //eventName已存在
        if (events.TryGetValue(eventName, out actions))
        {
            actions.Add(callback);
        }
        //eventName不存在
        else
        {
            actions = new List<Delegate>();

            actions.Add(callback);
            events.Add(eventName, actions);
        }
    }


    /// <summary>
    /// 注册事件，1个返回参数
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="callback"></param>
    public static void AddEvent<T>(string eventName, Action<T> callback)
    {
        CommonAdd(eventName, callback);
    }

    /// <summary>
    /// 派发事件 1个参数
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="arg"></param>
    public static void DispatchEvent<T>(string eventName, T arg)
    {
        List<Delegate> actions = null;

        if (events.ContainsKey(eventName))
        {
            events.TryGetValue(eventName, out actions);

            foreach (var act in actions)
            {
                act.DynamicInvoke(arg);
            }
        }
    }
}
