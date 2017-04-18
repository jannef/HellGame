using UnityEngine;

namespace fi.tamk.hellgame.dataholders
{
    public abstract class GpuAcceleratedBulletBehavior : AdvancedBulletBehavior
    {
        [SerializeField] protected ComputeShader BehaviorComputeShader;
        protected ComputeBuffer CsBuffer;

        public abstract override void BatchedAction(ref ParticleSystem.Particle[] particleBuffer, int numberOfParticles, params object[] parameters);

        public override void Action(ref ParticleSystem.Particle particle)
        {
            throw new UnityException("GpuAcceleratedBulletBehavior can't give out per particle data.");
        }

        public abstract override void InitializeBatch(int maxBatchSize);

        protected void InternalBatchedAction<T>(ref T[] inBuffer, ref ParticleSystem.Particle[] particleBuffer, int numberOfParticles)
            where T : struct
        {
            var kernelIndex = BehaviorComputeShader.FindKernel("CSMain");
            uint x, y, z;
            BehaviorComputeShader.GetKernelThreadGroupSizes(kernelIndex, out x, out y, out z);

            DataToShader(ref inBuffer, ref particleBuffer, numberOfParticles);
            CsBuffer.SetData(inBuffer);
            BehaviorComputeShader.SetBuffer(kernelIndex, "dataBuffer", CsBuffer);

            BehaviorComputeShader.Dispatch(kernelIndex, numberOfParticles / (int)z + 1, 1, 1);

            CsBuffer.GetData(inBuffer);
            ShaderToData(ref inBuffer, ref particleBuffer, numberOfParticles);
        }

        protected abstract void DataToShader<T>(ref T[] inBuffer, ref ParticleSystem.Particle[] particles, int numberOfParticles) where T : struct;
        protected abstract void ShaderToData<T>(ref T[] inBuffer, ref ParticleSystem.Particle[] particles, int numberOfParticles) where T : struct;

        public override void ReleaseResources()
        {
            base.ReleaseResources();
            if (CsBuffer != null) CsBuffer.Dispose();
        }
    }
}
