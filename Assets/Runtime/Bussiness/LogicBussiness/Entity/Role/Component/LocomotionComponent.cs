using FixMath.NET;
using UnityEngine;
using ZeroPhysics.Physics3D;

namespace Transformer.Bussiness.LogicBussiness
{

    public class LocomotionComponent
    {

        FP64 moveSpeed;
        public FP64 MoveSpeed => moveSpeed;
        public void SetMoveSpeed(FP64 v) => moveSpeed = v;

        FP64 jumpSpeed;
        public FP64 JumpSpeed => jumpSpeed;
        public void SetJumpSpeed(FP64 v) => jumpSpeed = v;

        Box3DRigidbody boxRB;
        public Box3DRigidbody BoxRB => boxRB;

        public LocomotionComponent() { }

        public void Inject(Box3DRigidbody boxRB)
        {
            this.boxRB = boxRB;
        }

        public void Move(FPVector3 addV)
        {
            var v = boxRB.LinearV;
            v.x = addV.x;
            v.z = addV.z;
            boxRB.SetLinearV(v);
        }

        public void Jump()
        {
            var v = boxRB.LinearV;
            v.y += jumpSpeed;
            boxRB.SetLinearV(v);
        }

    }

}