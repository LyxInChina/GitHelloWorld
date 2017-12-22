// HelloWorld.cpp: ����Ŀ�ļ���

#include <iostream>//Ĭ��·��
#include <stdio.h>

#define AUDIO32_t 0x7fffffff	


void ForEach(int p[], int len);
void Func1(char p[]);
void ArrayAndPointer();
void StructAndPointer();
void TestStructMemoryAllocation();


int main(int argc, char* argv[])
{
    printf("Hello World");
	float ff = 1.2f;
	float *p_ch = &ff;
	int32_t sf = (int32_t)((*p_ch) * (float)AUDIO32_t);

	printf("%d", sf);

	//int res[4] = { 100,3,5,7 };
	//ForEach(res, 4);
	//char ress[6] = { 'a','b','c','d','e','f' };
	//Func1(ress);
	//ArrayAndPointer();
	//StructAndPointer();
	//TestStructMemoryAllocation();
    return 0;
}

void ForEach(int p[],int len)
{
	int* ptr = p;
	for (size_t i = 0; i < len; i++)
	{
		//printf("index :" + i + L" value :" + *ptr);
		//printf(ptr);
		ptr++;
		//int temp = sizeof(*ptr);
		int sss = sizeof(ptr);
		int64_t s = (int64_t)p;
		s = s + sizeof(int);
		printf("%x\n", s);
	}

	int *pp = p;
	for (size_t i = 0; i < len; i++)
	{
		//Console::WriteLine(L"index :" + i + L" value :" + *pp);
		(*pp)++;
	}

}

void Func1(char ary[])
{
	char* p= ary;
	char** ptr = &p;

	ptr++;//����Χ��

	std::cout << ptr << std::endl;
	std::cout << *ptr << std::endl;
	std::cout << **ptr << std::endl;
}

void ArrayAndPointer()
{
	int array[3] = { 123,456,789};
	printf("array: %d\n", sizeof(array));
	printf("*array: %d\n", sizeof(*array));
	printf("*(array+0):%d\n", sizeof(*(array+0)));
	printf("array+0:%d\n", sizeof(array + 0));
}

struct MyStruct
{
	int a;
	char b;
	int c[3];
	char d[3];
};

struct MyStruct1
{
	int a;
	char b;
	int c[3];
	char d[3];
};

struct MyStruct2
{
	char b[5];//5->8
	int a;//4
	char c;//1->4
	//=8+4+4=16
};

struct MyStruct3
{
	char b[3]; //3->4
	int a;		//4
	char c;//1->4
	//=4+4+4=12
};

struct MyStruct4
{
	char b[13];//13->16
	int a;//4 ->8
	double d;//8
	char c;//1->8
	//= 16+8+8+8 = 40
};

struct MyStruct5
{
	double d;//8
	char b[13];//13->16 
	int a;//4
	char c;//1->4
	// = 8+16+4+4=32
};

void StructAndPointer()
{
	struct MyStruct ms = { 22,'a',{123,456,789},{'x','y','z'} };
	struct MyStruct *p = &ms;
	printf("%d\n", p->a);
	printf("%d\n", p->b);
	printf("%d\n", p->c);
	printf("%d\n", p->d);	
}

void TestStructMemoryAllocation()
{
	printf("_______________________\n");
	printf("%d\n", sizeof(MyStruct2));
	printf("%d\n", sizeof(MyStruct3));
	printf("%d\n", sizeof(MyStruct4));
	printf("%d\n", sizeof(MyStruct5));
	MyStruct2 s = { {'a','b','c','d','e'},9,'o' };
	MyStruct2 *p = &s;
	printf("%p\n", &p->b);
	printf("%p\n", &p->a);
	printf("%p\n", &p->c);
}


