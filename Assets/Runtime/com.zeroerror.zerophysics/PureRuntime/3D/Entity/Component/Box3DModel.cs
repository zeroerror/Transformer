using FixMath.NET;
using ZeroPhysics.Generic;

namespace ZeroPhysics.Physics3D
{

    public class Box3DModel
    {

        FPVector3 center;
        public FPVector3 Center => center;

        FPQuaternion rotation;
        public FPQuaternion Rot => rotation;

        FPVector3 scaledSize;

        public FPVector3[] vertices;
        public FPVector3 Max => vertices[1];
        public FPVector3 Min => vertices[6];

        FPVector3 p0_aabb;
        FPVector3 p1_aabb;
        FPVector3 p2_aabb;
        FPVector3 p3_aabb;
        FPVector3 p4_aabb;
        FPVector3 p5_aabb;
        FPVector3 p6_aabb;
        FPVector3 p7_aabb;

        FPVector3 p0_rot;
        FPVector3 p1_rot;
        FPVector3 p2_rot;
        FPVector3 p3_rot;
        FPVector3 p4_rot;
        FPVector3 p5_rot;
        FPVector3 p6_rot;
        FPVector3 p7_rot;

        FPVector3 p0;
        FPVector3 p1;
        FPVector3 p2;
        FPVector3 p3;
        FPVector3 p4;
        FPVector3 p5;
        FPVector3 p6;
        FPVector3 p7;

        public Box3DModel(TransformComponent3D trans, in FPVector3 size)
        {
            this.vertices = new FPVector3[8];
            var scaledSize = trans.Scale * size;
            UpdateScaleAndSize(scaledSize);
            UpdateRot(trans.Rotation);
            UpdateCenter(trans.Center);
        }

        public void Update(TransformComponent3D trans, in FPVector3 size)
        {
            var scaledSize = trans.Scale * size;
            bool hasChangedSize = scaledSize != this.scaledSize;
            if (hasChangedSize)
            {
                UpdateScaleAndSize(scaledSize);
            }

            bool hasRotated = rotation != trans.Rotation;
            if (hasRotated)
            {
                UpdateRot(trans.Rotation);
            }

            bool hasMoved = center != trans.Center;
            if (hasMoved)
            {
                UpdateCenter(trans.Center);
            }
        }

        public BoxType GetBoxType()
        {
            return rotation == FPQuaternion.Identity ? BoxType.AABB : BoxType.OBB;
        }

        public FPVector2 GetAxisX_SelfProjectionSub()
        {
            var halfX = scaledSize.x * FP64.Half;
            return new FPVector2(-halfX, halfX);
        }

        public FPVector2 GetAxisY_SelfProjectionSub()
        {
            var halfY = scaledSize.y * FP64.Half;
            return new FPVector2(-halfY, halfY);
        }

        public FPVector2 GetAxisZ_SelfProjectionSub()
        {
            var halfZ = scaledSize.z * FP64.Half;
            return new FPVector2(-halfZ, halfZ);
        }

        public Axis3D GetAxisX()
        {
            Axis3D axis = new Axis3D();
            axis.origin = center;
            var dir = FPVector3.Right;
            if (rotation != FPQuaternion.Identity) dir = rotation * dir;
            axis.dir = dir;
            return axis;
        }

        public Axis3D GetAxisY()
        {
            Axis3D axis = new Axis3D();
            axis.origin = center;
            var dir = FPVector3.Up;
            if (rotation != FPQuaternion.Identity) dir = rotation * dir;
            axis.dir = dir;
            return axis;
        }

        public Axis3D GetAxisZ()
        {
            Axis3D axis = new Axis3D();
            axis.origin = center;
            var dir = FPVector3.Forward;
            if (rotation != FPQuaternion.Identity) dir = rotation * dir;
            axis.dir = dir;
            return axis;
        }

        void UpdateScaleAndSize(in FPVector3 scaledSize)
        {
            this.scaledSize = scaledSize;
            var halfScaledSize = scaledSize * FP64.Half;
            p0_aabb = new FPVector3(-halfScaledSize.x, halfScaledSize.y, halfScaledSize.z);
            p1_aabb = new FPVector3(halfScaledSize.x, halfScaledSize.y, halfScaledSize.z);
            p2_aabb = new FPVector3(-halfScaledSize.x, halfScaledSize.y, -halfScaledSize.z);
            p3_aabb = new FPVector3(halfScaledSize.x, halfScaledSize.y, -halfScaledSize.z);
            p4_aabb = new FPVector3(-halfScaledSize.x, -halfScaledSize.y, halfScaledSize.z);
            p5_aabb = new FPVector3(halfScaledSize.x, -halfScaledSize.y, halfScaledSize.z);
            p6_aabb = new FPVector3(-halfScaledSize.x, -halfScaledSize.y, -halfScaledSize.z);
            p7_aabb = new FPVector3(halfScaledSize.x, -halfScaledSize.y, -halfScaledSize.z);
        }

        void UpdateRot(in FPQuaternion rot)
        {
            this.rotation = rot;
            if (rot != FPQuaternion.Identity)
            {
                p0_rot = rot * p0_aabb;
                p1_rot = rot * p1_aabb;
                p2_rot = rot * p2_aabb;
                p3_rot = rot * p3_aabb;
                p4_rot = rot * p4_aabb;
                p5_rot = rot * p5_aabb;
                p6_rot = rot * p6_aabb;
                p7_rot = rot * p7_aabb;
            }
            else
            {
                p0_rot = p0_aabb;
                p1_rot = p1_aabb;
                p2_rot = p2_aabb;
                p3_rot = p3_aabb;
                p4_rot = p4_aabb;
                p5_rot = p5_aabb;
                p6_rot = p6_aabb;
                p7_rot = p7_aabb;
            }
        }

        void UpdateCenter(in FPVector3 newCenter)
        {
            this.center = newCenter;
            p0 = p0_rot + newCenter;
            p1 = p1_rot + newCenter;
            p2 = p2_rot + newCenter;
            p3 = p3_rot + newCenter;
            p4 = p4_rot + newCenter;
            p5 = p5_rot + newCenter;
            p6 = p6_rot + newCenter;
            p7 = p7_rot + newCenter;
            vertices[0] = p0;
            vertices[1] = p1;
            vertices[2] = p2;
            vertices[3] = p3;
            vertices[4] = p4;
            vertices[5] = p5;
            vertices[6] = p6;
            vertices[7] = p7;
        }

    }

}
