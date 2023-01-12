using ZeroPhysics.Physics3D;
using FixMath.NET;

namespace ZeroPhysics.Generic
{

    public class CollisionModel
    {

        public PhysicsBody3D bodyA;
        public PhysicsBody3D bodyB;

        CollisionType collisionType;
        public CollisionType CollisionType => collisionType;
        public void SetCollisionType(CollisionType v) => collisionType = v;

        FPVector3 beHitDirA;
        public FPVector3 BeHitDirA => beHitDirA;
        public void SetBeHitDirA(FPVector3 v) => beHitDirA = v;

        FP64 firctionCoe_combined;
        public FP64 FirctionCoe_combined => firctionCoe_combined;
        public void SetFirctionCoe_combined(FP64 v) => firctionCoe_combined = v;

        public CollisionModel()
        {
        }

    }

}