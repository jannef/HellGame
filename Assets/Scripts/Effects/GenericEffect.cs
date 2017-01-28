using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fi.tamk.hellgame.interfaces;
using System;

namespace fi.tamk.hellgame.effects
{
    public delegate void Runnable(float[] args);

    public class GenericEffect : MonoBehaviour
    {
        public static GenericEffect GetGenericEffect(Transform atWhere)
        {
            var go = new GameObject();
            go.transform.position = atWhere.position;
            go.transform.rotation = atWhere.rotation;

            var de = go.AddComponent<GenericEffect>();
            return de;
        }

        public static GameObject GetGenericEffectGO(Transform atWhere)
        {
            return GetGenericEffect(atWhere).gameObject;
        }

        public float LifeTime;

        public void SetOnUpdateCycle(Runnable runnableDelegate, float[] args)
        {
            OnTickDelegates.Add(new EffectDelegateHolder(runnableDelegate, args));
        }
        protected List<EffectDelegateHolder> OnStartDelegates = new List<EffectDelegateHolder>();

        public void SetOnstart(Runnable runnableDelegate, float[] args)
        {
            OnStartDelegates.Add(new EffectDelegateHolder(runnableDelegate, args));
        }
        protected List<EffectDelegateHolder> OnTickDelegates = new List<EffectDelegateHolder>();

        public void SetOnEnd(Runnable runnableDelegate, float[] args)
        {
            OnEndDelegates.Add(new EffectDelegateHolder(runnableDelegate, args));
        }
        protected List<EffectDelegateHolder> OnEndDelegates = new List<EffectDelegateHolder>();

        protected float _timer = 0f;
        bool _once = true;

        protected virtual void Update()
        {
            if (_once)
            {
                foreach(EffectDelegateHolder effect in OnStartDelegates) {
                    effect.runnableDelegate.Invoke(effect.args);
                }
                _once = false;
            }

            _timer += Time.deltaTime;
            OnUpdate();
            //TODO: Pooling
            if (_timer > LifeTime)
            {
                foreach (EffectDelegateHolder effect in OnEndDelegates)
                {
                    effect.runnableDelegate.Invoke(effect.args);
                }
                Destroy(gameObject);
            }
        }

        protected virtual void OnUpdate()
        {
            foreach (EffectDelegateHolder effect in OnTickDelegates)
            {
                effect.runnableDelegate.Invoke(effect.args);
            }
        }
    }
}
