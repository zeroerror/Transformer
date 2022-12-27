using FixMath.NET;
using GameArki.FreeInput;
using Transformer.Bussiness.LogicBussiness.Facade;
using Transformer.Bussiness.LogicBussiness.Phase;

namespace Transformer.Bussiness.LogicBussiness
{

    public class LogicCore
    {

        public LogicFacade logicFacade;

        InputPhase inputPhase;
        EntityLogicPhase logicPhase;

        public LogicCore()
        {
            logicFacade = new LogicFacade();
            inputPhase = new InputPhase();
            logicPhase = new EntityLogicPhase();
        }

        public void Inject(FreeInputCore inputCore, AllTemplate template)
        {
            logicFacade.Inject(inputCore, template);
            inputPhase.Inject(logicFacade);
            logicPhase.Inject(logicFacade);
        }

        public void Tick(FP64 dt)
        {
            // - Phase
            inputPhase.Tick();
            logicPhase.Tick(dt);
        }

    }

}