using FixMath.NET;
using ZeroPhysics.Utils;

namespace ZeroPhysics.Physics3D {

    public static class Bounce3DUtils {

        static readonly FP64 Bounce_Epsilon = FP64.EN1;

        public static FPVector3 GetBouncedV(in FPVector3 v, in FPVector3 beHitDir, in FP64 bounceCoefficient) {
            if (beHitDir == FPVector3.Zero || v == FPVector3.Zero) {
                return v;
            }

            var v_normalized = v.normalized;
            var cosv = FPVector3.Dot(v_normalized, beHitDir);
            cosv = FP64.Clamp(cosv, -FP64.One, FP64.One);
            if (cosv >= 0) {
                return v;
            }

            var vLen = v.Length();
            if (vLen < Bounce_Epsilon) {
                return FPVector3.Zero;
            }

            if (cosv == -1) {
                return -bounceCoefficient * vLen * v_normalized;
            }

            var sinv = -cosv;
            vLen *= sinv;
            var crossAxis = FPVector3.Cross(v, beHitDir);
            crossAxis.Normalize();
            var rot = FPQuaternion.CreateFromAxisAngle(crossAxis, FPUtils.rad_180);
            var eraseDir = rot * beHitDir;
            return v - (1 + bounceCoefficient) * vLen * eraseDir;
        }

    }

}