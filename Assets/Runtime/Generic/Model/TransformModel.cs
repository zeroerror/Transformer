using System;
using FixMath.NET;

namespace Transformer.Generic
{

    [Serializable]
    public struct TransformModel
    {

        public int centerX;
        public int centerY;
        public int centerZ;
        public int rotationX;
        public int rotationY;
        public int rotationZ;
        public int rotationW;
        public int scaleX;
        public int scaleY;
        public int scaleZ;
        public int sizeX;
        public int sizeY;
        public int sizeZ;

        public FPVector3 ToFPCenter()
        {
            return new FPVector3(centerX * FP64.EN2, centerY * FP64.EN2, centerZ * FP64.EN2);
        }

        public FPQuaternion ToFPQuaternion()
        {
            return new FPQuaternion(rotationX * FP64.EN2, rotationY * FP64.EN2, rotationZ * FP64.EN2, rotationW * FP64.EN2);
        }

        public FPVector3 ToFPScale()
        {
            return new FPVector3(scaleX * FP64.EN2, scaleY * FP64.EN2, scaleZ * FP64.EN2);
        }

        public FPVector3 ToFPSize()
        {
            return new FPVector3(sizeX * FP64.EN2, sizeY * FP64.EN2, sizeZ * FP64.EN2);
        }

    }

}