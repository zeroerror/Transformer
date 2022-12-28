using GameArki.TripodCamera;

namespace Transformer.RendererBussiness.Facade
{

    public class RendererFacade
    {

        TCCore camCore;



        public RendererFacade() { }

        public void Inject(TCCore camCore)
        {
            this.camCore = camCore;
        }
        
    }

}