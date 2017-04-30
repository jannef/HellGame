using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningDoor : MonoBehaviour
{
    [SerializeField] private Transform hindeTransform;
    [SerializeField] private Vector3 openingAxis;
    [SerializeField] private float openingLenght;
    [SerializeField] private float openingDegreeAmount;
    [SerializeField] private AnimationCurve openingCurve;
    [SerializeField] private float closingLenght;
    [SerializeField] private AnimationCurve closingCurve;
    [SerializeField] bool startOpen = false;
    [SerializeField] private float quickOpeningLenght;
    [SerializeField] private AnimationCurve quickOpeningCurve;
    [SerializeField] private float ClosingDelay = 1.5f;

    void Awake()
    {
        if (startOpen)
            transform.RotateAround(hindeTransform.position, openingAxis, openingDegreeAmount);
    }

    public void CloseDoor()
    {
        StartCoroutine(OpeningCoroutine(-openingDegreeAmount, closingCurve, closingLenght));
    }

    public void InstantlyOpenDoor()
    {
        transform.RotateAround(hindeTransform.position, openingAxis, openingDegreeAmount);
    }

    public void DelayedCloseDoor()
    {
        StartCoroutine(StaticCoroutines.DoAfterDelay(ClosingDelay, CloseDoor));
    }

    public void OpenDoor()
    {
        StartCoroutine(OpeningCoroutine(openingDegreeAmount, openingCurve, openingLenght));
    }

    public void SlamOpen()
    {
        StartCoroutine(OpeningCoroutine(openingDegreeAmount, quickOpeningCurve, quickOpeningLenght));
        DelayedCloseDoor();
    }

    IEnumerator OpeningCoroutine(float openingDegreeAmount, AnimationCurve easing, float length)
    {
        var t = 0f;
        var lastT = 0f;
        var baseSpeed = openingDegreeAmount;
        yield return null;

        while (t < 1)
        {
            t += WorldStateMachine.Instance.DeltaTime / length;
            transform.RotateAround(hindeTransform.position, openingAxis, (easing.Evaluate(t) - lastT) * baseSpeed);
            lastT = easing.Evaluate(t);
            yield return null;
        }

        yield return null;
    }
} 
