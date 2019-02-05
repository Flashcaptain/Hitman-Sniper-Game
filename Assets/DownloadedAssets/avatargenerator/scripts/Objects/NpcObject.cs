using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Casanova.Prelude;
using UnityEngine;

public class NpcObject : MonoBehaviour
{
    public int Id;
    public GameAction CurrentlyActiveAction;
    public bool InEventRadius;
    public bool IsInEvent;

    public bool IsEventActor;
    public EventObject MyActingEvent;
    public bool FirstAction;

    public Animator Animator;

    public Dictionary<int, GameAction> CurrentNodesCollection;
    public EventObject MyEvent;
    public GenericVector AccumulatedValues;
    public GenericVector PersonalityValuesGenericVector;
    public GraphTraveler GraphTraveler;

    public NpcMovementController MovementController;

    //This is set to a dummy value in order to avoid NPCs freezing on start.
    public Vector3 CurrentActionPosition = new Vector3(-1.0f, -1.0f, -1.0f);

    public static List<NpcObject> AllPersons = new List<NpcObject>();

    public static NpcObject Instantiate(List<Tuple<int, int>> personalityValues, string modelName)
    {
        if (modelName.Equals("random"))
        {
            var randomIndexForModelName = Random.Range(0, SettingsParser.ModelNames.Count);
            modelName = SettingsParser.ModelNames[randomIndexForModelName];
        }
        var randomIndexForPosition = Random.Range(0, SettingsParser.Spawnpoints.Count);
        var randomPosition = SettingsParser.Spawnpoints[randomIndexForPosition];

        var npcObject = (Instantiate(
                Resources.Load(modelName),
                randomPosition,
                Quaternion.identity)
            as GameObject).GetComponent<NpcObject>();

        npcObject.Id = AllPersons.Count;
        npcObject.PersonalityValuesGenericVector = personalityValues.SetUpGenericVector();
        npcObject.AccumulatedValues = new GenericVector(personalityValues.Count);
        npcObject.CurrentNodesCollection = ActionsParser.NormalActions;
        npcObject.GraphTraveler = new GraphTraveler(npcObject);
        npcObject.MovementController = NpcMovementController.CreateComponent(npcObject.gameObject, npcObject);
        npcObject.Animator = npcObject.gameObject.GetComponent<Animator>();
        npcObject.gameObject.AddComponent<UnityEngine.AI.NavMeshAgent>();
        npcObject.gameObject.AddComponent<AudioSource>();
        npcObject.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().stoppingDistance = 2.2f;
        npcObject.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 2.0f;
        npcObject.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().angularSpeed = 12.0f;


        npcObject.gameObject.AddComponent<AudioSource>();
        var audio = npcObject.gameObject.GetComponent<AudioSource>();
        audio.clip = Resources.Load<AudioClip>("Footstep01");
        audio.playOnAwake = false;
        audio.loop = true;
        audio.rolloffMode = AudioRolloffMode.Linear;
        audio.maxDistance = 5.0f;
        audio.minDistance = 0.0f;
        audio.spatialBlend = 1.0f;
        AllPersons.Add(npcObject);

        return npcObject;
    }

    public float PlayAnimation(int actionId)
    {
        var action = CurrentNodesCollection[actionId];
        var animationName = action.AnimationName;


        var animationplayer = gameObject.GetComponent<Animation>();
        animationplayer.Play(animationName);
        var time = animationplayer.GetClip(animationName).length;

        if (IsEventActor)
        {
            if (MyActingEvent != null)
            {
                MyActingEvent.IsReady = true;
            }
        }
        return time + 0.1f;
    }

    public bool IsInterestedInEvent()
    {
        if (!(EventController.ActiveEvents.Count > 0))
            return false;

        IEnumerable<EventObject> eventsInRadius;
        //EventActors can also look at events that are not ready yet, since they are the ones who have to make them ready
        if (IsEventActor)
        {
            eventsInRadius = EventController.ActiveEvents.Values
                .Where(ev => Vector3.Distance(transform.position, ev.Position) <= ev.Radius);
        }
        else
        {
            eventsInRadius = EventController.ActiveEvents.Values
                .Where(ev => Vector3.Distance(transform.position, ev.Position) <= ev.Radius && ev.IsReady );
        }

        var eventsInterested = new List<EventObject>();

        var isInterestedEvent = false;

        foreach (var ev in eventsInRadius)
        {
            foreach (var minimum in ev.PersonalityMinimums)
            {
                if (minimum.Item2 > PersonalityValuesGenericVector.Points[minimum.Item1])
                {
                    isInterestedEvent = false;
                    break;
                }
                isInterestedEvent = true;
            }

            if (isInterestedEvent)
                eventsInterested.Add(ev);
        }

        if (!eventsInterested.Any()) return false;

        //todo: Shouldn this be the event with the highest interestrating?
        MyEvent = eventsInterested[Random.Range(0, eventsInterested.Count)];

        //Todo: Make this specific for an event using an id instead of a bool
        //if (IsEventActor)
            MyActingEvent = MyEvent;

        return true;
    }

    public void UpdateAccumulatedValues(int actionId)
    {
        AccumulatedValues.Sum(
            CurrentNodesCollection[actionId].PersonalityModifiers.ToGenericVector());
    }

    public void MoveTo(int actionId)
    {
        var action = CurrentNodesCollection[actionId];
        var position = action.ClaimablePositions[Id];

        //if the action is different from the currentaction
        if (CurrentActionPosition != position)
        {
            UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            agent.updateRotation = true;
            agent.angularSpeed = 999;
            agent.speed = 3;
            agent.destination = position;
        }
    }

    public bool IsPositionAvailable(int actionId)
    {
        var action = _GetAction(actionId);
        return action.IsPositionAvailable(Id);
    }

    public void ClaimPosition(int actionId)
    {
        var action = _GetAction(actionId);
        if (action.ClaimablePositions.ContainsKey(Id))
        {
            RemoveClaimToPosition(actionId);
        }

        action.CreatePosition(Id);
    }

    public Vector3 GetClaimedPosition(int actionId)
    {
        var action = _GetAction(actionId);
        return action.ClaimablePositions[Id];
    }

    public void RemoveClaimToPosition(int actionId)
    {
        if (ActionExists(actionId))
        {
            var action = _GetAction(actionId);
            action.ClaimablePositions.Remove(Id);
        }
    }

    /* HELPERS */

    //make action positions be the interaciton position
    public void ChangeActionPositions(Vector3 newPosition)
    {
        foreach (var action in CurrentNodesCollection)
        {
            action.Value.Position = newPosition;
        }
    }


    private GameAction _GetAction(int actionId)
    {
        var actionForId = CurrentNodesCollection[actionId];
        return actionForId;
    }

    private bool ActionExists(int actionId)
    {
        return CurrentNodesCollection.ContainsKey(actionId);
    }

    private IEnumerator PauseAnimation(string animationName, float time)
    {
        yield return new WaitForSeconds(time);
        Animator.enabled = false;
    }

    private IEnumerator StopAnimation(string animationName, float time)
    {
        yield return new WaitForSeconds(time);
//        Animator.SetBool(animationName, false);
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          