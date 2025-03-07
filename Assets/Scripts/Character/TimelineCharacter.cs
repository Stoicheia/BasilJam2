using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.Timeline;
using World;

namespace Character
{
    [RequireComponent(typeof(SignalReceiver))]
    public class TimelineCharacter : MonoBehaviour
    {
        public const float OBJECT_SEARCH_RADIUS = 10;
        
        [SerializeField] private Animator _characterMoveAnimator;
        [SerializeField] private Animator _spriteAnimator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _objectAnchor;
        [SerializeField] private float _spriteAnimSpeed;
        [SerializeField][ReadOnly] private SignalReceiver _signalReceiver;

        private void Awake()
        {
            _signalReceiver = GetComponent<SignalReceiver>();
        }

        [Button]
        public void PlaySpriteAnim(AnimationName anim)
        {
            _spriteAnimator.Play(anim.ToString());
        }

        public void PlayIdle()
        {
            Debug.Log("PlayIdle");
            PlaySpriteAnim(AnimationName.Idle);
        }

        public void PlayMove()
        {
            Debug.Log("PlayMove");
            PlaySpriteAnim(AnimationName.Move);
        }

        public void PickNearestObject()
        {
            Debug.Log("PickNearest");
            Collider[] inRange = Physics.OverlapSphere(_objectAnchor.position, OBJECT_SEARCH_RADIUS);
            var closest = inRange.Select(x => x.GetComponent<PickableObject>()).Where(x => x != null)
                .OrderBy(x => Vector3.Distance(x.transform.position, _objectAnchor.position)).FirstOrDefault();
            if (closest == null)
            {
                Debug.LogWarning($"Called PickNearestObject, but no object within {OBJECT_SEARCH_RADIUS} units was found.");
                return;
            }
            AttachObject(closest.transform);
        }

        public void PlaceObjectOnNearestLocation()
        {
            Debug.Log("PlaceNearest");
            Collider[] inRange = Physics.OverlapSphere(_objectAnchor.position, OBJECT_SEARCH_RADIUS);
            var closest = inRange.Select(x => x.GetComponent<ObjectLocation>()).Where(x => x != null)
                .OrderBy(x => Vector3.Distance(x.transform.position, _objectAnchor.position)).FirstOrDefault();
            if (closest == null)
            {
                Debug.LogWarning($"Called PlaceObjectOnNearestLocation, but no location within {OBJECT_SEARCH_RADIUS} units was found.");
                return;
            }
            DetachObject(closest);
        }
        
        [Button]
        public void AttachObject(Transform obj)
        {
            obj.position = _objectAnchor.position;
            obj.parent = _objectAnchor;
        }

        [Button]
        public void DetachObject(ObjectLocation target)
        {
            SpriteRenderer existingObject = _objectAnchor.GetComponentInChildren<SpriteRenderer>();
            if (existingObject == null)
            {
                Debug.LogError("Attempted to detach object with no object attached");
                return;
            }
            existingObject.transform.position = target.Anchor.position;
            existingObject.transform.parent = target.transform;
        }
        
        private void OnValidate()
        {
            _signalReceiver = GetComponent<SignalReceiver>();
        }
        
        private void Update()
        {
            _spriteAnimator.speed = _spriteAnimSpeed;
        }
    }

    [Serializable]
    public enum AnimationName
    {
        Idle, Move, Chat
    }
}
