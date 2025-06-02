using UnityEngine;

namespace TyesStuff.Animation.TweenLib
{
    public class TweenLibTween
    {
        public string TweenName = "New Tween";

        public bool DestroyOnFinish = true;

        public float Duration { get; private set; } = 0f;
        private float m_Time = 0f;

        private TweenLibTweenStyle m_AttachedStyle;

        public bool IsPlaying { get; private set; } = false;

        public TweenLibTween(string Name, float TweenDuration, TweenLibTweenStyle Style)
        {
            TweenName = Name;
            Duration = TweenDuration;
            m_AttachedStyle = Style;
        }

        public void DeltaStep()
        {
            m_Time += Time.deltaTime;
        }

        public void Play()
        {
            IsPlaying = true;
        }
        public void Pause()
        {
            IsPlaying = false;
        }
        public void Stop()
        {
            IsPlaying = false;
            m_Time = 0f;
        }
    }
}