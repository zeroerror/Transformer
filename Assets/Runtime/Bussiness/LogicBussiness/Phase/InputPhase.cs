using FixMath.NET;
using Transformer.LogicBussiness.Facade;
using Transformer.LogicBussiness.Generic;

namespace Transformer.LogicBussiness.Phase
{

    public class InputPhase
    {

        LogicFacade logicFacade;

        public void Inject(LogicFacade logicFacade)
        {
            this.logicFacade = logicFacade;
        }

        public InputPhase() { }

        public void Tick()
        {
            var roleRepo = logicFacade.Repo.RoleRepo;
            roleRepo.ForeachAll((role) =>
            {
                var inputComponent = role.InputComponent;
                var idComponent = role.IDComponent;

                inputComponent.Reset();

                // - Owner Input
                if (idComponent.ControlType == ControlType.Owner)
                {
                    var inputCore = logicFacade.InputCore;
                    FPVector3 moveDir = FPVector3.Zero;
                    if (inputCore.Getter.GetPressing(1)) moveDir.z = 1;
                    if (inputCore.Getter.GetPressing(2)) moveDir.z = -1;
                    if (inputCore.Getter.GetPressing(3)) moveDir.x = -1;
                    if (inputCore.Getter.GetPressing(4)) moveDir.x = 1;
                    moveDir.Normalize();
                    inputComponent.moveDir = moveDir;
                }
            });

        }

    }

}