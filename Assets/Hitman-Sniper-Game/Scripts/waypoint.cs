using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoint : MonoBehaviour{

    [SerializeField]
    protected float debugDrawRadius = 1.0F;

    public virtual void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, debugDrawRadius);
    }
}
