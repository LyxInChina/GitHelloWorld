# VSX基本概念

## 什么是Visual Studio Package

- 构建Visual Studio的基本单元，可以通过Package扩展VS IDE：服务，界面元素，编辑器，设计器，项目；
- 每个Package必须被PLK签名
- 实现了IVsPackage接口的类型

## VS调试

- 选择启动类型未类库
- 调试选项中-启动外部程序，选择VS程序：C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\IDE\devenv.exe
- 附加启动参数：/rootsuffix Exp
- 若调试启动的VS新实例为：实验实例，则进入正确的插件调试模式；

## 服务

- Package之间或者与Package相关对象的契约
- 区分：全局服务-global service，本地服务-local service

### 使用服务

- 服务是隐藏的，即必须通过名字进行调用，即查找服务提供文档，
- 使用服务必须知道的信息：服务的名字，接口的名字
- [可用服务列表](https://docs.microsoft.com/zh-cn/visualstudio/extensibility/internals/list-of-available-services)

### 加载Package和访问服务

- 按需加载：仅加载被调用的Package
- Siting VSPackage:提供全局服务的service provider
- 访问全局服务：
  - 1.使用sit对象，通过GetService访问全局服务；
  - 2.得到VSPackage实例，访问；
  - 3.通过静态方法：Package.GetGlobalService

### 注册服务

- Package定义类添加特性：ProvideServiceAttribute

### Interoperability程序集和Managed Package Framework

- Interoperability程序集：使用.net类型包装了com类型，
- 使用VSX中com对象方法：1.使用非托管代码；2.使用Introp程序集；
- MPF：在com intreop程序集上建立的框架
- VSX中的Interop程序集：
- VS SDK中的Microsoft.VisualStudio*系列
- MPF中的Shell系列

### 命令

- [CommandEventsClass](https://docs.microsoft.com/zh-cn/dotnet/api/envdte.commandeventsclass?view=visualstudiosdk-2017)

#### 监听命令的执行

- 1.查询命令列表：EnvDTE.DTE.Commands
  
```C#
foreach(Command cmd in DTE.Commands)
{
    //TODO:Print cmd information
}
```

- 2.使用接口CommandEvents监听命令
- 接口CommandEvents：可以使用实例EnvDTE.DTE.Events.CommandEvents
- 订阅接口的BeforeExecute事件或者AfterExecute事件可以在命令执行前或命令执行后，捕捉到事件发生；

#### 插件运行过程中动态修改菜单项显示内容

- 1.在Package初始化加载命令是 使用OleMenuCommand包装命令CommandID
```C#
	OleMenuCommand ocmd = new OleMenuCommand(UpdateRuntimeEnvironmentMenuItemClick, updateRuntimeEnvironmentCommandID);
    ocmd.BeforeQueryStatus += new EventHandler(ocmd_BeforeQueryStatus);
    mcs.AddCommand(ocmd);
```
- 2.订阅OleMenuCommand实例的BeforeQueryStatus事件:

- 3.在订阅的事件参数中：通过sender强转为OleMenuCommand实例，通过该实例即可修改菜单项内容
```C#
   void ocmd_BeforeQueryStatus(object sender, EventArgs e)
   {
       var myCommand = sender as OleMenuCommand;
       if (null != myCommand)
       {
           if (mListenCmdEvents)
           {
               myCommand.Text = "停止监听事件";
           }
           else
           {
               myCommand.Text = "开始监听事件";
           }
       } 
   }
```
- 4.还需要VSCT文件中针对要修改的Button项，否则会出错，添加参数：<CommandFlag>TextChanges</CommandFlag>
- [CommandFlag元素说明](https://docs.microsoft.com/zh-cn/visualstudio/extensibility/command-flag-element)
```XML
    <Button guid="guidXaptoolsCmdSet" id="cmdidUpdateRuntimeEnv" priority="0x108" type="Button">
    <Parent guid="guidXaptoolsGroup" id="idXaptoolsOptionsGroup" />
    <Icon guid="guidImageReload" id="pngReload" />
    <CommandFlag>TextChanges</CommandFlag>
    <Strings>
        <CommandName>cmdidUpdateRuntimeEnvironment</CommandName>
        <ButtonText>开始监听事件</ButtonText>
    </Strings>
    </Button>
```
#### 命令执行

- 通过代码的方式执行命令
- 1.使用IOleCommandTarget，通过可用服务列表中获取；
  - 访问全局服务：
  - 1.使用sit对象，通过GetService访问全局服务；
  - 2.得到VSPackage实例，访问；
  - 3.通过静态方法：Package.GetGlobalService
- 2.调用IOleCommandTarget中的Exec方法；

#### 命令对象说明
- COM接口：Command - 基础命令对象，仅有GUID，ID，通过接口获得
- 托管对象：CommandID - 托管命令对象，通过GUID和ID构建
- 托管对象：MenuCommand - 命令菜单项，可以更改部分命令外观属性
- 托管对象：OleMenuCommand - 命令菜单项，可以更改命令外观属性
