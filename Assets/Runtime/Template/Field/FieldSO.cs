using UnityEngine;
using FixMath.NET;
using Transformer.Generic;

namespace Transformer.Template
{

    [CreateAssetMenu(fileName = "so_tm_field", menuName = "Transformer/Template/" + nameof(FieldSO))]
    public class FieldSO : ScriptableObject
    {

        public int typeID;
        public string typeName;

        // - Born Pos
        public int bornPosX;
        public int bornPosY;
        public int bornPosZ;

        // - Box And RigidbodyBox
        public TransformModel[] transformModels;
        public int rbBoxStartIndex;

        public FPVector3 ToFPBornPos()
        {
            return new FPVector3(bornPosX * FP64.EN2, bornPosY * FP64.EN2, bornPosZ * FP64.EN2);
        }

        public Vector3 ToBornPos()
        {
            return new Vector3(bornPosX * 0.01f, bornPosY * 0.01f, bornPosZ * 0.01f);
        }

    }

}