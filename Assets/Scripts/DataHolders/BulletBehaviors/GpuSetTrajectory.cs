using System;
using UnityEngine;

namespace fi.tamk.hellgame.dataholders
{
    public class GpuSetTrajectory : GpuAcceleratedBulletBehavior
    {
        protected Vector3 TargetWorldPosition;
        protected float DesiredMagnitude = -1;

        private struct BufferDataType
        {
            public Vector3 position;
            public Vector3 velocity;
        }

        private BufferDataType[] _inBuffer;

        protected override void DataToShader<T>(ref T[] inBuffer, ref ParticleSystem.Particle[] particles,
            int numberOfParticles)
        {
            BehaviorComputeShader.SetVector("targetPosition", TargetWorldPosition);
            BehaviorComputeShader.SetFloat("magnitude", DesiredMagnitude);
            for (var i = 0; i < numberOfParticles; ++i)
            {
                _inBuffer[i].position = particles[i].position;
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
            CsBuffer = new ComputeBuffer(maxBatchSize, 24); // two vector3 makes 24 bytes
            _inBuffer = new BufferDataType[maxBatchSize];
        }

        public override void BatchedAction(ref ParticleSystem.Particle[] particleBuffer, int numberOfParticles, params object[] parameters)
        {
            /*
             * Parameters
             * 0: Transform     - Desired target location
             * 1: float         - Desired velocity magnitude
             */
            try
            {
                TargetWorldPosition = (parameters[0] as Transform).position;
                DesiredMagnitude = parameters.Length >= 2 ? (float)parameters[1] : -1f;
            }
            catch (Exception)
            {
                throw new UnityException("Check GpuSetTrajectory.cs comments to see correct parameters for this bulletbBehaviour!");
            }
            
            InternalBatchedAction<BufferDataType>(ref _inBuffer, ref particleBuffer, numberOfParticles);
        }
    }
}
