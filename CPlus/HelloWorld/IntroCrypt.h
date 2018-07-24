#pragma once

#ifndef NULL
#ifdef __cplusplus
#define NULL    0
#else
#define NULL    ((void *)0)
#endif
#endif

typedef struct IntroStruct
{	
	const char* charsA;
	const char* charsB;
	int C;
	bool A;
	bool B;
	char D;
	//MyIntroStruct(char* charsA,char* charsB,int C,bool BB);
};

class IntroCrypt
{
public:
	IntroCrypt();
	~IntroCrypt();
};



