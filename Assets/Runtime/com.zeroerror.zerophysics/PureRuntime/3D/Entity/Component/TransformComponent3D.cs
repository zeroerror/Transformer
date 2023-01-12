using FixMath.NET;

namespace ZeroPhysics.Physics3D
{

    public class TransformComponent3D
    {

        FPVector3 center;
        public FPVector3 Center => center;
        public void SetCenter(in FPVector3 v) => center = v;

        FPQuaternion rotation;
        public FPQuaternion Rotation => rotation;
        public void SetRotation(in FPQuaternion v) => rotation = v;

        FPVector3 scale;
        public FPVector3 Scale => scale;
        public void SetScale(in FPVector3 v) => scale = v;

    }

}