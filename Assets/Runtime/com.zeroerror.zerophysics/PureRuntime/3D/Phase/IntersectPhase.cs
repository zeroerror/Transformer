using FixMath.NET;
using ZeroPhysics.Physics3D.Facade;

namespace ZeroPhysics.Physics3D {

    public class IntersectPhase {

        Physics3DFacade physicsFacade;

        public IntersectPhase() { }

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

            // - RB & SB
            for (int i = 0; i < boxRBs.Length; i++) {
                if (!boxRBIDInfos[i]) continue;

                var rb = boxRBs[i];
                var rbBox = rb.Box;
                for (int j = 0; j < boxes.Length; j++) {
                    if (!boxInfos[j]) {
                        continue;
                    }

                    var box = boxes[j];
                    if (!Intersect3DUtils.HasCollision(rbBox, box)) {
                        collisionService.RemoveCollision(rb, box);
                        continue;
                    }

                    collisionService.AddCollision(rb, box);
                }

            }
        }

    }

}