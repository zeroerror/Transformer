using System;
using System.Collections.Generic;

namespace Transformer.Bussiness.LogicBussiness.Repo
{

    public class RoleRepo
    {

        Dictionary<int, RoleEntity> allRoles;

        public RoleRepo()
        {
            allRoles = new Dictionary<int, RoleEntity>();
        }

        public void Add(RoleEntity role)
        {
            allRoles.Add(role.IDComponent.ID, role);
        }

        public void Remove()
        {

        }

        public void Get()
        {

        }

        public void ForeachAll(Action<RoleEntity> action)
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