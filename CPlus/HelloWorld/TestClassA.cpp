#include "stdafx.h"
#include "TestClassA.h"
#include <iostream>


extern "C" __declspec(dllexport)
int GetStrLen(char* str)
{
	size_t len = strlen(str);
	return (int)len;
}

extern "C" __declspec(dllexport)
char GetASCIIChar(int a)
{
	if (a >= 48 && a <= 126)
	{
		return toascii(a);
	}
	else 
	{
		return '!';
	}
}
