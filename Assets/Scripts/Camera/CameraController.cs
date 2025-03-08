using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Unity.Cinemachine;
using UnityEngine;

namespace Cams
{
    public class CameraController : MonoBehaviour
    {
        public SubCamera ActiveSubCamera { get; private set; }
        
        [SerializeField] private List<SubCamera> _virtualCameras;
        [SerializeField] private CinemachineBrain _brain;

        private void Awake()
        {
            GetVirtualCameras();
        }

        [Button]
        public SubCamera SwitchCamera(int index)
        {
            SubCamera activeCamInfo = _virtualCameras[index % _virtualCameras.Count];
            SwitchCamera(activeCamInfo);
            return activeCamInfo;
        }

        public void SwitchCamera(SubCamera cam)
        {
            CinemachineCamera activeCam = cam.Camera;
            foreach (var c in _virtualCameras)
            {
                if (c.Camera == activeCam)
                {
                    c.gameObject.SetActive(true);
                }
                else
                {
                    c.gameObject.SetActive(false);
                }
            }
            
            ActiveSubCamera = cam;
        }

        public int GetIndexOf(SubCamera cam)
        {
            return _virtualCameras.IndexOf(cam);
        }

        [Button]
        public void GetVirtualCameras()
        {
            _virtualCameras = GetComponentsInChildren<SubCamera>(true).ToList();
        }
    }
}