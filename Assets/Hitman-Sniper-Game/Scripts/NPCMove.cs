using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour{
    public GameObject playerLocation;

    bool _patrolWaiting;

    public float _totalWaitTime = 3f;


    float _switchProbability = 0.2f;

    public List<waypoint> _wayPoints;

    NavMeshAgent _navMeshAgent;
    int _currentPatrolIndex;
    bool _travelling;
    bool _waiting;
    bool _parolForward;
    float _waitTimer;


    // Use this for initialization
    void Start(){
 
            _navMeshAgent = this.GetComponent<NavMeshAgent>();

            if (_navMeshAgent == null){
                Debug.LogError("The nav mesh agent component is not attatched to" + gameObject.name);
            }

            else{
                if (_wayPoints != null && _wayPoints.Count >= 2){
                    _currentPatrolIndex = 0;
                    SetDestination();
                }
                else{
                    Debug.Log("Insufficient patrol point for bacis patrolling behaviour");
                }
            }

        }

    // Update is called once per frame
    void Update(){
            if (_travelling && _navMeshAgent.remainingDistance <= 1.0f){
                _travelling = false;

                if (_patrolWaiting){
                    _waiting = true;
                    _waitTimer = 0f;
                }

                else{
                    ChangeWayPoint();
                    SetDestination();
                }

            if (_waiting){
                _waitTimer += Time.deltaTime;
                if (_waitTimer >= _totalWaitTime){
                    _waiting = false;

                    ChangeWayPoint();
                    SetDestination();
                }
            }
        }
    }


    private void SetDestination(){

            if (_wayPoints != null){
                Vector3 targetVector = _wayPoints[_currentPatrolIndex].transform.position;
                _navMeshAgent.SetDestination(targetVector);
                _travelling = true;
                _patrolWaiting = true;
            }
        }

    private void DestinationPlayer(){
        if (_wayPoints != null){
            Vector3 targetVector = playerLocation.transform.position;
            _navMeshAgent.SetDestination(targetVector);
            _travelling = true;
        }
    }

    private void ChangeWayPoint(){
            if (Random.Range(0f, 1f) <= _switchProbability){
                _parolForward = !_parolForward;
            }

            if (_parolForward){
                _currentPatrolIndex = (_currentPatrolIndex + 1) % _wayPoints.Count;
            }

            else{
                if (--_currentPatrolIndex < 0){
                    _currentPatrolIndex = _wayPoints.Count - 1;
                }
            }
    }
}
 