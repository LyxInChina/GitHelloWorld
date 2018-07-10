# MS VS doc
* VS2017 SDK

## EnvDTE接口说明
> [EnvDTE]:(https://docs.microsoft.com/zh-cn/dotnet/api/?redirectedfrom=MSDN&view=visualstudiosdk-2017)
### 接口DTE
- DLL:EnvDTE.dll
- NP:EnvDTE
- VS顶级自动化对象模型
- 获取：GetService<typeof(DTE)>
### 接口IVsSolutionEvents
- Dll:Microsoft.VisualStudio.Shell.Interop.dll
- NP:Microsoft.VisualStudio.Shell.Interop
- 监听解决方案通知的接口，用于追踪解决方案或者解决方案的工程的打开，关闭，加载，卸载事件
- 解决方案和项的，打开，关闭，加载，卸载有本质的不同
- 使用 查询SVsSolution对象为IVsSolution接口，调用AdviseSolutionEvents方法，返回一个指向IVsSolutionEvents的指针；
- 使用
```
    //服务提供者
    IServiceProvider provider = Instance of Implement to Package(一般为一个package示例)
    //获取IVsSolution接口示例
    IVsSolution solution = IServiceProvider.GetService(typeof(SVsSolution));
    //
    solution.AdviseSolutionEvents(ptr_IVsSolution,out uint)
```
### 接口IVsHierarchy
- Dll:Microsoft.VisualStudio.Shell.Interop.dll
- NP:Microsoft.VisualStudio.Shell.Interop
- 提供VSPackages的层次管理的工程层次的实现
- 层次节点的通用接口，每个节点都包含根节点，有专用的属性与之关联。层次结构对象的每个节点用一个VSITEMID标识。标识对于本接口可见，而且是一个典型的指针，指向被层次派生类的私有数据
- VSITEMID 是一个dword类型的唯一标识
### 接口Soulutio
- DLL:EnvDTE.dll
- NP:EnvDTE
- 在IDE内提供所有的工程以及解决方案维度的属性
### 接口Project
- DLL:EnvDTE.dll
- NP:EnvDTE
- 通用工程对象
- 特定的工程对象在其他程序集内， VB和VC#在VSLangProj命名空间
- 类似于抽象类型，提供所有其他具体工程的通用信息
### 接口VSProject
- DLL:VSLangProj
- NP:VSLangProj.dll
- 包含VB或者VC#工程的特定信息，可以从Project.Object强转，若该Object是VSProject类型
### 接口Reference 
- DLL:VSLangProj
- NP:VSLangProj.dll
- 标识工程中的一个引用，在工程中可以使用任何引用中包含的公共成员，可以是.Net工程，.Net 程序集，COM对象
### 接口

## VSX基本概念

### 什么是Visual Studio Package

- 构建Visual Studio的基本单元，可以通过Package扩展VS IDE：服务，界面元素，编辑器，设计器，项目；
- 每个Package必须被PLK签名
- 实现了IVsPackage接口的类型

### 服务

- Package之间或者与Package相关对象的契约
- 区分：全局服务-global service，本地服务-local service

#### 使用服务

- 服务是隐藏的，即必须通过名字进行调用，即查找服务提供文档，
- 使用服务必须知道的信息：服务的名字，接口的名字

#### 加载Package和访问服务

- 按需加载：仅加载被调用的Package
- Siting VSPackage:提供全局服务的service provider
- 访问全局服务：1.使用sit对象，通过GetService访问全局服务；2.得到VSPackage实例，访问；3.通过静态方法：Package.GetGlobalService

#### 注册服务

- Package定义类添加特性：ProvideServiceAttribute

#### Interoperability程序集和Managed Package Framework

- Interoperability程序集：使用.net类型包装了com类型，
- 使用VSX中com对象方法：1.使用非托管代码；2.使用Introp程序集；
- MPF：在com intreop程序集上建立的框架
- VSX中的Interop程序集：
- VS SDK中的Microsoft.VisualStudio*系列
- MPF中的Shell系列

#### VSCT文件
#### VSCT文件

> [VSCT XML架构参考]:(https://docs.microsoft.com/zh-cn/visualstudio/extensibility/vsct-xml-schema-reference)
- 作用：1.用户对象和所需资源，代码绑定行为
- 根元素：CommandTable，指定namespace和xml架构
- 对象标识：guid+数字
- 头文件：*.h 允许从外部加载对象标识
- stdidcmd.h:包含VS公开的所有命令ID，菜单项以cmdid开头，标准编辑器命令以ECMD_开头
- vsshlids.h:包括VS外壳听的菜单命令ID
- msobtnid.h:包括MS office中用到的命令ID
- 可以在Symbols节点为以上GUID和命令ID设定标识
- 