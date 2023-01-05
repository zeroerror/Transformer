using System;
using UnityEngine;

namespace Transformer.Template
{

    [CreateAssetMenu(fileName = "so_tm_role", menuName = "Transformer/Template/" + nameof(RoleTemplateModel))]
    public class RoleTemplateModel : ScriptableObject
    {

        public int typeID;
        public string typeName;

        // - Locomotion
        public int moveSpeed_CM;
        public int jumpForce_CM;

        // - RB
        public int width_CM;
        public int height_CM;
        public int length_CM;
        public int sizeX_CM;
        public int sizeY_CM;
        public int sizeZ_CM;


    }

}