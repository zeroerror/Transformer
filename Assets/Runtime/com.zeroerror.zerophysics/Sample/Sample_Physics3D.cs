using UnityEngine;
using FixMath.NET;
using ZeroPhysics.Physics3D;
using ZeroPhysics.Extensions;
using ZeroPhysics.Generic;

namespace ZeroPhysics.Sample {

    public class Sample_Physics3D : MonoBehaviour {

        bool canRun = false;

        public Transform rbBoxRoot;
        Transform[] rbBoxTfs;

        public Transform boxRoot;
        Transform[] boxTfs;

        PhysicsWorld3DCore physicsCore;
        void Start() {
            if (rbBoxRoot == null) return;
            canRun = true;
            physicsCore = new PhysicsWorld3DCore(new FPVector3(0, -10, 0));
            InitBox3Ds();
        }

        void Update() {
            if (!canRun) return;
            if (rbBoxTfs == null) return;
            if (boxTfs == null) return;

            var getterAPI = physicsCore.GetterAPI;
            var rbBoxes = getterAPI.GetAllBoxRBs();
            var boxes = getterAPI.GetAllBoxes();
            for (int i = 0; i < rbBoxes.Count; i++) {
                var bc = rbBoxTfs[i];
                var rb = rbBoxes[i];
                var box = rb.Box;
                UpdateBox(bc.transform, box);
                rb.SetBounceCoefficient(FP64.ToFP64(bounce));
                box.SetFirctionCoe(FP64.ToFP64(firctionCoe_rbBox));
            }

            for (int i = 0; i < boxes.Count; i++) {
                var bc = boxTfs[i];
                var box = boxes[i];
                box.SetFirctionCoe(FP64.ToFP64(firctionCoe_box));
            }
        }

        void FixedUpdate() {
            physicsCore.Tick(FP64.ToFP64(UnityEngine.Time.fixedDeltaTime));
            // - Collsion Info
            var collisionInfos = physicsCore.GetterAPI.GetCollisionInfos();
            // Debug.Log($"碰撞事件数量: {collisionInfos.Length}");
            for (int i = 0; i < collisionInfos.Length; i++) {
                var collision = collisionInfos[i];
                var body_a = collision.bodyA;
                var body_b = collision.bodyB;
                var type_a = body_a.PhysicsType;
                var type_b = body_b.PhysicsType;
                if (type_a == PhysicsType3D.Box3D && type_b == PhysicsType3D.Box3DRigidbody) {
                    var box = body_a as Box3D;
                    var rb = body_a as Box3DRigidbody;
                    OnCollision(box, rb, collision);
                }
                if (type_b == PhysicsType3D.Box3D && type_a == PhysicsType3D.Box3DRigidbody) {
                    var rb = body_a as Box3DRigidbody;
                    var box = body_b as Box3D;
                    OnCollision(box, rb, collision);
                }
            }
        }

        #region [Collision]

        void OnCollision(Box3D box, Box3DRigidbody boxRB, CollisionModel collision) {
            if (collision.CollisionType == CollisionType.Enter) OnCollsionEnter(box, boxRB);
            else if (collision.CollisionType == CollisionType.Stay) OnCollsionStay(box, boxRB);
            else if (collision.CollisionType == CollisionType.Exit) OnCollsionExit(box, boxRB);
        }

        void OnCollsionEnter(Box3D box, Box3DRigidbody boxRB) {
            // UnityEngine.Debug.Log($" OnCollsionEnter : box:{box.name} boxRB:{boxRB.name} ");
        }

        void OnCollsionStay(Box3D box, Box3DRigidbody boxRB) {
            // UnityEngine.Debug.Log($" OnCollsionStay : box:{box.name} boxRB:{boxRB.name} ");

        }

        void OnCollsionExit(Box3D box, Box3DRigidbody boxRB) {
            // UnityEngine.Debug.Log($" OnCollsionExit : box:{box.name} boxRB:{boxRB.name} ");
        }

        #endregion

        public void OnDrawGizmos() {
            if (!canRun) return;
            if (rbBoxTfs == null) return;
            if (boxTfs == null) return;

            var getterAPI = physicsCore.GetterAPI;

            var rbs = getterAPI.GetAllBoxRBs();
            for (int i = 0; i < rbs.Count; i++) {
                var box = rbs[i].Box;
                Gizmos.color = Color.green;
                box.DrawBoxPoint();
                box.DrawBoxBorder();
            }

            var boxes = getterAPI.GetAllBoxes();
            for (int i = 0; i < boxes.Count; i++) {
                var box = boxes[i];
                Gizmos.color = Color.black;
                box.DrawBoxPoint();
                box.DrawBoxBorder();
            }
        }

        void InitBox3Ds() {
            var setterAPI = physicsCore.SetterAPI;

            var rbCount = rbBoxRoot.childCount;
            rbBoxTfs = new Transform[rbCount];
            for (int i = 0; i < rbCount; i++) {
                rbBoxTfs[i] = rbBoxRoot.GetChild(i);
            }
            for (int i = 0; i < rbCount; i++) {
                var tf = rbBoxTfs[i].transform;
                var rb = setterAPI.SpawnRBBox(tf.position.ToFPVector3(), tf.rotation.ToFPQuaternion(), tf.localScale.ToFPVector3(), Vector3.one.ToFPVector3());
                rb.name = $"RBBOX_{i}";
            }
            Debug.Log($"Total RBBox: {rbCount}");

            var boxCount = boxRoot.childCount;
            boxTfs = new Transform[boxCount];
            for (int i = 0; i < boxCount; i++) {
                boxTfs[i] = boxRoot.GetChild(i);
            }
            for (int i = 0; i < boxCount; i++) {
                var tf = boxTfs[i].transform;
                var box = setterAPI.SpawnBox(tf.position.ToFPVector3(), tf.rotation.ToFPQuaternion(), tf.localScale.ToFPVector3(), Vector3.one.ToFPVector3());
                box.name = $"Box_{i}";
            }
            Debug.Log($"Total Box: {boxCount}");
        }

        void UpdateBox(Transform src, Box3D box) {
            box.SetScale(src.localScale.ToFPVector3());
            box.SetRotation(src.rotation.ToFPQuaternion());
            src.position = box.Center.ToVector3();
        }

        public float bounce = 0f;
        public float firctionCoe_box = 5f;
        public float firctionCoe_rbBox = 1f;
        void OnGUI() {
            GUILayout.BeginHorizontal();
            GUILayout.Label($"弹性系数:{bounce}", GUILayout.Width(100));
            bounce = GUILayout.HorizontalSlider(bounce, 0, 1, GUILayout.Width(200));
            GUILayout.EndHorizontal();

            // GUILayout.BeginHorizontal();
            // GUILayout.Label($"摩擦系数(静态Box):{firctionCoe_box}", GUILayout.Width(200));
            // firctionCoe_box = GUILayout.HorizontalSlider(firctionCoe_box, 0, 5, GUILayout.Width(200));
            // GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"摩擦系数(RBBOX):{firctionCoe_rbBox}", GUILayout.Width(200));
            firctionCoe_rbBox = GUILayout.HorizontalSlider(firctionCoe_rbBox, 0, 5, GUILayout.Width(200));
            GUILayout.EndHorizontal();
        }

    }

}