using FixMath.NET;
using Transformer.LogicBussiness.Facade;

namespace Transformer.LogicBussiness.Factory
{

    public class LogicFactory
    {

        LogicFacade facade;

        public LogicFactory()
        {

        }

        public void Inject(LogicFacade facade)
        {
            this.facade = facade;
        }

        public RoleLogicEntity SpawnRole(int typeID, in FPVector3 spawnPos)
        {
            var roleTemplate = facade.Template.RoleTemplate;
            var model = roleTemplate.TryGet(typeID);

            // - Spawn Entity;
            RoleLogicEntity role = new RoleLogicEntity();
            var idc = role.IDComponent;
            var lc = role.LocomotionComponent;
            idc.SetTypeID(typeID);
            lc.SetMoveSpeed(model.moveSpeed_CM * FP64.EN2);
            lc.SetJumpSpeed(model.jumpForce_CM * FP64.EN2);

            // - Spawn Rigidbody;
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