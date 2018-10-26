# CH22 CLR寄宿和AppDomain

- 寄宿hosting：允许是任务应用程序能利用CLR的功能，允许享有的应用程序部分使用托管代码编写；
- AppDomain：允许第三方的不受信任的代码在一个现有的进程中允许，CLR保证数据结构 代码 安全上下文不会被滥用或破坏；

## 1.CLR寄宿

- 为什么所有托管代码和程序集文件必须使用Windows PE文件格式(EXE文件或者DLL文件)？
- .net Framework构建在Windows平台顶部，Miscrosoft将CLR实现为一个包含在DLL中的COM服务器，即：为CLR定义标准COM接口，然后分配GUID，
- 参照：MetaHost.h头文件，该文件中定义了GUID和ICLRMetaHost接口；
- 如何创建CLR COM服务实例？
- 应该调用MetaHost.h文件中声明的CLRCreateInstance函数（返回ICLRMetaHost接口），该函数在MSCorEE.dll(shim)文件中实现，其本身不包含CLR COM服务器；
  - MSCorEE.dll只有一个版本，与最新版本的CLR一致，对于x64 Windows 会有两个版本的MSCorEE.dll x86版本在SysWOW64下另一个在System32下；
  - CLR代码位置：
    - 1.0和2.0在MSCorWks.dll
    - 4.0 在Clr.dll
- ICLRMetaHost中函数GetRuntime指定宿主创建的CLR版本，然后垫片MSCorEE.dll将CLR加载到宿主进程中
- 对于启动的托管程序，垫片会提前应用程序的CLR版本信息，可以在XML文件中修改：requiredRuntime以及supportRuntime覆盖默认行为
- GetRuntime返回一个ICLRRuntimeInfo指针，使用该指针的GetInstance方法获取ICLRRuntimeHost接口：
  - ICLRRuntimeHost接口：
    - 1.设置宿主管理器
    - 2.获取CLR管理器
    - 3.初始化并启动CLR
    - 4.加载一个程序集并执行其中代码
    - 5.停止CLR
- 创建CLR COM服务实例
> 使用 MetaHost.h->调用 CLRCreateInstance(MSCorEE.dll) -> 返回 接口ICLRMetaHost -> 调用 GetRuntime(传入CLR版本) -> 返回指针ICLRRuntimeInfo
> -> 调用方法 GetInstance -> 获取ICLRRuntimeHost接口 ->该接口定义CLR相关操作
- 使用ClrVer.exe检测给定进程中的加载的CLR版本
- CLR加载到进程中后，不能卸载，除非终止进程
- 一个进程中可以加载多个版本CLR，兼容运行
- CLR宿主使用 参考: Customizing the Microsoft .Net Framework Common Language Runtime --- by Steven Pratschners

## 2.AppDomain

- AppDomain功能：
  - 1.不能直接访问其他AppDomain中的代码，按引用封送，按值封送；
  - 2.AppDomain可以卸载；
  - 3.AppDomain可以单独保护，可以设置最大权限集；
  - 4.AppDomain可以单独实施配置：加载程序集的方式；
- 为什么不支持卸载程序集？
> [参考文档](https://blogs.msdn.microsoft.com/jasonz/2004/05/31/why-isnt-there-an-assembly-unload-method/)
>> 1.可能正在应用程序域中运行该程序集内的代码，在程序集级别跟踪代码引用太昂贵
>> 2.即使已经跟踪了所有的代码引用，但是释放程序集也只能释放原数据和IL，JIT中的代码还在保留在AppDomain的Loader Heap内，JIT申请的代码在调用buffer内是连续的；
>> 3.

- 每个AppDomain都有自己的Loader堆，每个Loader堆记录自AppDomain创建后已访问过的类型。Loader堆中每个类型对象都有一个方法表，方法表中的每个记录项指向JIT编译的本地代码（方法至少执行过一次）
- 多个AppDomain中加载同一个DLL，使用该DLL中同一个类型时
  - 不共享类型对象，每个AppDomain都会为同一个类型分配类型对象
  - 不共享本地代码，调用同一个类型的方法时，由IL代码经过JIT编译后生产的本地代码（native code）与每个AppDomain相关联
  - 不共享类型静态字段，由多个AppDomain使用的类型在每个AppDomain中都有一组静态字段
- AppDomain中立，特例：MSCorLib.dll，该DLL提供CLR基础类型库，所有AppDomain共享该程序集中的类型，以AppDomain中立方式进行加载，该AppDomain维护一个特殊的Loader堆，代价：永远不能被卸载，除非终止进程
- 跨AppDomain边界访问对象
  - 按引用封送
  - 按值封送
  - 完全不能封送

