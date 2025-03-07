using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game
{
    public class EndScreen : MonoBehaviour
    {
        public event Action<CharacterInfo> OnSelect;
        
        [Header("Init")]
        [SerializeField] private List<CharacterInfo> _characters;
        [SerializeField] private EndScreenCharacterPanel _charPrefab;
        
        [Header("Fields")]
        [SerializeField] private HorizontalLayoutGroup _charactersRoot;
        [SerializeField] private Button _selectButton;
        [SerializeField] private TextMeshProUGUI _selectButtonText;

        [Header("State")] 
        [SerializeField] private EndScreenCharacterPanel _currentlySelected;

        private Dictionary<string, EndScreenCharacterPanel> _charNameToPanel;

        private void OnEnable()
        {
            _selectButton.onClick.AddListener(HandleConfirm);
            RenderCharacters();
        }
        
        private void OnDisable()
        {
            _selectButton.onClick.RemoveListener(HandleConfirm);
        }

        [Button]
        public void RenderCharacters()
        {
            _charNameToPanel = new Dictionary<string, EndScreenCharacterPanel>();
            List<Transform> ts = new List<Transform>();
            
            foreach (Transform child in _charactersRoot.transform)
            {
                ts.Add(child);
            }
            
            foreach (Transform child in ts)
            {
                EndScreenCharacterPanel panel = child.GetComponent<EndScreenCharacterPanel>();
                if (panel != null)
                {
                    panel.OnSelect -= HandleSelectCharacter;
                }
                DestroyImmediate(child.gameObject);
            }
            
            foreach (CharacterInfo character in _characters)
            {
                EndScreenCharacterPanel panelInstance = Instantiate(_charPrefab, _charactersRoot.transform);
                panelInstance.Render(character);
                panelInstance.ToggleSelect(false);
                panelInstance.OnSelect += HandleSelectCharacter;
                _charNameToPanel.Add(character.Name, panelInstance);
            }
            _selectButton.gameObject.SetActive(false);
        }

        private void HandleSelectCharacter(CharacterInfo c)
        {
            _selectButton.gameObject.SetActive(true);
            _currentlySelected = _charNameToPanel[c.Name];
            foreach (var panel in _charNameToPanel.Values)
            {
                panel.ToggleSelect(false);
            }
            _currentlySelected.ToggleSelect(true);
            _selectButtonText.text = $"{c.Name} did it";
        }
        
        private void HandleConfirm()
        {
            OnSelect?.Invoke(_currentlySelected.Character);
        }
    }

    [Serializable]
    public struct CharacterInfo
    {
        public Sprite Sprite;
        public string Name;
    }
}