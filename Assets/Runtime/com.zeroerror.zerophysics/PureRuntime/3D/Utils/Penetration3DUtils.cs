using FixMath.NET;
using ZeroPhysics.Generic;
using ZeroPhysics.Utils;

namespace ZeroPhysics.Physics3D {

    public static class Penetration3DUtils {

        public static FPVector3 PenetrationCorrection(Box3D box1, FP64 mtvCoe1, Box3D box2, FP64 mtvCoe2) {
            var mtv = GetMTV(box1.GetModel(), box2.GetModel());
            mtv *= FPUtils.mtv_multy;
            var mtv1 = mtv * mtvCoe1;
            var mtv2 = -mtv * mtvCoe2;
            var newCenter1 = box1.Center + mtv1;
            var newCenter2 = box2.Center + mtv2;
            box1.SetCenter(newCenter1);
            box2.SetCenter(newCenter2);
            return mtv;
        }

        public static FPVector3 GetMTV(Box3DModel model1, Box3DModel model2) {
            FP64 len_min = FP64.MaxValue;
            FPVector3 dir = FPVector3.Zero;

            // 针对Box，求 3 + 3 = 6 个面的法向量上的投影，即12次投影计算

            var axis = model1.GetAxisX();
            var pjSub1 = model1.GetAxisX_SelfProjectionSub();
            var pjSub2 = Projection3DUtils.GetProjectionSub(model2, axis);
            UpdateMTV(ref len_min, ref dir, axis, pjSub1, pjSub2);

            axis = model1.GetAxisY();
            pjSub1 = model1.GetAxisY_SelfProjectionSub();
            pjSub2 = Projection3DUtils.GetProjectionSub(model2, axis);
            UpdateMTV(ref len_min, ref dir, axis, pjSub1, pjSub2);

            axis = model1.GetAxisZ();
            pjSub1 = model1.GetAxisZ_SelfProjectionSub();
            pjSub2 = Projection3DUtils.GetProjectionSub(model2, axis);
            UpdateMTV(ref len_min, ref dir, axis, pjSub1, pjSub2);

            bool isBox1Aixs = dir != FPVector3.Zero;

            axis = model2.GetAxisX();
            pjSub2 = model2.GetAxisX_SelfProjectionSub();
            pjSub1 = Projection3DUtils.GetProjectionSub(model1, axis);
            UpdateMTV(ref len_min, ref dir, axis, pjSub1, pjSub2);

            axis = model2.GetAxisY();
            pjSub2 = model2.GetAxisY_SelfProjectionSub();
            pjSub1 = Projection3DUtils.GetProjectionSub(model1, axis);
            UpdateMTV(ref len_min, ref dir, axis, pjSub1, pjSub2);

            axis = model2.GetAxisZ();
            pjSub2 = model2.GetAxisZ_SelfProjectionSub();
            pjSub1 = Projection3DUtils.GetProjectionSub(model1, axis);
            UpdateMTV(ref len_min, ref dir, axis, pjSub1, pjSub2);

            if (!isBox1Aixs) dir = -dir;

            return len_min * dir;
        }

        static void UpdateMTV(ref FP64 len_min, ref FPVector3 dir, Axis3D axis, FPVector2 pjSub1, FPVector2 pjSub2) {
            var l1 = pjSub2.y - pjSub1.x;
            var l2 = pjSub1.y - pjSub2.x;
            var lm = FP64.Min(l1, l2);
            if (lm < len_min) {
                len_min = lm;
                dir = l1 < l2 ? axis.dir : -axis.dir;
            }
        }

    }

}