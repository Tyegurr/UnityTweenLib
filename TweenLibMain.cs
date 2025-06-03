using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TyesStuff.Animation.TweenLib
{
    public class TweenLibMain : MonoBehaviour
    {
        public static TweenLibMain Instance;

        public List<TweenLibCollection> TweenCollections = new List<TweenLibCollection>();
        private Dictionary<string, TweenLibTweenStyle> m_NameToStyle = new Dictionary<string, TweenLibTweenStyle>();

        private List<TweenLibTween> m_CurrentTweens = new List<TweenLibTween>();
        private Dictionary<string, TweenLibTween> m_NameToCurrentTween = new Dictionary<string, TweenLibTween>();

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            } else
            {
                Debug.LogError("Another TweenLibMain component already exists in the scene!");
                Destroy(gameObject);
            }

            // Getting Tween Collections
            foreach (TweenLibCollection Collection in TweenCollections)
            {
                foreach (TweenLibTweenStyle Style in Collection.TweenStyles)
                {
                    m_NameToStyle.Add(Style.Name, Style);
                }
            }
        }

        #region Static methods to manage Tweens
        public static TweenLibTween CreateTween(string TweenName, string TweenStyleName, float Duration)
        {
            string TweenNameChecked = TweenName;

            if (Instance.m_NameToCurrentTween.ContainsKey(TweenName))
            {
                TweenNameChecked += Random.Range(11, 99).ToString(); // TODO: you should probably change this to something better
            }

            TweenLibTween NewTween = new TweenLibTween(TweenNameChecked, Duration, Instance.m_NameToStyle[TweenStyleName]);

            Instance.m_CurrentTweens.Add(NewTween);
            Instance.m_NameToCurrentTween.Add(TweenNameChecked, Instance.m_CurrentTweens.LastOrDefault());

            return Instance.m_CurrentTweens[Instance.m_CurrentTweens.Count - 1];
        }
        public static void DestroyTweenByName(string TweenName)
        {
            Instance.m_CurrentTweens.Remove(Instance.m_NameToCurrentTween[TweenName]);
            Instance.m_NameToCurrentTween.Remove(TweenName);
        }
        public static void DestroyTween(TweenLibTween Tween)
        {
            Instance.m_NameToCurrentTween.Remove(Tween.TweenName);
            Instance.m_CurrentTweens.Remove(Tween);
        }
        #endregion

        private void Update()
        {
            foreach (TweenLibTween Tween in m_CurrentTweens)
            {
                if (Tween.IsPlaying)
                {
                    Tween.DeltaStep();
                }

                // check if the tween is okay to be removed
                if (Tween.CurrentTime > (Tween.Duration + Tween.Delay) && Tween.DestroyOnFinish)
                {
                    if (Tween.OnTweenEnded != null)
                        Tween.OnTweenEnded();
                    DestroyTween(Tween);
                    return;
                }
            }
        }
    }
}