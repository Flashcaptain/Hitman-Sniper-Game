﻿﻿﻿﻿﻿﻿﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Casanova.Prelude;
using UnityEngine;


public class UnityEvent
{
    public EventObject _eventObject;

    public bool IsDestroyed
    {
        get { return _eventObject == null; }
    }

    public int AmountOfParticipants
    {
        get { return _eventObject.AmountOfParticipants; }
        set { _eventObject.AmountOfParticipants = value; }
    }

    public int Completeness
    {
        get { return _eventObject.Completeness; }
        set { _eventObject.Completeness = value; }
    }

    public float Radius
    {
        get { return _eventObject.Radius; }
        set { _eventObject.Radius = value; }
    }

    public Vector3 Position
    {
        get { return _eventObject.Position; }
        set { _eventObject.Position = value; }
    }

    public List<Tuple<int, int>> PersonalityMinimums
    {
        get { return _eventObject.PersonalityMinimums; }
        set { _eventObject.PersonalityMinimums = value; }
    }

    public int InterestLevel
    {
        get { return _eventObject.InterestLevel; }
        set { _eventObject.InterestLevel = value; }
    }


    public bool IsPlayerControlled
    {
        get { return _eventObject.IsPlayerControlled; }
        set { _eventObject.IsPlayerControlled = value; }
    }

    public string TriggerKey
    {
        get { return _eventObject.TriggerKey; }
        set { _eventObject.TriggerKey = value; }
    }

    public int MaxAmountOfParticipants
    {
        get { return _eventObject.MaxAmountOfParticipants; }
        set { _eventObject.MaxAmountOfParticipants = value; }
    }

    public GameObject GameObject
    {
        get { return _eventObject.gameObject; }
    }

    public int Id
    {
        get { return _eventObject.Id; }
        set { _eventObject.Id = value; }
    }

    public static int AmountOfEvents
    {
        get { return ActionsParser.Events.Count; }
        set { }
    }

    public static int AmountOfPlayerEvents
    {
        get { return ActionsParser.PlayerEvents.Count(x => x.Value.IsPlayerControlled); }
        set { }
    }

    public static UnityEvent SpawnRandomEvent(string type)
    {
        if (type.Equals("playerEvent"))
            return new UnityEvent {_eventObject = EventController.GetPlayerEvent()};
        else
        {
            var unityEvent = new UnityEvent {_eventObject = EventController.SpawnRandomEvent()};
            return unityEvent;
        }
    }

    public void Destroy()
    {
        _eventObject.DestroyEvent();
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 