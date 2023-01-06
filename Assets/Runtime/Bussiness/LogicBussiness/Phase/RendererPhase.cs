using FixMath.NET;
using Transformer.Bussiness.LogicBussiness.Facade;

namespace Transformer.Bussiness.LogicBussiness.Phase
{

    public class RendererPhase
    {

        LogicFacade logicFacade;

        public RendererPhase() { }

        public void Inject(LogicFacade logicFacade)
        {
            this.logicFacade = logicFacade;
        }

        public void Tick(FP64 dt)
        {
            var rendererSetterAPI = logicFacade.RendererSetterAPI;
            var roleRepo = logicFacade.Repo.RoleRepo;
            roleRepo.ForeachAll((role) =>
            {
                var id = role.IDComponent.ID;
                var centerFP = role.LocomotionComponent.RbBox.Box.Center;
                rendererSetterAPI.SetRoleLocomotion_Center(id, centerFP);
            });
        }

    }

}
