using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Minigame
{
    public class MurderGame : SingletonMonoBehaviour<MurderGame>
    {
        public Character Victim;
        [SerializeField] private Transform _waypointsRoot;
        [SerializeField] private Transform _fleeWaypointsRoot;
        [SerializeField] private List<Waypoint> _waypoints;
        [SerializeField] private List<Waypoint> _fleeWaypoints;
        [SerializeField] private List<Character> _seekers;
        [SerializeField] private float _gameEndsAfter;

        private void Start()
        {
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

        public void MurderHappened(Character murderer)
        {
            Debug.Log($"{murderer} did it.");
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