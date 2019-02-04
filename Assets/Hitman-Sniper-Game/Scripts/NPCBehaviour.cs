using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    public bool isTarget = false;

    public List<string> targetDescriptions;

    public void OnHit()
    {
        if (isTarget)
        {
            Debug.Log("VICTORY!!!!!!!!!!!!!");
        }
        else
        {
            Debug.Log("U HIT AN INNOCENT!!!!!!!!!!!!!");
        }
    }
}
