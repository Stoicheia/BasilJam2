using System;
using UnityEngine;

namespace World
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PickableObject : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        
        private Collider _collider;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }
}