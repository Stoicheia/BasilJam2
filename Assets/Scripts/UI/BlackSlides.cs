using System;
using System.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BlackSlides : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Image _blackTop;
        [SerializeField] private Image _blackBot;
        [SerializeField] private RectTransform _blackTopIn;
        [SerializeField] private RectTransform _blackTopOut;
        [SerializeField] private RectTransform _blackBotIn;
        [SerializeField] private RectTransform _blackBotOut;

        [Header("Settings")] 
        [SerializeField] private Ease _closeEase;
        [SerializeField] private Ease _openEase;

  
        private void Awake()
        {
            ResetImmediate();
        }

        [Button]
        public void ResetImmediate()
        {
            _blackTop.transform.position = _blackTopOut.position;
            _blackBot.transform.position = _blackBotOut.position;
        }

        [Button]
        public async Task Close(float time)
        {
            Sequence closeSeq = DOTween.Sequence();
            closeSeq.Join(_blackTop.transform.DOMoveY(_blackTopIn.position.y, time).SetEase(_closeEase));
            closeSeq.Join(_blackBot.transform.DOMoveY(_blackBotIn.position.y, time).SetEase(_closeEase));
            await closeSeq.AsyncWaitForCompletion();
        }

        [Button]
        public async Task Open(float time)
        {
            Sequence openSeq = DOTween.Sequence();
            openSeq.Join(_blackTop.transform.DOMoveY(_blackTopOut.position.y, time).SetEase(_openEase));
            openSeq.Join(_blackBot.transform.DOMoveY(_blackBotOut.position.y, time).SetEase(_openEase));
            await openSeq.AsyncWaitForCompletion();
        }
    }

}