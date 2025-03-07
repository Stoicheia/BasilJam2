using System;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;

namespace Cams
{
    public class CameraSwitcher : MonoBehaviour
    {
        public CameraInfo ActiveCamera => _controller.ActiveCamera;
        
        [SerializeField] private CameraController _controller;
        [SerializeField] private BlackSlides _slides;
        [SerializeField] private PostProcessController _pp;

        [Header("Settings")] 
        [SerializeField] private float _closeTime;
        [SerializeField] private float _stayTime;
        [SerializeField] private float _openTime;

        [Header("Input")]
        [SerializeField] private KeyCode _leftButton;
        [SerializeField] private KeyCode _rightButton;
        
        [Header("State")] 
        [field: SerializeField] public CameraState State { get; set; }
        [SerializeField] private int _cameraIndex;

        private void Awake()
        {
            SwitchCameraInstantly(0);
        }

        private void Update()
        {
            ProcessInput();
        }

        [Button]
        public async Task SwitchCamera(int index)
        {
            State = CameraState.Closing;
            //_ = _pp.Close();
            await _slides.Close(_closeTime);
            _controller.SwitchCamera(index);
            _cameraIndex = index;
            await Task.Delay((int)(_stayTime*1000));
            _ = _pp.Open();
            await _slides.Open(_openTime);
            State = CameraState.Free;
        }

        public void SwitchCameraInstantly(int index)
        {
            _controller.SwitchCamera(index);
            _ = _pp.Open();
            _cameraIndex = index;
        }

        private void ProcessInput()
        {
            if (Input.GetKeyDown(_leftButton))
            {
                if (State == CameraState.Closing)
                {
                    Debug.LogWarning("A camera switch is already in progress!");
                    return;
                }
                _ = SwitchCamera(_cameraIndex - 1);
            }
            else if (Input.GetKeyDown(_rightButton))
            {
                if (State == CameraState.Closing)
                {
                    Debug.LogWarning("A camera switch is already in progress!");
                    return;
                }
                _ = SwitchCamera(_cameraIndex + 1);
            }
        }
    }
    
    
    [Serializable]
    public enum CameraState
    {
        Closing, Free
    }
}