﻿﻿﻿﻿﻿﻿﻿using System.Collections.Generic;
﻿using System.CodeDom;
using System.Linq;
using System.Runtime.InteropServices;
using Assets;
using Casanova.Prelude;
 using UnityEngine;


public static class ActionsParser
{
    private const string ACTIONS = "actions";
    private const string EVENTS = "events";

    public static Dictionary<int, Event> Events = new Dictionary<int, Event>();
    public static Dictionary<int, Event> PlayerEvents = new Dictionary<int, Event>();
    public static Dictionary<int, GameAction> EventReactions = new Dictionary<int, GameAction>();
    public static Dictionary<int, GameAction> EventActions = new Dictionary<int, GameAction>();
    public static Dictionary<int, GameAction> NormalActions = new Dictionary<int, GameAction>();


    public static void ParseNormalActions()
    {
        var actions = XmlNodule.Load(ACTIONS);
        foreach (var action in actions)
        {
            var actionInstance = new NormalAction
            {
                Id = action.Get("actionId").ToInt(),
                Position = action.Get("position").ToVector3(),
                ActionName = action.Get("actionname").ToString(),
                AnimationName = action.Get("animationname").ToString(),
                PersonalityModifiers = GetNodePersonalityModifiers(action)
            };

            foreach (var neighbour in action.Get("neighbours"))
            {
                if (neighbour.ToString() != "")
                    actionInstance.NeighbourIds.Add(neighbour.ToInt());
            }
            NormalActions.Add(actionInstance.Id, actionInstance);
        }
    }


    public static void ParseEventsActions(string fileName)
    {
        var eventActionsNodule = XmlNodule.Load(fileName);

        foreach (var eventAction in eventActionsNodule)
        {
            var eventActionObject = new EventAction
            {
                Id = eventAction.Get("id").ToInt(),
                ActionName = eventAction.Get("name").ToString(),
                AnimationName = eventAction.Get("animationname").ToString(),
                Position = eventAction.Get("position").ToVector3(),
                PersonalityModifiers = GetNodePersonalityModifiers(eventAction)
            };

            if (fileName.Equals("eventActions"))
                EventActions.Add(eventActionObject.Id, eventActionObject);
            else
                EventReactions.Add(eventActionObject.Id, eventActionObject);
        }
    }

    public static void ParseEvents()
    {
        var eventsNodule = XmlNodule.Load(EVENTS);

        foreach (var Event in eventsNodule)
        {
            var eventObject = new Event
            {
                Id = Event.Get("id").ToInt(),
                Name = Event.Get("name").ToString(),
                ModelName = Event.Get("modelname").ToString(),
                IsPlayerControlled = Event.Get("playercontrolled").ToBool(),
                TriggerKey = Event.Get("triggerkey").ToString(),
                Radius = Event.Get("radius").ToInt(),
                AnimationName = Event.Get("animationname").ToString(),
                Position = Event.Get("position").ToVector3(),
                InterestLevel = Event.Get("interestlevel").ToInt(),
                MaxAmountOfParticipants = Event.Get("maxamountofparticipants").ToInt()
            };

            var personalityMinimums = new List<Tuple<int, int>>();
            foreach (var minimum in Event.Get("personalityMinimums"))
            {
                var personalityId = minimum.Get("id").ToInt();
                var personalityMinimum = minimum.Get("value").ToInt();
                var newMinimumTuple = new Tuple<int, int>(personalityId, personalityMinimum);
                personalityMinimums.Add(newMinimumTuple);
            }
            eventObject.PersonalityMinimums = personalityMinimums;

            var npcActionIds = Event.Get("NpcAnimationActions").Select(actionId => actionId.ToInt()).ToList();
            var associatedActionIds = Event.Get("associatedreactions").Select(actionId => actionId.ToInt()).ToList();
            eventObject.AssociatedActions = associatedActionIds;
            eventObject.NpcActionIds = npcActionIds;

            if (eventObject.IsPlayerControlled)
                PlayerEvents.Add(eventObject.Id, eventObject);
            else
                Events.Add(eventObject.Id, eventObject);
        }
    }

    private static Dictionary<int, int> GetNodePersonalityModifiers(XmlNodule nodule)
    {
        return nodule.Get("modifiers")
            .ToDictionary(modifier => modifier.Get("id").ToInt(), modifier => modifier.Get("value").ToInt());
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   