# UnitTest

- �ο����ϣ�
- [����԰����](http://www.cnblogs.com/yubaolee/p/DotNetCoreUnitTest.html)

## ʹ��MSTest.TestFramework

- ��װʹ��MSTest.TestFramework
- ������.net4.5����
- ��װ��Nuget�����½���������
- �ڲ��������������**[TestClass]**
- �ڲ������͵Ĳ��Է����������**[TestMethod]**

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

- ���Է����ڣ�ʹ��Assert������֤���Է������

### ʹ��Moqģ�����

- �ο����ϣ�
- [moq4 GitHubԴ����](https://github.com/moq/moq4)
- [����԰����](https://www.cnblogs.com/haogj/archive/2011/07/22/2113496.html)
- [����԰����](https://www.cnblogs.com/cgzl/p/9294431.html)

- 1.���Moq���ã�
- 2.��ʼ��Ҫģ��Ľӿڶ���
  - a.��ָ��Mock��Ϊ��Strict���ϸ�δָ������ʱ�׳��쳣��Loose�����ģ�δָ������ʱ����Ĭ��ֵ��Default��Ĭ��ΪLoose
  - b.����ָ������ӿڣ�

```C#
mo.As<T2>();
```

- 3.ָ���ӿڲ����ͷ���ֵ��
  - a.����ʹ��Itָ����������
  - b.���Զ��巵��ֵ����
  - c.���Ը��ݲ�ͬ����ָ���׳��쳣
  - d.���Դ����¼�
- 4.���ã�
- 5.���Խ����

```C#
var mo = new Mock<T>(); 
// �˴�����ָ��Mock��Ϊ����δָ������ʱ��β����׳��쳣���Ƿ���Ĭ��ֵ
//var mo = new Mock<T>(MockBehavior.Strict); 
//ָ�����������ͷ���ֵ
mo.Setup(i=>i.SomeMethod([paras])).Returns([para]);
//���÷���
va r= mo.Object.SomeMethod([paras]);
//���� ���
Assert������
```

### ��������

- 1.��VS��ʹ��MSTest��������MSTest Nuget����д����Է����󣬲����������е�Ԫ������������ԣ�������������������Ŀ****δ�����κ� .NET NuGet ������ ������
  - ������ڲ�����Ŀ������MSTest.TestAdapter,���ɡ�
  - ԭ��Ӣ�İ� [΢�����˵��](https://docs.microsoft.com/zh-cn/visualstudio/test/test-explorer-faq?view=vs-2017)
- 2.00
