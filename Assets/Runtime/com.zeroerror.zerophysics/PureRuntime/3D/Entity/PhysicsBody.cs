using ZeroPhysics.Generic;

namespace ZeroPhysics.Physics3D
{

    public interface PhysicsBody3D
    {

        PhysicsType3D PhysicsType { get; }
        ushort ID { get; }

    }

}