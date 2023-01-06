using Transformer.Bussiness.RendererBussiness.Domain;

namespace Transformer.Bussiness.RendererBussiness.Facade
{

    public class RendererDomain
    {

        public RoleDomain RoleDomain { get; private set; }

        public RendererDomain()
        {
            RoleDomain = new RoleDomain();
        }

        public void Inject(RendererFacade facade)
        {
            RoleDomain.Inject(facade);
        }

    }

}