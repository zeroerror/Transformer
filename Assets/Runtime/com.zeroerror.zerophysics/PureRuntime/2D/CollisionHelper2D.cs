using FixMath.NET;
using ZeroPhysics.Generic;

namespace ZeroPhysics.Physics2D
{

    public static class CollisionHelper2D
    {

        public static bool HasCollision(Box2D box1, Box2D box2)
        {
            if (box1.BoxType == BoxType.OBB || box2.BoxType == BoxType.OBB)
            {
                return HasCollision_OBB(box1, box2);
            }

            return HasCollision_AABB(box1, box2);
        }

        public static bool HasCollision(Sphere2D sphere1, Sphere2D sphere2)
        {
            var center1 = sphere1.Center;
            var center2 = sphere2.Center;
            var xDiff = center1.x - center2.x;
            var yDiff = center1.y - center2.y;
            var radiusSum = sphere1.Radius_scaled + sphere2.Radius_scaled;
            return (xDiff * xDiff + yDiff * yDiff) <= (radiusSum * radiusSum);
        }

        public static bool HasCollision(Sphere2D sphere, Box2D box)
        {
            if (!HasCollision(sphere.Box, box)) return false;
            if (sphere.HasCollisionWithSphere(box.A)) return true;
            if (sphere.HasCollisionWithSphere(box.B)) return true;
            if (sphere.HasCollisionWithSphere(box.C)) return true;
            if (sphere.HasCollisionWithSphere(box.D)) return true;

            var axisX = box.GetAxisX();
            var axisX_PjSub1 = box.GetAxisX_SelfProjectionSub();
            var axisX_PjSub2 = sphere.GetProjectionSub(axisX);
            var spherePjCenter_X = (axisX_PjSub2.x + axisX_PjSub2.y) / 2;
            bool xOverlapCenter = spherePjCenter_X > axisX_PjSub1.x && spherePjCenter_X < axisX_PjSub1.y;

            // - AABB: 经前置条件过滤后, 以Box的2个轴做投影,若出现SphereCenter的投影在Box投影内，则必定碰撞
            if (box.BoxType == BoxType.AABB) return xOverlapCenter;

            // - OBB: 经前置条件过滤后, 以Box的2个轴做投影,若出现SphereCenter的投影在Box投影内, 只要另外一个轴上的投影有相交，则必定碰撞
            var axisY = box.GetAxisY();
            var axisY_PjSub1 = box.GetAxisY_SelfProjectionSub();
            var axisY_PjSub2 = sphere.GetProjectionSub(axisY);
            if (xOverlapCenter)
            {
                return !(axisY_PjSub1.y < axisY_PjSub2.x || axisY_PjSub1.x > axisY_PjSub2.y);
            }

            var spherePjCenter_Y = (axisY_PjSub2.x + axisY_PjSub2.y) / 2;
            bool yOverlapCenter = spherePjCenter_Y > axisX_PjSub1.x && spherePjCenter_Y < axisX_PjSub1.y;
            return yOverlapCenter && !(axisX_PjSub1.y < axisX_PjSub2.x || axisX_PjSub1.x > axisX_PjSub2.y);
        }

        static bool HasCollision_AABB(Box2D box1, Box2D box2)
        {
            var ltPos1 = box1.A;
            var rbPos1 = box1.C;
            var ltPos2 = box2.A;
            var rbPos2 = box2.C;
            // - Axis x
            var diff_x1 = ltPos1.x - rbPos2.x;
            var diff_x2 = rbPos1.x - ltPos2.x;
            bool hasCollisionX = (diff_x1 < 0 && diff_x2 > 0) || diff_x1 == 0 || diff_x2 == 0;

            // - Axis y
            var diff_y1 = ltPos1.y - rbPos2.y;
            var diff_y2 = rbPos1.y - ltPos2.y;
            bool hasCollisionY = (diff_y1 > 0 && diff_y2 < 0) || diff_y1 == 0 || diff_y2 == 0;

            return hasCollisionX && hasCollisionY;
        }

