using System;
using System.Collections.Generic;

namespace Transformer.LogicBussiness.Repo
{

    public class RoleLogicRepo
    {

        Dictionary<int, RoleLogicEntity> allRoles;

        public RoleLogicRepo()
        {
            allRoles = new Dictionary<int, RoleLogicEntity>();
        }

        public void Add(RoleLogicEntity role)
        {
            allRoles.Add(role.IDComponent.ID, role);
        }

        public void Remove()
        {

        }

        public void Get()
        {

        }

        public void ForeachAll(Action<RoleLogicEntity> action)
        {
            var e = allRoles.Values.GetEnumerator();
            while (e.MoveNext())
            {
                var r = e.Current;
                action.Invoke(r);
            }
        }

    }

}