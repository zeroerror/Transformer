using FixMath.NET;
using ZeroPhysics.Generic;
using ZeroPhysics.Utils;

namespace ZeroPhysics.Physics3D {

    public static class Intersect3DUtils {

        public static bool HasCollision(Box3D box1, Box3D box2) {
            if (box1.GetBoxType() == BoxType.OBB || box2.GetBoxType() == BoxType.OBB) return HasCollision_OBB(box1.GetModel(), box2.GetModel());
            else return HasCollision_AABB(box1.GetModel(), box2.GetModel());
        }

        public static bool HasCollision_AABB(in Box3DModel box1, in Box3DModel box2) {
            var min1 = box1.Min;
            var max1 = box1.Max;
            var min2 = box2.Min;
            var max2 = box2.Max;

            return HasCollision_AABB(min1, max1, min2, max2);
        }

        public static bool HasCollision_AABB(in FPVector3 min1, in FPVector3 max1, in FPVector3 min2, in FPVector3 max2) {

            // - Axis x
            var diff_x1 = min1.x - max2.x;
            var diff_x2 = min2.x - max1.x;
            bool hasCollisionX = !(diff_x1 > 0 || diff_x2 > 0);

            // - Axis y
            var diff_y1 = min1.y - max2.y;
            var diff_y2 = min2.y - max1.y;
            bool hasCollisionY = !(diff_y1 > 0 || diff_y2 > 0);

            // - Axis y
            var diff_z1 = min1.z - max2.z;
            var diff_z2 = min2.z - max1.z;
            bool hasCollisionZ = !(diff_z1 > 0 || diff_z2 > 0);

            return hasCollisionX && hasCollisionY && hasCollisionZ;
        }

        public static bool HasCollision_OBB(in Box3DModel box1, in Box3DModel box2) {
            // - 6 Axis
            if (!HasIntersectsWithAxisX_LeftBox(box1, box2)) return false;
            if (!HasIntersectsWithAxisY_LeftBox(box1, box2)) return false;
            if (!HasIntersectsWithAxisZ_LeftBox(box1, box2)) return false;
            if (!HasIntersectsWithAxisX_LeftBox(box2, box1)) return false;
            if (!HasIntersectsWithAxisY_LeftBox(box2, box1)) return false;
            if (!HasIntersectsWithAxisZ_LeftBox(box2, box1)) return false;

            // - 9 Axis
            Axis3D axis = new Axis3D();
            var b1AxisX = box1.GetAxisX();
            var b1AxisY = box1.GetAxisY();
            var b1AxisZ = box1.GetAxisZ();
            var b2AxisX = box2.GetAxisX();
            var b2AxisY = box2.GetAxisY();
            var b2AxisZ = box2.GetAxisZ();
            // - x Cross x
            axis.dir = FPVector3.Cross(b1AxisX.dir, b2AxisX.dir);
            if (!HasIntersects_WithAxis(box1, box2, axis)) return false;
            // - x Cross y
            axis.dir = FPVector3.Cross(b1AxisX.dir, b2AxisY.dir);
            if (!HasIntersects_WithAxis(box1, box2, axis)) return false;
            // - x Cross z
            axis.dir = FPVector3.Cross(b1AxisX.dir, b2AxisZ.dir);
            if (!HasIntersects_WithAxis(box1, box2, axis)) return false;
            // - y Cross x
            axis.dir = FPVector3.Cross(b1AxisY.dir, b2AxisX.dir);
            if (!HasIntersects_WithAxis(box1, box2, axis)) return false;
            // - y Cross y
            axis.dir = FPVector3.Cross(b1AxisY.dir, b2AxisY.dir);
            if (!HasIntersects_WithAxis(box1, box2, axis)) return false;
            // - y Cross z
            axis.dir = FPVector3.Cross(b1AxisY.dir, b2AxisZ.dir);
            if (!HasIntersects_WithAxis(box1, box2, axis)) return false;
            // - z Cross x
            axis.dir = FPVector3.Cross(b1AxisZ.dir, b2AxisX.dir);
            if (!HasIntersects_WithAxis(box1, box2, axis)) return false;
            // - z Cross y
            axis.dir = FPVector3.Cross(b1AxisZ.dir, b2AxisY.dir);
            if (!HasIntersects_WithAxis(box1, box2, axis)) return false;
            // - z Cross z
            axis.dir = FPVector3.Cross(b1AxisZ.dir, b2AxisZ.dir);
            if (!HasIntersects_WithAxis(box1, box2, axis)) return false;

            return true;
        }

