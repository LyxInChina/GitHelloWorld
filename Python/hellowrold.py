#! note
"""
eg
"""
import os
import re
import time

import itchat
from itchat.content import *


def main():
    a = 10
    b = 'string0'
    print("hello world")
    print(a)
    print(b)
    print(os.getcwd())
    testOs()
    # fib(100)
    #start_itchat()

def testSet():
    sets = set([1,2])
    lists = [1,2,3,1,2]
    #去掉重复项
    sets1 = set(lists)
    sets = sets1
    #add
    sets.add(1)
    sets.update([4,5])
    #delete
    #不存在会报错
    sets.remove(2)
    sets.remove(2)
    #不存在不会报错
    sets.discard(2)
    sets.pop()

    #集合操作
    s1 = set([1,2,3,4,5,6])
    s2 = set([1,2,3,11,12,13])
    #交集
    si1 = s1.intersection(s2)
    si2 = s1 & s2
    
 
def testOs():
    curPath = os.getcwd();
    dirs = os.listdir(curPath);
    files = os.
    sep = os.sep;
    print(curPath)
    print(files)
    print(sep)

def fib(max):
    a, b = 0, 1
    while a < max:
        a=a+1
    yield

@itchat.msg_register([TEXT])
def text_reply(msg):
    res = re.search('ok', msg['Text'])
    if res:
        itchat.send(('fine thanks'), msg['FromUserName'])
    else:
        itchat.send(('are you ok?'), msg['FromUserName'])
    


@itchat.msg_register([PICTURE, RECORDING, VIDEO, SHARING])
def other_msg(msg):
    itchat.send(('hello?'), msg['FromUserName'])



def start_itchat():
    itchat.auto_login(enableCmdQR=True,hotReload=True)
    itchat.run()

if __name__ == '__main__':
    main()

# this is mark
