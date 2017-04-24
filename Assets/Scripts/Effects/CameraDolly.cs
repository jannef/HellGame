using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDolly : MonoBehaviour {
    public Transform _dollyPointParent;
    private CameraDollyPoint[] _cameraDollyPoints;
    private int _currentDollyPointIndex = 0;
    private float _timer = 0f;
    private CameraDollyPoint _lastPoint;
    private CameraDollyPoint _destinationDollyPoint;

    private Vector3 velocity = Vector3.zero;
    [SerializeField] private float smoothTime;

	// Use this for initialization
	void Start () {
        _cameraDollyPoints = _dollyPointParent.GetComponentsInChildren<CameraDollyPoint>();
        NextPoint();
	}

    private void NextPoint()
    {
        _timer = 0;
        _currentDollyPointIndex++;
        _lastPoint = _cameraDollyPoints[_currentDollyPointIndex % (_cameraDollyPoints.Length)];
        _destinationDollyPoint = _cameraDollyPoints[(_currentDollyPointIndex + 1) % (_cameraDollyPoints.Length)];
    }
	
	// Update is called once per frame
	void Update () {
        _timer += WorldStateMachine.Instance.DeltaTime;
        Vector3 pos;

        if (_timer < _destinationDollyPoint.TimeToReachThisPoint)
        {
            pos = Vector3.Lerp(_lastPoint.transform.position, _destinationDollyPoint.transform.position,
            _destinationDollyPoint.MovementToThisCurve.Evaluate(_timer / _destinationDollyPoint.TimeToReachThisPoint));

            transform.forward = Vector3.Lerp(_lastPoint.transform.forward, _destinationDollyPoint.transform.forward,
            _destinationDollyPoint.RotationToThisCurve.Evaluate(_timer / _destinationDollyPoint.TimeToReachThisPoint));
        } else
        {
            NextPoint();
            pos = Vector3.Lerp(_lastPoint.transform.position, _destinationDollyPoint.transform.position,
            _destinationDollyPoint.MovementToThisCurve.Evaluate(_timer / _destinationDollyPoint.TimeToReachThisPoint));

            transform.forward = Vector3.Lerp(_lastPoint.transform.forward, _destinationDollyPoint.transform.forward,
            _destinationDollyPoint.RotationToThisCurve.Evaluate(_timer / _destinationDollyPoint.TimeToReachThisPoint));
            
        }


        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smoothTime);


    }
}
