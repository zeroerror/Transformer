using Transformer.LogicBussiness.Facade;
using Transformer.LogicBussiness.Generic;

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

        public void SpawnRole(int typeID, ControlType controlType, UnityEngine.Rigidbody rb)
        {
            var factory = facade.Factory;
            var idService = facade.IDService;
            var roleRepo = facade.Repo.RoleRepo;

            // - Spawn
            var role = factory.SpawnRole(typeID);

            // - Set
            var idc = role.IDComponent;
            var lc = role.LocomotionComponent;
            idc.SetID(idService.GetRoleID());
            idc.SetControlType(controlType);
            lc.Inject(rb);

            // - Add
            roleRepo.Add(role);
        }

    }

}