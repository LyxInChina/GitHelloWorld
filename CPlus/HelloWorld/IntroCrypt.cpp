
#include "stdafx.h"
#include "IntroCrypt.h"
#include "stdio.h"
#include "stdlib.h"
#include "windows.h"
#include <iostream>
#include <thread>

#define Random(x)(rand()%x)

MyIntroStruct curStruct;

IntroCrypt::IntroCrypt()
{
}

IntroCrypt::~IntroCrypt()
{
}

extern "C" __declspec(dllexport)
int TestFunc(int a)
{
	printf("%d",a);
	return a * 1000;
}

extern "C" __declspec(dllexport)
bool Funci(int a, int b)
{
	return (a+b)%2==1;
}

extern "C" __declspec(dllexport)
int Funci2(char * c)
{
	return 0x2345;
}

extern "C" __declspec(dllexport)
char * Funcc(int a)
{
	char* c = "abcdef";
	return c;
}

extern "C" __declspec(dllexport)
char Funcc2()
{
	char c = 'g';
	return c;
}

extern "C" __declspec(dllexport)
int GetMyIntroStruct(MyIntroStruct* mystr, int* c)
{
	size_t count = 1;
	*c = (int)count;
	if(mystr == nullptr){
		return -1;
		//mystr = new MyIntroStruct[count];
	}
	for (size_t i = 0; i < *c; i++)
	{
		char buf[32];		
		itoa(i, buf, 10);
		mystr[i].A = false;
		mystr[i].B = true;
		mystr[i].C = (i+2)*100;
		mystr[i].charsA = "LLLLL";
		mystr[i].charsB = "OKOKOK";
		mystr[i].D = 'Q';
	}
	return 0;
}

extern "C" __declspec(dllexport)
int SetMyIntroStruct(MyIntroStruct mystr, int index)
{
	curStruct = mystr;
	printf(curStruct.charsA);
	return 0;
}

void printStr(MyIntroStruct* str)
{
	printf("%d,%d,A::%s,B::%s", str->B, str->C, str->charsA, str->charsB);
}


//MyIntroStruct::MyIntroStruct(char * ca, char * cb, int c, bool b)
//{
//	BB = b;
//	C = c;
//	charsA = ca;
//	charsB = cb;
//}
