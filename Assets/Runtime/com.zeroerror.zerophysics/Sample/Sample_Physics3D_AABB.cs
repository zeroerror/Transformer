using System.Collections.Generic;
using UnityEngine;
using ZeroPhysics.Extensions;
using ZeroPhysics.Physics3D;

namespace ZeroPhysics.Sample
{

    public class Sample_Physics3D_AABB : MonoBehaviour
    {

        bool isRun = false;

        Box3D[] boxes;
        Transform[] bcs;
        public Transform Boxes;
        PhysicsWorld3DCore physicsCore;

        public void Start()
        {
            if (Boxes == null) return;
            isRun = true;

            physicsCore = new PhysicsWorld3DCore(new FixMath.NET.FPVector3(0, -10, 0));

            var bcCount = Boxes.childCount;
            bcs = new Transform[bcCount];
            for (int i = 0; i < bcCount; i++)
            {
                var bc = Boxes.GetChild(i);
                bcs[i] = bc;
            }

            boxes = new Box3D[bcCount];
            var setterAPI = physicsCore.SetterAPI;
            for (int i = 0; i < bcCount; i++)
            {
                var bcTF = bcs[i].transform;
                boxes[i] = setterAPI.SpawnBox(bcTF.position.ToFPVector3(), bcTF.rotation.ToFPQuaternion(), bcTF.localScale.ToFPVector3(), Vector3.one.ToFPVector3());
            }
        }

        public void OnDrawGizmos()
        {
            if (!isRun) return;
            if (bcs == null) return;
            if (boxes == null) return;

            // Gizmos.DrawLine(Vector3.zero + Vector3.up * 10f, Vector3.zero + Vector3.down * 10f);
            // Gizmos.DrawLine(Vector3.zero + Vector3.left * 10f, Vector3.zero + Vector3.right * 10f);
            // Gizmos.DrawLine(Vector3.zero + Vector3.forward * 10f, Vector3.zero + Vector3.back * 10f);

            Dictionary<int, Box3D> collisionBoxDic = new Dictionary<int, Box3D>();
            for (int i = 0; i < boxes.Length - 1; i++)
            {
                for (int j = i + 1; j < boxes.Length; j++)
                {
                    if (Intersect3DUtils.HasCollision(boxes[i], boxes[j]))
                    {
                        collisionBoxDic[i] = boxes[i];
                        if (!collisionBoxDic.ContainsKey(j))
                        {
                            collisionBoxDic[j] = boxes[j];
                        }
                    }
                }
            }


            for (int i = 0; i < boxes.Length; i++)
            {
                var bc = bcs[i];
                var box = boxes[i];
                UpdateBox(bc.transform, box);
                Gizmos.color = Color.green;
                DrawBoxPoint(box);
                if (collisionBoxDic.ContainsKey(i))
                {
                    Gizmos.color = Color.red;
                }
                box.DrawBoxBorder();
            }

        }

        void UpdateBox(Transform src, Box3D box)
        {
            box.SetCenter(src.position.ToFPVector3());
            box.SetScale(src.localScale.ToFPVector3());
        }

        void DrawBoxPoint(Box3D box)
        {
            var min = box.GetModel().Min.ToVector3();
            var max = box.GetModel().Max.ToVector3();
            Gizmos.DrawSphere(min, 0.1f);
            Gizmos.DrawSphere(max, 0.1f);
        }

    }

}