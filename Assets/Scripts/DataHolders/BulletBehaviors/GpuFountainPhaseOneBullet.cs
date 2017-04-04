using UnityEngine;
using fi.tamk.hellgame.world;
using fi.tamk.hellgame.utils;

namespace fi.tamk.hellgame.dataholders
{
    public sealed class GpuFountainPhaseOneBullet : GpuAcceleratedBulletBehavior
    {
        [SerializeField] private float _accelerationAmounth = 0f;
        [SerializeField] private float _radius = 5f;

        private struct BufferDataType
        {
            public Vector3 position;
            public Vector3 velocity;
            public float lifetimeLeft;
        }

        private BufferDataType[] _inBuffer;

        protected override void DataToShader<T>(ref T[] inBuffer, ref ParticleSystem.Particle[] particles,
            int numberOfParticles)
        {
            if (numberOfParticles < 1) return;

            BehaviorComputeShader.SetFloat("accelerate", _accelerationAmounth);
            BehaviorComputeShader.SetFloat("delta", WorldStateMachine.Instance.DeltaTime);
            BehaviorComputeShader.SetFloat("totalLifetime", particles[0].startLifetime);
            BehaviorComputeShader.SetFloat("size", _radius);

            var player = ServiceLocator.Instance.GetNearestPlayer(Vector3.zero);
            BehaviorComputeShader.SetVector("playerPosition", player == null ? Vector3.zero : player.position);

            for (var i = 0; i < numberOfParticles; ++i)
            {
                _inBuffer[i].velocity = particles[i].velocity;
                _inBuffer[i].lifetimeLeft = particles[i].remainingLifetime;
                _inBuffer[i].position = particles[i].position;
            }
        }

        protected override void ShaderToData<T>(ref T[] inBuffer, ref ParticleSystem.Particle[] particles,
            int numberOfParticles)
        {
            for (var i = 0; i < numberOfParticles; ++i)
            {
                particles[i].velocity = _inBuffer[i].velocity;

                // Lifetime buffer holds radius to set after the shader returns...
                particles[i].size = _inBuffer[i].lifetimeLeft;
            }
        }

        public override void InitializeBatch(int maxBatchSize)
        {
            CsBuffer = new ComputeBuffer(maxBatchSize, 16); // 2*vector3 + float = 2*12+4=28 bytes
            _inBuffer = new BufferDataType[maxBatchSize];
        }

        public override void BatchedAction(ref ParticleSystem.Particle[] particleBuffer, int numberOfParticles)
        {
            InternalBatchedAction<BufferDataType>(ref _inBuffer, ref particleBuffer, numberOfParticles);
        }
    }
}
