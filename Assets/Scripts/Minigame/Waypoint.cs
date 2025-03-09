using UnityEngine;
using World;

namespace Minigame
{
    public class Waypoint : MonoBehaviour
    {
        [field: SerializeField] public PickableObject Object { get; set; }

        public void ToggleGuideGraphics(bool on)
        {
            GetComponentInChildren<MeshRenderer>().gameObject.SetActive(on);
        }
    }
}