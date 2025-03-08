using System;
using Cams;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CameraNameDisplay : MonoBehaviour
    {
        [SerializeField] private CameraSwitcher _cameraSwitcher;
        [SerializeField] private TextMeshProUGUI _cameraNameField;

        private void Update()
        {
            _cameraNameField.text = _cameraSwitcher.ActiveSubCamera.Name;
        }
    }
}