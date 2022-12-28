using FixMath.NET;
using Transformer.LogicBussiness.Facade;
using Transformer.LogicBussiness.Generic;

namespace Transformer.LogicBussiness.Phase
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

                // - Move
                var moveDir = inputComponent.moveDir;
                var vel = moveDir * lc.MoveSpeed;
                lc.Move(vel);
                // - Jump
                if (inputComponent.jump) lc.Jump();
                inputComponent.Reset();

            });

        }

    }

}