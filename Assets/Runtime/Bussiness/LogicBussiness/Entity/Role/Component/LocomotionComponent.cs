using FixMath.NET;
using UnityEngine;

namespace Transformer.LogicBussiness
{

    public class LocomotionComponent
    {

        FP64 moveSpeed;
        public FP64 MoveSpeed => moveSpeed;
        public void SetMoveSpeed(FP64 v) => moveSpeed = v;

        FP64 jumpSpeed;
        public FP64 JumpSpeed => jumpSpeed;
        public void SetJumpSpeed(FP64 v) => jumpSpeed = v;

        Rigidbody rb;   // temp

        public LocomotionComponent() { }

        public void Inject(Rigidbody rb)
        {
            this.rb = rb;
        }

        public void Move(FPVector3 addV)
        {
            var v = rb.velocity;
            v.x = addV.x.AsFloat();
            v.z = addV.z.AsFloat();
            rb.velocity = v;
        }

    }

}