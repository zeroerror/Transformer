using FixMath.NET;
using Transformer.LogicBussiness.Facade;

namespace Transformer.LogicBussiness.Phase
{

    public class PhysicsPhase
    {

        LogicFacade facade;

        public PhysicsPhase() { }

        public void Inject(LogicFacade facade)
        {
            this.facade = facade;
        }

        public void Tick(FP64 dt)
        {
            facade.PhysicsCore.Tick(dt);
        }

    }

}