using System;
using System.Collections.Generic;
using Cutscene;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        private int _totalActCount => _cutsceneManagers.Count;
        [SerializeField] private List<CutsceneManager> _cutsceneManagers;
        
        [Header("Screens")]
        [SerializeField] private SlideUI _endScreenAnim;
        [SerializeField] private EndScreen _endScreen;
        [SerializeField] private SlideUI _correctScreenAnim;
        [SerializeField] private SlideUI _wrongScreenAnim;

        [Header("Game Config")]
        [SerializeField] private string _nameOfCorrectCharacter;

        [Header("State")] 
        [SerializeField] [ReadOnly] private int _actIndex;
        [SerializeField] [ReadOnly] private CutsceneManager _activeCutscene;

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
            PlayAct(0);
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
        
        private async void HandleSelectCharacter(CharacterInfo c)
        {
            if (c.Name == _nameOfCorrectCharacter)
            {
                Debug.Log("You were right");
                await _correctScreenAnim.Open();
                _ = _endScreenAnim.Close();
            }
            else
            {
                Debug.Log("You were wrong");
                await _wrongScreenAnim.Open();
                _ = _endScreenAnim.Close();
            }
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        [Button]
        public void PlayAct(int index)
        {
            if (_activeCutscene != null)
            {
                _activeCutscene.Pause();
            }
            _actIndex = index;
            _activeCutscene = _cutsceneManagers[index];
            _activeCutscene.PlayFromStart();
        }
        
        public void OnNextAct()
        {
            _actIndex++;
            if (_actIndex >= _totalActCount)
            {
                _ = _endScreenAnim.Open();
            }
            else
            {
                PlayAct(_actIndex);
            }
        }
    }
}