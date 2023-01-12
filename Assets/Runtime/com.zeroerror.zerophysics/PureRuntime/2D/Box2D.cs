using FixMath.NET;
using ZeroPhysics.Generic;

namespace ZeroPhysics.Physics2D
{

    public class Box2D
    {

        #region [Field]

        BoxType boxType;
        public BoxType BoxType => boxType;
        public void SetBoxType(BoxType v) => boxType = v;

        // - Position
        FPVector2 center;
        public FPVector2 Center => center;

        // - Rotation
        public FP64 rotDeg;
        public FP64 Rotation => rotDeg;

        // - Scale
        FPVector2 scale;
        public FPVector2 Scale => scale;

        FP64 width_scaled;
        FP64 width_half_scaled;

        FP64 height_scaled;
        FP64 height_half_scaled;

        FP64 length_scaled;
        FP64 length_half_scaled;

        FP64 radius;
        public FP64 Radius => radius;

        FP64 width_half;
        public FP64 Width_half => width_half;

        FP64 width;
        public FP64 Width => width;

        FP64 height_half;
        public FP64 Height_half => height_half;

        public FP64 height;
        public FP64 Height => height;

        FPVector2 a;
        public FPVector2 A => a;

        FPVector2 b;
        public FPVector2 B => b;

        FPVector2 c;
        public FPVector2 C => c;

        FPVector2 d;
        public FPVector2 D => d;


        #endregion

        public Box2D(FPVector2 center, FP64 width, FP64 height, FP64 deg, FPVector2 scale)
        {
            UpdateWidth(width);
            UpdateHeight(height);
            UpdateCenter(center);
            UpdateRotAngle(deg);
            UpdateScale(scale);
        }

        public void UpdateCenter(FPVector2 v)
        {
            center = v;
            UpdateAllPoints();
        }

        public void UpdateRotAngle(FP64 v)
        {
            rotDeg = v;
            UpdateAllPoints();
        }

        public void UpdateScale(FPVector2 v)
        {
            this.scale = v;
            width_scaled = width * v.x;
            width_half_scaled = width_scaled / 2;
            height_scaled = height * v.y;
            height_half_scaled = height_scaled / 2;
        }

        public void UpdateWidth(FP64 v)
        {
            width = v;
            width_half = v / 2;

            width_scaled = v * scale.x;
            width_half_scaled = width_scaled / 2;

            UpdateAllPoints();
        }

        public void UpdateHeight(FP64 v)
        {
            height = v;
            height_half = v / 2;

            height_scaled = v * scale.y;
            height_half_scaled = height_scaled / 2;

            UpdateAllPoints();
        }

        public FPVector2 GetProjectionSub(Axis2D axis)
        {
            FPVector2 center = axis.center;
            FPVector2 dir = axis.dir;
            FP64 p0 = FPVector2.Dot(a - center, dir);
            FP64 p1 = FPVector2.Dot(b - center, dir);
            FP64 p2 = FPVector2.Dot(c - center, dir);
            FP64 p3 = FPVector2.Dot(d - center, dir);
            FP64[] arry = new FP64[] { p0, p1, p2, p3 };
            FP64 min = FP64.Min(arry);
            FP64 max = FP64.Max(arry);
            FPVector2 sub = new FPVector2(min, max);
            return sub;
        }

        #region [Private]

        void UpdateAllPoints()
        {
            UpdateA();
            UpdateB();
            UpdateC();
            UpdateD();
        }

        void UpdateA()
        {
            FPVector2 v = new FPVector2(-width_half_scaled, height_half_scaled);
            v = FPVector2.GetRotated(v, rotDeg);
            v += center;
            a = v;
        }

        void UpdateB()
        {
            FPVector2 v = new FPVector2(width_half_scaled, height_half_scaled);
            v = FPVector2.GetRotated(v, rotDeg);
            v += center;
            b = v;
        }

        void UpdateC()
        {
            FPVector2 v = new FPVector2(width_half_scaled, -height_half_scaled);
            v = FPVector2.GetRotated(v, rotDeg);
            v += center;
            c = v;
        }

        void UpdateD()
        {
            FPVector2 v = new FPVector2(-width_half_scaled, -height_half_scaled);
            v = FPVector2.GetRotated(v, rotDeg);
            v += center;
            d = v;
        }

        #endregion

        public Axis2D GetAxisX()
        {
            Axis2D axis2D;
            axis2D.center = center;
            axis2D.dir = FPVector2.GetRotated(FPVector2.UnitX, rotDeg);
            return axis2D;
        }

        public Axis2D GetAxisY()
        {
            Axis2D axis2D;
            axis2D.center = center;
            axis2D.dir = FPVector2.GetRotated(FPVector2.UnitY, rotDeg);
            return axis2D;
        }

        public FPVector2 GetAxisX_SelfProjectionSub() => new FPVector2(-width_half_scaled, width_half_scaled);
        public FPVector2 GetAxisY_SelfProjectionSub() => new FPVector2(-height_half_scaled, height_half_scaled);

    }

}
