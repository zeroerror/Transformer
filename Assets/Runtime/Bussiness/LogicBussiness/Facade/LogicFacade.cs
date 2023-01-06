using GameArki.FreeInput;
using ZeroPhysics.Physics3D;
using Transformer.Bussiness.LogicBussiness.Factory;
using Transformer.Bussiness.LogicBussiness.Service;
using Transformer.Template;
using Transformer.Bussiness.RendererAPI;

namespace Transformer.Bussiness.LogicBussiness.Facade
{

    public class LogicFacade
    {

        public FreeInputCore InputCore { get; private set; }
        public PhysicsWorld3DCore PhysicsCore { get; private set; }

        public AllTemplate Template { get; private set; }
        public LogicRepo Repo { get; private set; }
        public LogicDomain Domain { get; private set; }
        public LogicFactory Factory { get; private set; }
        public IDService IDService { get; private set; }

        // - API
        RendererSetter rendererSetterAPI;
        public IRendererSetter RendererSetterAPI => rendererSetterAPI;

        public LogicFacade()
        {
            Repo = new LogicRepo();
            Domain = new LogicDomain();
            Domain.Inject(this);
            Factory = new Factory.LogicFactory();
            Factory.Inject(this);
            IDService = new IDService();
        }

        public void Inject(FreeInputCore inputCore, PhysicsWorld3DCore physicsCore, AllTemplate template,RendererSetter rendererSetterAPI)
        {
            this.InputCore = inputCore;
            this.PhysicsCore = physicsCore;
            this.Template = template;
            this.rendererSetterAPI = rendererSetterAPI;
        }

    }

}