using Transformer.LogicBussiness.Facade;
using Transformer.LogicBussiness.Generic;
using FixMath.NET;

namespace Transformer.LogicBussiness.Domain
{

    public class RoleDomain
    {

        LogicFacade facade;

        public RoleDomain()
        {

        }

        public void Inject(LogicFacade facade)
        {
            this.facade = facade;
        }

        public void SpawnRole(int typeID, ControlType controlType, in FPVector3 spawnPos)
        {
            var factory = facade.Factory;
            var idService = facade.IDService;
            var roleRepo = facade.Repo.RoleRepo;

            // - Spawn Entity
            var role = factory.SpawnRole(typeID, spawnPos);

            // - Set Entity
            var idc = role.IDComponent;
            var lc = role.LocomotionComponent;
            idc.SetID(idService.GetRoleID());
            idc.SetControlType(controlType);

            // - Add
            roleRepo.Add(role);
        }

    }

}