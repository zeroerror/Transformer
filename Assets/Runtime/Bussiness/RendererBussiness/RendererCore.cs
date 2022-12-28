using GameArki.TripodCamera;
using Transformer.RendererBussiness.Facade;

namespace Transformer.RendererBussiness
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