using FixMath.NET;
using Transformer.Generic;
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

        public void Update()
        {
            var roleRepo = logicFacade.Repo.RoleRepo;
            roleRepo.ForeachAll((role) =>
            {
                var inputComponent = role.InputComponent;
                var idComponent = role.IDComponent;

                // - Owner Input
                if (idComponent.ControlType == ControlType.Owner)
                {
                    var inputCore = logicFacade.InputCore;
                    FPVector3 moveDir = FPVector3.Zero;
                    var getter = inputCore.Getter;
                    if (getter.GetPressing(InputBindIDCollection.MOVE_FORWARD)) moveDir.z = 1;
                    if (getter.GetPressing(InputBindIDCollection.MOVE_BACKWARD)) moveDir.z = -1;
                    if (getter.GetPressing(InputBindIDCollection.MOVE_LEFT)) moveDir.x = -1;
                    if (getter.GetPressing(InputBindIDCollection.MOVE_RIGHT)) moveDir.x = 1;
                    if (getter.GetDown(InputBindIDCollection.JUMP)) inputComponent.jump = true;
                    moveDir.Normalize();
                    inputComponent.moveDir = moveDir;
                }
            });
        }

    }

}