using System.Collections.Generic;

namespace ZeroPhysics.Service
{

    public class IDService
    {

        public bool[] boxIDInfos;
        public bool[] boxRBIDInfos;
        public bool[] sphereIDInfos;

        public IDService(int sBoxMax, int rbBoxMax, int sphereMax)
        {
            boxIDInfos = new bool[sBoxMax];
            boxRBIDInfos = new bool[rbBoxMax];
            sphereIDInfos = new bool[sphereMax];
        }

        public ushort FetchID_Box()
        {
            for (ushort i = 0; i < boxIDInfos.Length; i++)
            {
                if (!boxIDInfos[i])
                {
                    boxIDInfos[i] = true;
                    return i;
                }
            }

            throw new System.Exception($"IDService: Box ID Run Out!");
        }

        public ushort FetchID_BoxRB()
        {
            for (ushort i = 0; i < boxRBIDInfos.Length; i++)
            {
                if (!boxRBIDInfos[i])
                {
                    boxRBIDInfos[i] = true;
                    return i;
                }
            }

            throw new System.Exception($"IDService: BoxRB ID Run Out!");
        }

        public ushort FetchID_Sphere()
        {
            for (ushort i = 0; i < sphereIDInfos.Length; i++)
            {
                if (!sphereIDInfos[i])
                {
                    sphereIDInfos[i] = true;
                    return i;
                }
            }

            throw new System.Exception($"IDService: Sphere ID Run Out!");
        }

        public void PutBackID_Box(int id)
        {
            boxIDInfos[id] = false;
        }

        public void PutBackID_RBBox(int id)
        {
            boxRBIDInfos[id] = false;
        }

        public void PutBackID_Sphere(int id)
        {
            sphereIDInfos[id] = false;
        }

    }

}