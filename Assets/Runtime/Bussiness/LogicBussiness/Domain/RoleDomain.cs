using Transformer.Bussiness.LogicBussiness.Generic;
using FixMath.NET;

namespace Transformer.Bussiness.LogicBussiness.Domain {

    public class RoleDomain {

        Facade.LogicFacade facade;

        public RoleDomain() {

        }

        public void Inject(Facade.LogicFacade facade) {
            this.facade = facade;
        }

        public RoleEntity SpawnRole(int typeID, ControlType controlType, in FPVector3 spawnPos) {
            var factory = facade.Factory;
            var idService = facade.IDService;
            var roleRepo = facade.Repo.RoleRepo;

            // - Spawn Entity
            var role = factory.SpawnRole(typeID, spawnPos);

            // - Set Entity
            var idc = role.IDComponent;
            idc.SetEntityType(EntityType.Role);
            idc.SetTypeID(typeID);
            idc.SetID(idService.GetRoleID());
            idc.SetControlType(controlType);

            // - Add
            roleRepo.Add(role);

            return role;
        }

        public void RoleLocomotion(RoleEntity role) {
            var inputComponent = role.InputComponent;
            var lc = role.LocomotionComponent;

            // - Move
            var moveDir = inputComponent.moveDir;
            lc.Move(moveDir);

            // - Jump
            if (inputComponent.jump) {
                lc.Jump();
            }

            inputComponent.Reset();
        }

    }

}