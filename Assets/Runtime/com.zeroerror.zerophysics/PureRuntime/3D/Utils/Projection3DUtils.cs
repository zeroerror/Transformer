using System;
using FixMath.NET;
using ZeroPhysics.Generic;

namespace ZeroPhysics.Physics3D {

    public static class Projection3DUtils {

        public static FPVector2 GetProjectionSub(Box3DModel box, Axis3D axis) {
            FPVector3 axisOrigin = axis.origin;
            FPVector3 axisDir = axis.dir;
            FPVector3[] vertices = box.vertices;
            Span<FP64> pArray = new FP64[8];
            for (int i = 0; i < vertices.Length; i++) {
                pArray[i] = FPVector3.Dot(vertices[i] - axisOrigin, axisDir);
            }
            var min = FP64.Min(pArray);
            var max = FP64.Max(pArray);
            FPVector2 sub = new FPVector2(min, max);
            return sub;
        }

    }

}