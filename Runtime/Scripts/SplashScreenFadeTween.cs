using UnityEngine;
using DG.Tweening;

namespace SplashScreenSystem
{
    /// <summary>
    /// Tween component that handles fading in and out of a CanvasGroup.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class SplashScreenFadeTween : SplashScreenTween
    {
        [SerializeField] [Tooltip("The canvas group to fade. If left null, it will be obtained from this GameObject.")] 
        CanvasGroup canvasGroup;

        [SerializeField] [Tooltip("Duration of the fade in transition.")] 
        float enterDuration = 0.5f;

        [SerializeField] [Tooltip("Duration of the fade out transition.")] 
        float exitDuration = 0.5f;

        [SerializeField] [Tooltip("Ease type for the fade in transition.")] 
        Ease enterEase = Ease.OutQuad;

        [SerializeField] [Tooltip("Ease type for the fade out transition.")] 
        Ease exitEase = Ease.OutQuad;

        [SerializeField] [Tooltip("Alpha value to start fading in from.")] 
        float startAlpha = 0f;

        [SerializeField] [Tooltip("Alpha value to end fading in at (and start fading out from).")] 
        float endAlpha = 1f;

        [SerializeField] [Tooltip("Alpha value to end fading out at.")] 
        float exitEndAlpha = 0f;

        Tween fadeTween;

        public CanvasGroup CanvasGroup => canvasGroup;
        public float EnterDurationParam => enterDuration;
        public float ExitDurationParam => exitDuration;
        public Ease EnterEase => enterEase;
        public Ease ExitEase => exitEase;
        public float StartAlpha => startAlpha;
        public float EndAlpha => endAlpha;
        public float ExitEndAlpha => exitEndAlpha;

        public override float EnterDuration => enterDuration;
        public override float ExitDuration => exitDuration;

        void Awake()
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }
        }

        void OnDestroy()
        {
            fadeTween?.Kill();
        }

        /// <summary>
        /// Plays the fade enter transition.
        /// </summary>
        public override void PlayEnter()
        {
            if (canvasGroup == null)
            {
                return;
            }

            fadeTween?.Kill();
            canvasGroup.alpha = startAlpha;
            fadeTween = canvasGroup.DOFade(endAlpha, enterDuration)
                .SetEase(enterEase)
                .SetUpdate(true);
        }

        /// <summary>
        /// Plays the fade exit transition.
        /// </summary>
        public override void PlayExit()
        {
            if (canvasGroup == null)
            {
                return;
            }

            fadeTween?.Kill();
            canvasGroup.alpha = endAlpha;
            fadeTween = canvasGroup.DOFade(exitEndAlpha, exitDuration)
                .SetEase(exitEase)
                .SetUpdate(true);
        }
    }
}
