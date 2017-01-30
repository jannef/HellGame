using UnityEngine;

namespace fi.tamk.hellgame.effector
{
    public class PlayerEffector : Effector
    {
        [SerializeField] private float _shakeIntensity;
        [SerializeField] private float _shakeLenght;
        [SerializeField] private float _slowDownScale;
        [SerializeField] private float _slowDownLenght;

        public override void Activate()
        {
            base.Activate();
            Effect.SetOnstart(SlowDown, new float[2] { _slowDownLenght, _slowDownScale });
            Effect.LifeTime = _slowDownLenght * _slowDownScale;
            Effect.SetOnEnd(ScreenShakeEffect, new float[2] { _shakeIntensity, _shakeLenght });
        }
    }
}
