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
    private string _knockbackTrigger;

    [SerializeField]
    private string _ZoomInTrigger;

    [SerializeField]
    private string _ZoomOutTrigger;

    [SerializeField]
    private string _holdBreathTrigger;

    [SerializeField]
    private string _releaseBreathTrigger;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private Animator _animatorSniperRifle;

    [SerializeField]
    private GameObject _camera;

    [SerializeField]
    private GameObject _scope;

    [SerializeField]
    private float _fireCooldown;

    [SerializeField]
    private float _holdBreathCooldown;

    private float _cameraSpeed;
    private float _holdBreathCooldownMax;
    private bool _isZoomed = false;
    private bool _isHoldingBreath = false;
    private bool _canFire = true;
    private Vector2 _rotation = new Vector2(0, 0);


    void Start()
    {
        _holdBreathCooldownMax = _holdBreathCooldown;
        _cameraSpeed = _zoomOutSpeed;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_canFire && _isZoomed)
            {
                ResetTriggers();
                Fire();
                StartCoroutine(StartFireCooldown());
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ResetTriggers();
            ScopeIn();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ResetTriggers();
            _isHoldingBreath = true;
            StartCoroutine(HoldingBreath());
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _isHoldingBreath = false;
        }

        _rotation.y += Input.GetAxis("Mouse X");
        _rotation.x += -Input.GetAxis("Mouse Y");
        transform.eulerAngles = (Vector2)_rotation * _cameraSpeed;
    }
    void ResetTriggers()
    {
        _animator.ResetTrigger(_knockbackTrigger);
        _animator.ResetTrigger(_releaseBreathTrigger);
        _animator.ResetTrigger(_holdBreathTrigger);
        _animator.ResetTrigger(_ZoomInTrigger);
        _animator.ResetTrigger(_ZoomOutTrigger);
    }

        void Fire()
    {
        Ray ray = new Ray(_camera.transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _range))
        {
            Debug.Log(hit);
            NPCBehaviour NPC = hit.collider.gameObject.GetComponent<NPCBehaviour>();
            if (NPC != null)
            {
                NPC.OnHit();
            }
        }
    }
    void ScopeIn()
    {
        if (_isZoomed)
        {
            _camera.transform.position = transform.position;
            _animatorSniperRifle.SetTrigger(_ZoomOutTrigger);
            _animator.SetTrigger(_ZoomOutTrigger);
            _isZoomed = false;
            _scope.SetActive(false);
            _cameraSpeed = _zoomOutSpeed;
        }
        else
        {
            _camera.transform.Translate(0, 0, _zoomRange);
            _animatorSniperRifle.SetTrigger(_ZoomInTrigger);
            _animator.SetTrigger(_ZoomInTrigger);
            _isZoomed = true;
            _scope.SetActive(true);
            _cameraSpeed = _zoomInSpeed;
        }
    }

    private IEnumerator StartFireCooldown()
    {
        _canFire = false;
        _animator.SetTrigger(_knockbackTrigger);
        yield return new WaitForSeconds(_fireCooldown);
        _canFire = true;
    }

    private IEnumerator HoldingBreath()
    {
        _animator.SetTrigger(_holdBreathTrigger);
        while (_isHoldingBreath && _isZoomed && _canFire && _holdBreathCooldown > 0)
        {
            yield return new WaitForSeconds(0.1f);
            _holdBreathCooldown -= 0.1f;
        }
        _isHoldingBreath = false;
        StartCoroutine(ReleasingBreath());
    }

    private IEnumerator ReleasingBreath()
    {
        _animator.SetTrigger(_releaseBreathTrigger);
        while (!_isHoldingBreath && _holdBreathCooldown < _holdBreathCooldownMax)
        {
            yield return new WaitForSeconds(0.1f);
            _holdBreathCooldown += 0.1f;
        }
        _holdBreathCooldown = _holdBreathCooldownMax;
    }
}
