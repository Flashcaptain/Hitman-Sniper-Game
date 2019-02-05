using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets
{
    public class PlayerObject : MonoBehaviour
    {
        public bool AllowMovement;

        public static PlayerObject Instantiate()
        {
            var playerGameObjectModelName = GameObject.Find("Player");
            var playerObject = playerGameObjectModelName.GetComponent<PlayerObject>();
            return playerObject;
        }

        public void TriggerPlayerEvent(int eventId)
        {
            var eventForId = EventController.PlayerEvents.First(x => x.Value.Id == eventId).Value;
            if (!EventController.ActiveEvents.Any())
            {
                if (!EventController.ActiveEvents.ContainsKey(eventId))
                {
                    EventController.ActiveEvents.Add(eventId, eventForId);
                    EventPlayer.PlayEventAmbience(eventForId);
                    EventController.PlayerEvents[eventId].TriggerEvent();

                    //Events that have npc's associated with them as actors have IsReady set by those actors.
//                    if (eventForId.NpcActionIds.Count == 0)
                        eventForId.IsReady = true;
                }
                else
                {
                    EventController.ActiveEvents.Remove(eventForId.Id);
                    eventForId.IsReady = false;
                }
            }
        }
    }
}                                        