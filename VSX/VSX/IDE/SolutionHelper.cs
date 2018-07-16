using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.BuildEngine;

namespace VSX.IDE
{
    public class SolutionHelper
    {
        // 根据解决方案获取projects
        /// <summary>
        /// 根据解决方案sln文件获取该解决方案内所有的工程信息
        /// </summary>
        /// <param name="slnPath"></param>
        /// <returns></returns>
        public static List<Project> GetAllProjectsFromSlnFile(string slnPath)
        {
            List<Project> projects = new List<Project>();
            String xmlReader = SolutionWrapperProject.Generate(slnPath, null, null);
            Engine engine = new Engine(Engine.GlobalEngine.BinPath);
            Project project = new Project(engine);
            project.LoadXml(xmlReader);
            foreach (BuildItemGroup ig in project.ItemGroups)
            {
                foreach (BuildItem item in ig)
                {
                    if (item.Name.Equals("_SolutionProjectProjects"))
                    {
                        string projPath = "";
                        Project _Project = new Project(engine);
                        _Project.Load(projPath);
                        projects.Add(_Project);
                    }
                }
            }
            return projects;
        }
    }
}
