using Transformer.Bussiness.LogicBussiness.Facade;

namespace Transformer.Bussiness.LogicBussiness.Domain
{

    public class AllDomain
    {

        public RoleDomain RoleDomain { get; private set; }

        public AllDomain()
        {
            RoleDomain = new RoleDomain();
        }

        public void Inject(LogicFacade facade)
        {
            RoleDomain.Inject(facade);
        }

    }

}