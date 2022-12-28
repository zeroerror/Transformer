using FixMath.NET;
using GameArki.FreeInput;
using Transformer.LogicBussiness.Facade;
using Transformer.LogicBussiness.Phase;

namespace Transformer.LogicBussiness
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