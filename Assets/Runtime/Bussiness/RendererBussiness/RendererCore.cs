using GameArki.TripodCamera;
using Transformer.Bussiness.RendererBussiness.Facade;

namespace Transformer.Bussiness.RendererBussiness
{

    public class RendererCore
    {

        RendererFacade rendererFacade;

        public RendererCore()
        {
            rendererFacade = new RendererFacade();
        }

        public void Inject(TCCore camCore)
        {
            rendererFacade.Inject(camCore);
        }

        public void Tick()
        {
        }

    }

}