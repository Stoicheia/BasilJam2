using System;
using UnityEngine;
using World;
using Random = UnityEngine.Random;

namespace Minigame
{
    [Serializable]
    public class SearcherAI : CharacterAI
    {
        public override void Init()
        {
            Parent.SetRandomWaypointTarget();
            Parent.OnReachTarget += HandleReachTarget;
        }
        
        public override void Tick()
        {
        }

        public override void Exit()
        {
            Parent.OnReachTarget -= HandleReachTarget;
        }
        
        private void HandleReachTarget(MoveLogic info)
        {
            Debug.Log($"{Parent.name} reached target (searcher)");
            Waypoint reached = info.Point;
            PickableObject pickable = reached.Object;
            if (pickable != null)
            {
                Parent.AttachObject(pickable.transform);
                if (Parent.Severity != Severity.IsTheMurderer && Parent.Severity != Severity.IsMurdering)
                {
                    Parent.Severity = Severity.IsCarryingKnife;
                }
                reached.Object = null;
                if (MurderGame.Instance.HasFreeTable)
                {
                    Parent.AI = new TablingAI();
                }
                else
                {
                    Parent.AI = new MurdererAI();
                }
            }
            else
            {
                Parent.SetRandomWaypointTarget();
            }
        }
    }
}