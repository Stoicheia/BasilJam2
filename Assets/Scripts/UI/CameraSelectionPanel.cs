using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cams;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using SubCamera = Cams.SubCamera;

namespace UI
{
    public class CameraSelectionPanel : SerializedMonoBehaviour
    {
        [OdinSerialize] private Dictionary<Button, SubCamera> _button2Camera;
        [SerializeField] private CameraSwitcher _cameraSwitcher;
        [SerializeField] private TextMeshProUGUI _warningText;
        [SerializeField] private float _warningTextAppearSecs;

        private void OnEnable()
        {
            foreach (var b2c in _button2Camera)
            {
                Button button = b2c.Key;
                SubCamera cam = b2c.Value;
                
                button.onClick.AddListener(() => HandleClickSwitch(cam));
            }
            _warningText.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            foreach (var b2c in _button2Camera)
            {
                Button button = b2c.Key;
                
                button.onClick.RemoveAllListeners();
            }
        }
        
        private void HandleClickSwitch(SubCamera subC)
        {
            if (_cameraSwitcher.State == CameraState.Closing)
            {
                StartCoroutine(WarningText(_warningTextAppearSecs));
                return;
            }

            _ = _cameraSwitcher.SwitchCamera(subC);
        }

        private IEnumerator WarningText(float secs)
        {
            _warningText.gameObject.SetActive(true);
            yield return new WaitForSeconds(secs);
            _warningText.gameObject.SetActive(false);
        }

        [Button]
        public void AddFields(Transform buttonsRoot)
        {
            List<Button> buttons = buttonsRoot.GetComponentsInChildren<Button>().ToList();
            _button2Camera = new Dictionary<Button, SubCamera>();
            foreach (var button in buttons)
            {
                _button2Camera.Add(button, null);
            }
        }
    }
}