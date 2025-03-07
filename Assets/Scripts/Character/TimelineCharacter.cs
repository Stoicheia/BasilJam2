using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    public class TimelineCharacter : MonoBehaviour
    {
        [SerializeField] private Animator _characterMoveAnimator;
        [SerializeField] private Animator _spriteAnimator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _objectAnchor;

        [Button]
        public void PlaySpriteAnim(AnimationName anim)
        {
            _spriteAnimator.Play(anim.ToString());
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
    }

    [Serializable]
    public enum AnimationName
    {
        Idle, Move, Chat
    }
}
