using FixMath.NET;
using UnityEngine;

namespace Transformer.Extension
{

    public static class VectorExtension
    {

        public static FPVector3 ToFPVector3(this Vector3 v)
        {
            return new FPVector3(FP64.ToFP64(v.x), FP64.ToFP64(v.y), FP64.ToFP64(v.z));
        }

        public static Vector3 ToVector3(this FPVector3 v)
        {
            return new Vector3((float)v.x, (float)v.y, (float)v.z);
        }

    }

}