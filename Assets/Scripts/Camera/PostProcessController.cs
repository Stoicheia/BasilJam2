using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Cams
{
    public class PostProcessController : MonoBehaviour
    {
        [SerializeField] private Volume _volume;
        [Header("Settings")]
        [SerializeField] private float _grainTime;
        [SerializeField] private float _distortTime;
        [SerializeField] private Ease _grainEase;
        [SerializeField] private Ease _distortEase;
        [SerializeField] private float _grainStart;
        [SerializeField] private float _grainEnd;
        [SerializeField] private float _distortStart;
        [SerializeField] private float _distortEnd;
        [SerializeField] private float _chromeStart;
        [SerializeField] private float _chromeEnd;
        
        public async Task Open()
        {
            SetGrain(_grainStart);
            SetLensDistort(_distortStart);
            SetChrome(_chromeStart);
            
            Sequence startSeq = DOTween.Sequence();
            var grain = DOVirtual.Float(_grainStart, _grainEnd, _grainTime, SetGrain).SetEase(_grainEase);
            startSeq.Join(grain);
            var lensDistort = DOVirtual.Float(_distortStart, _distortEnd, _distortTime, SetLensDistort).SetEase(_distortEase);
            startSeq.Join(lensDistort);
            var chrome = DOVirtual.Float(_chromeStart, _chromeEnd, _distortTime, SetChrome).SetEase(_distortEase);
            startSeq.Join(chrome);
            await startSeq.AsyncWaitForCompletion();
        }

        public async Task Close()
        {
            SetLensDistort(_distortEnd);
            var lensDistort = DOVirtual.Float(_distortEnd, _distortStart, _distortTime, SetLensDistort).SetEase(Ease.OutCubic);
            await lensDistort.AsyncWaitForCompletion();
        }

        public void SetGrain(float t)
        {
            bool hasGrain = _volume.profile.TryGet(out FilmGrain grain);
            if (!hasGrain)
            {
                Debug.LogError("No grain found on post-processing volume");
                return;
            }

            grain.intensity.value = t;
        }

        public void SetVignette(float t)
        {
            bool hasVig = _volume.profile.TryGet(out Vignette vignette);
            if (!hasVig)
            {
                Debug.LogError("No vigleik found on post-processing volume");
                return;
            }

            vignette.intensity.value = t;
        }
        
        public void SetLensDistort(float t)
        {
            bool hasLensDistort = _volume.profile.TryGet(out LensDistortion lensDistortion);
            if (!hasLensDistort)
            {
                Debug.LogError("No lens distort found on post-processing volume");
                return;
            }

            lensDistortion.intensity.value = t;
        }
        
        public void SetChrome(float t)
        {
            bool hasLensDistort = _volume.profile.TryGet(out ChromaticAberration aberration);
            if (!hasLensDistort)
            {
                Debug.LogError("No chromatic aberration found on post-processing volume");
                return;
            }

            aberration.intensity.value = t;
        }
    }
}