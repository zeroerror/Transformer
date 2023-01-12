using System.Collections.Generic;
using System.Linq;
using FixMath.NET;
using ZeroPhysics.Generic;
using ZeroPhysics.Physics3D;

namespace ZeroPhysics.Service {

    public class CollisionService {

        Dictionary<ulong, CollisionModel> collisionDic;

        public CollisionService() {
            collisionDic = new Dictionary<ulong, CollisionModel>();
        }

        public void AddCollision(PhysicsBody3D a, PhysicsBody3D b) {
            var ida = CombinePhysicsBodyKey(a);
            var idb = CombinePhysicsBodyKey(b);
            var dicKey = CombineDicKey(ida, idb);
            bool getFromDic = collisionDic.TryGetValue(dicKey, out var info);
            if (getFromDic && info.CollisionType == CollisionType.Enter) {
                info.SetCollisionType(CollisionType.Stay);
            }
            if (!getFromDic) {
                // 添加Enter
                info = new CollisionModel();
                info.bodyA = a;
                info.bodyB = b;
                collisionDic.Add(dicKey, info);
            }
            if (!getFromDic || info.CollisionType == CollisionType.None || info.CollisionType == CollisionType.Exit) {
                info.SetCollisionType(CollisionType.Enter);
            }

            // // UnityEngine.Debug.Log($"Collision {info.CollisionType.ToString()} ------------  A: {a} &&&&&&&& {b}");
        }

        public void UpdateBeHitDir(PhysicsBody3D a, PhysicsBody3D b, in FPVector3 beHitDir) {
            var ida = CombinePhysicsBodyKey(a);
            var idb = CombinePhysicsBodyKey(b);
            var dicKey = CombineDicKey(ida, idb);
            if (!collisionDic.TryGetValue(dicKey, out var collision)) {
                return;
            }

            if (beHitDir == FPVector3.Zero) {
                return;
            }

            var hasSwap = SwapBiggerToLeft(ref ida, ref idb);
            collision.SetBeHitDirA((FPVector3)(hasSwap ? -beHitDir : beHitDir));
        }

        public void RemoveCollision(PhysicsBody3D a, PhysicsBody3D b) {
            var ida = CombinePhysicsBodyKey(a);
            var idb = CombinePhysicsBodyKey(b);
            var dicKey = CombineDicKey(ida, idb);
            if (!collisionDic.TryGetValue(dicKey, out var collision)) {
                return;
            }

            if (collision.CollisionType == CollisionType.Enter || collision.CollisionType == CollisionType.Stay) {
                collision.SetCollisionType(CollisionType.Exit);
                // UnityEngine.Debug.Log($"Collision Exit ------------  A: {a} &&&&&&&& {b}");
            } else if (collision.CollisionType == CollisionType.Exit) {
                collisionDic.Remove(dicKey);
                // UnityEngine.Debug.Log($"Collision Dic Remove  ------------  A: {a} &&&&&&&& {b}");
            }

        }

        public CollisionModel[] GetAllCollisions() {
            var values = collisionDic.Values;
            int count = values.Count;
            CollisionModel[] infoArray = new CollisionModel[count];
            values.CopyTo(infoArray, 0);
            return infoArray;
        }

        public bool HasCollision(PhysicsBody3D a) {
            var id = CombinePhysicsBodyKey(a);
            foreach (var key in collisionDic.Keys) {
                var id1 = (uint)key;
                var id2 = (uint)(key >> 32);
                if (id == id1 || id == id2) {
                    var col = collisionDic[key];
                    if (col.CollisionType != CollisionType.None) {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool TryGetCollision(PhysicsBody3D a, out CollisionModel collision) {
            collision = null;
            var id = CombinePhysicsBodyKey(a);
            foreach (var key in collisionDic.Keys) {
                var id1 = (uint)key;
                var id2 = (uint)(key >> 32);
                if (id == id1 || id == id2) {
                    var col = collisionDic[key];
                    if (col.CollisionType != CollisionType.None) {
                        collision = col;
                        return true;
                    }
                }
            }

            return false;
        }

        public bool TryGetCollision(PhysicsBody3D a, PhysicsBody3D b, out CollisionModel collision) {
            collision = null;
            var ida = CombinePhysicsBodyKey(a);
            var idb = CombinePhysicsBodyKey(b);
            var dicKey = CombineDicKey(ida, idb);
            return collisionDic.TryGetValue(dicKey, out collision) && collision.CollisionType != CollisionType.None;
        }

        ulong CombineDicKey(uint ida, uint idb) {
            SwapBiggerToLeft(ref ida, ref idb);
            ulong key = (ulong)(idb);
            key |= (ulong)ida << 32;
            return key;
        }

        uint CombinePhysicsBodyKey(PhysicsBody3D a) {
            byte t = (byte)a.PhysicsType;
            ushort id = a.ID;
            uint key = (uint)id;
            key |= (uint)t << 16;
            return key;
        }

        bool SwapBiggerToLeft(ref uint ida, ref uint idb) {
            if (idb < ida) {
                return false;
            }

            ida = ida ^ idb;
            idb = ida ^ idb;
            ida = ida ^ idb;
            return true;
        }

    }

}