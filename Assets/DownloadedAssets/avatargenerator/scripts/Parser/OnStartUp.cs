using Assets;
using UnityEngine;

public class OnStartUp : MonoBehaviour
{
    void Awake()
    {
        ActionsParser.ParseEvents();
        ActionsParser.ParseEventsActions("eventActions");
        ActionsParser.ParseEventsActions("eventReactions");
        ActionsParser.ParseNormalActions();

        //This parses the spawnpositions and modelnames from settings.xml
        SettingsParser.ParseAll();
        PersonParser.ParsePersons();

        //making all actions and events available for use by the npcs and player respectively
        UnityAction.SpawnActions();
        UnityEventController.SpawnAllPlayerEvents();

//        UnityPlayer.Initialize();
//        ApplyPlayerSettings();
    }

//    private void ApplyPlayerSettings()
//    {
//        var playerObject = GameObject.Find("Player");
//        var playerObjectComponent = playerObject.GetComponent<PlayerObject>();
//
//        float newNavMeshRadius;
//
//        if (playerObjectComponent.AllowMovement)
//            newNavMeshRadius = 1.5f;
//        else
//            newNavMeshRadius = 4.5f;
//
//        playerObject.GetComponent<NavMeshObstacle>().radius = newNavMeshRadius;
//        playerObject.GetComponent<FPSInputController>().enabled = playerObjectComponent.AllowMovement;
//    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                            