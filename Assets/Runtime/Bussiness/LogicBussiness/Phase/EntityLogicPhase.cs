using FixMath.NET;

namespace Transformer.Bussiness.LogicBussiness.Phase {

    public class EntityLogicPhase {

        Facade.LogicFacade facade;

        public EntityLogicPhase() { }

        public void Inject(Facade.LogicFacade facade) {
            this.facade = facade;
        }

        public void Tick(FP64 dt) {
            var roleRepo = facade.Repo.RoleRepo;
            var domain = facade.Domain;
            var roleDomain = domain.RoleDomain;

            roleRepo.ForeachAll((role) => {
                roleDomain.RoleLocomotion(role);
            });
        }

    }

}