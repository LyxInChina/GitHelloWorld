#include "BeautyofProgramming.h"
#include "stdafx.h"

#include <string>
#include <utility>

BeautyofProgramming::BeautyofProgramming()
{
}


BeautyofProgramming::~BeautyofProgramming()
{
}
class TheNumberOne
{
public:
	TheNumberOne();
	~TheNumberOne();
	int CalcNumerOne(int n)
	{
		if (n <= 0)
			return 0;
		int res = 0;
		int temp = n;
		while (temp/10>0)
		{
			temp = temp % 10;

		}

		return res;
	}
private:

};

TheNumberOne::TheNumberOne()
{
}

TheNumberOne::~TheNumberOne()
{
}

