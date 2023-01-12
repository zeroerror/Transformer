using FixMath.NET;
using Transformer.Bussiness.LogicBussiness.Config;
using Transformer.Bussiness.LogicBussiness.Facade;
using Transformer.Bussiness.LogicBussiness.Generic;

namespace Transformer.Bussiness.LogicBussiness.Factory {

    public class LogicFactory {

        LogicFacade facade;

        public LogicFactory() {

        }

        public void Inject(LogicFacade facade) {
            this.facade = facade;
        }

        public RoleEntity SpawnRole(int typeID, in FPVector3 spawnPos) {
            var roleTemplate = facade.Template.RoleTemplate;
            var model = roleTemplate.TryGet(typeID);

            // - Spawn Entity
            RoleEntity role = new RoleEntity();
            var lc = role.LocomotionComponent;
            lc.SetMoveMaxSpeed(model.moveSpeed_CM * FP64.EN2);
            lc.SetMoveAccelerate(GameConfig.moveAccelerate);
            lc.SetJumpSpeed(model.jumpForce_CM * FP64.EN2);

            // - Spawn Rigidbody
            var physicsAPI = facade.PhysicsCore.SetterAPI;
            var rot = FPQuaternion.Identity;
            var scale = new FPVector3(1, 1, 1);
            var size = new FPVector3(1, 1, 1);
            var rb = physicsAPI.SpawnRBBox(spawnPos, rot, scale, size);
            lc.Inject(rb);

            return role;
        }

    }

}