using System;
using System.Collections.Generic;

namespace Transformer.Bussiness.RendererBussiness.Repo
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
            allRoles.Add(role.ID, role);
        }

        public void Remove()
        {

        }

        public RoleEntity Get(int id)
        {
            return allRoles[id];
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