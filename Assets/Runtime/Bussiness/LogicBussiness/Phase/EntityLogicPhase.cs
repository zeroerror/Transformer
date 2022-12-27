using FixMath.NET;
using Transformer.Bussiness.LogicBussiness.Facade;
using Transformer.Bussiness.LogicBussiness.Generic;

namespace Transformer.Bussiness.LogicBussiness.Phase
{

    public class EntityLogicPhase
    {

        LogicFacade facade;

        public EntityLogicPhase() { }

        public void Inject(LogicFacade facade)
        {
            this.facade = facade;
        }

        public void Tick(FP64 dt)
        {
            var roleRepo = facade.Repo.RoleRepo;
            roleRepo.ForeachAll((role) =>
            {
                var inputComponent = role.InputComponent;
                var lc = role.LocomotionComponent;
                var moveDir = inputComponent.moveDir;
                var vel = moveDir * lc.MoveSpeed;
                lc.Move(vel);
            });

        }

    }

}