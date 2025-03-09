using UnityEngine;
using World;

namespace Minigame
{
    public class TablingAI : CharacterAI
    {
        private ObjectLocation _objectLocation;
        
        public override void Init()
        {
            if (!MurderGame.Instance.HasFreeTable)
            {
                Parent.AI = new SearcherAI();
            }

            _objectLocation = MurderGame.Instance.RandomTable;
            Parent.SetWaypointTarget(_objectLocation.Waypoint);
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
            if (!_objectLocation.HasObject)
            {
                Parent.PlaceObjectOnNearestLocation();
                Parent.AI = new SearcherAI();
            }
            else
            {
                Parent.AI = new MurdererAI();
            }
        }
    }
}