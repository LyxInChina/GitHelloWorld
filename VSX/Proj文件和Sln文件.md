# CSProj文件和Sln文件

## CSProj文件

```XML
<?xml version="1.0" encoding="utf-16"?>
<Project ToolsVersion="4.0" DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProductVersion>8.0.30703</ProductVersion>
    <SchemaVersion>2.0</SchemaVersion>
    <ProjectGuid>{31AC3873-B1D5-4825-BA9D-282A8FAB0E90}</ProjectGuid>
    <OutputType>Library</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <RootNamespace>LibN</RootNamespace>
    <AssemblyName>LibN</AssemblyName>
    <TargetFrameworkVersion>v4.0</TargetFrameworkVersion>
    <FileAlignment>512</FileAlignment>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\Debug\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\Release\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="EntityFramework">
      <SpecificVersion>False</SpecificVersion>
      <HintPath>..\..\..\..\..\repo\EntityFramework.6.0.0\lib\net40\EntityFramework.dll</HintPath>
    </Reference>
    <Reference Include="EntityFramework.SqlServer">
      <SpecificVersion>False</SpecificVersion>
      <HintPath>..\..\..\..\..\repo\EntityFramework.6.0.0\lib\net40\EntityFramework.SqlServer.dll</HintPath>
    </Reference>
    <Reference Include="System" />
    <Reference Include="System.Core" />
    <Reference Include="System.Xml.Linq" />
    <Reference Include="System.Data.DataSetExtensions" />
    <Reference Include="Microsoft.CSharp" />
    <Reference Include="System.Data" />
    <Reference Include="System.Xml" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include="Properties\AssemblyInfo.cs" />
  </ItemGroup>
  <ItemGroup>
    <Content Include="App.config.transform" />
    <Content Include="Web.config.transform" />
  </ItemGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
  <!-- To modify your build process, add your task inside one of the targets below and uncomment it. 
       Other similar extension points exist, see Microsoft.Common.targets.
  <Target Name="BeforeBuild">
  </Target>
  <Target Name="AfterBuild">
  </Target>
  -->
  <Import Condition="Exists('..\..\..\..\..\repo\Fody.1.28.3\build\Fody.targets')" Project="..\..\..\..\..\repo\Fody.1.28.3\build\Fody.targets" />
  <Target Name="EnsureNuGetPackageBuildImports" BeforeTargets="PrepareForBuild">
  </Target>
</Project>
```

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

