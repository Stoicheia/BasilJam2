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
            if(Parent.Severity != Severity.IsTheMurderer)
                Parent.Severity = Severity.IsMurdering;
        }
        
        public override void Tick()
        {
            
        }

        public override void Exit()
        {
            Parent.OnReachTarget -= HandleReachTarget;
        }
        
        private void HandleReachTarget(MoveLogic target)
        {
            if (!Parent.HasObject())
            {
                Parent.AI = new SearcherAI();
                return;
            }
            Debug.Log($"{Parent.name} reached target (murderer)");
            if (MurderGame.Instance.MurderHappened(Parent))
            {
                Parent.Severity = Severity.IsTheMurderer;
                target.Follow.ReceiveInteraction(Parent);
            }
            
            Parent.AI = new VictimFleerAI();
        }
    }
}