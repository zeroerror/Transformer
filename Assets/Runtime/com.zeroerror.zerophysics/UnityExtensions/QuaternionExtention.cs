using UnityEngine;
using FixMath.NET;

namespace ZeroPhysics.Extensions
{

    public static class QuaternionExtention
    {
        public static FPQuaternion ToFPQuaternion(this in Quaternion q)
        {
            return new FPQuaternion((FP64)q.x, (FP64)q.y, (FP64)q.z, (FP64)q.w);
        }

        public static Quaternion ToQuaternion(this in FPQuaternion q)
        {
            return new Quaternion(q.x.AsFloat(), q.y.AsFloat(), q.z.AsFloat(), q.w.AsFloat());
        }

    }

}