using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Cams
{
    [RequireComponent(typeof(CinemachineCamera))]
    public class CameraInfo : MonoBehaviour
    {
        [field: SerializeField] public CinemachineCamera Camera { get; private set; }
        [field: SerializeField] public string Name { get; private set; }

        private void Awake()
        {
            Camera = GetComponentInChildren<CinemachineCamera>();
        }

        private void OnValidate()
        {
            Camera = GetComponentInChildren<CinemachineCamera>();
        }
    }
}