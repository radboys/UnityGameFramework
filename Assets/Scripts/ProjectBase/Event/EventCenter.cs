using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEventInfo { }

public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> action;

    public EventInfo(UnityAction<T> action)
    {
        this.action = action;
    }
}

public class EventInfo : IEventInfo
{
    public UnityAction action;

    public EventInfo(UnityAction action)
    {
        this.action = action;
    }
}

public class EventCenter : BaseManager<EventCenter>
{
    private Dictionary<string, IEventInfo> EventsDictionary = new();

    public void AddEventsListener<T>(string eventName, UnityAction<T> action)
    {
        if (EventsDictionary.TryGetValue(eventName, out IEventInfo eventInfo))
        {
            (eventInfo as EventInfo<T>).action += action;
        }
        else
        {
            EventsDictionary.Add(eventName, new EventInfo<T>(action));
        }
    }

    public void AddEventsListener(string eventName, UnityAction action)
    {
        if (EventsDictionary.TryGetValue(eventName, out IEventInfo eventInfo))
        {
            (eventInfo as EventInfo).action += action;
        }
        else
        {
            EventsDictionary.Add(eventName, new EventInfo(action));
        }
    }

    public void EventTrigger<T>(string eventName, T info)
    {
        if (EventsDictionary.TryGetValue(eventName, out IEventInfo eventInfo) && eventInfo is EventInfo<T> typedEventInfo)
        {
            typedEventInfo.action?.Invoke(info);
        }
    }

    public void EventTrigger(string eventName)
    {
        if (EventsDictionary.TryGetValue(eventName, out IEventInfo eventInfo) && eventInfo is EventInfo typedEventInfo)
        {
            typedEventInfo.action?.Invoke();
        }
    }

    public void RemoveEventsListener<T>(string eventName, UnityAction<T> action)
    {
        if (EventsDictionary.TryGetValue(eventName, out IEventInfo eventInfo) && eventInfo is EventInfo<T> typedEventInfo)
        {
            typedEventInfo.action -= action;
            if (typedEventInfo.action == null)
            {
                EventsDictionary.Remove(eventName);
            }
        }
    }

    public void RemoveEventsListener(string eventName, UnityAction action)
    {
        if (EventsDictionary.TryGetValue(eventName, out IEventInfo eventInfo) && eventInfo is EventInfo typedEventInfo)
        {
            typedEventInfo.action -= action;
            if (typedEventInfo.action == null)
            {
                EventsDictionary.Remove(eventName);
            }
        }
    }

    public void Clear()
    {
        EventsDictionary.Clear();
    }
}
