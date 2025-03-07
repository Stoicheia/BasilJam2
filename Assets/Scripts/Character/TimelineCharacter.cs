using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Timeline;

namespace Character
{
    [RequireComponent(typeof(SignalReceiver))]
    public class TimelineCharacter : MonoBehaviour
    {
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
            PlaySpriteAnim(AnimationName.Idle);
        }

        public void PlayMove()
        {
            PlaySpriteAnim(AnimationName.Move);
        }
        
        [Button]
        public void AttachObject(SpriteRenderer objRenderer)
        {
            objRenderer.transform.position = _objectAnchor.position;
            objRenderer.transform.parent = _objectAnchor;
        }

        [Button]
        public void DetachObject(Transform target)
        {
            SpriteRenderer existingObject = _objectAnchor.GetComponentInChildren<SpriteRenderer>();
            if (existingObject == null)
            {
                Debug.LogError("Attempted to detach object with no object attached");
                return;
            }
            existingObject.transform.position = target.position;
            existingObject.transform.parent = target;
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
