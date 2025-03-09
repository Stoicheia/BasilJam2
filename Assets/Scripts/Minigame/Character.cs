using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.AI;
using World;
using Random = UnityEngine.Random;

namespace Minigame
{
    public class Character : SerializedMonoBehaviour
    {
        public const float OBJECT_SEARCH_RADIUS = 10;
        public const float MURDER_RADIUS = 1f;
        public event Action<MoveLogic> OnReachTarget;

        public CharacterAI AI
        {
            get => _ai;
            set
            {
                if (_ai != null)
                {
                    _ai.Exit();
                }
                _ai = value;
                _ai.Parent = this;
                _ai.Init();
            }
        }
        [field: SerializeField] public List<Waypoint> Waypoints { get; set; }
        [SerializeField] protected NavMeshAgent _agent;
        [SerializeField] protected Animator _spriteAnimator;
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected Transform _objectAnchor;
        [OdinSerialize] private CharacterAI _ai;
        [SerializeField] private float _spriteAnimSpeed;

        [field: SerializeField] public MoveLogic MoveLogic { get; set; }

        [Header("Movement")] 
        [SerializeField] protected float _moveSpeedPoint;
        [SerializeField] protected float _accelerationPoint;
        [SerializeField] protected float _moveSpeedFollow;
        [SerializeField] protected float _accelerationFollow;

        private void Start()
        {
            _agent.updateRotation = false;
        }

        private void Update()
        {
            _spriteAnimator.speed = _spriteAnimSpeed;
            switch (MoveLogic.Mode)
            {
                case MoveMode.Still:
                    _agent.speed = 0;
                    break;
                case MoveMode.Point:
                    _agent.speed = _moveSpeedPoint;
                    _agent.acceleration = _accelerationPoint;
                    _agent.destination = MoveLogic.Point.transform.position;
                    if (IsCloseToTarget(_agent.stoppingDistance))
                    {
                        OnReachTarget?.Invoke(MoveLogic);
                    }
                    break;
                case MoveMode.Follow:
                    _agent.speed = _moveSpeedFollow;
                    _agent.acceleration = _accelerationFollow;
                    _agent.destination = MoveLogic.Follow.transform.position;
                    if (IsCloseToTarget(MURDER_RADIUS))
                    {
                        MoveLogic.Follow.ReceiveInteraction(this);
                        OnReachTarget?.Invoke(MoveLogic);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            AI.Tick();
        }

        private bool IsCloseToTarget(float threshold)
        {
            return _agent.remainingDistance <= threshold;
        }

        public void SetWaypointTarget(Waypoint point)
        {
            MoveLogic = new MoveLogic()
            { 
                Mode = MoveMode.Point,
                Point = point,
                Follow = null
            };
        }
        
        public void SetRandomWaypointTarget()
        {
            MoveLogic = new MoveLogic()
            { 
                Mode = MoveMode.Point,
                Point = Waypoints[Random.Range(0, Waypoints.Count)],
                Follow = null
            };
        }

        public void SetFollowTarget(Character other)
        {
            MoveLogic = new MoveLogic()
            {
                Mode = MoveMode.Follow,
                Point = null,
                Follow = other
            };
        }

        public void SetStill()
        {
            MoveLogic = new MoveLogic()
            {
                Mode = MoveMode.Still,
                Point = null,
                Follow = null
            };
        }
        
        public void PickNearestObject()
        {
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

        public void ReceiveInteraction(Character follower)
        {
            Debug.Log($"Interacted by {follower.name}");
        }
    }

    [Serializable]
    public struct MoveLogic
    {
        public MoveMode Mode;
        public Waypoint Point;
        public Character Follow;
    }

    [Serializable]
    public enum MoveMode
    {
        Still, Point, Follow
    }
}