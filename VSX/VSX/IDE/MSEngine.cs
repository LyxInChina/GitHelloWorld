using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.BuildEngine;

namespace VSX.IDE
{
    public class MSEngine
    {
        public void OldFunc(EnvDTE.Project project)
        {
            Engine engine = new Engine(Engine.GlobalEngine.BinPath);
            Project _Project = new Project(engine);
            _Project.Load(project.FullName);
        }

        public void Func()
        {
            Microsoft.Build.Evaluation.ProjectCollection c = new Microsoft.Build.Evaluation.ProjectCollection();
            Microsoft.Build.Evaluation.Project project = new Microsoft.Build.Evaluation.Project();

        }

    }
}
