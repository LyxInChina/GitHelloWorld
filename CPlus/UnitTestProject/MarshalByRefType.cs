using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    public class MarshalByRefType : MarshalByRefObject
    {
        /// <summary>
        /// 修改对象的租期时间
        /// </summary>
        /// <returns></returns>
        [SecurityPermission(SecurityAction.Demand,Flags = SecurityPermissionFlag.Infrastructure)]
        public override object InitializeLifetimeService()
        {
            ILease lease = (ILease)base.InitializeLifetimeService();
            if (lease.CurrentState == LeaseState.Initial)
            {
                lease.InitialLeaseTime = TimeSpan.FromMinutes(1);
                lease.SponsorshipTimeout = TimeSpan.FromMinutes(2);
                lease.RenewOnCallTime = TimeSpan.FromSeconds(2);
            }
            return lease;
        }
    }

    [Serializable]
    public class MarshalByValType
    {
        
    }

    [Serializable]
    public struct MarshalByValStructType
    {

    }
}
