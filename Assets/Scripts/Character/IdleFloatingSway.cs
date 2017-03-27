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
    private float timer = 0;

	// Use this for initialization
	void Start () {
        _startingLocalPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        timer += WorldStateMachine.Instance.DeltaTime / SwayingFrequency;
        transform.localPosition = Vector3.LerpUnclamped(_startingLocalPosition + SwayingAmount, _startingLocalPosition - SwayingAmount, SwayingCurve.Evaluate(timer));
        if (timer > 1)
        {
            timer = 0;
        }

        ChangeRotation();
	}

    private void ChangeRotation()
    {
        transform.localRotation = Quaternion.Euler((transform.localPosition.z - _startingLocalPosition.z) * DegreeTipped, transform.localRotation.y, (transform.localPosition.x - _startingLocalPosition.x) * DegreeTipped);
    }
}
