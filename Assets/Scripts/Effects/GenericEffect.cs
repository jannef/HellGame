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

        [SerializeField] float LifeTime;

        public void SetOnUpdateCycle(Runnable runnableDelegate, float[] args)
        {
            OnUpdateCycle = runnableDelegate;
            OnUpdateParams = args;
        }
        protected Runnable OnUpdateCycle;
        protected float[] OnUpdateParams;

        public void SetOnstart(Runnable runnableDelegate, float[] args)
        {
            OnStart = runnableDelegate;
            OnstartParams = args;
        }
        protected Runnable OnStart;
        protected float[] OnstartParams;

        public void SetOnEnd(Runnable runnableDelegate, float[] args)
        {
            OnEnd = runnableDelegate;
            OnEndParams = args;
        }
        protected Runnable OnEnd;
        protected float[] OnEndParams;

        protected float _timer = 0f;
        bool _once = true;

        protected virtual void Update()
        {
            if (_once)
            {
                if (OnStart != null) OnStart.Invoke(OnstartParams);
                _once = false;
            }

            _timer += Time.deltaTime;
            OnUpdate();
            //TODO: Pooling
            if (_timer > LifeTime)
            {
                if (OnEnd != null) OnEnd.Invoke(OnEndParams);
                Destroy(gameObject);
            }
        }

        protected virtual void OnUpdate()
        {
            if (OnUpdateCycle != null) OnUpdateCycle.Invoke(OnUpdateParams);
        }
    }
}
