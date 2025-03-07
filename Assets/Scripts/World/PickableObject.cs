using System;
using UnityEngine;

namespace World
{
    [RequireComponent(typeof(Collider))]
    public class PickableObject : MonoBehaviour
    {
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }
    }
}