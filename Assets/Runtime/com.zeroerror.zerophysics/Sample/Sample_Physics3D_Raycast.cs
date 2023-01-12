using System.Collections.Generic;
using UnityEngine;
using FixMath.NET;
using ZeroPhysics.Physics3D;
using ZeroPhysics.Generic;
using ZeroPhysics.Extensions;

namespace ZeroPhysics.Sample
{

    public class Sample_Physics3D_Raycast : MonoBehaviour
    {

        public Transform rayStart;
        public Transform rayEnd;

        public Transform boxRoot;
        public BoxType boxType;
        Transform[] box_tfs;
        Box3D[] boxes;

        public Transform sphereRoot;
        Transform[] sphere_tfs;
        Sphere3D[] spheres;

        Ray3D ray;

        PhysicsWorld3DCore physicsCore;

        void Start()
        {
            if (boxRoot == null) return;
            if (sphereRoot == null) return;
            isRun = true;
            physicsCore = new PhysicsWorld3DCore(new FPVector3(0, 10, 0));

            var count = boxRoot.childCount;
            box_tfs = new Transform[count];
            for (int i = 0; i < count; i++)
            {
                box_tfs[i] = boxRoot.GetChild(i);
            }

            var setterAPI = physicsCore.SetterAPI;
            boxes = new Box3D[count];
            for (int i = 0; i < count; i++)
            {
                var tf = box_tfs[i].transform;
                var pos = tf.position.ToFPVector3();
                var rotation = tf.rotation.ToFPQuaternion();
                var localScale = tf.localScale.ToFPVector3();
                boxes[i] = setterAPI.SpawnBox(pos, rotation, localScale, Vector3.one.ToFPVector3());
            }
            Debug.Log($"Total Box: {count}");

            count = sphereRoot.childCount;
            sphere_tfs = new Transform[count];
            for (int i = 0; i < count; i++)
            {
                sphere_tfs[i] = sphereRoot.GetChild(i);
            }
            spheres = new Sphere3D[count];
            for (int i = 0; i < count; i++)
            {
                var tf = sphere_tfs[i].transform;
                var pos = tf.position.ToFPVector3();
                var rotation = tf.rotation.ToFPQuaternion();
                var localScale = tf.localScale.ToFPVector3();
                var size = Vector3.one.ToFPVector3();
                spheres[i] = setterAPI.SpawnSphere(pos, rotation, localScale, size);
            }
            Debug.Log($"Total Sphere: {count}");

            var rayStartPos = rayStart.position;
            var rayEndPos = rayEnd.position;
            var posDiff = (rayEndPos - rayStartPos);
            ray = new Ray3D(rayStartPos.ToFPVector3(), posDiff.normalized.ToFPVector3(), FP64.ToFP64(posDiff.magnitude));
        }

        bool isRun = false;
        bool[] collisionList_box = new bool[100];
        bool[] collisionList_sphere = new bool[100];
        List<Vector3> hitPointList = new List<Vector3>();

        public void OnDrawGizmos()
        {
            if (!isRun) return;
            for (int i = 0; i < collisionList_box.Length; i++) { collisionList_box[i] = false; }
            for (int i = 0; i < collisionList_sphere.Length; i++) { collisionList_sphere[i] = false; }
            hitPointList.Clear();

            var rayStartPos = rayStart.position;
            var rayEndPos = rayEnd.position;
            ray.origin = rayStartPos.ToFPVector3();
            var posDiff = (rayEndPos - rayStartPos);
            ray.dir = posDiff.normalized.ToFPVector3();
            ray = new Ray3D(rayStartPos.ToFPVector3(), posDiff.normalized.ToFPVector3(), FP64.ToFP64(posDiff.magnitude));

            bool hasCollision = false;

            // - Box
            for (int i = 0; i < boxes.Length; i++)
            {
                var b = boxes[i];
                UpdateBox(box_tfs[i], b);
                if (Raycast3DUtils.RayBoxWithPoints(ray, b.GetModel(), out var p1, out var p2))
                {
                    collisionList_box[i] = true;
                    if (p1 != FPVector3.Zero) hitPointList.Add(p1.ToVector3());
                    if (p2 != FPVector3.Zero) hitPointList.Add(p2.ToVector3());
                    hasCollision = true;
                }
            }
            for (int i = 0; i < boxes.Length; i++)
            {
                Gizmos.color = Color.green;
                if (collisionList_box[i]) Gizmos.color = Color.red;
                boxes[i].DrawBoxBorder();
            }

            // - Sphere
            for (int i = 0; i < spheres.Length; i++)
            {
                var s = spheres[i];
                UpdateSphere3D(sphere_tfs[i], s);
                if (Raycast3DUtils.RayWithSphere(ray, s, out var hps))
                {
                    collisionList_sphere[i] = true;
                    hps.ForEach((p) =>
                    {
                        hitPointList.Add(p.ToVector3());
                    });
                    hasCollision = true;
                }
            }

            Gizmos.color = Color.red;
            for (int i = 0; i < spheres.Length; i++)
            {
                if (collisionList_sphere[i])
                {
                    DrawSphere3D(spheres[i]);
                }
            }

            // - Ray
            Gizmos.color = Color.green;
            if (hasCollision) Gizmos.color = Color.red;
            Gizmos.DrawLine(ray.origin.ToVector3(), ray.origin.ToVector3() + (ray.dir * ray.length).ToVector3());

            // - Hit Points
            Gizmos.color = Color.white;
            hitPointList.ForEach((p) =>
            {
                Gizmos.DrawSphere(p, 0.08f);
            });
        }
        void UpdateBox(Transform src, Box3D box)
        {
            box.SetCenter(src.position.ToFPVector3());
            box.SetScale(src.localScale.ToFPVector3());
            box.SetRotation(src.rotation.ToFPQuaternion());
        }

        void DrawSphere3D(Sphere3D sphere)
        {
            Gizmos.DrawSphere(sphere.Center.ToVector3(), sphere.Radius_scaled.AsFloat());
        }

        void UpdateSphere3D(Transform src, Sphere3D sphere)
        {
            sphere.SetCenter(src.position.ToFPVector3());
            sphere.SetScale(src.localScale.ToFPVector3());
        }

    }

}