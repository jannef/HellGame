using System.Collections;
using System.Collections.Generic;
using System.Linq;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.world;
using UnityEngine;
using Random = UnityEngine.Random;

namespace fi.tamk.hellgame.effects
{
    /// <summary>
    /// Controls animations on sphere boss.
    /// </summary>
    public class SphereShellController : MonoBehaviour
    {
        /// <summary>
        /// Distribution for random rotation speeds.
        /// </summary>
        [SerializeField] private AnimationCurve _rotationSpeedDistribution;

        /// <summary>
        /// Maxium value that correspondes with 1.0f at the curve.
        /// </summary>
        [SerializeField] private float _maxRotationSpeed = 97.3f;

        /// <summary>
        /// Float animation curve for the floating shell pieces.
        /// </summary>
        [SerializeField] private AnimationCurve _floatCurve;

        /// <summary>
        /// Float height that correspondes with 1.0f on float curve.
        /// </summary>
        [SerializeField] private float _maxFloatHeight = 0.13f;

        /// <summary>
        /// Duration that corresponds with the span [0f, 1f] with the float curve.
        /// </summary>
        [SerializeField] private float _floatDuration = 4.1f;

        /// <summary>
        /// Explosive force applied to parts of shell when one is blown off.
        /// </summary>
        [SerializeField] private float _explosiveForce = 500.34f;

        /// <summary>
        /// Reference to pulse script. Found in
        /// </summary>
        [SerializeField] private SpehereShellPulse _pulse;

        /// <summary>
        /// Reference to rotation script.
        /// </summary>
        [SerializeField] private IdleRotation _rotation;

        /// <summary>
        /// Health component reference to the boss actor of the Sphere Boss.
        /// </summary>
        private HealthComponent _healthToMonitor;

        /// <summary>
        /// List of references to shell parts on Spere Boss, populated from Awake()
        /// </summary>
        private readonly List<FloatingSpherePart> _parts = new List<FloatingSpherePart>();

        /// <summary>
        /// Calculated increment % ]0f,1f[ between blowing of pieces, depending on number of pieces found
        /// in Awake()
        /// </summary>
        private float _healthIncrement = 0f;

        /// <summary>
        /// Next health % ]0f, 1f[ breakpoint
        /// </summary>
        private float _nextIndicatorPoint;

        /// <summary>
        /// Initializes references to individual components that make up the shell of the boss.
        /// </summary>
        private void Awake()
        {
            _parts.AddRange(GetComponentsInChildren<FloatingSpherePart>()); // this before InitializeHealthMonitoring()!
            foreach (var part in _parts)
            {
                part.Init(GetRandomizedRotationSpeed());
            }

            InitializeHealthMonitoring(); // _parts must be populated before this is called!
        }

        /// <summary>
        /// Starts animations on the boss.
        /// </summary>
        public void StartAnimations()
        {
            _pulse.enabled = true;
            _rotation.enabled = true;
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
