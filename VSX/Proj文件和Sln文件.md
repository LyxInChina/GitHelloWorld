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

- 