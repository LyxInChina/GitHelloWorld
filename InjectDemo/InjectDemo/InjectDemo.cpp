// InjectDemo.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <windows.h>
using namespace std;


int _tmain(int argc, _TCHAR* argv[])
{
	int pid;
	void *pNativeDllRemote;
	FARPROC pLoadLibrary;
	//TCHAR szNativeDllPath[_MAX_PATH]=_T("D:\\Code\\InjectDemo\\Debug\\NativeDll.dll");
	TCHAR szNativeDllPath[_MAX_PATH] = _T("F:\\ZhangLiZhong\\Demons\\CSharp\\InjectDemo\\Debug\\NativeDll.dll");

	//F:\ZhangLiZhong\Demons\CSharp\InjectDemo\Debug
	cout<<"input the process id to inject"<<endl;
	cin>>pid;
	
	HANDLE hProcess = OpenProcess(PROCESS_ALL_ACCESS,0,pid);
	if(hProcess==0)
		return 1;
	
	HMODULE hKernel32 = ::GetModuleHandle(_T("Kernel32"));
	if(sizeof(TCHAR)==2)
		pLoadLibrary= ::GetProcAddress(hKernel32,"LoadLibraryW"); //if path is unicode, use "LoadLibraryW"
	else
		pLoadLibrary= ::GetProcAddress(hKernel32,"LoadLibraryA");
	pNativeDllRemote=VirtualAllocEx(hProcess,NULL,sizeof(szNativeDllPath),MEM_COMMIT,PAGE_READWRITE);
	::WriteProcessMemory(hProcess,pNativeDllRemote,(void*)szNativeDllPath,sizeof(szNativeDllPath),NULL);
	HANDLE hThread = CreateRemoteThread(hProcess,NULL,0,(LPTHREAD_START_ROUTINE)pLoadLibrary,pNativeDllRemote,0,NULL);
	::WaitForSingleObject(hThread,INFINITE);
	//DWORD exitcode;
	//GetExitCodeThread(hThread,&exitcode);
	::CloseHandle(hThread);
	return 0;
}

