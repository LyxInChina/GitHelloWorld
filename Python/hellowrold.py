# note
"""
eg
"""
import os
import re
import time

import itchat
from itchat.content import *

# 程序入口点

if __name__ == '__main__':
    main()

def main():
    a = 10
    b = 'string0'
    print("hello world")
    print(a)
    print(b)
    print(os.getcwd())
    # start_itchat()

# 基础知识
def baseKnowledge():
    # 单行注释
    '''多
    行
    注
    释
    '''
    """多
    行
    注
    释
    """
    # 控制台输出
    print('{_name},okok,{_you}'.format(_name='My', _you='who?'))
    # 控制台输入
    x = input('输入X')
    # 判断语句
    if True:
        print(True)
        elif False:
            print(False)
            else:
                print('ok')
    # 循环
    b = True
    while b:
        b = False
        print('while')
        # 跳过本次循环
        continue
        # 结束循环
        break
        else:
            print('while else')

    for i in range(10):
        print('rang in'+i)
        else:
            print('for in else')
            pass:

    # 导入模块
    import os
# 变量类型
# 整形 int，long（python2中特有）
    x = 1
    print(type(x))
# 虚数型 j
    x1 = 1 + 2j
    print(type(x1))
# bytes p2中bytes与字符串一样 p3中不一样
    b = 'abcd'
    c = b.encode()  # 转化为比特型
    print(type(b))
    print(type(c))
    d = c.decode()  # 转化为字符型
    print(type(d))

def ListType():
# 列表
    lis = [1, 2, 3, 4, 5]
    # 切片 顾头不顾尾
    print(lis[2:2])
    print(lis[-2])
    print(lis[-2:])
    # add
    lis.append(6)
    lis.insert(2, 7)
    # delete
    del lis[2]
    lis.remove(3)
    lis.pop()
    # 查找索引和个数
    index = lis.index(4)
    count = lis.count(4)
    # 改变顺序
    nlis = lis.reverse()
    nlis1 = lis.sort()
    # 复制：深复制 和 浅复制
    slis = lis.copy()  # 浅复制 只复制引用
    import copy
    slis1 = copy.copy(lis)  # 浅复制 只复制引用
    slis2 = list(lis)  # 浅复制 只复制引用
    dlis = copy.deepcopy(lis)  # 深复制
    # 列表合并
    lis2 = [11, 12, 13]
    lis.extend(lis2)  # 并入lis列表后面
    # 高效循环
    step = 1  # 步长
    for i in lis[::step]
        print(i)
    # 元组 列表中存储的信息不能修改,可以切片
    tupl = (1, 2, 3, 4)
    return

# 字符操作
def CharType():
    x = 'who are you'
    xcap = x.capitalize()
    xupp = x.upper()
    xlow = x.lower()
    xtit = x.title()
    xswa = x.swapcase()
    x.replace('o', '0')
    # 设置替换密文
    tran = x.maketrans('h', '1')
    x.translate(tran)  # 替换加密
    # 文本填充对齐
    x.center(14,'*')
    x.ljust(16,'|')
    x.rindex(18,'@')
    x.zfill(20)# 文本居右 用0填充
    # 去掉空格
    y = '  who are you  '
    y.lstrip()
    y.rstrip()
    y.strip()
    # 查询
    res = y.find('r')
    res = y.rfind('r')
    # 拆分成列表
    res = y.split()
    res = y.split('o')
    res = y.splitlines()
    # 判断
    res = y.endswith('u') # 是否以参数结尾
    res = y.isalnum()# 是否为阿拉伯数字
    res = y.isalpha()# 是否为纯英文
    res = y.isdigit()# 是否为整型
    res = y.isidentifier()# 是否为合法的标识符-变量名
    return

# 字典类型
def DictionaryType():
    dic = { '1':'aa','2':'bb','3':'cc'}
    dic['2']='ee'
    # 删除
    dic.pop('2')
    dic.popitem()
    # 查找
    res = dic.get('2')
    res = '2' in dic
    # 合并
    dic2 = {'a'：11,'b':22,'c':33}
    dic.update(dic2)
    return

# 集合类型
def SetType():
    sets = set([1, 2])
    lists = [1, 2, 3, 1, 2]
    # 去掉重复项
    sets1 = set(lists)
    sets = sets1
    # add
    sets.add(1)
    sets.update([4, 5])
    # delete
    # 不存在会报错
    sets.remove(2)
    sets.remove(2)
    # 不存在不会报错
    sets.discard(2)
    sets.pop()

    # 集合操作
    s1 = set([1, 2, 3, 4, 5, 6])
    s2 = set([1, 2, 3, 11, 12, 13])
    # 交集
    si1 = s1.intersection(s2)
    si2 = s1 & s2
    # 并集
    su1 = s1.union(s2)
    su2 = s1 | s2
    # 差集
    sd1 = s1.difference(s2)
    sd2 = s1 - s2
    # 对称差集 所有A集合和B集合的共有元素之外的其他元素
    ss1 = s1.symmetric_difference(s2)
    ss2 = s1 ^ s2
    ss3 = (s1 - s2) + (s2 - s1)
    # 判断是否为子集
    res1 = s1.issubset(s2)
    res2 = s1 in s2
    # 判断是否为父集
    res3 = s1.issuperset(s2)
    # 未相交判断
    res4 = s1.isdisjoint(s2)
    res5 = (s1 & s2) == null
    return

def fileOperation():
    # 打开文件
    file = open('text.txt','r')
    file.close()
    with file = open('t.txt','r'):
        descriptor = file.fileno()
        #opearte file

    with open('t.txt','r') as f:
        descriptor = f.fileno()
    
    with open('t.txt','r') as f,\
        open('t1.txt','r') as fn:
        descriptor = f.fileno()
        descriptor1 = fn.fileno()
    
    # 读文件
    lis = f.readlines() # 读取全部数据 每行一个元素
    res =[]
    line = f.readline()
    while line:
        res.append(line)
        line = f.readline()
    # 读取指针位置
    pos = f.tell()# 返回指针当前位置
    f.seekable(pos)# 将指针移动到参数位置，0表示起点
    f.seek()

    return

def testOs():
    curPath = os.getcwd()
    dirs = os.listdir(curPath)
    files = os.system(curPath)
    sep = os.sep
    print(curPath)
    print(dirs)
    print(files)
    print(sep)







# this is mark
