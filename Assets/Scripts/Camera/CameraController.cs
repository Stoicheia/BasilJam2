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
        public CameraInfo ActiveCamera { get; private set; }
        
        [SerializeField] private List<CameraInfo> _virtualCameras;
        [SerializeField] private CinemachineBrain _brain;

        private void Awake()
        {
            GetVirtualCameras();
        }

        [Button]
        public CameraInfo SwitchCamera(int index)
        {
            CameraInfo activeCamInfo = _virtualCameras[index % _virtualCameras.Count];
            CinemachineCamera activeCam = activeCamInfo.Camera;
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
            
            ActiveCamera = activeCamInfo;
            return activeCamInfo;
        }

        [Button]
        public void GetVirtualCameras()
        {
            _virtualCameras = GetComponentsInChildren<CameraInfo>(true).ToList();
        }
    }
}