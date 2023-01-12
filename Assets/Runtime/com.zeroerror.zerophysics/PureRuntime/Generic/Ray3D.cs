using FixMath.NET;
using ZeroPhysics.Generic;

namespace ZeroPhysics.Generic
{

    public class Ray3D
    {

        public FPVector3 origin;
        public FPVector3 dir;
        public FP64 length;

        public Ray3D(FPVector3 origin, FPVector3 dir, FP64 length)
        {
            this.origin = origin;
            this.dir = dir.normalized;
            this.length = length;
        }

        public FPVector2 GetProjectionSub(Axis3D axis)
        {
            var p1 = origin;
            var p2 = GetEnd();
            var axisDir = axis.dir;
            var axisOrigin = axis.origin;
            var proj1 = FPVector3.Project(p1 - axisOrigin, axisDir).Length();
            var proj2 = FPVector3.Project(p2 - axisOrigin, axisDir).Length();
            return proj1 < proj2 ? new FPVector2(proj1, proj2) : new FPVector2(proj2, proj1); ;
        }

        public FPVector3 GetEnd()
        {
            return origin + dir * length;
        }

    }

}