        internal static bool HasIntersectsWithAxisX_LeftBox(in Box3DModel box1, in Box3DModel box2) {
            var b1AxisX = box1.GetAxisX();
            var box1_projSub = box1.GetAxisX_SelfProjectionSub();
            var box2_projSub = Projection3DUtils.GetProjectionSub(box2, b1AxisX);
            return HasIntersects(box1_projSub, box2_projSub);
        }

        internal static bool HasIntersectsWithAxisY_LeftBox(in Box3DModel box1, in Box3DModel box2) {
            var b1AxisY = box1.GetAxisY();
            var box1_projSub = box1.GetAxisY_SelfProjectionSub();
            var box2_projSub = Projection3DUtils.GetProjectionSub(box2, b1AxisY);
            return HasIntersects(box1_projSub, box2_projSub);
        }

        internal static bool HasIntersectsWithAxisZ_LeftBox(in Box3DModel box1, in Box3DModel box2) {
            var b1AxisZ = box1.GetAxisZ();
            var box1_projSub = box1.GetAxisZ_SelfProjectionSub();
            var box2_projSub = Projection3DUtils.GetProjectionSub(box2, b1AxisZ);
            return HasIntersects(box1_projSub, box2_projSub);
        }

        internal static bool HasIntersects_WithAxis(in Box3DModel model1, in Box3DModel model2, in Axis3D axis) {
            var box1_projSub = Projection3DUtils.GetProjectionSub(model1, axis);
            var box2_projSub = Projection3DUtils.GetProjectionSub(model2, axis);
            return HasIntersects(box1_projSub, box2_projSub);
        }

        internal static bool IsInsideBox(in Box3DModel model, FPVector3 point, in FP64 epsilon) {
            var px = point.x;
            var py = point.y;
            var pz = point.z;
            if (model.GetBoxType() == BoxType.AABB) {
                var min = model.Min;
                var max = model.Max;
                return px >= (min.x - epsilon) && px <= (max.x + epsilon)
                && py <= (min.y + epsilon) && py >= (max.y - epsilon)
                && pz <= (min.z + epsilon) && pz >= (max.z - epsilon);
            } else {
                point -= model.Center;
                // - Axis x
                var axis = model.GetAxisX();
                var pj = FPVector3.Dot(point, axis.dir);
                var pjSub = model.GetAxisX_SelfProjectionSub();
                if (pj < (pjSub.x - epsilon) || pj > (pjSub.y + epsilon)) return false;

                // - Axis y
                axis = model.GetAxisY();
                pj = FPVector3.Dot(point, axis.dir);
                pjSub = model.GetAxisY_SelfProjectionSub();
                if (pj < (pjSub.x - epsilon) || pj > (pjSub.y + epsilon)) return false;

                // - Axis z
                axis = model.GetAxisZ();
                pj = FPVector3.Dot(point, axis.dir);
                pjSub = model.GetAxisZ_SelfProjectionSub();
                if (pj < (pjSub.x - epsilon) || pj > (pjSub.y + epsilon)) return false;

                return true;
            }
        }

        public static bool HasIntersects(in FPVector2 sub1, in FPVector2 sub2) {
            return !(sub1.y < sub2.x - FPUtils.epsilon_intersect || sub2.y < sub1.x - FPUtils.epsilon_intersect);
        }

    }

}