        public static bool HasCollision_AABB(FPVector2 ltPos1, FPVector2 rbPos1, FPVector2 ltPos2, FPVector2 rbPos2)
        {
            // - Axis x
            var diff_x1 = ltPos1.x - rbPos2.x;
            var diff_x2 = rbPos1.x - ltPos2.x;
            bool hasCollisionX = (diff_x1 < 0 && diff_x2 > 0) || diff_x1 == 0 || diff_x2 == 0;

            // - Axis y
            var diff_y1 = ltPos1.y - rbPos2.y;
            var diff_y2 = rbPos1.y - ltPos2.y;
            bool hasCollisionY = (diff_y1 > 0 && diff_y2 < 0) || diff_y1 == 0 || diff_y2 == 0;

            return hasCollisionX && hasCollisionY;
        }

        static bool HasCollision_OBB(Box2D box1, Box2D box2)
        {
            var box1_projSub = box1.GetAxisX_SelfProjectionSub();
            var box2_projSub = box2.GetProjectionSub(box1.GetAxisX());
            if (box1_projSub.y < box2_projSub.x) return false;
            if (box1_projSub.x > box2_projSub.y) return false;

            box1_projSub = box1.GetAxisY_SelfProjectionSub();
            box2_projSub = box2.GetProjectionSub(box1.GetAxisY());
            if (box1_projSub.y < box2_projSub.x) return false;
            if (box1_projSub.x > box2_projSub.y) return false;

            box2_projSub = box2.GetAxisX_SelfProjectionSub();
            box1_projSub = box1.GetProjectionSub(box2.GetAxisX());
            if (box1_projSub.y < box2_projSub.x) return false;
            if (box1_projSub.x > box2_projSub.y) return false;

            box2_projSub = box2.GetAxisY_SelfProjectionSub();
            box1_projSub = box1.GetProjectionSub(box2.GetAxisY());
            if (box1_projSub.y < box2_projSub.x) return false;
            if (box1_projSub.x > box2_projSub.y) return false;

            return true;
        }

        public static void GetOBBMinMaxPos(Box2D box, ref FPVector2 pos_minX, ref FPVector2 pos_maxX, ref FPVector2 pox_minY, ref FPVector2 pos_maxY)
        {
            var a = box.A;
            var b = box.B;
            var c = box.C;
            var d = box.D;
            var rotAngle = box.Rotation;
            var count = (int)(rotAngle / 90);
            bool isPositive = rotAngle > 0;
            if (count == 0)
            {
                if (isPositive)
                {
                    pos_minX = a;
                    pos_maxX = c;
                    pos_maxY = b;
                    pox_minY = d;
                }
                else
                {
                    pos_minX = d;
                    pos_maxX = b;
                    pos_maxY = a;
                    pox_minY = c;
                }
            }
            else if (count == 1)
            {
                if (isPositive)
                {
                    pos_minX = b;
                    pos_maxX = d;
                    pos_maxY = c;
                    pox_minY = a;
                }
                else
                {
                    pos_minX = c;
                    pos_maxX = a;
                    pos_maxY = d;
                    pox_minY = b;
                }
            }
            else if (count == 2)
            {
                if (isPositive)
                {
                    pos_minX = c;
                    pos_maxX = a;
                    pos_maxY = d;
                    pox_minY = b;
                }
                else
                {
                    pos_minX = b;
                    pos_maxX = d;
                    pos_maxY = c;
                    pox_minY = a;
                }
            }
            else if (count == 3)
            {
                if (isPositive)
                {
                    pos_minX = d;
                    pos_maxX = b;
                    pos_maxY = a;
                    pox_minY = c;
                }
                else
                {
                    pos_minX = a;
                    pos_maxX = c;
                    pos_maxY = b;
                    pox_minY = d;
                }
            }
        }

    }
}
