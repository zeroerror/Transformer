namespace Transformer.LogicBussiness.Service
{

    public class IDService
    {

        public int roleAutoID;

        public IDService() { }

        public int GetRoleID()
        {
            return roleAutoID++;
        }

    }

}