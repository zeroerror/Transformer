using UnityEngine;
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

    }

}