using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Serialization;
using World;

namespace Minigame
{
    public class MurderGame : SingletonMonoBehaviour<MurderGame>
    {
        public Character Victim;
        public bool WasMurdered;
        public Transform GravestonePrefab;
        public List<ObjectLocation> Tables;
        public ObjectLocation RandomTable => Tables.First(x => !x.HasObject);
        public bool HasFreeTable => Tables.Any(x => !x.HasObject);
        [SerializeField] private Transform _waypointsRoot;
        [SerializeField] private Transform _fleeWaypointsRoot;
        [SerializeField] private List<Waypoint> _waypoints;
        [SerializeField] private List<Waypoint> _fleeWaypoints;
        [SerializeField] private List<Character> _seekers;
        [SerializeField] private float _gameEndsAfter;

        private void Start()
        {
            WasMurdered = false;
            _waypoints = _waypointsRoot.GetComponentsInChildren<Waypoint>().ToList();
            _waypoints.ForEach(x => x.ToggleGuideGraphics(false));
            _fleeWaypoints = _fleeWaypointsRoot.GetComponentsInChildren<Waypoint>().ToList();
            _fleeWaypoints.ForEach(x => x.ToggleGuideGraphics(false));
            _seekers.ForEach(x =>
            {
                x.Waypoints = _waypoints;
                x.AI = new SearcherAI();
            });
            
            Victim.Waypoints = _fleeWaypoints;
            Victim.AI = new VictimFleerAI();
        }

        public bool MurderHappened(Character murderer)
        {
            if (WasMurdered) return false;
            Debug.Log($"{murderer} did it.");
            WasMurdered = true;
            return true;
        }

        public void EndGame()
        {
            Debug.Log("The game ended.");
        }

        private IEnumerator EndGameAfter(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            EndGame();
        }
    }
}