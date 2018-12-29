using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Permissions;
using System.Security.Authentication;

namespace HelloWorld.SecurityAndPermission
{
    public class SecurityAndPermissionHelp
    {

        public static void SecurityTest()
        {
            System.Security.PermissionSet permissionSet = new System.Security.PermissionSet(PermissionState.None);
            System.Security.PermissionSet permissionSet2 = new System.Security.PermissionSet(PermissionState.Unrestricted);
            System.Security.Policy.Evidence evidence = new System.Security.Policy.Evidence();



        }
    }
}
