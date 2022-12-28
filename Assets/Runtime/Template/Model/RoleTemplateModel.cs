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
        public int moveSpeedCM;
        public int jumpForceCM;


    }

}