using System.Collections.Generic;
using UnityEngine;
using FixMath.NET;
using ZeroPhysics.Extensions;
using ZeroPhysics.Generic;
using ZeroPhysics.Physics2D;

namespace ZeroPhysics.Sample
{

    public class Sample_Phsics2D_OBB : MonoBehaviour
    {

        bool isRun = false;

        Box2D[] boxes;
        BoxCollider[] bcs;
        public Transform Boxes;

        public void Start()
        {
            if (Boxes == null) return;
            isRun = true;

            var bcCount = Boxes.childCount;
            bcs = new BoxCollider[bcCount];
            for (int i = 0; i < bcCount; i++)
            {
                var bc = Boxes.GetChild(i);
                bcs[i] = bc.GetComponent<BoxCollider>();
            }

            boxes = new Box2D[bcCount];
            for (int i = 0; i < bcCount; i++)
            {
                var bcTF = bcs[i].transform;
                boxes[i] = new Box2D(bcTF.position.ToFPVector2(), 1, 1, FP64.ToFP64(bcTF.rotation.z), bcTF.localScale.ToFPVector2());
                boxes[i].SetBoxType(BoxType.OBB);
            }
        }

        public void OnDrawGizmos()
        {
            if (!isRun) return;
            if (bcs == null) return;
            if (boxes == null) return;

            Dictionary<int, Box2D> collisionBoxDic = new Dictionary<int, Box2D>();
            for (int i = 0; i < boxes.Length - 1; i++)
            {
                for (int j = i + 1; j < boxes.Length; j++)
                {
                    if (CollisionHelper2D.HasCollision(boxes[i], boxes[j]))
                    {
                        if (!collisionBoxDic.ContainsKey(i)) collisionBoxDic[i] = boxes[i];
                        if (!collisionBoxDic.ContainsKey(j)) collisionBoxDic[j] = boxes[j];
                    }
                }
            }

            Gizmos.DrawLine(Vector3.zero + Vector3.up * 10f, Vector3.zero + Vector3.down * 10f);
            Gizmos.DrawLine(Vector3.zero + Vector3.left * 10f, Vector3.zero + Vector3.right * 10f);

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
                DrawBoxBorder(box);
            }

        }

        void DrawBoxBorder(Box2D box)
        {
            Gizmos.DrawLine(box.A.ToVector2(), (box.B.ToVector2()));
            Gizmos.DrawLine(box.B.ToVector2(), (box.C.ToVector2()));
            Gizmos.DrawLine(box.C.ToVector2(), (box.D.ToVector2()));
            Gizmos.DrawLine(box.D.ToVector2(), (box.A.ToVector2()));
        }

        void DrawBoxPoint(Box2D box)
        {
            var a = box.A;
            var b = box.B;
            var c = box.C;
            var d = box.D;
            Gizmos.DrawSphere(a.ToVector2(), 0.1f);
            Gizmos.DrawSphere(b.ToVector2(), 0.1f);
            Gizmos.DrawSphere(c.ToVector2(), 0.1f);
            Gizmos.DrawSphere(d.ToVector2(), 0.1f);
        }

        void UpdateBox(Transform src, Box2D box)
        {
            box.UpdateCenter(src.position.ToFPVector2());
            box.UpdateScale(src.localScale.ToFPVector2());
            box.UpdateRotAngle(FP64.ToFP64(src.rotation.eulerAngles.z));
        }
    }

}