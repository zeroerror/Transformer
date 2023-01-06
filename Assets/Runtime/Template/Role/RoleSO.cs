using System;
using UnityEngine;

namespace Transformer.Template
{

    [CreateAssetMenu(fileName = "so_tm_role", menuName = "Transformer/Template/" + nameof(RoleSO))]
    public class RoleSO : ScriptableObject
    {

        public int typeID;
        public string typeName;

        // - Locomotion
        public int moveSpeed_CM;
        public int jumpForce_CM;

        // - RB
        public int sizeX_CM;
        public int sizeY_CM;
        public int sizeZ_CM;

        public Vector3 ToSize()
        {
            return new Vector3(sizeX_CM / 100f, sizeY_CM / 100f, sizeZ_CM / 100f);
        }


    }

}