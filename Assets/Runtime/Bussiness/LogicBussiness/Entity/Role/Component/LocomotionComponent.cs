using FixMath.NET;
using ZeroPhysics.Physics3D;

namespace Transformer.Bussiness.LogicBussiness {

    public class LocomotionComponent {

        FP64 moveMaxSpeed;
        public FP64 MoveMaxSpeed => moveMaxSpeed;
        public void SetMoveMaxSpeed(in FP64 v) => moveMaxSpeed = v;

        FP64 moveAccelerate;
        public void SetMoveAccelerate(in FP64 v) => moveAccelerate = v;

        FP64 jumpSpeed;
        public FP64 JumpSpeed => jumpSpeed;
        public void SetJumpSpeed(in FP64 v) => jumpSpeed = v;

        Box3DRigidbody boxRB;
        public Box3DRigidbody BoxRB => boxRB;

        FPVector3 moveV;

        public LocomotionComponent() { }

        public void Inject(Box3DRigidbody boxRB) {
            this.boxRB = boxRB;
        }

        public void Move(in FPVector3 dir) {
            if (dir == FPVector3.Zero) {
                return;
            }

            var v = boxRB.LinearV;
            boxRB.SetLinearV(v + moveAccelerate * dir);
        }

        public void Jump() {
            var v = boxRB.LinearV;
            v.y += jumpSpeed;
            boxRB.SetLinearV(v);
        }

    }

}