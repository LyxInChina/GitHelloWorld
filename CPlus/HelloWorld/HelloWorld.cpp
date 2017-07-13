// HelloWorld.cpp: 主项目文件。

#include "stdafx.h"//当前路径
#include <iostream>//默认路径
#include <stdio.h>

using namespace System;

void ForEach(int p[], int len);
void Func1(char p[]);
int main(array<System::String ^> ^args)
{
    Console::WriteLine(L"Hello World");
	int t1 = 998;
	int *pt = &t1;
	Console::WriteLine(&t1);
	Console::WriteLine(pt);
	Console::WriteLine(*pt);
	Console::WriteLine(&pt);

	char a[3] = { 'a','c','f' };
	char * p = (char *)a;
	Console::WriteLine(L" p is:" + *p);
	p++; 
	Console::WriteLine(L" p is:" + *p);	
	p = p + sizeof(char);
	Console::WriteLine(L" p is:" + *p);
	
	int res[4] = { 100,3,5,7 };
	ForEach(res, 4);

	char ress[6] = { 'a','b','c','d','e','f' };
	Func1(ress);

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
 


/*
	pointer

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
	指针加减运算以单元为单位进行
	指针加减一个整数后结果是一个新指针，该新指针类型与原指针类型一致，所指向类型也相同
	两个指针不能加运算 非法操作
	两个指针可以减运算，但必须类型相同

	&和*
	&取地址运算符 结果是一个指针
	*间接运算符 结果是所指东西
	指针表达式 表达式的结果是一个指针
	左值 若指针表达式的结果指针已经明确具有了指针自身占据的内存

	数组与指针


*/
