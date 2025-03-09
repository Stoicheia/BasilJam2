using System;
using UnityEngine;
using UnityEngine.AI;

namespace Minigame
{
    [Serializable]
    public class MurdererAI : CharacterAI
    {
        public override void Init()
        {
            Parent.SetFollowTarget(MurderGame.Instance.Victim);
            Parent.OnReachTarget += HandleReachTarget;
        }
        
        public override void Tick()
        {
            
        }

        public override void Exit()
        {
            Parent.OnReachTarget -= HandleReachTarget;
        }
        
        private void HandleReachTarget(MoveLogic _)
        {
            Debug.Log($"{Parent.name} reached target (murderer)");
            MurderGame.Instance.MurderHappened(Parent);
            Parent.AI = new VictimFleerAI();
        }
    }
}