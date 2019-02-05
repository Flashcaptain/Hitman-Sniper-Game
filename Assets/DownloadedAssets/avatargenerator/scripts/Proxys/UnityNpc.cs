﻿﻿﻿﻿using System.Collections.Generic;
using System.Linq;
 using System.Xml.Schema;
 using Casanova.Prelude;
using UnityEngine;

public class UnityNpc : MonoBehaviour
{
    private NpcObject _npcObject;

    public int Id
    {
        get { return _npcObject.Id; }
    }

    public bool IsInEventRadius
    {
        get { return _npcObject.InEventRadius; }
        set { _npcObject.InEventRadius = value; }
    }

    public bool IsEventActor
    {
        get { return _npcObject.IsEventActor; }
        set { _npcObject.IsEventActor = value; }
    }

    public Vector3 Position
    {
        get { return _npcObject.transform.position; }
        set { _npcObject.transform.position = value; }
    }

    public List<int> ActionsToPerform
    {
        get
        {
            _npcObject.GraphTraveler.Actions = _npcObject.CurrentNodesCollection;
            var path = _npcObject.GraphTraveler.GetBestPath();
            return path;
        }
    }

    public void FreeEventActors()
    {
        var eventActors = Enumerable.Where(NpcObject.AllPersons, x => x.IsEventActor).ToList();
        eventActors.ForEach(x => x.IsEventActor = false);
    }

    public bool IsInEvent
    {
        get { return _npcObject.IsInEvent; }
        set { _npcObject.IsInEvent = value; }
    }

    public Vector3 CurrentActionPosition
    {
        get { return _npcObject.CurrentActionPosition; }
    }

    public bool IsInterestedInEvent()
    {
        return _npcObject.IsInterestedInEvent();
    }

    public static UnityNpc Spawn(List<Tuple<int, int>> personalityValues, string prefabname)
    {
        var unityNpc = new UnityNpc {_npcObject = NpcObject.Instantiate(personalityValues, prefabname)};
        return unityNpc;
    }

    public void MoveTo(int actionId)
    {
        _npcObject.MoveTo(actionId);
    }

    public bool IsPositionAvailable(int actionId)
    {
        return _npcObject.IsPositionAvailable(actionId);
    }

    public void ClaimPosition(int actionId)
    {
        _npcObject.ClaimPosition(actionId);
    }

    public Vector3 GetClaimedPosition(int actionId)
    {
        return _npcObject.GetClaimedPosition(actionId);
    }

    public void RemoveClaimToPosition(int actionId)
    {
        _npcObject.RemoveClaimToPosition(actionId);
    }


    public void UpdateAccumulatedValues(int actionId)
    {
        _npcObject.UpdateAccumulatedValues(actionId);
    }

    public float PlayAnimation(int actionId)
    {
        return _npcObject.PlayAnimation(actionId);
    }

    public void RemoveClaimToAllPositions()
    {
        foreach (var action in _npcObject.CurrentNodesCollection)
        {
            RemoveClaimToPosition(action.Value.Id);
        }
    }

    public void UpdateCurrentNodesCollection()
    {
        if (IsEventActor)
        {
            if(_npcObject.IsInEvent)
                _npcObject.CurrentNodesCollection = GetNpcActionsForEventId(_npcObject.MyEvent.Id);
        }
        else if (IsInEvent)
        {
            _npcObject.CurrentNodesCollection = GetAssociatedActionsForEventId(_npcObject.MyEvent.Id);
        }
        else
        {
            _npcObject.CurrentNodesCollection = ActionsParser.NormalActions;
        }
    }

    private static Dictionary<int, GameAction> GetAssociatedActionsForEventId(int eventId)
    {
        var currentEvent = GetEventForId(eventId);
        return
            currentEvent.AssociatedActions.ToDictionary(x => x, x => ActionsParser.EventReactions[x]);
    }

    //This returns the appropriate action AS AN event (IE: An npc fainting), NOT a reaction to an event.
    private static Dictionary<int, GameAction> GetNpcActionsForEventId(int eventId)
    {
        var eventForId = GetEventForId(eventId);
        return ActionsParser.EventActions.Where(x => eventForId.NpcActionIds.Contains(x.Value.Id)).ToDictionary(x => x.Key, x => x.Value);
    }

    private static Event GetEventForId(int eventId)
    {
        return ActionsParser.Events.ContainsKey(eventId) ? ActionsParser.Events[eventId]
            : ActionsParser.PlayerEvents[eventId];
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          