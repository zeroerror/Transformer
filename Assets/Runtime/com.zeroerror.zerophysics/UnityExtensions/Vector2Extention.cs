using UnityEngine;
using FixMath.NET;

namespace ZeroPhysics.Extensions
{

    public static class Vector2Extention
    {

        public static FPVector2 ToFPVector2(this in Vector2 v)
        {
            return new FPVector2((FP64)v.x, (FP64)v.y);
        }

        public static FPVector2 ToFPVector2(this in Vector3 v)
        {
            return new FPVector2((FP64)v.x, (FP64)v.y);
        }

        public static Vector2 ToVector2(this in FPVector2 v)
        {
            return new Vector2(v.x.AsFloat(), v.y.AsFloat());
        }

        public static Vector2 ToVector2(this in FPVector3 v)
        {
            return new Vector2(v.x.AsFloat(), v.y.AsFloat());
        }

    }

}