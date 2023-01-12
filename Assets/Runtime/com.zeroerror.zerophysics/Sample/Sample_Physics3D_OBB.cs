using UnityEngine;
using FixMath.NET;
using ZeroPhysics.Physics3D;
using ZeroPhysics.Extensions;
using ZeroPhysics.Generic;

namespace ZeroPhysics.Sample
{

    public class Sample_Physics3D_OBB : MonoBehaviour
    {

        bool canRun = false;

        public Transform Boxes;
        Box3D[] boxes;
        Transform[] boxTfs;

        int[] collsionArray;

        PhysicsWorld3DCore physicsCore;

        void Start()
        {
            if (Boxes == null) return;
            canRun = true;
            physicsCore = new PhysicsWorld3DCore(new FPVector3(0, -10, 0));
            InitBox3Ds();
        }

        void FixedUpdate()
        {
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
            }
            Debug.Log($"Total Box: {bcCount}");
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
            var model = box.GetModel();
            var proj = Projection3DUtils.GetProjectionSub(model, axis3D);
            Gizmos.color = Color.white;
            Gizmos.color = Color.black;
            axis3D.dir.Normalize();
            Gizmos.DrawLine((axis3D.dir * proj.x + axis3D.origin).ToVector3(), (axis3D.dir * proj.y + axis3D.origin).ToVector3());
        }

        void UpdateBox(Transform src, Box3D box)
        {
            box.SetCenter(src.position.ToFPVector3());
            box.SetScale(src.localScale.ToFPVector3());
            box.SetRotation(src.rotation.ToFPQuaternion());
        }

    }

}