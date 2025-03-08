using Cams;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CameraSelectionButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textField;
        [SerializeField] private Image _spriteField;
        [field: SerializeField] public Button Button { get; private set; }
        [field: SerializeField] public SubCamera Camera { get; private set; }
        [SerializeField] private Sprite _inactiveSprite;
        [SerializeField] private Sprite _activeSprite;

        public void Render()
        {
            _textField.text = Camera.Name;
        }

        public void SetActiveSprite()
        {
            _spriteField.sprite = _activeSprite;
        }

        public void SetInactiveSprite()
        {
            _spriteField.sprite = _inactiveSprite;
        }
    }
}