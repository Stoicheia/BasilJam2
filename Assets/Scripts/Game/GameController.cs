using System;
using Cutscene;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private CutsceneManager _cutsceneManager;
        
        [Header("Screens")]
        [SerializeField] private SlideUI _endScreenAnim;
        [SerializeField] private EndScreen _endScreen;

        [Header("Game Config")]
        [SerializeField] private string _nameOfCorrectCharacter;

        [Header("Debug")] 
        [SerializeField] private bool _debugMode;
        [SerializeField][ShowIf("_debugMode")] private KeyCode _goToEndKey = KeyCode.K;

        private void OnEnable()
        {
            _endScreen.OnSelect += HandleSelectCharacter;
        }
        
        private void OnDisable()
        {
            _endScreen.OnSelect -= HandleSelectCharacter;
        }

        private void Start()
        {
            _endScreenAnim.CloseImmediate();
            StartGame();
        }

        public void StartGame()
        {
            _cutsceneManager.PlayFromStart();
        }

        private void Update()
        {
            if (_debugMode)
            {
                DebugModeUpdate();
            }
        }

        private void DebugModeUpdate()
        {
            if (Input.GetKeyDown(_goToEndKey))
            {
                _ = _endScreenAnim.Open();
            }
        }
        
        private void HandleSelectCharacter(CharacterInfo c)
        {
            if (c.Name == _nameOfCorrectCharacter)
            {
                Debug.Log("You were right");
            }
            else
            {
                Debug.Log("You were wrong");
            }
        }
    }
}