using UnityEngine;
using fi.tamk.hellgame.world;

namespace fi.tamk.hellgame.dataholders
{
    public sealed class GpuBulletAcceleration : GpuAcceleratedBulletBehavior
    {
        [SerializeField] private float _accelerationAmounth = 0f;

        private struct BufferDataType
        {
            public Vector3 velocity;
        }

        private BufferDataType[] _inBuffer;

        protected override void DataToShader<T>(ref T[] inBuffer, ref ParticleSystem.Particle[] particles,
            int numberOfParticles)
        {
            BehaviorComputeShader.SetFloat("accelerate", _accelerationAmounth);
            BehaviorComputeShader.SetFloat("delta", WorldStateMachine.Instance.DeltaTime);
            for (var i = 0; i < numberOfParticles; ++i)
            {
                _inBuffer[i].velocity = particles[i].velocity;
            }
        }

        protected override void ShaderToData<T>(ref T[] inBuffer, ref ParticleSystem.Particle[] particles,
            int numberOfParticles)
        {
            for (var i = 0; i < numberOfParticles; ++i)
            {
                particles[i].velocity = _inBuffer[i].velocity;
            }
        }

        public override void InitializeBatch(int maxBatchSize)
        {
            CsBuffer = new ComputeBuffer(maxBatchSize, 12); // one vector3 makes 12 bytes
            _inBuffer = new BufferDataType[maxBatchSize];
        }

        public override void BatchedAction(ref ParticleSystem.Particle[] particleBuffer, int numberOfParticles)
        {
            InternalBatchedAction<BufferDataType>(ref _inBuffer, ref particleBuffer, numberOfParticles);
        }
    }
}
