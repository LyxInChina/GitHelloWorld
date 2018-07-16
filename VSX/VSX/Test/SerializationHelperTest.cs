using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSX
{
    /// <summary>
    /// 测试xml序列化和反序列化
    /// </summary>
    public class SerializationHelperTest
    {
        static void Main(string[] args)
        {
            Test_Project();
            Test_Module();
            Test_Nuget();
            Console.ReadKey();
        }

        static void Test_Project()
        {
            var proj = new Model.Project()
            {
                ArtifactId = "testid",
                Version = "testversion.03.2.3.45.",
            };
            proj.References = new Model.ProjectReference()
            {
                RNames = new List<Model.RName>()
                {
                    new Model.RName(){ Value="1001" },
                    new Model.RName(){ Value="1002" },
                    new Model.RName(){ Value="1003" },
                    new Model.RName(){ Value="1004" },
                    new Model.RName(){ Value="1005" },
                }
            };
            Common.SerializationHelper.Serializable(proj);
            Model.Project temp;
            Common.SerializationHelper.Deserializable(out temp);
        }

        static void Test_Module()
        {
            var module = new Model.Module()
            {
                ID = "my ID",
                Version = "My version",
            };
            module.Librars = new Model.LibraryCollection()
            {
                Libraries = new List<Model.Library>()
                 {
                     new Model.Library(){ ID="ID10011",Version="version1011" , Scope= Model.Scope.Compile},
                     new Model.Library(){ ID="ID10012",Version="version1021" , Scope= Model.Scope.Runtime},
                 }
            };
            module.Denpencies = new Model.DenpencyCollection()
            {
                Dependencies = new List<Model.Dependency>()
                {
                     new Model.Dependency(){ ID="ID001",Version="V002"},
                     new Model.Dependency(){ ID="ID002",Version="V003"},
                }
            };
            Common.SerializationHelper.Serializable(module);
            Model.Module temp2;
            Common.SerializationHelper.Deserializable(out temp2);
        }

        static void Test_Nuget()
        {
            var nuget = new Model.Nuget();
            nuget.ActivePkg = new Model.AddCollection()
            {
                Adds = new List<Model.Add>()
                 {
                      new Model.Add(){Key="Actk11",Value="v11",},
                      new Model.Add(){Key="Actk12",Value="v12",},
                      new Model.Add(){Key="Actk13",Value="v13",},
                 }
            };
            nuget.BindingRedic = new Model.AddCollection()
            {
                Adds = new List<Model.Add>()
                 {
                      new Model.Add(){Key="Bindk11",Value="v11",},
                      new Model.Add(){Key="Bindk12",Value="v12",},
                      new Model.Add(){Key="Bindk13",Value="v13",},
                 }
            };
            nuget.PkgSource = new Model.AddCollection()
            {
                Adds = new List<Model.Add>()
                 {
                      new Model.Add(){Key="PkgSk11",Value="v11",},
                      new Model.Add(){Key="PkgSk12",Value="v12",},
                      new Model.Add(){Key="PkgSk13",Value="v13",},
                 }
            };
            nuget.PkgRestore = new Model.AddCollection()
            {
                Adds = new List<Model.Add>()
                 {
                      new Model.Add(){Key="PkgRk11",Value="v11", ProtocolVersion="V.3.0"},
                      new Model.Add(){Key="PkgRk12",Value="v12",},
                      new Model.Add(){Key="PkgRk13",Value="v13",},
                 }
            };
            nuget.PkgManagement = new Model.AddCollection()
            {
                Adds = new List<Model.Add>()
                 {
                      new Model.Add(){Key="PkgMk11",Value="v11",},
                      new Model.Add(){Key="PkgMk12",Value="v12",},
                      new Model.Add(){Key="PkgMk13",Value="v13",},
                 }
            };
            Common.SerializationHelper.Serializable(nuget, ".Config");
            Model.Nuget temp3;
            Common.SerializationHelper.Deserializable(out temp3, ".Config");
        }
    }
}
