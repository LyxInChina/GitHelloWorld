// HelloWorld.cpp: 主项目文件。

#include "stdafx.h"//当前路径
#include <iostream>//默认路径
#include <stdio.h>

using namespace System;

void ForEach(int p[], int len);
void Func1(char p[]);
void ArrayAndPointer();
void StructAndPointer();
void TestStructMemoryAllocation();

int main(array<System::String ^> ^args)
{
    Console::WriteLine(L"Hello World");
	
	int res[4] = { 100,3,5,7 };
	ForEach(res, 4);
	char ress[6] = { 'a','b','c','d','e','f' };
	Func1(ress);
	ArrayAndPointer();
	StructAndPointer();
	TestStructMemoryAllocation();
    return 0;
}

void ForEach(int p[],int len)
{
	int* ptr = p;
	for (size_t i = 0; i < len; i++)
	{
		Console::WriteLine(L"index :" + i + L" value :" + *ptr);
		Console::WriteLine(ptr);
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
		Console::WriteLine(L"index :" + i + L" value :" + *pp);
		(*pp)++;
	}

}

void Func1(char ary[])
{
	char* p= ary;
	char** ptr = &p;

	ptr++;//出范围了

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


/*Pointer
	[] ()优先级都比*高

	int p				整型p
	int *p				指针p，指向整型的指针p
	int p[3]			数组p，数组长度为3，元素为整数，即整型数据组成的数组
	int *p[3]			数组p，数组长度为3，元素为指向整型的指针,即整型数据的指针组成的数组
	int (*p)[3]			指针p，指向内容为数组，即指向整型数据的数组的指针
	int **p				指针p，指向内容为指针，指针类型为int，即指向 指向整型的指针 的指针
	int p(int)			函数p，参数为整型，返回值为整型
	int (*p)(int)		指针p，指向内容为函数，函数参数为整型，返回值为整型，即指向 参数为整型返回值为整型的函数 的指针
	int *(*p(int))[3]	函数p，函数参数为整型，返回值为指针，该指针是指向 长度为3的数组-数组元素为指向整型的指针 的指针

	指针的类型		语法上：在声明指针的语句中去掉指着名剩下的部分就是指针类型			
	指针所指向的类型	在指针声明语句中去掉指针名与左侧一个*后剩下的部分
	指针的值（指针所指向的内存区或地址） 指指针本身存储的数值，该值被编译器当做一个地址，32位程序中所有类型指针值都为一个32位的整型（32位程序里内存地址都是32位长度）
		指针所指向的区域为：首地址为指针值，长度为指针所指向的类型的长度（sizeof）
	指针本身所占的内存区大小 sizeof函数测量，32位平台指针长度为4个字节（32位）

	指针算术运算
	四种运算 ++，--，+，-
	指针加减运算以 单元 为单位进行
	指针加减一个整数后结果是一个新指针，该新指针类型与原指针类型一致，所指向类型也相同
	两个指针不能加运算 非法操作
	两个指针可以减运算，但必须类型相同

	&和*
	&a取地址运算符 结果是一个指针,指针类型为a*
	*a间接运算符 结果是所指东西
	指针表达式 表达式的结果是一个指针
	左值 若指针表达式的结果指针已经明确具有了指针自身占据的内存

	数组与指针
	数组的数组名可看做一个指针
	sizeof(array)				array代表数组本身，此时返回整个数组大小
	sizeof(*array)				array表示指针，此时返回数组单元大小
	sizeof(array+n) n=0,1,2...	array表示指针，返回结果为指针自身类型的大小
	sizeof(指针名称)				返回指针自身类型的大戏

	指针与结构类型
	可以声明指向结构类型的指针
	->指向运算符
	所有C/C++编译器在排列数组单元时，总是把各个数组单元存放在连续的存储空间里，单元和单元之间没有空隙
	对于结构体，在某些编译情况下需要字节对齐，需要在相邻的两个成员间添加填充字节，导致成员间存在间隙
	结构体存储分配与对齐
	内存分配原则：编译器按照成员顺序一个接一个给每个成员分配内存，只有当存储成员满足正确的边界对齐要求时，
	成员之间可能出现用于填充的额外内存空间
	原则：
		1.数据成员对齐规则：结构的数据成员，第一个数据成员在offset为0的位置，以后每个数据成员存储起始位置要从该成员大小的整数倍开始；
		2.结构体作为成员，结构体成员要从其内部最大元素大小的整数倍地址开始存储；
		3.收尾工作：结构体的总大小，必须是其内部最大成员的整数倍，不足要补齐；
	Mark:节省对齐空间：将占用空间小的类型放在前面，占用空间大的放在后面；
	操作系统的默认对齐系数
	操作系统为快速访问内存采取的策略 - 操作系统对基本数据类型存放位置限制，要求这些位置必须是某个数值k的整数倍
	内存对齐的目的是使各个基本数据类型的首地址为对应k的倍数

	指针与函数
	指针声明为指向函数的指针

	指针类型转换
	可以把一个指针的地址当做整型取出
	也可以将一个整型作为地址赋予一个指针

	指针的安全问题
*/
