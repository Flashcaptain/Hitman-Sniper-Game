﻿﻿﻿﻿using Assets;
using UnityEngine;

public class UnityPlayer : MonoBehaviour
{
    private PlayerObject _playerObject;

    public Vector3 Position
    {
        get { return _playerObject.transform.position; }
    }

    public static UnityPlayer Initialize()
    {
        var playerGameObject = GameObject.Find("Player");

        var unityPlayer = playerGameObject.GetComponent<UnityPlayer>();
        unityPlayer._playerObject = PlayerObject.Instantiate();

        return unityPlayer;
    }

    public void TriggerPlayerEvent(int eventId)
    {
        _playerObject.TriggerPlayerEvent(eventId);
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          