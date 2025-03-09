using System;
using System.Collections.Generic;
using Audio;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Timeline;

namespace Cams
{
    [RequireComponent(typeof(CinemachineCamera))]
    [RequireComponent(typeof(SignalReceiver))]
    public class SubCamera : MonoBehaviour
    {
        [field: SerializeField] public CinemachineCamera Camera { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public float PanRange { get; private set; }
        [field: SerializeField] public float PanSpeed { get; private set; }

        [SerializeField] private float _panCenter;
        [SerializeField] private float _panMin;
        [SerializeField] private float _panMax;
        [SerializeField] private float _currentPanAngle;
        [SerializeField] private bool _setMinMaxAuto;
        [SerializeField] private int _ambienceIndex;
        
        private void Awake()
        {
            Camera = GetComponentInChildren<CinemachineCamera>();
            _panCenter = Camera.transform.eulerAngles.y;

            if (_setMinMaxAuto)
            {
                _panMin = _panCenter - PanRange / 2;
                _panMax = _panCenter + PanRange / 2;
            }

            _currentPanAngle = _panCenter;
        }

        private void OnValidate()
        {
            Camera = GetComponentInChildren<CinemachineCamera>();

            if (_setMinMaxAuto)
            {
                _panMin = _panCenter - PanRange / 2;
                _panMax = _panCenter + PanRange / 2;
            }
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _currentPanAngle -= PanSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                _currentPanAngle += PanSpeed * Time.deltaTime;
            }
            _currentPanAngle = Mathf.Clamp(_currentPanAngle, _panMin, _panMax);
            //UpdateRotation();
        }

        private void UpdateRotation()
        {
            Vector3 euler = Camera.transform.eulerAngles;
            euler.y = _currentPanAngle;
            Camera.transform.eulerAngles = euler;
        }

      
    }
}