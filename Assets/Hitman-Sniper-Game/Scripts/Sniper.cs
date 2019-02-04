using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    [SerializeField]
    private float _range = 20;

    [SerializeField]
    private float _zoomRange = 20;

    [SerializeField]
    private float _zoomInSpeed = 1;

    [SerializeField]
    private float _zoomOutSpeed = 3;

    [SerializeField]
    private GameObject _camera;

    [SerializeField]
    private GameObject _scope;

    private float _cameraSpeed;
    private bool _isZoomed;

    Vector2 _rotation = new Vector2(0, 0);

    void Start()
    {
        _cameraSpeed = _zoomOutSpeed;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ScopeIn();
        }

        _rotation.y += Input.GetAxis("Mouse X");
        _rotation.x += -Input.GetAxis("Mouse Y");
        transform.eulerAngles = (Vector2)_rotation * _cameraSpeed;
    }

    void Fire()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _range))
        {
            Debug.Log(hit);
            //Human human = hit.collider.gameObject.GetComponent<Human>();
            //if (human != null)
            //{
            //    human.OnHit();
            //}
        }
    }
    void ScopeIn()
    {
        if (_isZoomed)
        {
            _camera.transform.position = transform.position;
            _isZoomed = false;
            _scope.SetActive(false);
            _cameraSpeed = _zoomOutSpeed;
        }
        else
        {
            _camera.transform.Translate (0,0, _zoomRange);
            _isZoomed = true;
            _scope.SetActive(true);
            _cameraSpeed = _zoomInSpeed;
        }
    }
}
