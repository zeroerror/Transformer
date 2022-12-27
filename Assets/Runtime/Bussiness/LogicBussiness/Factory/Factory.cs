using FixMath.NET;
using Transformer.Bussiness.LogicBussiness.Facade;

namespace Transformer.Bussiness.LogicBussiness.Factory
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

        public RoleLogicEntity SpawnRole(int typeID)
        {
            var roleTemplate = facade.Template.RoleTemplate;
            var model = roleTemplate.TryGet(typeID);

            RoleLogicEntity role = new RoleLogicEntity();
            var idc = role.IDComponent;
            var ldc = role.LocomotionComponent;
            idc.SetTypeID(typeID);
            ldc.SetMoveSpeed(model.moveSpeedCM * FP64.EN2);
            ldc.SetJumpSpeed(model.jumpForceCM * FP64.EN2);

            return role;
        }

    }

}