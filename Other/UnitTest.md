# UnitTest

- 参考资料：
- http://www.cnblogs.com/yubaolee/p/DotNetCoreUnitTest.html 

## 使用MSTest.TestFramework

- 安装使用MSTest.TestFramework
- 条件：.net4.5以上
- 安装完Nuget包后，新建测试类型
- 在测试类型添加特性**[TestClass]**
- 在测试类型的测试方法添加特性**[TestMethod]**

```C#
[TestClass]
public class TestMain
{
    [TestMethod]
    public void TestITarget()
    {
    }
}
```

- 测试方法内：使用Assert断言验证测试方法结果

### 使用Moq模拟测试

- 参考资料：
- https://www.cnblogs.com/haogj/archive/2011/07/22/2113496.html
- https://github.com/moq/moq4
- https://www.cnblogs.com/cgzl/p/9294431.html

- 1.添加Moq引用；
- 2.初始化要模拟的接口对象：
  - a.可指定Mock行为：Strict：严格，未指定操作时抛出异常；Loose：宽泛的，未指定操作时返回默认值；Default：默认为Loose
  - b.可以指定多个接口：mo.As<T2>();
- 3.指定接口参数和返回值；
  - a.可以使用It指定参数规则
  - b.可以定义返回值规则
  - c.可以根据不同参数指定抛出异常
  - d.可以触发事件
- 4.调用；
- 5.断言结果；

```C#
var mo = new Mock<T>(); 
// 此处可以指定Mock行为：即未指定方法时如何操作抛出异常还是返回默认值
//var mo = new Mock<T>(MockBehavior.Strict); 
//指定方法参数和返回值
mo.Setup(i=>i.SomeMethod([paras])).Returns([para]);
//调用方法
va r= mo.Object.SomeMethod([paras]);
//断言 结果
Assert。。。
```
