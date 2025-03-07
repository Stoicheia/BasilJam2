using System.Threading.Tasks;
using DG.Tweening;
using Game;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI
{
    public class SlideUI : MonoBehaviour
    {
        [Header("Dependencies")] 
        [SerializeField] private Transform _content;
        [SerializeField] private Transform _openPos;
        [SerializeField] private Transform _closePos;
        
        [Header("Settings")]
        [SerializeField] private float _openTime;
        [SerializeField] private float _closeTime;
        [SerializeField] private Ease _openEase;
        [SerializeField] private Ease _closeEase;

        public async Task Open()
        {
            _content.gameObject.SetActive(true);
            _content.position = _closePos.position;
            await _content.DOMove(_openPos.position, _openTime).SetEase(_openEase).AsyncWaitForCompletion();
        }
        
        public async Task Close()
        {
            await _content.DOMove(_closePos.position, _closeTime).SetEase(_closeEase).AsyncWaitForCompletion();
            _content.gameObject.SetActive(false);
        }
        
        [Button]
        public void CloseImmediate()
        {
            _content.position = _closePos.position;
        }
        
        [Button]
        public void OpenImmediate()
        {
            _content.position = _openPos.position;
        }
    }
}