
C++ ��̬��ʾ��
��ʹ��crpycppΪ����

1.ʹ���ⲿC++�⣻
	ʹ�ö�̬�⣺
	�ⲿ��ͷ�ļ���lib�ļ���dll�ļ�
	���ͷ�ļ�����Ŀ--����--c/c++--��������Ŀ¼=�����ͷ�ļ�Ŀ¼��
	���lib�ļ�����Ŀ--����--������--����--���ӿ�Ŀ¼=�����lib���ļ�Ŀ¼��
	ָ������ļ�����Ŀ--����--������--����--����������=�����������lib�ļ�����
	��Ҫ��Ϸ��Ҫ���������ĳ������ݿ��ļ�����Ŀ--����--������--����--���������ĳ������ݿ��ļ����ǣ�

2.�Զ���DLL���ܣ�
	
3.��ӵ���������
	ʹ�� extern "C" __declspec(dllexport) ���Ҫ�����ĺ���
	eg:
	extern "C" __declspec(dllexport)
	int Test(int a)
	{
		return a*100;
	}
	ʹ��.defģ���ļ�
	ʹ��Depends���߲鿴�����ĺ�������Щ��

4.���ɶ�̬�⣻
	�Զ����ƽű� --- ����Ŀ����ʱ�Զ��������������
	ʹ��Depends���߲鿴�����⣻
	����Ȼȱ�������⣬ʹ��processmonitor���߽�����ϣ�
	���������¼���ӣ�
	copy "$(ProjectDir)lib\x64\*.*" "$(OutputPath)*.*"

5.�ⲿ����ʱ�����е���
	���ӽ��е���
	��Ҫ����C++�⣬��Ҫ�����ڵ�pdb�ļ����Ƶ���Ŀ·���£�Ȼ����и��ӵ���

6.C#��ʹ�ã�
	[DllImport(DLLPATH, EntryPoint = "GetNdiName")]
    public static extern void GetNdiName(IntPtr sources,ref int count);
-----------------------------------------------------------------------
-----------------------------------------------------------------------
-----------------------------------------------------------------------

Mark:

7.C++��������ֵ���������
	1.����ֵ����/�ṹ��
		ֵ���ͣ���C#����������ͣ�
		�ṹ�壺��C#�����Ӧ�Ľṹ�壺(���ݽṹ���ڴ�ռ���Ҫ��C#�߼�������)
			C++�������� Fun(struct * s,int * i);//����ṹ��s������i��
			C#�������� Fun(IntPtr s,ref int i);
	2.�����������ͣ�
	//�������ݽṹ
	[StructLayoutAttribute(LayoutKind.Sequential)]//ָ�����ݽṹ�ڴ�ֲ�Ϊ˳��洢����
    public struct NDIlib_source_t
    {   
        public IntPtr p_ndi_name;//�ַ�������
        public IntPtr p_ip_address;//�ַ�������
    }
		1.������Ҫȷ������ֵ�Ŀռ��С��������Ԫ�ش�С*Ԫ�ظ�����
			Ԫ�ش�С��
			eg��int size = Marshal.SizeOf(typeof(NDIlib_source_t));
				int totalsize = count*size;
		2.��ָ�����ռ䣺
			eg: IntPtr data = Marshal.AllocHGlobal(total);
		3.����C++������������ֵ���ݷŵ�C#ָ����ָ��ռ��У�
		4.��ȡ���ݲ�ת��ΪC#�ж�����������ͣ�
			dataָ��ƫ��һ�����ݽṹ��
			InPtr res = IntPtr.Add(data, size);
			//����ת��
			var src = (NDIlib_source_t)Marshal.PtrToStructure(res, typeof(NDIlib_source_t))

	8.C++ʹ�ÿⷽʽ��
		1.���øÿ�ľ�̬�⣨static��-Static��
		2.���ÿ�Ķ�̬�⣨dll��-DLL-ONLY��
		3.ͬʱʹ�þ�̬��Ͷ�̬��-DLL-Import,��ʱ�������п����루MT/MTD����


