using System.Collections;
using System.Collections.Generic;
using System.Linq;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.world;
using UnityEngine;
using Random = UnityEngine.Random;

namespace fi.tamk.hellgame.effects
{
    public class SphereShellController : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _rotationSpeedDistribution;
        [SerializeField] private float _maxRotationSpeed = 97.3f;

        [SerializeField] private AnimationCurve _floatCurve;
        [SerializeField] private float _maxFloatHeight = 0.13f;
        [SerializeField] private float _floatDuration = 4.1f;

        [SerializeField] private float _explosiveForce = 500.34f;
        private HealthComponent _healthToMonitor;
        private readonly List<FloatingSpherePart> _parts = new List<FloatingSpherePart>();

        private float _healthIncrement = 0f;
        private float _nextIndicatorPoint;

        private void Awake()
        {
            _parts.AddRange(GetComponentsInChildren<FloatingSpherePart>()); // this before InitializeHealthMonitoring()!
            foreach (var part in _parts)
            {
                part.Init(GetRandomizedRotationSpeed());
            }

            InitializeHealthMonitoring(); // _parts must be populated before this is called!
            StartCoroutine(FloatCycle());
            StartCoroutine(ChangeRotationSpeeds());
        }

        /// <summary>
        /// Sets up the pieces to act up as a health bar.
        /// </summary>
        private void InitializeHealthMonitoring()
        {
            if (transform.parent == null) throw new UnityException("SphereBoss model is not parented to the actual boss!");
            _healthToMonitor = transform.parent.gameObject.GetComponent<HealthComponent>() ?? new UnityException("SphereBoss' HealthComponent could not be located!").Throw<HealthComponent>();
            _healthToMonitor.HealthChangeEvent += MonitorHealth;

            _healthIncrement = 1f / (_parts.Count + 1);
            _nextIndicatorPoint = 1f - _healthIncrement;
        }

        /// <summary>
        /// Checks if the boss has reached a point at which to blow a piece off.
        /// </summary>
        /// <param name="percentage">current percentage the boss is at.</param>
        /// <param name="currenthp">Not used.</param>
        /// <param name="maxhp">Not used.</param>
        private void MonitorHealth(float percentage, int currenthp, int maxhp)
        {
            if (percentage < _nextIndicatorPoint)
            {
                _nextIndicatorPoint -= _healthIncrement;
                BlowOffRandomPiece();
            }
        }

        /// <summary>
        /// Gets a randomized speed within configured parameters.
        /// </summary>
        /// <returns>The random speed.</returns>
        private float GetRandomizedRotationSpeed()
        {
            return Mathf.Lerp(-_maxRotationSpeed, _maxRotationSpeed,_rotationSpeedDistribution.Evaluate(Random.value));
        }

        /// <summary>
        /// Signals random piece to engage floating animation.
        /// </summary>
        private void FloatRandomPiece()
        {
            _parts[Random.Range(0, _parts.Count - 1)].HoverAwayFromCore(Random.Range(0f, _maxFloatHeight), _floatDuration, _floatCurve);
        }


        /// <summary>
        /// Changes rotarion speeds of the system at random intervals;
        /// </summary>
        /// <returns></returns>
        private IEnumerator ChangeRotationSpeeds()
        {
            while (true)
            {
                var timer = Random.Range(3f, 6f);
                while (timer > 0f)
                {
                    timer -= WorldStateMachine.Instance.DeltaTime;
                    yield return null;
                }

                if (_parts.Count >= 1)
                {
                    foreach (var part in _parts)
                    {
                        if (Random.value > 0.5f) part.ChangeRotationSpeedOverTime(GetRandomizedRotationSpeed(), Random.Range(1f, 4f));
                    }
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Calls FloatRandomPiece() at random intervals.
        /// </summary>
        /// <returns>Not used.</returns>
        private IEnumerator FloatCycle()
        {
            while (true)
            {
                var timer = Random.Range(0f, 3f);
                while (timer > 0f)
                {
                    timer -= WorldStateMachine.Instance.DeltaTime;
                    yield return null;
                }

                if (_parts.Count >= 1)
                {
                    FloatRandomPiece();
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Releases random piece of the shell from it's parent and gives it physics & explosive force.
        /// </summary>
        public void BlowOffRandomPiece()
        {
            if (_parts.Count < 1) return;
            var part = _parts[Random.Range(0, _parts.Count - 1)];
            _parts.Remove(part);

            part.ReleaseFromParent();
            part.Rigidbody.AddExplosionForce(_explosiveForce, transform.position + Random.insideUnitSphere, 5f);
        }
    }
}
