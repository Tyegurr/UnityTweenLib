using System.Collections.Generic;
using UnityEngine;

namespace TyesStuff.Animation.TweenLib
{
    [CreateAssetMenu(fileName = "Untitled Tween Collection", menuName = "Tye's Stuff/Animation/Tween Lib Collection", order = 1)]
    public class TweenLibCollection : ScriptableObject
    {
        public List<TweenLibTweenStyle> TweenStyles = new List<TweenLibTweenStyle>();
    }
}