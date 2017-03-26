using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleFloatingSway : MonoBehaviour {
    [SerializeField] private AnimationCurve SwayingCurve;
    [SerializeField] private Vector3 SwayingAmount;
    [SerializeField] private float SwayingFrequency;
    [SerializeField] private float DegreeTipped;
    private Vector3 _startingLocalPosition;
    private Vector3 _startingUp;
    private float timer = 0;
    private Vector3 _currentDestination;

	// Use this for initialization
	void Start () {
        _startingLocalPosition = transform.localPosition;
        _startingUp = transform.up;
	}
	
	// Update is called once per frame
	void Update () {
        timer += WorldStateMachine.Instance.DeltaTime / SwayingFrequency;
        transform.localPosition = Vector3.LerpUnclamped(_startingLocalPosition + SwayingAmount, _startingLocalPosition - SwayingAmount, SwayingCurve.Evaluate(timer));
        if (timer > 1)
        {
            timer = 0;
        }
	}
}
