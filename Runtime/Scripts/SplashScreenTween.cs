using UnityEngine;

namespace SplashScreenSystem
{
    /// <summary>
    /// Abstract base class for all splash screen tween effects.
    /// </summary>
    public abstract class SplashScreenTween : MonoBehaviour
    {
        [SerializeField] [Tooltip("Delay in the middle (hold time) between the enter transition and exit transition.")]
        float middleDelay = 0.5f;

        public float MiddleDelay => middleDelay;

        /// <summary>
        /// Gets the duration of the enter transition.
        /// </summary>
        public abstract float EnterDuration { get; }

        /// <summary>
        /// Gets the duration of the exit transition.
        /// </summary>
        public abstract float ExitDuration { get; }

        protected virtual void OnEnable()
        {
            PlayEnter();
        }

        /// <summary>
        /// Plays the enter transition.
        /// </summary>
        public abstract void PlayEnter();

        /// <summary>
        /// Plays the exit transition.
        /// </summary>
        public abstract void PlayExit();
    }
}
