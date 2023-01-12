using FixMath.NET;
using ZeroPhysics.Physics3D.Facade;

namespace ZeroPhysics.Physics3D {

    public class PenetrationPhase {

        Physics3DFacade physicsFacade;

        public PenetrationPhase() { }

        public void Inject(Physics3DFacade physicsFacade) {
            this.physicsFacade = physicsFacade;
        }

        public void Tick(in FP64 time) {
            var idService = physicsFacade.Service.IDService;
            var collisionService = physicsFacade.Service.CollisionService;
            var boxRBs = physicsFacade.boxRBs;
            var boxRBIDInfos = idService.boxRBIDInfos;
            var boxes = physicsFacade.boxes;
            var boxInfos = idService.boxIDInfos;

            // - RB & RB
            // for (int i = 0; i < boxRBs.Length - 1; i++)
            // {
            //     if (!boxRBIDInfos[i]) continue;

            //     var rb1 = boxRBs[i];
            //     var boxRB1 = rb1.Box;
            //     var oldBeHitDir1 = rb1.BeHitDir;
            //     var newBeHitDir1 = oldBeHitDir1;
            //     boxRB1.SetFirctionCoe_combined(FP64.Zero);

            //     for (int j = i + 1; j < boxRBs.Length; j++)
            //     {
            //         if (!boxRBIDInfos[j]) continue;

            //         var rb2 = boxRBs[j];
            //         var boxRB2 = rb2.Box;
            //         var oldBeHitDir2 = rb2.BeHitDir;
            //         var newBeHitDir2 = oldBeHitDir2;
            //         boxRB2.SetFirctionCoe_combined(FP64.Zero);

            //         if (!Intersect3DUtils.HasCollision(rb1.Box, rb2.Box)) continue;

            //         var mtv = Penetration3DUtils.PenetrationCorrection(rb1.Box, FP64.Half, rb2.Box, FP64.Half);
            //         var beHitDir = mtv.normalized;
            //         var firctionCoe1 = boxRB1.FrictionCoe;
            //         var firctionCoe2 = boxRB2.FrictionCoe;
            //         var firctionCoe_combined = firctionCoe1 < firctionCoe2 ? firctionCoe1 : firctionCoe2;

            //         var v1 = Penetration3DUtils.GetBouncedV(rb1.LinearV, beHitDir, rb1.BounceCoefficient);
            //         rb1.SetLinearV(v1);
            //         boxRB1.SetFirctionCoe_combined(firctionCoe_combined);

            //         var v2 = Penetration3DUtils.GetBouncedV(rb2.LinearV, -beHitDir, rb2.BounceCoefficient);
            //         rb2.SetLinearV(v2);
            //         rb2.SetBeHitDir(-beHitDir);
            //         boxRB2.SetFirctionCoe_combined(firctionCoe_combined);
            //     }

            //     rb1.SetBeHitDir(newBeHitDir1);
            // }

            // - RB & SB
            for (int i = 0; i < boxRBs.Length; i++) {
                if (!boxRBIDInfos[i]) continue;

                var rb = boxRBs[i];
                var rbBox = rb.Box;
                if (!collisionService.HasCollision(rb)) continue;

                for (int j = 0; j < boxes.Length; j++) {
                    if (!boxInfos[j]) continue;

                    var box = boxes[j];
                    if (!collisionService.TryGetCollision(rb, box, out var collision)) continue;
                    if (collision.CollisionType == Generic.CollisionType.Exit) continue;

                    var mtv = Penetration3DUtils.PenetrationCorrection(rbBox, 1, box, 0);
                    var beHitDir = mtv.normalized;
                    collisionService.UpdateBeHitDir(rb, box, beHitDir);

                    var firctionCoe1 = rbBox.FrictionCoe;
                    var firctionCoe2 = box.FrictionCoe;
                    var firctionCoe_combined = firctionCoe1 < firctionCoe2 ? firctionCoe1 : firctionCoe2;
                    collision.SetFirctionCoe_combined(firctionCoe_combined);
                }

            }
        }

    }

}