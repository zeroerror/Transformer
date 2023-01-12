using System.Collections.Generic;
using ZeroPhysics.Physics3D.Facade;

namespace ZeroPhysics.Physics3D.Domain
{

    public class DataDomain
    {

        Physics3DFacade physicsFacade;

        public DataDomain() { }

        public void Inject(Physics3DFacade physicsFacade)
        {
            this.physicsFacade = physicsFacade ;
        }

        public List<Box3D> GetAllBoxes()
        {
            var boxes = physicsFacade.boxes;
            var idService = physicsFacade.Service.IDService;
            var infos = idService.boxIDInfos;
            var len = infos.Length;
            List<Box3D> all = new List<Box3D>();
            for (int i = 0; i < len; i++)
            {
                if (infos[i]) all.Add(boxes[i]);
            }
            return all;
        }

        public List<Box3DRigidbody> GetAllRBBoxes()
        {
            var rbBoxes = physicsFacade.boxRBs;
            var idService = physicsFacade.Service.IDService;
            var infos = idService.boxRBIDInfos;
            var len = infos.Length;
            List<Box3DRigidbody> all = new List<Box3DRigidbody>();
            for (int i = 0; i < len; i++)
            {
                if (infos[i]) all.Add(rbBoxes[i]);
            }
            return all;
        }

    }

}