using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace TyesStuff.Animation.TweenLib
{
    [Serializable] // for unity debugging purposes
    public class TweenLibTween
    {
        public string TweenName = "New Tween";

        public bool DestroyOnFinish = true;

        public float Duration { get; private set; } = 0f;
        public float CurrentTime { get; private set; } = 0f;
        public bool Reverse = false;

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

            float CurrentInterval = CurrentTime / Duration;
            if (Reverse)
            {
                CurrentInterval = 1f - (CurrentTime / Duration);
            }
            float CurveInterval = m_AttachedStyle.Curve.Evaluate(CurrentInterval);

            foreach (TweenLibTweenProperty Property in m_Properties)
            {
                Type TargetObjectType = TargetObject.GetType();
                PropertyInfo ObjectProp = TargetObjectType.GetProperty(Property.PropertyName);
                ObjectProp.SetValue(TargetObject, TypeToLerpOperation.NameToOperation[ObjectProp.PropertyType.ToString()](Property.StartValue, Property.EndValue, CurveInterval));
            }
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