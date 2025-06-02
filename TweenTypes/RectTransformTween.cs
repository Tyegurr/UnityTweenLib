using UnityEngine;

namespace TyesStuff.Animation.TweenLib
{
    public class RectTransformTween : TweenLibTween
    {
        private RectTransform m_TargetRT;

        public RectTransformTween(string Name, float Duration, TweenLibTweenStyle Style, RectTransform TargetRectTransform) : base(Name, Duration, Style)
        {
            m_TargetRT = TargetRectTransform;
        }
    }
}