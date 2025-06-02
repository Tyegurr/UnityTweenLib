namespace TyesStuff.Animation.TweenLib
{
    public class TweenLibTweenProperty
    {
        public string PropertyName = string.Empty;
        public object StartValue;
        public object EndValue;

        public TweenLibTweenProperty(string PropName, object Start, object End)
        {
            PropertyName = PropName;
            StartValue = Start;
            EndValue = End;
        }
    }
}