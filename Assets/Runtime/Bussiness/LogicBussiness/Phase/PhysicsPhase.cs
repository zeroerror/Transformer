using FixMath.NET;
using Transformer.Bussiness.LogicBussiness.Facade;

namespace Transformer.Bussiness.LogicBussiness.Phase
{

    public class PhysicsPhase
    {

        LogicFacade logicFacade;

        public PhysicsPhase() { }

        public void Inject(LogicFacade logicFacade)
        {
            this.logicFacade = logicFacade;
        }

        public void Tick(FP64 dt)
        {
            logicFacade.PhysicsCore.Tick(dt);
        }

    }

}