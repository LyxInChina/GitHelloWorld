# MS VSX doc
* VS2017 SDK
 
## 参考资料
 - http://dotneteers.net/blogs/tags/VSX/default.aspx
 - https://blog.csdn.net/liuruxin/article/details/18258749

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
- 要获得Project，需要调用GetProperty(uint32,int,object)方法
- 即GetProperty(VSConstants.VSITEMID_ROOT,VSHPROPID_ExtObject,out object)
- 

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

### VSCT文件

#### VSCT文件

> [VSCT XML架构参考]:(https://docs.microsoft.com/zh-cn/visualstudio/extensibility/vsct-xml-schema-reference)
- 作用：1.定义用户对象、所需资源以及代码绑定行为：2.提供了命令编译架构元素的表，基于XML表配置文件，其中定义了由VSPackage提供的的IDE的命令元素
- 命令元素包括：菜单项、菜单、工具栏、组合窗
- VSCT编译器可以在vsct文件上运行预编译程序，由于是典型的C++预编译程序，所以你可以使用C++文件中的同样的符号来定义自己的包含项和宏命令(macros)
- 可选元素
- 有些VSCT元素是可选的。如果一个Parent参数没有指定，将会暗指Group_Undefined:0。
- 若Icon参数没有指定，则默认为guidOfficeIcon:msotcidNoIcon
- 所有的GUID和ID值都必须使用符号化的名称。这些名称可以定义在头文件中或者vsct文件的Symbols区域内。这些符号名称必须是本都额包含<Include>元素。
- 根元素：CommandTable，指定namespace和xml架构
- 对象标识：guid+数字
- 头文件：*.h 允许从外部加载对象标识
- stdidcmd.h:包含VS公开的所有命令ID，菜单项以cmdid开头，标准编辑器命令以ECMD_开头
- vsshlids.h:包括VS外壳听的菜单命令ID
- msobtnid.h:包括MS office中用到的命令ID
- 可以在Symbols节点为以上GUID和命令ID设定标识

##### CommadnTable元素

- vsct根元素
- 结构
```xml
<CommandTable xmlns="http://schemas.microsoft.com/VisualStudio/2005-10-18/CommandTable" xmlns:xs="http://www.w3.org/2001/XMLSchema" >  
  <Extern>... </Extern>  
  <Include>... </Include>  
  <Define>... </Define>  
  <Commands>... </Commands>  
  <CommandPlacements>... </CommandPlacements>  
  <VisibilityConstraints>... </VisibilityConstraints>  
  <KeyBindings>... </KeyBindings>  
  <UsedCommands... </UsedCommands>  
  <Symbols>... </Symbols>  
</CommandTable> 
```
- 特性:xmlns - 必须的 language- 可选
- Extern 编译器预处理指令，编译时需要合并的外部头文件，该头文件路径必须再Include指定或者VSCT编译器的指定路径，文件可以是其他vsct文件或者C++头文件
- Include 包含在编译的文件路径
- Define 定义更新名称和值的符号
- Commands 定了一所有的VSPackage命令的父元素
- CommandPlacements 定义命令放在命令栏的位置
- VisibilityConstraints 决定了命令和工具栏的静态可见属性
- KeyBindings 指定组合快捷键
- UsedCommands 允许VSPackage实现自己版本的功能并且被其他VSPackage原生支持
- Symbols 包含编译器的所有的符号数据，GUID ID

##### Extern

- 定义格式必须为
```C++
 #define [符号] [值]
 //值也可能是其他符号
```
- 特性：
> href 必须  头文件路径
> Condition 可选
> Language 可选

##### Include

- 指定了要插入当前文件的位于支持路径下的文件，其中所有的元素和定义类型都将成为编译结果的一部分
- VS2010头文件位置：C:\Program Files (x86)\Microsoft Visual Studio 2010 SDK\VisualStudioIntegration\Common\Inc
- VS2017头文件位置：C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\VSSDK\VisualStudioIntegration\Common\Inc
```xml
<Include href="stdidcmd.h" />  
```
- 特性:
> href 必须 头文件路径
> Condition 可选

##### Define

- 定义具有名称和值的符号
```xml
<Define name="Mode" value="Standard" />
```

##### Commands

- 工具栏上命令的集合：菜单、组、按钮、组合、位图
- 每个子元素由命令ID标识：GUID和数字标识对
```xml
<Commands package="GuidMyPackage" >  
  <Menus>... </Menus>  
  <Groups>... </Groups>  
  <Buttons>... </Buttons>  
  <Combos>... </Combos>  
  <Bitmaps>... </Bitmaps>  
</Commands>  
```
- 特性
> package 标识提供命令的VSPackage的GUID

###### Menus 
> 定义所有菜单和工具栏的VSPackage实现
``` xml
<Menus>  
  <Menu>... </Menu>  
  <Menu>... </Menu>  
</Menus>  
```
- Menu
> 定义一个菜单项，有6种菜单：Context，Menu，MenuControler，ToolBar,ToolWindowToolBar
``` xml
<Menu guid="guidMyCommandSet" id="MyCommand" priority="0x100" type="button">  
  <Parent>... </Parent>  
  <CommandFlag>... </CommandFlag>  
  <Strings>... </Strings>  
</Menu>  
```
> 特性
>> GUID 必须，ID 必须
>> priority 可选，指定菜单相对位置的数字值
>> ToolbarPriorityInBand 可选 指定工具栏停靠时工具栏条上相对位置
>> type 可选 指定元素类型，默认值为Menu，
>>> Context 窗口右键菜单；

- Parent 父节点 用于指定菜单位置
- 对于VS中的显示位置的设置
- guid：应该从include中的头文件中进行查找
- ID：也应该从include中的头文件中进行查找，该值指定具体位置
- 对于VS一般从 vsshlids.h头文件中进行查找
  ```xml
  <Parent guid="guidSHLMainMenu" id="IDG_VS_MM_BUILDDEBUGRUN" />
  ```
  |GUID|ID|位置|
  |----|----|----|
  |guidSHLMainMenu|IDG_VS_MM_BUILDDEBUGRUN|主菜单|
  |guidSHLMainMenu|IDG_VS_TOOLS_OPTIONS|工具菜单下拉列表|
  |guidSHLMainMenu|IDG_VS_CTXT_SOLUTION_BUILD|解决方案右键菜单|
  |guidSHLMainMenu|IDG_VS_CTXT_PROJECT_ADD|工程右键菜单|
  |guidSHLMainMenu|IDM_VS_CTXT_REFERENCE|工程引用右键菜单|
  |guidSHLMainMenu|IDM_VS_CTXT_CODEWIN|代码菜单|


###### Groups
> 包含VSPackage命令组入口
``` xml
<Groups>  
  <Group>... </Group>  
  <Group>... </Group>  
</Groups>  
```
- Group
> 定义VSPackage命令的组
``` xml
<Group guid="guidMyCommandSet" id="MyGroup" priority="0x101">  
  <Parent>... </Parent>  
</Group>  
```
###### Buttons
> 表示单个Button元素的组
``` xml
<Buttons>  
  <Button>... </Button>  
  <Button>... </Button>  
</Buttons>  
```
- Button
> 定义按钮相关内容：位置（父节点）、图标、命令、文本内容
``` xml
<Button guid="guidMyCommandSet" id="MyCommand" priority="0x102" type="button">  
  <Parent>... </Parent>  
  <Icon>... </Icon>  
  <CommandFlag>... </CommandFlag>  
  <Strings>... </Strings>  
</Button>  
```

## VSIX打包

### 手动将扩展打包

- 微软官网[链接](https://msdn.microsoft.com/zh-cn/library/ff407026.aspx)
- 
- 创建一个 Visual Studio 扩展，其类型受 VSIX 架构支持。
- 创建一个 XML 文件，将其命名为 extension.vsixmanifest。
- 根据 VSIX 架构填写 extension.vsixmanifest 文件。 有关示例清单，请参阅 PackageManifest 元素（根元素，VSX- 构）。
- 再创建一个 XML 文件，将其命名为 [Content_Types].xml。
- 按 结构的 Content_types].xml 文件 中的指定填写 [Content_Types].xml 文件。
- 将这两个 XML 文件与要部署的扩展一起放在某个目录。
- 如果是项目模板或项模板，则将包含该模板的 .zip 文件放在 XML 文件所在的文件夹中。 不要将 XML 文件放在 .zip - 中。
- 在其他所有情况下，将 XML 文件放在生成输出所在的目录中。
- 在 Windows 资源管理器中，右键单击包含扩展内容和这两个 XML 文件的文件夹，单击“发送到”，然后单击“压缩(zipped)- 夹”。
- 将生成的 .zip 文件重命名为 Filename.vsix，其中 Filename 是用于安装包的可再发行文件的名称。
```xml
<Vsix Version="1.0.0" xmlns="http://schemas.microsoft.com/developer/vsx-schema/2010">
   <Identifier ID="Package ID">
      <AllUsers>... </AllUsers>
      <Author>... </Author>
      <Description>... </Description>
      <GettingStartedGuide>... </GettingStartedGuide>
      <Icon>... </Icon>
      <InstalledByMSI>... </InstalledByMSI>
      <License>... </License>
      <Locale>... </Locale>
      <MoreInfoUrl>... </MoreInfoUrl>
      <Name>... </Name>
      <PreviewImage>... </PreviewImage>
      <SupportedFrameworkRuntimeEdition>... </SupportedFrameworkRuntimeEdition>
      <SupportedProducts>... </SupportedProducts>
      <SystemComponent>... </SystemComponent>
      <Version>... </Version>
    </Identifier>
    <Reference ID="Namespace.ToReference" minversion="1.0" maxversion="2.1">
      <Name>...</Name>
      <MoreInfoUrl>...</MoreInfoUrl>
      <VSIXPath>...</VSIXPath>
    </Reference>
    <Content>
      <Assembly>...</Assembly>
      <CustomExtension>...</CustomExtension>
      <ItemTemplate>...</ItemTemplate>
      <MEFComponent>...<MEFComponent>
      <ProjectTemplate>...</ProjectTemplate>
      <ToolboxControl>...</ToolboxControl>
      <VSPackage>...</VSPackage>
    </Content>
</Vsix>
```
