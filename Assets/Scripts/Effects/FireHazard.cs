using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHazard : MonoBehaviour
{
    public GameObject IndicatorGameObject { get { return _indicator.gameObject; } }

    [SerializeField] private ParticleSystem _fire;
    [SerializeField] private ParticleSystem _indicator;

    public void StartHazard()
    {
        _fire.Play();
        _indicator.Play();
    }

    public void EndHazard()
    {
        _fire.Pause();
        _indicator.Pause();
    }

    public void StopIndicator()
    {
        _indicator.Stop();
    }
}
