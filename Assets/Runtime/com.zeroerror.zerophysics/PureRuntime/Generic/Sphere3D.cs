using FixMath.NET;
using ZeroPhysics.Generic;

namespace ZeroPhysics.Physics3D
{

    public class Sphere3D
    {
        
        int instanceID;
        public int InstanceID => instanceID;
        public void SetInstanceID(int v) => instanceID = v;

        // ====== Component
        // - Trans
        TransformComponent3D trans;
        public TransformComponent3D Trans => trans;

        public FPVector3 Center => trans.Center;
        public void SetCenter(in FPVector3 v) => trans.SetCenter(v);

        public FPQuaternion Rotation => trans.Rotation;
        public void SetRotation(in FPQuaternion v) => trans.SetRotation(v);

        public FPVector3 Scale => trans.Scale;
        public void SetScale(in FPVector3 v) => trans.SetScale(v);

        FP64 radius_scaled;
        public FP64 Radius_scaled => radius_scaled;
        public void UpdateScaledRadius() => radius_scaled = radius * FP64.Max(new FP64[3] { Scale.x, Scale.y, Scale.z });

        FP64 radius;
        public FP64 Radius => radius;
        public void SetRadius(in FP64 v) => radius = v;

        public Sphere3D()
        {
            trans = new TransformComponent3D();
        }

        public FPVector2 GetProjectionSub(Axis3D axis)
        {
            var axisOrigin = axis.origin;
            var axisDir = axis.dir;
            var p0 = FPVector3.Project(Center - axisOrigin, axisDir).Length();
            FPVector2 sub = new FPVector2(p0 - radius_scaled, p0 + radius_scaled);
            return sub;
        }

        public bool IsPosInsideSphere(FPVector3 tarPos)
        {
            var center = Center;
            var xDiff = Center.x - tarPos.x;
            var yDiff = Center.y - tarPos.y;
            var zDiff = Center.z - tarPos.z;
            return (xDiff * xDiff + yDiff * yDiff + zDiff * zDiff) <= (radius * radius);
        }

    }

}
