using System.Collections.Generic;
using UnityEngine;


namespace VamVam.Source.Utils {

    [System.Serializable]
    public class SpriteAnimation {
        [SerializeField] private List<Sprite> _spriteFrames;
        [SerializeField] private int _frameRate;
    }

    /// <summary> 
    /// this class is intended to be used on sprite animations instead
    /// of Unity's animator.
    /// ! Currently WIP !
    /// </summary>
    public abstract class FlipBook {

        [SerializeField] private List<SpriteAnimation> _unitAnimations;
        

    }
}