using UnityEngine;
using FixMath.NET;
using ZeroPhysics.Physics2D;
using ZeroPhysics.Generic;
using ZeroPhysics.Extensions;

namespace ZeroPhysics.Sample
{

    public class Sample_Physics2D_SphereAndBox : MonoBehaviour
    {

        bool isRun = false;

        Box2D[] allBoxes;
        Sphere2D[] allSpheres;
        Transform[] tfs;
        public Transform spheresAndBoxes;

        public BoxType boxType;

        int[] collsionArray;

        public void Start()
        {
            if (spheresAndBoxes == null) return;
            isRun = true;

            var bcCount = spheresAndBoxes.childCount;
            collsionArray = new int[bcCount];
            tfs = new Transform[bcCount];
            for (int i = 0; i < bcCount; i++)
            {
                var bc = spheresAndBoxes.GetChild(i);
                tfs[i] = bc;
            }

            allBoxes = new Box2D[bcCount];
            allSpheres = new Sphere2D[bcCount];
            int boxCount = 0;
            int sphereCount = 0;
            for (int i = 0; i < bcCount; i++)
            {
                var bcTF = tfs[i].transform;
                if (bcTF.GetComponent<BoxCollider>())
                {
                    allBoxes[i] = new Box2D(bcTF.position.ToFPVector2(), 1, 1, FP64.ToFP64(bcTF.rotation.eulerAngles.z), bcTF.localScale.ToFPVector2());
                    allBoxes[i].SetBoxType(boxType);
                    boxCount++;
                }
                else if (bcTF.GetComponent<SphereCollider>())
                {
                    allSpheres[i] = new Sphere2D(bcTF.position.ToFPVector2(), FP64.ToFP64(bcTF.GetComponent<SphereCollider>().radius), FP64.ToFP64(bcTF.rotation.eulerAngles.z), FP64.ToFP64(bcTF.localScale.x));
                    sphereCount++;
                }
            }
            Debug.Log($"Total Box: {boxCount}");
            Debug.Log($"Total Sphere: {sphereCount}");
        }

        public void OnDrawGizmos()
        {
            if (!isRun) return;
            if (tfs == null) return;
            if (allBoxes == null) return;

            for (int i = 0; i < collsionArray.Length; i++)
            {
                collsionArray[i] = 0;
            }

            for (int i = 0; i < allBoxes.Length - 1; i++)
            {
                var box1 = allBoxes[i];
                if (box1 == null) continue;
                for (int j = i + 1; j < allBoxes.Length; j++)
                {
                    var box2 = allBoxes[j];
                    if (box2 == null) continue;
                    if (CollisionHelper2D.HasCollision(box1, box2))
                    {
                        collsionArray[i] = 1;
                        collsionArray[j] = 1;
                    }
                }
            }

            for (int i = 0; i < allSpheres.Length - 1; i++)
            {
                var sphere1 = allSpheres[i];
                if (sphere1 == null) continue;
                for (int j = i + 1; j < allSpheres.Length; j++)
                {
                    var sphere2 = allSpheres[j];
                    if (sphere2 == null) continue;
                    if (CollisionHelper2D.HasCollision(sphere1, sphere2))
                    {
                        collsionArray[i] = 1;
                        collsionArray[j] = 1;
                    }
                }
            }

            for (int i = 0; i < allSpheres.Length; i++)
            {
                var sphere = allSpheres[i];
                if (sphere == null) continue;
                for (int j = 0; j < allBoxes.Length; j++)
                {
                    var box = allBoxes[j];
                    if (box == null) continue;
                    if (CollisionHelper2D.HasCollision(sphere, box))
                    {
                        collsionArray[i] = 1;
                        collsionArray[j] = 1;
                    }
                }
            }

            for (int i = 0; i < allBoxes.Length; i++)
            {
                var bc = tfs[i];
                var box = allBoxes[i];
                if (box == null) continue;
                UpdateBox(bc.transform, box);
                Gizmos.color = Color.green;
                DrawBoxPoint(box);
                if (collsionArray[i] == 1) { Gizmos.color = Color.red; DrawBoxBorder(box); }
            }

            for (int i = 0; i < allSpheres.Length; i++)
            {
                var bc = tfs[i];
                var sphere = allSpheres[i];
                if (sphere == null) continue;
                UpdateSphere(bc.transform, sphere);
                Gizmos.color = Color.green;
                var b = sphere.Box;
                if (collsionArray[i] == 1)
                {
                    Gizmos.color = Color.red;
                    DrawSphereBorder(sphere);
                }
            }

        }

        void DrawProjectionSub(Axis2D axis2D, Box2D box)
        {
            var proj = box.GetProjectionSub(axis2D);
            Gizmos.color = Color.white;
            Gizmos.color = Color.black;
            Gizmos.DrawLine((axis2D.dir * proj.x + axis2D.center).ToVector2(), (axis2D.dir * proj.y + axis2D.center).ToVector2());
        }

        void UpdateBox(Transform src, Box2D box)
        {
            box.UpdateCenter(src.position.ToFPVector2());
            box.UpdateScale(src.localScale.ToFPVector2());
            box.UpdateRotAngle(FP64.ToFP64(src.rotation.eulerAngles.z));
        }

        void UpdateSphere(Transform src, Sphere2D sphere)
        {
            sphere.UpdateCenter(src.position.ToFPVector2());
            sphere.UpdateScale(FP64.ToFP64(src.localScale.x));
        }

        void DrawBoxPoint(Box2D box)
        {
            var a = box.A.ToVector2();
            var b = box.B.ToVector2();
            var c = box.C.ToVector2();
            var d = box.D.ToVector2();
            Gizmos.color = Color.red;
            float size = 0.08f;
            Gizmos.DrawSphere(a, size);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(b, size);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(c, size);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(d, size);
            Gizmos.color = Color.red;
        }

        void DrawBoxBorder(Box2D box)
        {
            var a = box.A.ToVector2();
            var b = box.B.ToVector2();
            var c = box.C.ToVector2();
            var d = box.D.ToVector2();
            Gizmos.DrawLine(a, b);
            Gizmos.DrawLine(b, c);
            Gizmos.DrawLine(c, d);
            Gizmos.DrawLine(d, a);
        }

        void DrawSphereBorder(Sphere2D sphere)
        {
            Gizmos.DrawSphere(sphere.Center.ToVector2(), sphere.Radius.AsFloat());
        }

    }

}