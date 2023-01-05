using GameArki.TripodCamera;

namespace Transformer.RendererBussiness.Facade
{

    public class RendererFacade
    {

        TCCore camCore;

        // TODO
        // public AllDomain Domain { get; private set; }
        // public RendererFactory Factory { get; private set; }

        public RendererFacade() { }

        public void Inject(TCCore camCore)
        {
            this.camCore = camCore;
        }

    }

}