using GameArki.FreeInput;
using Transformer.Bussiness.LogicBussiness.Domain;
using Transformer.Bussiness.LogicBussiness.Factory;
using Transformer.Bussiness.LogicBussiness.Service;

namespace Transformer.Bussiness.LogicBussiness.Facade
{

    public class LogicFacade
    {

        public FreeInputCore InputCore { get; private set; }
        public AllTemplate Template { get; private set; }

        public AllLogicRepo Repo { get; private set; }
        public AllDomain Domain { get; private set; }
        public LogicFactory Factory { get; private set; }
        public IDService IDService { get; private set; }

        public LogicFacade()
        {
            Repo = new AllLogicRepo();
            Domain = new AllDomain();
            Domain.Inject(this);
            Factory = new LogicFactory();
            Factory.Inject(this);
            IDService = new IDService();
        }

        public void Inject(FreeInputCore inputCore, AllTemplate template)
        {
            this.InputCore = inputCore;
            this.Template = template;
        }

    }

}