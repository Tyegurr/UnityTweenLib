using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TyesStuff.Animation.TweenLib
{
    public class TweenLibTween
    {
        public string TweenName = "New Tween";

        public bool DestroyOnFinish = true;

        public float Duration { get; private set; } = 0f;
        public float CurrentTime { get; private set; } = 0f;

        private TweenLibTweenStyle m_AttachedStyle;

        public bool IsPlaying { get; private set; } = false;

        public object TargetObject;
        
        private List<TweenLibTweenProperty> m_Properties = new List<TweenLibTweenProperty>();
        public Dictionary<string, TweenLibTweenProperty> m_NameToProperty = new Dictionary<string, TweenLibTweenProperty>();
        public Action OnTweenEnded;

        public TweenLibTween(string Name, float TweenDuration, TweenLibTweenStyle Style)
        {
            TweenName = Name;
            Duration = TweenDuration;
            m_AttachedStyle = Style;
        }

        public void DeltaStep()
        {
            CurrentTime += Time.deltaTime;

            if (TargetObject == null) // don't go on further if there is no target object
                return;
        }

        public void AddProperty(string PropertyName, object StartValue,  object EndValue)
        {
            TweenLibTweenProperty Property = new TweenLibTweenProperty(PropertyName, StartValue, EndValue);

            m_Properties.Add(Property);
            m_NameToProperty.Add(PropertyName, m_Properties.LastOrDefault());
        }
        public void RemoveProperty(string PropertyName)
        {
            m_Properties.Remove(m_NameToProperty[PropertyName]);
            m_NameToProperty.Remove(PropertyName);
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
            CurrentTime = 0f;
        }
    }
}