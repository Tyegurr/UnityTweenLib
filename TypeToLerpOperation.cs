using System;
using System.Collections.Generic;
using UnityEngine;

namespace TyesStuff.Animation.TweenLib
{
    public class TypeToLerpOperation
    {
        public static Dictionary<string, Func<object, object, float, object>> NameToOperation = new Dictionary<string, Func<object, object, float, object>>
        {
            { "Float", (object A, object B, float T) => { return Mathf.LerpUnclamped((float)A, (float)B, T); } },
            { "UnityEngine.Vector2", (object A, object B, float T) => { return Vector2.LerpUnclamped((Vector2)A, (Vector2)B, T); } }
        };

    }
}