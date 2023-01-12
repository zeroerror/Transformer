using FixMath.NET;
using ZeroPhysics.Physics3D.Facade;
using ZeroPhysics.Service;
using ZeroPhysics.Utils;

namespace ZeroPhysics.Physics3D {

    public class VelocityPhase {

        Physics3DFacade physicsFacade;

        public VelocityPhase() { }

        public void Inject(Physics3DFacade physicsFacade) {
            this.physicsFacade = physicsFacade;
        }

        public void Tick(in FP64 dt) {
            var rbBoxes = physicsFacade.boxRBs;
            var service = physicsFacade.Service;
            var idService = service.IDService;
            var collisionService = service.CollisionService;
            var rbBoxIDInfos = idService.boxRBIDInfos;

            for (int i = 0; i < rbBoxes.Length; i++) {
                if (!rbBoxIDInfos[i]) continue;
                var rb = rbBoxes[i];
                var linearV = rb.LinearV;
                var m = rb.Mass;
                var outForce = rb.OutForce;
                if (outForce.LengthSquared() > FP64.Epsilon) {
                    linearV += GetOffsetV_ByForce(outForce, m, dt);
                }

                if (!collisionService.HasCollision(rb)) {
                    rb.SetLinearV(linearV);
                    continue;
                }

                // - 弹力累加
                CalculateBounce(rb, dt, ref linearV);
                UnityEngine.Debug.Log($"弹力过后 linearV {linearV}");

                ApplyFriction(rb, dt, m, outForce, ref linearV);
                UnityEngine.Debug.Log($"摩擦力过后 linearV {linearV}");

                rb.SetLinearV(linearV);
            }
        }

        void ApplyFriction(Box3DRigidbody rb, in FP64 dt, in FP64 mass, in FPVector3 outForce, ref FPVector3 linearV) {
            var boxes = physicsFacade.boxes;
            var service = physicsFacade.Service;
            var collisionService = service.CollisionService;
            var boxIDInfos = physicsFacade.Service.IDService.boxIDInfos;
            FPVector3 allFrictionForce = FPVector3.Zero;
            // 跟所有其他RB、SB进行 F = UN 计算 ，并且累加
            for (int j = 0; j < boxes.Length; j++) {
                if (!boxIDInfos[j]) continue;
                var box = boxes[j];
                if (!collisionService.TryGetCollision(rb, box, out var collision)) {
                    continue;
                }

                // - 摩擦力累加
                // - With SB
                FPVector3 beHitDirA = collision.BeHitDirA;
                FPVector3 beHitDir = collision.bodyA == rb ? beHitDirA : -beHitDirA;
                // 摩擦力不可能与外力在同一力线上
                var cos = FPVector3.Dot(linearV.normalized, beHitDir);
                if (FPUtils.IsNear(cos, FP64.One, FP64.EN1) || FPUtils.IsNear(cos, -FP64.One, FP64.EN1)) {
                    continue;
                }

                FP64 n = FPVector3.Dot(outForce, -beHitDir);
                FP64 u = collision.FirctionCoe_combined;
                FPVector3 force = u * n * GetFrictionDir(linearV, beHitDir);
                allFrictionForce += force;
            }

            // 对累加后的总滑动摩擦力进行计算
            // 根据速度计算摩擦力方向
            if (allFrictionForce != FPVector3.Zero
            && allFrictionForce.LengthSquared() != 0) {
                var offsetV_friction = GetOffsetV_ByForce(-allFrictionForce, mass, dt);
                var offsetLen = offsetV_friction.Length();
                var linearVLen = linearV.Length();
                if (offsetLen  > linearVLen ) {
                    // UnityEngine.Debug.Log($"摩擦力 停下");
                    linearV = FPVector3.Zero;
                } else {
                    // UnityEngine.Debug.Log($"摩擦力 减速 ");
                    linearV += offsetV_friction;
                }
            }

        }

        FPVector3 GetFrictionDir(in FPVector3 linearV, in FPVector3 beHitDir) {
            var crossAxis = FPVector3.Cross(linearV, beHitDir);
            crossAxis.Normalize();
            var rot = FPQuaternion.CreateFromAxisAngle(crossAxis, FPUtils.rad_90);
            var frictionDir = rot * -beHitDir;    // 撞击方向 绕轴旋转
            return frictionDir;
        }

        FPVector3 GetOffsetV_ByForce(in FPVector3 f, in FP64 m, in FP64 t) {
            var a = f / m;
            var offset = a * t;
            return offset;
        }

        void CalculateBounce(Box3DRigidbody rb, in FP64 dt, ref FPVector3 linearV) {
            var boxes = physicsFacade.boxes;
            var service = physicsFacade.Service;
            var collisionService = service.CollisionService;
            var boxIDInfos = physicsFacade.Service.IDService.boxIDInfos;
            // 跟所有其他RB、SB进行 F = UN 计算 ，并且累加
            for (int i = 0; i < boxes.Length; i++) {
                if (!boxIDInfos[i]) {
                    continue;
                }
                var box = boxes[i];
                if (!collisionService.TryGetCollision(rb, box, out var collision)) {
                    continue;
                }
                if (collision.CollisionType == Generic.CollisionType.Exit) {
                    continue;
                }
                FPVector3 beHitDirA = collision.BeHitDirA;
                FPVector3 beHitDir = collision.bodyA == rb ? beHitDirA : -beHitDirA;
                var v_bounced = Bounce3DUtils.GetBouncedV(linearV, beHitDir, rb.BounceCoefficient);
                linearV = v_bounced;
            }
        }

    }

}