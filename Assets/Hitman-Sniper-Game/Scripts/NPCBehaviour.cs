using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _ragdoll;

    [SerializeField]
    private ParticleSystem _bloodEffect;

    [SerializeField]
    private float _knockback = 1;

    public bool isTarget = false;
    public List<string> targetDescriptions;

    public void OnHit(RaycastHit hit, Ray ray)
    {

        GameObject sceneManagement = GameObject.Find("SceneManagement");

        if (isTarget)
        {
            sceneManagement.GetComponent<SceneManagement>().VictoryGame();
        }
        else
        {
            sceneManagement.GetComponent<SceneManagement>().DefeatGame();
        }

        if (_ragdoll != null)
        {
            _knockback *= 10000;
            GameObject ragdoll = Instantiate(_ragdoll, transform.position, transform.rotation);
            ragdoll.GetComponentInChildren<Rigidbody>().AddForceAtPosition(ray.direction * _knockback, hit.point);
            Destroy(this.gameObject);

            ParticleSystem blood = Instantiate(_bloodEffect, transform.position, transform.rotation);
        }
    }
}
