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
                moveV = FPVector3.Zero;
                return;
            }

            moveV += moveAccelerate * dir;
            moveV = moveV.Length() > moveMaxSpeed ? moveMaxSpeed * dir : moveV;
            var v = boxRB.LinearV;
            v.x = moveV.x;
            v.z = moveV.z;
            boxRB.SetLinearV(v);
        }

        public void Jump() {
            var v = boxRB.LinearV;
            v.y += jumpSpeed;
            boxRB.SetLinearV(v);
        }

    }

}