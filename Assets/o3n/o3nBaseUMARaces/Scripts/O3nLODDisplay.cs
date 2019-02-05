using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA;
using UMA.CharacterSystem;

namespace UMA.Examples
{
    public class O3nLODDisplay : MonoBehaviour
    {
        public GameObject LODDisplayPrefab;
        private TextMesh _lodDisplay;

        private int _lastSetLevel = -1;
        private Transform _cameraTransform;
        private O3nUMASimpleLOD _simpleLOD;

        private bool initialized = false;
        private DynamicCharacterAvatar _avatar;
        private UMAData _umaData;
        private GameObject tm;


        public void CharacterCreated(UMAData umaData)
        {
            initialized = true;
        }

        public void CharacterBegun(UMAData umaData)
        {
            initialized = true;
        }

        public void OnEnable()
        {
            //cache the camera transform for performance
            _cameraTransform = Camera.main.transform;
            _simpleLOD = GetComponent<O3nUMASimpleLOD>();

            _avatar = GetComponent<DynamicCharacterAvatar>();
            if (_avatar != null)
            {
                _avatar.CharacterBegun.AddListener(CharacterBegun);
            }
            else
            {
                _umaData = GetComponent<UMAData>();
                if (_umaData != null)
                    _umaData.CharacterCreated.AddListener(CharacterCreated);
            }


        }

        // Update is called once per frame
        void Update()
        {
            if (_simpleLOD == null)
                return;

            // Add the display prefab
            if (LODDisplayPrefab != null && initialized && tm == null)
            {
                tm = (GameObject)GameObject.Instantiate(LODDisplayPrefab, transform.position, transform.rotation);
                tm.transform.SetParent(transform);
                tm.transform.localPosition = new Vector3(0, 2f, 0f);
                tm.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                _lodDisplay = tm.GetComponent<TextMesh>();
            }

            if (_lodDisplay != null)
            {
                if (_lastSetLevel != _simpleLOD.CurrentLOD)
                {
                    _lastSetLevel = _simpleLOD.CurrentLOD;
                    _lodDisplay.text = string.Format("LOD #{0}", _lastSetLevel);
                }
                var delta = transform.position - _cameraTransform.position;
                delta.y = 0;
                _lodDisplay.transform.rotation = Quaternion.LookRotation(delta, Vector3.up);
            }
        }
    }
}
