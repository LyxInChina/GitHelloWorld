#include "stdafx.h"
#include "IntroCrypt.h"
#include "stdio.h"
#include "stdlib.h"
#include "windows.h"
#include <iostream>
#include <thread>

#define Random(x)(rand()%x)

IntroStruct curStruct;

IntroCrypt::IntroCrypt()
{
}

IntroCrypt::~IntroCrypt()
{
}

extern "C" __declspec(dllexport)
int Add(int a, int b)
{
	return a + b;
}

extern "C" __declspec(dllexport)
char * GetString(int a)
{
	char* c = "abcdef";
	return c;
}

int GetStrLen(char* str) 
{
	size_t len = strlen(str);
	return (int)len;
}

extern "C" __declspec(dllexport)
char GetChar()
{
	char c = 'g';
	return c;
}

extern "C" __declspec(dllexport)
int GetMyIntroStruct(IntroStruct* mystr, int* c)
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
int GetIntroStructs(IntroStruct* mystr, int* c)
{
	size_t count = rand() % 20;
	*c = (int)count;
	if (mystr == nullptr) {
		return -1;
	}
	for (size_t i = 0; i < *c; i++)
	{
		char buf[32];
		itoa(i, buf, 10);
		mystr[i].A = false;
		mystr[i].B = true;
		mystr[i].C = (i + 2) * 100;
		mystr[i].charsA = "LLLLL";
		mystr[i].charsB = "OKOKOK";
		mystr[i].D = 'Q';
	}
	return 0;
}

extern "C" __declspec(dllexport)
int SetMyIntroStruct(IntroStruct mystr, int index)
{
	curStruct = mystr;
	printf(curStruct.charsA);
	return 0;
}

void printStr(IntroStruct* str)
{
	printf("%d,%d,A::%s,B::%s", str->B, str->C, str->charsA, str->charsB);
}

