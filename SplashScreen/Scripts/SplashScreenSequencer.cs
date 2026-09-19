using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace SplashScreenSystem
{
    /// <summary>
    /// Coordinates the activation and timing of splash screen child elements, then invokes a completion event.
    /// </summary>
    public class SplashScreenSequencer : MonoBehaviour
    {
        [SerializeField] [Tooltip("Configure custom step GameObjects and order. Unspecified children will be automatically appended.")]
        List<GameObject> steps = new List<GameObject>();

        [SerializeField] [Tooltip("Delay (in seconds) to wait after a step's exit tweens complete before activating the next step.")]
        float delayBetweenSteps = 0.5f;

        [Header("Events")]
        [SerializeField] [Tooltip("Triggered when the entire splash sequence has finished playing.")]
        UnityEvent onSequenceComplete;

        /// <summary>
        /// Gets the UnityEvent triggered when the sequence completes.
        /// </summary>
        public UnityEvent OnSequenceComplete => onSequenceComplete;

        List<GameObject> activeSteps = new List<GameObject>();
        CancellationTokenSource cancellationTokenSource;

        void Awake()
        {
            // Build activeSteps ONLY from the explicitly configured steps list
            if (steps != null)
            {
                foreach (GameObject step in steps)
                {
                    if (step != null)
                    {
                        activeSteps.Add(step);

                        // Reset all CanvasGroups on this step to 0 alpha to prevent one-frame flashes when enabled
                        CanvasGroup[] canvasGroups = step.GetComponentsInChildren<CanvasGroup>(true);
                        foreach (CanvasGroup group in canvasGroups)
                        {
                            if (group != null)
                            {
                                group.alpha = 0f;
                            }
                        }

                        // Only disable the step GameObjects initially so they are hidden at start, leaving backgrounds/etc untouched
                        step.SetActive(false);
                    }
                }
            }
        }

        void Start()
        {
            cancellationTokenSource = new CancellationTokenSource();
            PlaySequenceAsync(cancellationTokenSource.Token).Forget();
        }

        void OnDestroy()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
            }
        }

        async UniTaskVoid PlaySequenceAsync(CancellationToken cancellationToken)
        {
            if (activeSteps.Count == 0)
            {
                Debug.LogWarning("SplashScreenSequencer: No steps or children found to sequence.");
                onSequenceComplete?.Invoke();
                return;
            }

            foreach (GameObject child in activeSteps)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                if (child == null)
                {
                    continue;
                }

                // 1. Activate child (this will trigger OnEnable on child components and automatically start their enter transitions)
                child.SetActive(true);

                // Find all SplashScreenTween components on this child
                SplashScreenTween[] tweens = child.GetComponentsInChildren<SplashScreenTween>(true);

                // 2. Determine maximum enter duration and middle delay of enabled tweens
                float maxEnterDuration = 0f;
                float maxMiddleDelay = 0f;
                foreach (SplashScreenTween tween in tweens)
                {
                    if (tween != null && tween.enabled)
                    {
                        if (tween.EnterDuration > maxEnterDuration)
                        {
                            maxEnterDuration = tween.EnterDuration;
                        }
                        if (tween.MiddleDelay > maxMiddleDelay)
                        {
                            maxMiddleDelay = tween.MiddleDelay;
                        }
                    }
                }

                // 3. Wait for (maxEnterDuration + maxMiddleDelay)
                float holdWaitTime = maxEnterDuration + maxMiddleDelay;
                if (holdWaitTime > 0f)
                {
                    await UniTask.Delay((int)(holdWaitTime * 1000f), delayType: DelayType.DeltaTime, cancellationToken: cancellationToken);
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                // 4. Trigger exit transitions on enabled tweens
                foreach (SplashScreenTween tween in tweens)
                {
                    if (tween != null && tween.enabled)
                    {
                        tween.PlayExit();
                    }
                }

                // 5. Determine maximum exit duration of enabled tweens
                float maxExitDuration = 0f;
                foreach (SplashScreenTween tween in tweens)
                {
                    if (tween != null && tween.enabled && tween.ExitDuration > maxExitDuration)
                    {
                        maxExitDuration = tween.ExitDuration;
                    }
                }

                // 6. Wait for exit transitions to complete
                if (maxExitDuration > 0f)
                {
                    await UniTask.Delay((int)(maxExitDuration * 1000f), delayType: DelayType.DeltaTime, cancellationToken: cancellationToken);
                }

                // 7. Deactivate the child GameObject
                child.SetActive(false);

                // 8. Wait for the delay between steps before enabling the next one
                if (delayBetweenSteps > 0f)
                {
                    await UniTask.Delay((int)(delayBetweenSteps * 1000f), delayType: DelayType.DeltaTime, cancellationToken: cancellationToken);
                }
            }

            // Once sequence is complete, invoke the completion event
            onSequenceComplete?.Invoke();
        }
    }
}
