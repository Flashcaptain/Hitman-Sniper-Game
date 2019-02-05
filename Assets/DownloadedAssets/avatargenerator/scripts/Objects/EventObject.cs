using System.Collections;
using System.Collections.Generic;
using Casanova.Prelude;
using UnityEngine;

public class EventObject : MonoBehaviour
{
    public int AmountOfParticipants;
    public int Completeness;
    public bool IsReady;
    public int Id;
    public int InterestLevel;
    public int MaxAmountOfParticipants;
    public string AnimationName;
    public string ModelName;
    public string Name;
    public float Radius;
    public bool IsPlayerControlled;
    public string TriggerKey;
    public Vector3 Position;

    public List<int> NpcActionIds = new List<int>();
    public List<int> AssociatedActions = new List<int>();
    public List<Tuple<int, int>> PersonalityMinimums = new List<Tuple<int, int>>();

    public static EventObject Instantiate(Event sourceEvent)
    {
        var gameObjectModelName = sourceEvent.ModelName;

        var eventObject = (Instantiate(
                Resources.Load(gameObjectModelName),
                sourceEvent.Position,
                Quaternion.identity)
            as GameObject).GetComponent<EventObject>();

        eventObject.Id = sourceEvent.Id;
        eventObject.InterestLevel = sourceEvent.InterestLevel;
        eventObject.MaxAmountOfParticipants = sourceEvent.MaxAmountOfParticipants;
        eventObject.AnimationName = sourceEvent.AnimationName;
        eventObject.ModelName = sourceEvent.ModelName;
        eventObject.Name = sourceEvent.Name;
        eventObject.Radius = sourceEvent.Radius;
        eventObject.IsPlayerControlled = sourceEvent.IsPlayerControlled;
        eventObject.TriggerKey = sourceEvent.TriggerKey;
        eventObject.NpcActionIds = sourceEvent.NpcActionIds;
        eventObject.AssociatedActions = sourceEvent.AssociatedActions;
        eventObject.PersonalityMinimums = sourceEvent.PersonalityMinimums;

        return eventObject;
    }

    void Start()
    {

    }


    void Update()
    {
        if (Completeness >= 100)
        {
            StopAllCoroutines();
            DestroyEvent();
        }
    }

    public void TriggerEvent()
    {
        StartCoroutine(UpdateCompleteness());
    }

    IEnumerator UpdateCompleteness()
    {
        yield return new WaitForSeconds(1);
        Completeness += 10;
        StartCoroutine(UpdateCompleteness());
    }

    public void DestroyEvent()
    {
        Completeness = 0;

        if (EventController.ActiveEvents.ContainsKey(Id))
        {
            EventPlayer.RemoveAmbience(EventController.ActiveEvents[Id]);
            EventController.ActiveEvents.Remove(Id);
        }

    }
}        