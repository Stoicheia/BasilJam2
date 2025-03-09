using System;
using UnityEngine;

namespace Minigame
{
    [Serializable]
    public class VictimFleerAI : CharacterAI
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
            Parent.SetRandomWaypointTarget();
        }
    }
}