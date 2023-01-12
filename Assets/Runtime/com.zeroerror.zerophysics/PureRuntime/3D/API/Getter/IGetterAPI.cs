using System;
using System.Collections.Generic;
using ZeroPhysics.Generic;

namespace ZeroPhysics.Physics3D.API
{

    public interface IGetterAPI
    {

        List<Box3DRigidbody> GetAllBoxRBs();
        List<Box3D> GetAllBoxes();
        CollisionModel[] GetCollisionInfos();

    }

}