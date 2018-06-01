using System;

namespace HelloWorld.Base
{
    public class SortAlgorithmLib
    {
        /// <summary>
        /// 直接插入排序
        /// </summary>
        /// <typeparam name="T">实现同类型比较接口IComparable<T>类型</typeparam>
        /// <param name="source">待排序队列</param>
        /// <param name="order">排序规则</param>
        public static void DirectInsertSort<T>(ref T[] source,SortOrder order)
            where T:IComparable<T>
        {
            var len = source.Length;
            if (len <= 0)
            {
                return;
            }
            if (order == SortOrder.ASC)
            {
                for (int i = 1; i < len; i++)
                {
                    if (source[i].CompareTo(source[i - 1]) < 0)
                    {
                        var temp = source[i];
                        int j = 0;
                        for (j = i - 1; j >= 0 && temp.CompareTo(source[j]) < 0; --j)
                        {
                            source[j + 1] = source[j];
                        }
                        source[j + 1] = temp;
                    }
                }
            }
            else if (order == SortOrder.DESC)
            {
                for (int i = 1; i < len; i++)
                {
                    if (source[i].CompareTo(source[i - 1]) > 0)
                    {
                        var temp = source[i];
                        int j = 0;
                        for (j = i - 1; j >= 0 && temp.CompareTo(source[j]) > 0; --j)
                        {
                            source[j + 1] = source[j];
                        }
                        source[j + 1] = temp;
                    }
                }
            }

        }

        /// <summary>
        /// 冒泡排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="order"></param>
        public static void BubbleSort<T>(ref T[] source, SortOrder order)
            where T:IComparable<T>
        {
            var len = source.Length;
            if (len<=0)
            {
                return;
            }
            var tmp =source[0];
            if (order==SortOrder.ASC)
            {
                for (int i = 0; i < len; i++)
                {
                    for (int j = len-1; j < i; j--)
                    {
                        if (source[j+1].CompareTo(source[j])>0)
                        {
                            tmp = source[j + 1];
                            source[j + 1] = source[j];
                            source[j] = tmp;
                        }
                    }
                }
            }
            else if (order==SortOrder.DESC)
            {
                for (int i = 0; i < len; i++)
                {
                    for (int j = len - 1; j >= i; j--)
                    {
                        if (source[j + 1].CompareTo(source[j]) < 0)
                        {
                            tmp = source[j + 1];
                            source[j + 1] = source[j];
                            source[j] = tmp;
                        }
                    }
                }
            }
            
        }

        /// <summary>
        /// 简单选择排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="order"></param>
        public static void SimpleSelectSort<T>(ref T[] source, SortOrder order)
            where T : IComparable<T>
        {
            var len = source.Length;
            if (len <= 0)
            {
                return;
            }
            var temp = source[0];
            var t = 0;
            if (order==SortOrder.ASC)
            {
                for (int i = 0; i < len; i++)
                {
                    t = i;
                    for (int j = i + 1; j < len; j++)
                    {
                        if (source[j + 1].CompareTo(source[j]) < 0)
                        {
                            t = j;
                        }
                    }
                    temp = source[i];
                    source[i] = source[t];
                    source[t] = temp;
                }
            }
            else if (order==SortOrder.DESC)
            {
                for (int i = 0; i < len; i++)
                {
                    t = i;
                    for (int j = i + 1; j < len; j++)
                    {
                        if (source[j + 1].CompareTo(source[j]) > 0)
                        {
                            t = j;
                        }
                    }
                    temp = source[i];
                    source[i] = source[t];
                    source[t] = temp;
                }
            }

        }

        /// <summary>
        /// 快速排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="order"></param>
        public static void QuickSort<T>(ref T[] source, SortOrder order)
            where T : IComparable<T>
        {
            var len = source.Length;
            if (len <= 0)
            {
                return;
            }
            if (order==SortOrder.ASC)
            {
                
            }
            else if (order==SortOrder.DESC)
            {
                
            }
        } 

    }

    /// <summary>
    /// 排序规则
    /// </summary>
    public enum SortOrder
    {
        /// <summary>
        /// 升序
        /// </summary>
        ASC=0,
        /// <summary>
        /// 倒序
        /// </summary>
        DESC=1
    }

}
