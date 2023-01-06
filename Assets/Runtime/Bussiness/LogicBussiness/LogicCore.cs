using GameArki.FreeInput;
using FixMath.NET;
using ZeroPhysics.Physics3D;
using Transformer.Bussiness.LogicBussiness.Facade;
using Transformer.Bussiness.LogicBussiness.Phase;
using Transformer.Template;
using Transformer.Bussiness.RendererAPI;

namespace Transformer.Bussiness.LogicBussiness
{

    public class LogicCore
    {

        public LogicFacade logicFacade;

        InputPhase inputPhase;
        EntityLogicPhase logicPhase;
        PhysicsPhase physicsPhase;
        RendererPhase rendererPhase;

        PhysicsWorld3DCore physicsCore;
        public PhysicsWorld3DCore PhysicsCore => physicsCore;

        public LogicCore()
        {
            logicFacade = new Facade.LogicFacade();

            inputPhase = new InputPhase();
            logicPhase = new EntityLogicPhase();
            physicsPhase = new PhysicsPhase();
            rendererPhase = new RendererPhase();

            physicsCore = new PhysicsWorld3DCore(new FPVector3(0, -10, 0));
        }

        public void Inject(FreeInputCore inputCore, AllTemplate template, RendererSetter rendererSetter)
        {
            logicFacade.Inject(inputCore, physicsCore, template, rendererSetter);

            inputPhase.Inject(logicFacade);
            logicPhase.Inject(logicFacade);
            physicsPhase.Inject(logicFacade);
            rendererPhase.Inject(logicFacade);
        }

        public void Update(float dt)
        {
            inputPhase.Update();
        }

        public void Tick(FP64 dt)
        {
            // - Phase
            logicPhase.Tick(dt);
            physicsPhase.Tick(dt);
            rendererPhase.Tick(dt);
        }

    }

}