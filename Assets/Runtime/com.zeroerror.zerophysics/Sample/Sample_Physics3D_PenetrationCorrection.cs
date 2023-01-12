using UnityEngine;
using FixMath.NET;
using ZeroPhysics.Physics3D;
using ZeroPhysics.Generic;
using ZeroPhysics.Extensions;

namespace ZeroPhysics.Sample
{

    public class Sample_Physics3D_PenetrationCorrection : MonoBehaviour
    {

        public BoxType boxType;

        public Transform Boxes;
        Box3D[] boxes;
        Transform[] boxTfs;

        int[] collsionArray;
        bool canRun = false;

        PhysicsWorld3DCore physicsCore;

        void Start()
        {
            canRun = Boxes != null;
            if (!canRun) return;
            physicsCore = new PhysicsWorld3DCore(new FPVector3(0, -10, 0));
            InitBox3Ds();
        }

        public void OnDrawGizmos()
        {
            if (!canRun) return;
            if (boxTfs == null) return;
            if (boxes == null) return;

            // - Collision 
            for (int i = 0; i < collsionArray.Length; i++) { collsionArray[i] = 0; }
            for (int i = 0; i < boxes.Length - 1; i++)
            {
                for (int j = i + 1; j < boxes.Length; j++)
                {
                    if (Intersect3DUtils.HasCollision(boxes[i], boxes[j])) { collsionArray[i] = 1; collsionArray[j] = 1; }
                }
            }

            // - Projection
            Axis3D axis3D = new Axis3D();
            axis3D.origin = FPVector3.Zero;
            axis3D.dir = FPVector3.UnitX;
            Gizmos.DrawLine((axis3D.origin - 100 * axis3D.dir).ToVector3(), (axis3D.origin + 100 * axis3D.dir).ToVector3());

            // - Update And DrawBox
            for (int i = 0; i < boxes.Length; i++)
            {
                var bc = boxTfs[i];
                var box = boxes[i];
                UpdateBox(bc.transform, box);
                Gizmos.color = Color.green;
                box.DrawBoxPoint();
                if (collsionArray[i] == 1) Gizmos.color = Color.red;
                box.DrawBoxBorder();
                DrawProjectionSub(axis3D, box);
            }
        }

        void DrawProjectionSub(Axis3D axis3D, Box3D box)
        {
            var proj = Projection3DUtils.GetProjectionSub(box.GetModel(), axis3D);
            Gizmos.color = Color.white;
            Gizmos.color = Color.black;
            axis3D.dir.Normalize();
            Gizmos.DrawLine((axis3D.dir * proj.x + axis3D.origin).ToVector3(), (axis3D.dir * proj.y + axis3D.origin).ToVector3());
        }

        float mtv1_coefficient = 1;
        float mtv2_coefficient = 0;
        void OnGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label($"mtv1_coefficient:{mtv1_coefficient}");
            GUILayout.Label($"mtv2_coefficient:{mtv2_coefficient}");
            GUILayout.EndHorizontal();
            mtv1_coefficient = GUILayout.HorizontalSlider(mtv1_coefficient, 0, 1, GUILayout.Width(200));
            mtv2_coefficient = 1 - mtv1_coefficient;
            if (GUILayout.Button("穿透恢复"))
            {
                UpdateAllBoxes();
                CollisionUpdate();
                for (int i = 0; i < boxes.Length - 1; i++)
                {
                    var c1 = collsionArray[i];
                    var b1 = boxes[i];
                    var btf1 = boxTfs[i];
                    for (int j = i + 1; j < boxes.Length; j++)
                    {
                        var btf2 = boxTfs[j];
                        var c2 = collsionArray[j];
                        var b2 = boxes[j];
                        if (c1 == 1 && c2 == 1)
                        {
                            // - Penetration
                            Penetration3DUtils.PenetrationCorrection(b1, FP64.ToFP64(mtv1_coefficient), b2, FP64.ToFP64(mtv2_coefficient));
                            btf1.position = b1.Center.ToVector3();
                            btf2.position = b2.Center.ToVector3();
                        }
                    }
                }
            }
        }

        void InitBox3Ds()
        {
            var bcCount = Boxes.childCount;
            collsionArray = new int[bcCount];
            boxTfs = new Transform[bcCount];
            for (int i = 0; i < bcCount; i++)
            {
                var bc = Boxes.GetChild(i);
                boxTfs[i] = bc;
            }

            var setterAPI = physicsCore.SetterAPI;
            boxes = new Box3D[bcCount];
            for (int i = 0; i < bcCount; i++)
            {
                var bcTF = boxTfs[i].transform;
                boxes[i] = setterAPI.SpawnBox(bcTF.position.ToFPVector3(), bcTF.rotation.ToFPQuaternion(), bcTF.localScale.ToFPVector3(), Vector3.one.ToFPVector3());
                Debug.Log($"BoxType: {boxes[i].GetBoxType()}");
            }
            Debug.Log($"Total Box: {bcCount}");
        }

        void UpdateAllBoxes()
        {
            for (int i = 0; i < boxes.Length; i++)
            {
                var bc = boxTfs[i];
                var box = boxes[i];
                UpdateBox(bc.transform, box);
            }
        }

        void CollisionUpdate()
        {
            for (int i = 0; i < collsionArray.Length; i++) { collsionArray[i] = 0; }
            for (int i = 0; i < boxes.Length - 1; i++)
            {
                for (int j = i + 1; j < boxes.Length; j++)
                {
                    if (Intersect3DUtils.HasCollision(boxes[i], boxes[j]))
                    {
                        collsionArray[i] = 1;
                        collsionArray[j] = 1;
                    }
                }
            }
        }

        void UpdateBox(Transform src, Box3D box)
        {
            box.SetCenter(src.position.ToFPVector3());
            box.SetScale(src.localScale.ToFPVector3());
            box.SetRotation(src.rotation.ToFPQuaternion());
        }

    }

}