using FixMath.NET;
using GameArki.FreeInput;
using Transformer.LogicBussiness.Facade;
using Transformer.LogicBussiness.Phase;
using ZeroPhysics.Physics3D;

namespace Transformer.LogicBussiness
{

    public class LogicCore
    {

        public LogicFacade logicFacade;

        InputPhase inputPhase;
        EntityLogicPhase logicPhase;
        PhysicsPhase physicsPhase;

        PhysicsWorld3DCore physicsCore;
        public PhysicsWorld3DCore PhysicsCore => physicsCore;

        public LogicCore()
        {
            logicFacade = new LogicFacade();
            inputPhase = new InputPhase();
            logicPhase = new EntityLogicPhase();
            physicsPhase = new PhysicsPhase();
            physicsCore = new PhysicsWorld3DCore(new FPVector3(0, -10, 0));
        }

        public void Inject(FreeInputCore inputCore, AllTemplate template)
        {
            logicFacade.Inject(inputCore, physicsCore, template);
            inputPhase.Inject(logicFacade);
            logicPhase.Inject(logicFacade);
            physicsPhase.Inject(logicFacade);
        }

        public void Update()
        {
            inputPhase.Update();
        }

        public void Tick(FP64 dt)
        {
            // - Phase
            logicPhase.Tick(dt);
            physicsPhase.Tick(dt);
        }

    }

}