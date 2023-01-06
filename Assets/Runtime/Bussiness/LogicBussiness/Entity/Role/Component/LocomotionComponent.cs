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

        Rigidbody3D_Box rbBox;
        public Rigidbody3D_Box RbBox => rbBox;

        public LocomotionComponent() { }

        public void Inject(Rigidbody3D_Box rbBox)
        {
            this.rbBox = rbBox;
        }

        public void Move(FPVector3 addV)
        {
            var v = rbBox.LinearV;
            v.x = addV.x;
            v.z = addV.z;
            rbBox.SetLinearV(v);
        }

        public void Jump()
        {
            var v = rbBox.LinearV;
            v.y += jumpSpeed;
            rbBox.SetLinearV(v);
        }

    }

}