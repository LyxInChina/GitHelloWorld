# Targets文件

## 说明

- MSBuild包括几个targets文件，这些文件包括：items，properties，targets，tasks等通用的脚本
- 用途：用于定义工程的构建过程
- 例如：C#工程会导入Microsoft.CSharp.targets,该targets文件会导入Microsoft.Common.targets，
- C#工程会自己定义工程的项和属性，但是C#工程的标准构建规则是通过targets文件导入的
- $(MSBuildToolsPath)值指定了那些通用targets文件的路径，若ToolVersion是4.0，文件位置在<WindowsInstallationPath>\Microsoft.NET\Framework\V4.0.30319\
- 通用targets文件
  > Microsoft.Common.targets:定义C#和VB工程的标准构建过程，被Microsoft.CSharp.targets和Microsoft.VisualBasic.targets文件导入，通过<Import Project="Microsoft.Common.targets" />声明导入；
  > Microsoft.CSharp.targets:定义C#工程的标准构建过程，导入声明：<Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" \>
  > Microsoft.VisualBasic.targets:定义VB工程的标准构建过程，导入声明：<Import Project="$(MSBuildToolsPath)\Microsoft.VisualBasic.targets" \>
- Directory.Build.targets
  > 用户定义文件，提供工程的自定义项，由Common自动导入，可以设置属性ImportDirectoryBuildTargets 为false禁止导入；







## 参考文件

- [MSBuild .targets files](https://docs.microsoft.com/zh-cn/visualstudio/msbuild/msbuild-dot-targets-files?view=vs-2017)

- [MSBuild 目标](https://docs.microsoft.com/zh-cn/visualstudio/msbuild/msbuild-targets?view=vs-2017)

- 