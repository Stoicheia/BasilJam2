using System;
using UnityEngine;

namespace Minigame
{
    [Serializable]
    public abstract class CharacterAI
    {
        public Character Parent { get; set; }
        public abstract void Init();
        public abstract void Tick();
        public abstract void Exit();
    }
}