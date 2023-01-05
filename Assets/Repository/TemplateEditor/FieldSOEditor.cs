using UnityEngine;
using Transformer.Template;
using Transformer.Generic;

namespace Transformer.TemplateEditor
{

    public class FieldSOEditor : MonoBehaviour
    {

        public FieldSO linkedSO;

        public GameObject logicRoot;
        public GameObject bornPos;

        [ContextMenu("保存")]
        public void Save()
        {
            Debug.Assert(linkedSO != null, "FieldSOEditor: root不能为Null");
            Debug.Assert(logicRoot != null, "FieldSOEditor: root不能为Null");

            // - Box And RigidbodyBox
            var boxes = logicRoot.GetComponentsInChildren<BoxCollider>();
            var boxlen = boxes.Length;
            var totalLen = boxlen;
            TransformModel[] allModels = new TransformModel[totalLen];
            for (int i = 0; i < boxlen; i++)
            {
                var b = boxes[i];
                var center = b.transform.position;
                var rotation = b.transform.rotation;
                var scale = b.transform.lossyScale;
                var size = b.size;
                var model = allModels[i];
                model.centerX = (int)(center.x * 100);
                model.centerY = (int)(center.y * 100);
                model.centerZ = (int)(center.z * 100);
                model.rotationX = (int)(rotation.x * 100);
                model.rotationY = (int)(rotation.y * 100);
                model.rotationZ = (int)(rotation.z * 100);
                model.rotationW = (int)(rotation.w * 100);
                model.scaleX = (int)(scale.x * 100);
                model.scaleY = (int)(scale.y * 100);
                model.scaleZ = (int)(scale.z * 100);
                model.sizeX = (int)(size.x * 100);
                model.sizeY = (int)(size.y * 100);
                model.sizeZ = (int)(size.z * 100);
                allModels[i] = model;
            }
            linkedSO.rbBoxStartIndex = boxlen;
            linkedSO.transformModels = allModels;
            Debug.Log($"Box数量:{boxlen}");
            Debug.Log($"总逻辑盒数量:{totalLen}");

            // - Born Pos
            var bornPos = this.bornPos.transform.position;
            linkedSO.bornPosX = (int)(bornPos.x * 100f);
            linkedSO.bornPosY = (int)(bornPos.y * 100f);
            linkedSO.bornPosZ = (int)(bornPos.z * 100f);
        }

    }

}