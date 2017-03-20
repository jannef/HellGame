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

    private void Start()
    {
        StartCoroutine(OpeningCoroutine());
    }

    public void OpenDoor()
    {
        //StartCoroutine(OpeningCoroutine());
    }

    IEnumerator OpeningCoroutine()
    {
        var t = 0f;
        var lastT = 0f;
        var baseSpeed = openingDegreeAmount;

        while (t < 1)
        {
            Debug.Log(t);
            t += WorldStateMachine.Instance.DeltaTime / openingLenght;
            transform.RotateAround(hindeTransform.position, openingAxis, (openingCurve.Evaluate(t) - lastT) * baseSpeed);
            lastT = openingCurve.Evaluate(t);
            yield return null;
        }

        yield return null;
    }
} 
