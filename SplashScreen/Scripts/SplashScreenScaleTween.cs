using UnityEngine;
using DG.Tweening;

namespace SplashScreenSystem
{
    /// <summary>
    /// Tween component that handles scaling a RectTransform.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class SplashScreenScaleTween : SplashScreenTween
    {
        [SerializeField] [Tooltip("The rect transform to scale. If left null, it will be obtained from this GameObject.")] 
        RectTransform rectTransform;

        [SerializeField] [Tooltip("Duration of the scale in transition.")] 
        float enterDuration = 0.5f;

        [SerializeField] [Tooltip("Duration of the scale out transition.")] 
        float exitDuration = 0.5f;

        [SerializeField] [Tooltip("Ease type for the scale in transition.")] 
        Ease enterEase = Ease.OutBack;

        [SerializeField] [Tooltip("Ease type for the scale out transition.")] 
        Ease exitEase = Ease.InBack;

        [SerializeField] [Tooltip("Scale vector to start scaling in from.")] 
        Vector3 startScale = Vector3.zero;

        [SerializeField] [Tooltip("Scale vector to end scaling in at (and start scaling out from).")] 
        Vector3 endScale = Vector3.one;

        [SerializeField] [Tooltip("Scale vector to end scaling out at.")] 
        Vector3 exitEndScale = Vector3.zero;

        Tween scaleTween;

        public RectTransform RectTransform => rectTransform;
        public float EnterDurationParam => enterDuration;
        public float ExitDurationParam => exitDuration;
        public Ease EnterEase => enterEase;
        public Ease ExitEase => exitEase;
        public Vector3 StartScale => startScale;
        public Vector3 EndScale => endScale;
        public Vector3 ExitEndScale => exitEndScale;

        public override float EnterDuration => enterDuration;
        public override float ExitDuration => exitDuration;

        void Awake()
        {
            if (rectTransform == null)
            {
                rectTransform = GetComponent<RectTransform>();
            }
        }

        void OnDestroy()
        {
            scaleTween?.Kill();
        }

        /// <summary>
        /// Plays the scale enter transition.
        /// </summary>
        public override void PlayEnter()
        {
            if (rectTransform == null)
            {
                return;
            }

            scaleTween?.Kill();
            rectTransform.localScale = startScale;
            scaleTween = rectTransform.DOScale(endScale, enterDuration)
                .SetEase(enterEase)
                .SetUpdate(true);
        }

        /// <summary>
        /// Plays the scale exit transition.
        /// </summary>
        public override void PlayExit()
        {
            if (rectTransform == null)
            {
                return;
            }

            scaleTween?.Kill();
            rectTransform.localScale = endScale;
            scaleTween = rectTransform.DOScale(exitEndScale, exitDuration)
                .SetEase(exitEase)
                .SetUpdate(true);
        }
    }
}
