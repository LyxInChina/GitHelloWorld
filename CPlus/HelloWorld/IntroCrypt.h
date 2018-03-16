#pragma once

#ifndef NULL
#ifdef __cplusplus
#define NULL    0
#else
#define NULL    ((void *)0)
#endif
#endif

typedef struct MyIntroStruct
{
	bool BB;
	int C;
	const char* charsA;
	const char* charsB;
	MyIntroStruct(char* charsA = NULL,char* charsB = NULL,int C = 99,bool BB = true);
};

class IntroCrypt
{
public:
	IntroCrypt();
	~IntroCrypt();
};



