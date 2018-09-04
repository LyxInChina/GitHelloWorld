# CSProj文件和Sln文件

## CSProj文件

### MSBuild与csproj文件

- MSBuild（Microsoft Build Engine）是Miscrosoft和Visual Studio的构建应用程序的平台，提供XML架构的的工程文件来控制如何构建平台过程和构建软件，VS使用MSBuild但是MSBuild不依赖VS的安装。通过在工程文件或者sln文件中调用MSBuild，可以在不安装VS时进行编排和构建工程。主要包含三部分内容：执行引擎、构造工程、任务；
- 执行引擎：定义构造工程的规范、解释构造工程、执行构造动作-引擎；
- 构造工程：描述构造任务，这里可以指未csproj文件（C#工程的项目内容管理和生成行为管理文件）-脚本；
- 任务：执行引擎的执行的动作定义-扩展能力；    
- [参考资料](https://www.cnblogs.com/shanyou/p/3452938.html)
- csproj文件，为一个xml形式的文件

### 构造工程 - 脚本文件

### 编辑工程的调试项属性

- 1.找到接口VSLangProj.ProjectConfigurationProperties；
- 2.使用工程的EnvDTE.Project.ConfigurationManager.ActiveConfiguration.Properties
- 3.[参考文件](https://msdn.microsoft.com/en-us/6323383a-43ee-4a60-be4e-9d7f0b53b168)
```C#
public static void SetProjectDebugProp(EnvDTE.Project proj, string startProgrm,Enum.StartActionType startActiconType = Enum.StartActionType.Program, string startArgs = null, string startWorkingDirectory = null)
{
    if (proj != null && proj.Object is VSLangProj.VSProject)
    {
        var log = OperationCenter.MLogger;
        EnvDTE.Configuration activeConf = proj.ConfigurationManager.ActiveConfiguration;
        EnvDTE.Property startProp = activeConf.Properties.Item("StartProgram");
        EnvDTE.Property startActionProp = activeConf.ConfigurationManager.ActiveConfiguration.Item("StartAction");
        EnvDTE.Property startArguments = activeConf.Properties.Item("StartArguments");
        EnvDTE.Property startWorkingDirProp = activeConf.Properties.Item("StartWorkingDirectory");
        log.Info("开始设置工程:[{0}]调试配置项", proj.Name);
        string oldValue = startProp.Value;
        startProp.Value = startProgrm;
        log.Info("[{0}]:OldValue:[{1}],NewValue:[{2}]",startProp.Name, oldValue, startProp.Value.ToString());
        int oldValue2 = startActionProp.Value;
        startActionProp.Value = (int)startActiconType;
        log.Info("[{0}]:OldValue:[{1}],NewValue:[{2}]",startActionProp.Name, oldValue2.ToString(), startActionProp.Value.ToString());
        if (startArgs != null)
        {
            oldValue = startArguments.Value;
            startArguments.Value = startArgs;
            log.Info("[{0}]:OldValue:[{1}],NewValue:[{2}]", startArguments.Name,oldValue, startArguments.Value.ToString());
        }
        if (startWorkingDirectory != null)
        {
            oldValue = startArguments.Value;
            startWorkingDirProp.Value = startWorkingDirectory;
            log.Info("[{0}]:OldValue:[{1}],NewValue:[{2}]", startWorkingDirProp.Name, oldValue, startWorkingDirProp.Value.ToString());
        }
        proj.Save();
        log.Info("设置工程:[{0}]调试配置项完成.", proj.Name);
    }
}
```

## sln文件

### 编辑解决方案中工程间的生成依赖

- 1.使用接口BuildDependency
- 2.在解决方案中找到接口对象BuildDependency
```C#
public static bool FindBuildDependencyByProjID(string projID, out BuildDependency build)
{
    var result = false;
    build = null;
    if (OperationCenter.MDTE != null)
    {
        var bd = OperationCenter.MDTE.Solution.SolutionBuild.BuildDependencies;
        foreach (BuildDependency bdy in bd)
        {
            if (bdy.Project.FullName.Equals(projID, StringComparison.OrdinalIgnoreCase))
            {
                build = bdy;
                result = true;
                break;
            }
        }
    }
    return result;
}
```
- 3.编辑BuildDependency新增工程依赖
```C#
public static void AddSlnBuildDependency(string projID, string[] projAddIDs)
{
    if (OperationCenter.MDTE != null)
    {
        BuildDependency build;
        if (FindBuildDependencyByProjID(projID, out build))
        {
            if (projAddIDs != null)
            {
                for (int i = 0; i < projAddIDs.Length; i++)
                {
                    try
                    {
                        build.AddProject(projAddIDs[i]);
                    }
                    catch (Exception ex)
                    {
                        OperationCenter.MLogger.Error("添加工程依赖-AddSlnBuildDependency Msg:{0}，Source:{1},Stack:{2}", ex.Message, ex.Source, ex.StackTrace);
                    }
                }
            }
            SolutionSave();
        }
    }
}
```