```TXT
Microsoft Visual Studio Solution File, Format Version 11.00
# Visual Studio 2010
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Lib_A", "..\..\Test\SolutionB\Lib_A\Lib_A.csproj", "{8F2407A8-96CF-422D-98D1-CE7EBAC6848A}"
EndProject
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "Solution Items", "Solution Items", "{53D0EA4C-F0B0-4117-828E-F33A14889796}"
	ProjectSection(SolutionItems) = preProject
		client.LibN.xml = client.LibN.xml
	EndProjectSection
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Lib", "Lib\Lib.csproj", "{E374DFAF-95DB-4783-AC2C-6DBAB2C65F51}"
	ProjectSection(ProjectDependencies) = postProject
		{EDF6BEA2-1D2C-45D5-B88B-41CB30F57FD9} = {EDF6BEA2-1D2C-45D5-B88B-41CB30F57FD9}
		{8F2407A8-96CF-422D-98D1-CE7EBAC6848A} = {8F2407A8-96CF-422D-98D1-CE7EBAC6848A}
	EndProjectSection
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "NO", "LibN5\NO.csproj", "{EDF6BEA2-1D2C-45D5-B88B-41CB30F57FD9}"
	ProjectSection(ProjectDependencies) = postProject
		{8F2407A8-96CF-422D-98D1-CE7EBAC6848A} = {8F2407A8-96CF-422D-98D1-CE7EBAC6848A}
	EndProjectSection
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Debug|Mixed Platforms = Debug|Mixed Platforms
		Debug|x86 = Debug|x86
		Release|Any CPU = Release|Any CPU
		Release|Mixed Platforms = Release|Mixed Platforms
		Release|x86 = Release|x86
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{8F2407A8-96CF-422D-98D1-CE7EBAC6848A}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{8F2407A8-96CF-422D-98D1-CE7EBAC6848A}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{8F2407A8-96CF-422D-98D1-CE7EBAC6848A}.Debug|Mixed Platforms.ActiveCfg = Debug|Any CPU
		{8F2407A8-96CF-422D-98D1-CE7EBAC6848A}.Debug|Mixed Platforms.Build.0 = Debug|Any CPU
		{8F2407A8-96CF-422D-98D1-CE7EBAC6848A}.Debug|x86.ActiveCfg = Debug|Any CPU
		{8F2407A8-96CF-422D-98D1-CE7EBAC6848A}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{8F2407A8-96CF-422D-98D1-CE7EBAC6848A}.Release|Any CPU.Build.0 = Release|Any CPU
		{8F2407A8-96CF-422D-98D1-CE7EBAC6848A}.Release|Mixed Platforms.ActiveCfg = Release|Any CPU
		{8F2407A8-96CF-422D-98D1-CE7EBAC6848A}.Release|Mixed Platforms.Build.0 = Release|Any CPU
		{8F2407A8-96CF-422D-98D1-CE7EBAC6848A}.Release|x86.ActiveCfg = Release|Any CPU
		{E374DFAF-95DB-4783-AC2C-6DBAB2C65F51}.Debug|Any CPU.ActiveCfg = Debug|x86
		{E374DFAF-95DB-4783-AC2C-6DBAB2C65F51}.Debug|Mixed Platforms.ActiveCfg = Debug|x86
		{E374DFAF-95DB-4783-AC2C-6DBAB2C65F51}.Debug|Mixed Platforms.Build.0 = Debug|x86
		{E374DFAF-95DB-4783-AC2C-6DBAB2C65F51}.Debug|x86.ActiveCfg = Debug|x86
		{E374DFAF-95DB-4783-AC2C-6DBAB2C65F51}.Debug|x86.Build.0 = Debug|x86
		{E374DFAF-95DB-4783-AC2C-6DBAB2C65F51}.Release|Any CPU.ActiveCfg = Release|x86
		{E374DFAF-95DB-4783-AC2C-6DBAB2C65F51}.Release|Mixed Platforms.ActiveCfg = Release|x86
		{E374DFAF-95DB-4783-AC2C-6DBAB2C65F51}.Release|Mixed Platforms.Build.0 = Release|x86
		{E374DFAF-95DB-4783-AC2C-6DBAB2C65F51}.Release|x86.ActiveCfg = Release|x86
		{E374DFAF-95DB-4783-AC2C-6DBAB2C65F51}.Release|x86.Build.0 = Release|x86
		{EDF6BEA2-1D2C-45D5-B88B-41CB30F57FD9}.Debug|Any CPU.ActiveCfg = Debug|x86
		{EDF6BEA2-1D2C-45D5-B88B-41CB30F57FD9}.Debug|Mixed Platforms.ActiveCfg = Debug|x86
		{EDF6BEA2-1D2C-45D5-B88B-41CB30F57FD9}.Debug|Mixed Platforms.Build.0 = Debug|x86
		{EDF6BEA2-1D2C-45D5-B88B-41CB30F57FD9}.Debug|x86.ActiveCfg = Debug|x86
		{EDF6BEA2-1D2C-45D5-B88B-41CB30F57FD9}.Debug|x86.Build.0 = Debug|x86
		{EDF6BEA2-1D2C-45D5-B88B-41CB30F57FD9}.Release|Any CPU.ActiveCfg = Release|x86
		{EDF6BEA2-1D2C-45D5-B88B-41CB30F57FD9}.Release|Mixed Platforms.ActiveCfg = Release|x86
		{EDF6BEA2-1D2C-45D5-B88B-41CB30F57FD9}.Release|Mixed Platforms.Build.0 = Release|x86
		{EDF6BEA2-1D2C-45D5-B88B-41CB30F57FD9}.Release|x86.ActiveCfg = Release|x86
		{EDF6BEA2-1D2C-45D5-B88B-41CB30F57FD9}.Release|x86.Build.0 = Release|x86
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
EndGlobal
```

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

- 4.此时查看解决方案的依赖项顺序 就会按照之前写入的工程依赖项，自动生成；

### 向已有解决方案中添加解决方案

- 1.VS自身添加现有项时，选择要添加的SLN 会自动添加SLN内所有的工程到当前解决方案中
- 2.使用Microsoft.Build.BuildEngine.SolutionWrapperProject解析SLN文件，找出所有的Project，然后添加Project，需要从原SLN中获取工程的GUID