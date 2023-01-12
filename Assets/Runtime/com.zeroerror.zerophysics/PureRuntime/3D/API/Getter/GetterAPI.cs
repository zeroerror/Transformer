using System;
using System.Collections.Generic;
using ZeroPhysics.Generic;
using ZeroPhysics.Physics3D.Facade;

namespace ZeroPhysics.Physics3D.API
{

    public class GetterAPI : IGetterAPI
    {

        Physics3DFacade physicsFacade;

        public GetterAPI() { }

        public void Inject(Physics3DFacade physicsFacade)
        {
            this.physicsFacade = physicsFacade;
        }

        List<Box3D> IGetterAPI.GetAllBoxes()
        {
            var domain = physicsFacade.Domain.DataDomain;
            return domain.GetAllBoxes();
        }

        List<Box3DRigidbody> IGetterAPI.GetAllBoxRBs()
        {
            var domain = physicsFacade.Domain.DataDomain;
            return domain.GetAllRBBoxes();
        }

        CollisionModel[] IGetterAPI.GetCollisionInfos()
        {
            var collisionService = physicsFacade.Service.CollisionService;
            return collisionService.GetAllCollisions();
        }
    }

}