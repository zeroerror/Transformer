using GameArki.FreeInput;
using FixMath.NET;
using ZeroPhysics.Physics3D;
using Transformer.Bussiness.LogicBussiness.Facade;
using Transformer.Bussiness.LogicBussiness.Phase;
using Transformer.Template;
using Transformer.Bussiness.RendererAPI;
using Transformer.Bussiness.LogicBussiness.Config;

namespace Transformer.Bussiness.LogicBussiness {

    public class LogicCore {

        public LogicFacade logicFacade;

        InputPhase inputPhase;
        EntityLogicPhase logicPhase;
        PhysicsPhase physicsPhase;
        RendererPhase rendererPhase;

        PhysicsWorld3DCore physicsCore;
        public PhysicsWorld3DCore PhysicsCore => physicsCore;

        FP64 restoreTime;
        FP64 intervalTime;

        public LogicCore() {
            logicFacade = new Facade.LogicFacade();

            inputPhase = new InputPhase();
            logicPhase = new EntityLogicPhase();
            physicsPhase = new PhysicsPhase();
            rendererPhase = new RendererPhase();

            physicsCore = new PhysicsWorld3DCore(new FPVector3(0, -FP64.ToFP64(GameConfig.gravity), 0));
            intervalTime = 1 / FP64.ToFP64(GameConfig.physics_frame_rate);
        }

        public void Inject(FreeInputCore inputCore, AllTemplate template, RendererSetter rendererSetter) {
            logicFacade.Inject(inputCore, physicsCore, template, rendererSetter);

            inputPhase.Inject(logicFacade);
            logicPhase.Inject(logicFacade);
            physicsPhase.Inject(logicFacade);
            rendererPhase.Inject(logicFacade);
        }

        public void Tick(float dt) {
            inputPhase.Update();
            restoreTime += FP64.ToFP64(dt);
            while (restoreTime >= intervalTime) {
                restoreTime -= intervalTime;
                // - Phase
                physicsPhase.Tick(intervalTime);
                logicPhase.Tick(intervalTime);
                rendererPhase.Tick(intervalTime);
            }
        }

    }

}