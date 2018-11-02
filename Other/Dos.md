# DOS 命令
```DOS
:: 注释
REM
set name=$(TargetName)-%Date:~0,4%%Date:~5,2%%Date:~8,2%%Time:~0,2%%Time:~3,2%.vsix
COPY $(TargetName).vsix %name%
```

https://github.com/zodiacon/WindowsInternals
https://docs.microsoft.com/zh-cn/sysinternals/downloads/