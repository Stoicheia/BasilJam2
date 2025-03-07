using System;
using UnityEngine;

namespace World
{
    [RequireComponent(typeof(Collider))]
    public class ObjectLocation : MonoBehaviour
    {
        private Collider _collider;
        [field: SerializeField] public Transform Anchor { get; private set; }

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }
    }
}