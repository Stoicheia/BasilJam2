using System;
using Minigame;
using UnityEngine;

namespace World
{
    [RequireComponent(typeof(Collider))]
    public class ObjectLocation : MonoBehaviour
    {
        private Collider _collider;
        public Waypoint Waypoint;
        [field: SerializeField] public Transform Anchor { get; private set; }

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }
        
        public bool HasObject => GetComponentInChildren<SpriteRenderer>() != null;

        public void Attach(PickableObject obj)
        {
            Waypoint.Object = obj;
        }

        public void Detach()
        {
            Waypoint.Object = null;
        }
    }
}