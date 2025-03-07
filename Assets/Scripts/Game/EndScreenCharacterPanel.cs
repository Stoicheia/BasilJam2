using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class EndScreenCharacterPanel : MonoBehaviour
    {
        public event Action<CharacterInfo> OnSelect;
        
        public CharacterInfo Character { get; private set; }
        [SerializeField] private TextMeshProUGUI _nameField;
        [SerializeField] private Image _spriteField;
        [SerializeField] private Image _confirmPanel;
        [SerializeField] private Button _selectButton;

        private void OnEnable()
        {
            _selectButton.onClick.AddListener(HandleClick);
        }
        
        private void OnDisable()
        {
            _selectButton.onClick.RemoveListener(HandleClick);
        }

        public void Render(CharacterInfo character)
        {
            _nameField.text = character.Name;
            _spriteField.sprite = character.Sprite;
            Character = character;
        }

        public void ToggleSelect(bool on)
        {
            _confirmPanel.gameObject.SetActive(on);
        }
        
        private void HandleClick()
        {
            OnSelect?.Invoke(Character);
        }
    }
}