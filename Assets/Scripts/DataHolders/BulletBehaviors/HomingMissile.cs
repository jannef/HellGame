using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.dataholders
{
    public class HomingMissile : AdvancedBulletBehavior
    {
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _degreesTurnedPerSecond;
        private Vector3 _playerPosition;

        public override void Action(ref ParticleSystem.Particle particle)
        {
            var rotatedVelocity = Vector3.RotateTowards(particle.velocity.normalized, (_playerPosition - particle.position).normalized, DeltaTime * _degreesTurnedPerSecond, 0f);
            particle.velocity = rotatedVelocity * _speed;
            particle.rotation3D = Quaternion.LookRotation(rotatedVelocity).eulerAngles;
        }

        public override void CacheFrameData(float deltaTime, params object[] additionalParams)
        {
            DeltaTime = deltaTime;
            var player = ServiceLocator.Instance.GetNearestPlayer(Vector3.zero);
            _playerPosition = (player == null ? Vector3.zero : player.position);
        }
    }
}
