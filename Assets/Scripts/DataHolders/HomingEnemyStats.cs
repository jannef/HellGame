using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingEnemyStats : MonoBehaviour {

    public float AccelerationAngle = 10f;
    public float AccelerationPerSecond = 5f;
    public float DecelerationPerSecond = 3f;
    public float MaxTurningSpeed = 4f;
    public float MinimumTurningSpeed = 2f;
    public AnimationCurve TurningSpeedEasing;
}
