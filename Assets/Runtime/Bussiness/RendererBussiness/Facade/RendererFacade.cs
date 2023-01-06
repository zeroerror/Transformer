using GameArki.TripodCamera;
using Transformer.Bussiness.RendererBussiness.Domain;
using Transformer.Bussiness.RendererBussiness.Factory;
using Transformer.Template;

namespace Transformer.Bussiness.RendererBussiness.Facade
{

    public class RendererFacade
    {

        public RendererDomain Domain { get; private set; }
        public RendererFactory Factory { get; private set; }
        public RendererRepo Repo { get; private set; }

        public TCCore CamCore { get; private set; }
        public AllTemplate Template { get; private set; }

        public RendererFacade()
        {
            Domain = new RendererDomain();
            Factory = new RendererFactory();
            Repo = new RendererRepo();

            Domain.Inject(this);
            Factory.Inject(this);
        }

        public void Inject(TCCore camCore, AllTemplate allTemplate)
        {
            this.CamCore = camCore;
            this.Template = allTemplate;
        }

    }

}