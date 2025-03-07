using System;
using UnityEngine;

namespace World
{
    [RequireComponent(typeof(Collider))]
    public class ObjectLocation : MonoBehaviour
    {
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }
    }
}