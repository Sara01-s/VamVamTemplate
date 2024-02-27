using UnityEngine;

namespace VVT {

    [System.Serializable]
    public struct LineSettings {
        public string Name;
        public Gradient ColorGradient;
        public AnimationCurve WidthCurve;
        [Min(0)] public int CornerVertices;
        [Min(0)] public int EndCapVertices;
    }
}