
# 参考资料
# '''
# https://www.cnblogs.com/fatlyz/p/4293669.html
# https://blog.csdn.net/c406495762/article/details/78123502
# https://cuijiahua.com/blog/spider/
# https://www.cnblogs.com/xuchunlin/p/8668069.html
# '''

# 导入http请求库
import requests
import lxml
import os
import re
import time
import sys
from bs4 import BeautifulSoup

# 视频列表页
URL="http://www.79yy.cn/type/1/1.html"
# 视频介绍页
URL2="http://www.79yy.cn/show/32276.html"
# 视频播放页
URL3="http://www.79yy.cn/play/32276/0/0.html"
URLX="http://api.yyxzpc.cn/mdparse/m3u8.php?id=https://cn2.zuidadianying.com/20181216/IdYeq4aP/index.m3u8"

def requestsTest():
    # get
    # 给请求指定一个请求头来模拟chrome浏览器
    headers = {
        'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36'}
    rep = requests.get(URL, headers=headers)
    rep = requests.post(URL)

def getPage(url):
    # 给请求指定一个请求头来模拟chrome浏览器
    headers = {
        'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36'}
    req = requests.get(url, headers=headers)
    # req.encoding = 'gb2312'
    # req.encoding = 'gbk'
    # req = requests.get(url)
    soup = BeautifulSoup(req.text, 'lxml')
    return soup

def main():
    lis =[]
    soup = getPage(URL)
    divs = soup.find_all('div',attrs={'class':'col-md-1-5 col-sm-4 col-xs-6'})
    if not divs is None:
        for div in divs:
            # soup2 = BeautifulSoup(div.text,'lxml')
            # if not soup2 is None:
                a = div.find('a')
                if not a is None:
                    lis.append((a.attrs['title'],a.attrs['href']))
    for dd in lis:
        print("Movie Name:"+dd[0])
        print("Movie Url:"+dd[1])

if __name__ == '__main__':
    print("Video Crawler Start!")
    main()
    print("Video Crawler Done!")
    