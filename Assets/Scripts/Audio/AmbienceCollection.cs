using UnityEngine;
using System.Collections.Generic;

namespace Audio
{
    [CreateAssetMenu(fileName = "Ambience Collection", menuName = "Ambience Collection", order = 0)]
    public class AmbienceCollection : ScriptableObject
    {
        public List<AmbienceInfo> AmbienceSequence;

        public AmbienceInfo GetAmbience(int index)
        {
            var ambiences = AmbienceSequence.FindAll(x => x.Index == index);
            if (ambiences.Count == 0)
            {
                return new AmbienceInfo()
                {
                    Clip = null
                };
            }
            else if (ambiences.Count > 1)
            {
                Debug.LogWarning($"Multiple ambiences with index {index} found. Not allowed; returning first one.");
            }
            return ambiences[0];
        }
    }
}