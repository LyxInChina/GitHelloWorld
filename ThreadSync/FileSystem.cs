using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSync
{
    public class FileSystem
    {

    }

    /*
        文件系统实现

    1.文件系统结构
        文件系统的两个设计问题：
            1.如何定义文件系统对用户的接口 - 定义文件、文件属性、文件操作
            2.创建数据结构和算法将逻辑文件系统映射到物理外存设备
        分层设计： 每层利用较低层的功能创建新功能为更高称服务
            设备-》I/O控制-》基本文件系统-》文件组织系统-》逻辑文件系统-》应用程序

        I/O控制为最底层 ： 设备驱动程序 + 中断处理程序 =》内存与磁盘间通信
        基本文件系统 ： 向设备驱动程序发送命令对磁盘上的物理块进行读写（使用实际物理块地址：驱动器 柱面cylinder、磁道 track 扇区sector）
        文件组织模块 file-organization module ： 联系逻辑块与物理块，将逻辑块地址转换成基本文件系统使用的物理块地址
        逻辑文件系统 管理元数据 - 包括文件系统所有的结构数据 通过文件控制块维护文件结构 ；负责保护和安全
        文件控制块 file control block FCB 包含文件信息

        Unix 使用Unix文件系统 UFS - 基于伯克利快速文件系统FFS
        Linux 标准文件系统 可扩展文件系统 extended file system  - ext2 ext3
    2.文件系统实现
        引导控制块 boot control block  - 系统从该卷引导操作系统所需的信息 UFS 称为引导快 NTFS 称为 分区引导扇区
        卷控制块 volume control block - 卷或分区的详细信息 ：分区块数 块大小 空闲块和FCB的数量和指针等 UFS - 超级块 NTFS - 存储在主控文件表中 Master File Table

    分区与安装
        分区： 
        生分区 raw 没有文件系统
        熟分区 cooked 有文件系统
        引导信息 通常为一组有序块，并作为镜像文件读入内存，该镜像文件按照预先指定的位置开始执行
        根分区 root partition 包括操作系统内核或其他系统文件，在引导时装入内存
    虚拟文件系统
        解决问题：
        1.操作系统整合多个文件系统为一个目录结构
        2.访问文件系统空间时，在文件系统间无缝移动
        数据结构 子程序 分开基本系统调用功能和实现细节
        三个层次
            1。文件系统接口 open()/read()/write()/close()/文件描述符
            2。虚拟文件系统VFS
                定义VFS接口，将文件系统通用操作与具体实现分开
                提供网络唯一标识文件机制（vnode文件表示结构）
            3。实现文件系统类型或远程文件系统协议
        Linux VFS结构
            索引节点对象 iNode object 
            文件对象 File object
            超级块对象 superblock object
            目录条目对象 dentry object
        

     */
}
