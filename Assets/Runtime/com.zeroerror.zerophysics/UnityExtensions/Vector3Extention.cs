using UnityEngine;
using FixMath.NET;

namespace ZeroPhysics.Extensions
{

    public static class Vector3Extention
    {

        public static FPVector3 ToFPVector3(this in Vector3 v)
        {
            return new FPVector3((FP64)v.x, (FP64)v.y, (FP64)v.z);
        }

        public static Vector3 ToVector3(this in FPVector3 v)
        {
            return new Vector3(v.x.AsFloat(), v.y.AsFloat(), v.z.AsFloat());
        }

        public static Vector3[] ToVector3Array(this FPVector3[] array)
        {
            Vector3[] ary = new Vector3[array.Length];
            for (int i = 0; i < ary.Length; i++)
            {
                ary[i] = array[i].ToVector3();
            }
            return ary;
        }

    }

}