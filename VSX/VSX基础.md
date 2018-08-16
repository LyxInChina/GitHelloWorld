# VSX基本概念

## 什么是Visual Studio Package

- 构建Visual Studio的基本单元，可以通过Package扩展VS IDE：服务，界面元素，编辑器，设计器，项目；
- 每个Package必须被PLK签名
- 实现了IVsPackage接口的类型

## 服务

- Package之间或者与Package相关对象的契约
- 区分：全局服务-global service，本地服务-local service

### 使用服务

- 服务是隐藏的，即必须通过名字进行调用，即查找服务提供文档，
- 使用服务必须知道的信息：服务的名字，接口的名字

### 加载Package和访问服务

- 按需加载：仅加载被调用的Package
- Siting VSPackage:提供全局服务的service provider
- 访问全局服务：1.使用sit对象，通过GetService访问全局服务；2.得到VSPackage实例，访问；3.通过静态方法：Package.GetGlobalService

### 注册服务

- Package定义类添加特性：ProvideServiceAttribute

#### Interoperability程序集和Managed Package Framework

- Interoperability程序集：使用.net类型包装了com类型，
- 使用VSX中com对象方法：1.使用非托管代码；2.使用Introp程序集；
- MPF：在com intreop程序集上建立的框架
- VSX中的Interop程序集：
- VS SDK中的Microsoft.VisualStudio*系列
- MPF中的Shell系列
