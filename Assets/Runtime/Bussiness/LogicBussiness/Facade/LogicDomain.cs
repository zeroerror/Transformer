using Transformer.Bussiness.LogicBussiness.Domain;

namespace Transformer.Bussiness.LogicBussiness.Facade
{

    public class LogicDomain
    {

        public RoleDomain RoleDomain { get; private set; }

        public LogicDomain()
        {
            RoleDomain = new RoleDomain();
        }

        public void Inject(Facade.LogicFacade facade)
        {
            RoleDomain.Inject(facade);
        }

    }

}