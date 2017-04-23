using UnityEngine;
using System.Collections;
using System.Reflection;
using fi.tamk.hellgame.character;

public class ChambersLifeBar : MonoBehaviour
{
    [SerializeField] protected HealthComponent HealthToTrack;

    private ParticleSystem _indicatorSystem;
    private Vector3 _originalWidth;
    private float _originalEmission;

    private MethodInfo _shapeboxWidth;
    private MethodInfo _emissionRate;
    public void Awake()
    {
        _indicatorSystem = GetComponent<ParticleSystem>();
        HealthToTrack.HealthChangeEvent += OnHealthChange;
        _originalWidth = _indicatorSystem.shape.box;
        _originalEmission = _indicatorSystem.emission.rateOverTimeMultiplier;

        _emissionRate = typeof(ParticleSystem.EmissionModule).GetMethod("set_rateOverTimeMultiplier");
        _shapeboxWidth = typeof(ParticleSystem.ShapeModule).GetMethod("set_box");
    }

    private void OnHealthChange(float percentage, int currenthp, int maxhp)
    {
        var toSet = _originalWidth;
        toSet.x *= percentage;

        _shapeboxWidth.Invoke(_indicatorSystem.shape, new object[] {toSet});
        _emissionRate.Invoke(_indicatorSystem.emission, new object[] {_originalEmission * percentage});

        if (currenthp <= 1) _indicatorSystem.Stop();
    }
}
