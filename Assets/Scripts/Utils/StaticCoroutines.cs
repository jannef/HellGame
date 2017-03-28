﻿using System;
using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.world;
using UnityEngine;

namespace fi.tamk.hellgame.utils
{

    public static class StaticCoroutines
    {

        public static IEnumerator BlinkCoroutine(Renderer renderer, float effectLenght, float startingFrequency, float endFrequency, AnimationCurve easingCurve, bool enableRendererAtEnd = true)
        {
            float t = 0;
            float lastBlinkT = startingFrequency;

            while (t <= 1)
            {
                t += WorldStateMachine.Instance.DeltaTime / effectLenght;

                if (t > lastBlinkT)
                {
                    if (renderer == null) {
                        yield break;
                    } 
                    lastBlinkT = t + Mathf.Lerp(startingFrequency, endFrequency, easingCurve.Evaluate(t));
                    renderer.enabled = !renderer.enabled;
                }

                yield return null;
            }

            

            if (renderer == null) yield break;
            renderer.enabled = enableRendererAtEnd;
        }

        public static IEnumerator DoAfterDelay(float lenght, Action callBack)
        {
            var t = 0f;
            while (t <= 1)
            {
                t += WorldStateMachine.Instance.DeltaTime / lenght;
                yield return null;
            }

            if (callBack != null) callBack.Invoke();
        }

        public static IEnumerator ConstantUIShakeRoutine(RectTransform shakedTransform, float intensity)
        {
            var originalPos = shakedTransform.anchoredPosition;
            var originalIntensity = intensity;

            while (true)
            {
                intensity = (Mathf.Abs(Mathf.Sin(Time.time * 4)) * originalIntensity * .77f) + (originalIntensity * .33f); 
                shakedTransform.anchoredPosition = originalPos + UnityEngine.Random.insideUnitCircle.normalized * intensity * WorldStateMachine.Instance.DeltaTime;
                yield return null;
            }
        }
    }
}
