// HelloWorld.cpp: ����Ŀ�ļ���

#include "stdafx.h"//��ǰ·��
#include <iostream>//Ĭ��·��
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

	ptr++;//����Χ��

	std::cout << ptr << std::endl;
	std::cout << *ptr << std::endl;
	std::cout << **ptr << std::endl;
}
 


/*
	pointer

	[] ()���ȼ�����*��

	int p				����p
	int *p				ָ��p��ָ�����͵�ָ��p
	int p[3]			����p�����鳤��Ϊ3��Ԫ��Ϊ������������������ɵ�����
	int *p[3]			����p�����鳤��Ϊ3��Ԫ��Ϊָ�����͵�ָ��,���������ݵ�ָ����ɵ�����
	int (*p)[3]			ָ��p��ָ������Ϊ���飬��ָ���������ݵ������ָ��
	int **p				ָ��p��ָ������Ϊָ�룬ָ������Ϊint����ָ�� ָ�����͵�ָ�� ��ָ��
	int p(int)			����p������Ϊ���ͣ�����ֵΪ����
	int (*p)(int)		ָ��p��ָ������Ϊ��������������Ϊ���ͣ�����ֵΪ���ͣ���ָ�� ����Ϊ���ͷ���ֵΪ���͵ĺ��� ��ָ��
	int *(*p(int))[3]	����p����������Ϊ���ͣ�����ֵΪָ�룬��ָ����ָ�� ����Ϊ3������-����Ԫ��Ϊָ�����͵�ָ�� ��ָ��

	ָ�������		�﷨�ϣ�������ָ��������ȥ��ָ����ʣ�µĲ��־���ָ������			
	ָ����ָ�������	��ָ�����������ȥ��ָ���������һ��*��ʣ�µĲ���
	ָ���ֵ��ָ����ָ����ڴ������ַ�� ָָ�뱾��洢����ֵ����ֵ������������һ����ַ��32λ��������������ָ��ֵ��Ϊһ��32λ�����ͣ�32λ�������ڴ��ַ����32λ���ȣ�
		ָ����ָ�������Ϊ���׵�ַΪָ��ֵ������Ϊָ����ָ������͵ĳ��ȣ�sizeof��
	ָ�뱾����ռ���ڴ�����С sizeof����������32λƽָ̨�볤��Ϊ4���ֽڣ�32λ��

	ָ����������
	�������� ++��--��+��-
	ָ��Ӽ������Ե�ԪΪ��λ����
	ָ��Ӽ�һ������������һ����ָ�룬����ָ��������ԭָ������һ�£���ָ������Ҳ��ͬ
	����ָ�벻�ܼ����� �Ƿ�����
	����ָ����Լ����㣬������������ͬ

	&��*
	&ȡ��ַ����� �����һ��ָ��
	*�������� �������ָ����
	ָ����ʽ ���ʽ�Ľ����һ��ָ��
	��ֵ ��ָ����ʽ�Ľ��ָ���Ѿ���ȷ������ָ������ռ�ݵ��ڴ�

	������ָ��


*/
