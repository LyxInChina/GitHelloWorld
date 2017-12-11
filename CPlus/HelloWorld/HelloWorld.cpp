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


/*Pointer
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
	ָ��Ӽ������� ��Ԫ Ϊ��λ����
	ָ��Ӽ�һ������������һ����ָ�룬����ָ��������ԭָ������һ�£���ָ������Ҳ��ͬ
	����ָ�벻�ܼ����� �Ƿ�����
	����ָ����Լ����㣬������������ͬ

	&��*
	&aȡ��ַ����� �����һ��ָ��,ָ������Ϊa*
	*a�������� �������ָ����
	ָ����ʽ ���ʽ�Ľ����һ��ָ��
	��ֵ ��ָ����ʽ�Ľ��ָ���Ѿ���ȷ������ָ������ռ�ݵ��ڴ�

	������ָ��
	������������ɿ���һ��ָ��
	sizeof(array)				array�������鱾����ʱ�������������С
	sizeof(*array)				array��ʾָ�룬��ʱ�������鵥Ԫ��С
	sizeof(array+n) n=0,1,2...	array��ʾָ�룬���ؽ��Ϊָ���������͵Ĵ�С
	sizeof(ָ������)				����ָ���������͵Ĵ�Ϸ

	ָ����ṹ����
	��������ָ��ṹ���͵�ָ��
	->ָ�������
	����C/C++���������������鵥Ԫʱ�����ǰѸ������鵥Ԫ����������Ĵ洢�ռ����Ԫ�͵�Ԫ֮��û�п�϶
	���ڽṹ�壬��ĳЩ�����������Ҫ�ֽڶ��룬��Ҫ�����ڵ�������Ա���������ֽڣ����³�Ա����ڼ�϶
	
	�ڴ����
	�ṹ��洢���������
	�ڴ����ԭ�򣺱��������ճ�Ա˳��һ����һ����ÿ����Ա�����ڴ棬ֻ�е��洢��Ա������ȷ�ı߽����Ҫ��ʱ��
	��Ա֮����ܳ����������Ķ����ڴ�ռ�
	ԭ��
		1.���ݳ�Ա������򣺽ṹ�����ݳ�Ա����һ�����ݳ�Ա��offsetΪ0��λ�ã��Ժ�ÿ�����ݳ�Ա�洢��ʼλ��Ҫ�Ӹó�Ա��С����������ʼ��
		2.�ṹ����Ϊ��Ա���ṹ���ԱҪ�����ڲ����Ԫ�ش�С����������ַ��ʼ�洢��
		3.��β�������ṹ����ܴ�С�����������ڲ�����Ա��������������Ҫ���룻
	Mark:��ʡ����ռ䣺��ռ�ÿռ�С�����ͷ���ǰ�棬ռ�ÿռ��ķ��ں��棻
	����ϵͳ��Ĭ�϶���ϵ��
	����ϵͳΪ���ٷ����ڴ��ȡ�Ĳ��� - ����ϵͳ�Ի����������ʹ��λ�����ƣ�Ҫ����Щλ�ñ�����ĳ����ֵk��������
	�ڴ�����Ŀ����ʹ���������������͵��׵�ַΪ��Ӧk�ı���

	ָ���뺯��
	ָ������Ϊָ������ָ��

	ָ������ת��
	���԰�һ��ָ��ĵ�ַ��������ȡ��
	Ҳ���Խ�һ��������Ϊ��ַ����һ��ָ��

	ָ��İ�ȫ����
*/
