using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HelloWorld.Algorithm
{
    

    public interface ICache<TK, TV>
    {
        void Clear();
        void Push(TK tK, TV tV);
        bool Pop(TK tK, out TV tV);
        TV MakeValue(TK tK);
    }

    public class LRU<TK, TV> : ICache<TK, TV>
    {
        private LinkedList<Tuple<TK, TV>> mLruLinkedList;
        private object mLockObj = new object();
        public uint Capacity { get; private set; }

        public LRU(int capacity)
        {
            System.Diagnostics.Contracts.Contract.Requires(capacity > 0);
            Capacity = (uint)capacity;
            mLruLinkedList = new LinkedList<Tuple<TK, TV>>();
        }

        public void Clear()
        {
            mLruLinkedList.Clear();
        }

        /// <summary>
        /// 添加数据项
        /// </summary>
        /// <param name="tK"></param>
        /// <param name="tV"></param>
        public void Push(TK tK, TV tV)
        {
            if (mLruLinkedList.Count >= Capacity)
            {
                //TODO:删除末尾的节点
                mLruLinkedList.RemoveLast();
                Push(tK, tV);
            }
            else
            {
                mLruLinkedList.AddFirst(new Tuple<TK, TV>(tK, tV));
            }
        }

        /// <summary>
        /// 访问数据项
        /// </summary>
        /// <param name="tK"></param>
        /// <param name="tV"></param>
        /// <returns></returns>
        public bool Pop(TK tK, out TV tV)
        {
            tV = default(TV);
            LinkedListNode<Tuple<TK, TV>> rNode;
            if (SearchData(mLruLinkedList, tK, out rNode))
            {
                //将查找的节点移动到链表头部
                Sort(mLruLinkedList, rNode);
                return true;
            }
            else
            {
                //若未找到则添加节点到链表头
                var tv = MakeValue(tK);
                Push(tK, tv);
                tV = tv;
            }
            return false;
        }

        public static void Sort<T>(LinkedList<T> link, LinkedListNode<T> rNode)
        {
            if (link == null)
            {
                link = new LinkedList<T>();
            }
            link.Remove(rNode);
            link.AddFirst(rNode);
        }

        public bool SearchData(LinkedList<Tuple<TK, TV>> link, TK tK, out LinkedListNode<Tuple<TK, TV>> rNode)
        {
            if (link.Count > 0)
            {
                return SearchData(link.First, tK, out rNode);
            }
            else
            {
                rNode = null;
                return false;
            }
        }

        public bool SearchData(LinkedListNode<Tuple<TK, TV>> node, TK tK, out LinkedListNode<Tuple<TK, TV>> rNode)
        {
            if (node != null)
            {
                if (node.Value.Item1.Equals(tK))
                {
                    rNode = node;
                    return true;
                }
                else
                {
                    return SearchData(node.Next, tK, out rNode);
                }
            }
            rNode = null;
            return false;
        }

        public TV MakeValue(TK tK)
        {
            return default(TV);
        }
    }

    public class LRU_K<TK, TV> : ICache<TK, TV>
    {
        private LinkedList<Tuple<TK, TV,int>> mLinkedList_his { get; set; }
        private LinkedList<Tuple<TK,TV>> mLinkedList { get; set; }

        public uint K { get; private set; }

        public uint Capacity { get; private set; }

        public LRU_K(int capacity,uint k)
        {
            System.Diagnostics.Contracts.Contract.Requires(capacity > 0);
            System.Diagnostics.Contracts.Contract.Requires(k > 0);
            Capacity = (uint)capacity;
            K = k;
            mLinkedList_his = new LinkedList<Tuple<TK, TV, int>>();
            mLinkedList = new LinkedList<Tuple<TK, TV>>();
        }

        public void Clear()
        {
            mLinkedList_his.Clear();
            mLinkedList.Clear();
        }

        public TV MakeValue(TK tK)
        {
            return default(TV);
        }

        public bool Pop(TK tK, out TV tV)
        {
            tV = default(TV);
            LinkedListNode<Tuple<TK, TV>> rNode;
            if (SearchData(mLinkedList,tK,out rNode))
            {
                Sort(mLinkedList, rNode);
            }
            else
            {
                LinkedListNode<Tuple<TK, TV,int>> rHisNode;
                if (SearchHistoryData(mLinkedList_his,tK,out rHisNode))
                {
                    var k = rHisNode.Value.Item3 + 1;
                    if (k >= K)
                    {
                        mLinkedList_his.Remove(rHisNode);
                        Push(rHisNode.Value.Item1, rHisNode.Value.Item2);
                    }
                    else
                    {
                        rHisNode.Value = new Tuple<TK, TV, int>(rHisNode.Value.Item1, rHisNode.Value.Item2, k);
                        Sort(mLinkedList_his, rHisNode);
                    }
                }
                else
                {
                    TV tv = MakeValue(tK);
                    PushHistory(tK, tv);
                    tV = tv;
                    return true;
                }
            }
            return false;
        }

        public void Push(TK tK, TV tV)
        {
            if (mLinkedList.Count >= Capacity)
            {
                //TODO:删除末尾的节点
                mLinkedList.RemoveLast();
                Push(tK, tV);
            }
            else
            {
                mLinkedList.AddFirst(new Tuple<TK, TV>(tK, tV));
            }
        }

        public void PushHistory(TK tK, TV tV)
        {
            if (mLinkedList_his.Count >= Capacity)
            {
                //TODO:删除末尾的节点
                mLinkedList_his.RemoveLast();
                PushHistory(tK, tV);
            }
            else
            {
                mLinkedList_his.AddFirst(new Tuple<TK, TV, int>(tK, tV, 0));
            }
        }

        public static void Sort<T>(LinkedList<T> link, LinkedListNode<T> rNode)
        {
            if (link == null)
            {
                link = new LinkedList<T>();
            }
            link.Remove(rNode);
            link.AddFirst(rNode);
        }

        public bool SearchData(LinkedList<Tuple<TK, TV>> link, TK tK, out LinkedListNode<Tuple<TK, TV>> rNode)
        {
            if (link.Count > 0)
            {
                return SearchData(link.First, tK, out rNode);
            }
            else
            {
                rNode = null;
                return false;
            }
        }

        public bool SearchHistoryData(LinkedList<Tuple<TK, TV, int>> link, TK tK, out LinkedListNode<Tuple<TK, TV, int>> rNode)
        {
            if (link.Count > 0)
            {
                return SearchHistoryData(link.First, tK, out rNode);
            }
            else
            {
                rNode = null;
                return false;
            }
        }

        public bool SearchData(LinkedListNode<Tuple<TK, TV>> node, TK tK, out LinkedListNode<Tuple<TK, TV>> rNode)
        {
            if (node != null)
            {
                if (node.Value.Item1.Equals(tK))
                {
                    rNode = node;
                    return true;
                }
                else
                {
                    return SearchData(node.Next, tK, out rNode);
                }
            }
            rNode = null;
            return false;
        }

        public bool SearchHistoryData(LinkedListNode<Tuple<TK, TV, int>> node, TK tK, out LinkedListNode<Tuple<TK, TV, int>> rNode)
        {
            if (node != null)
            {
                if (node.Value.Item1.Equals(tK))
                {
                    rNode = node;
                    return true;
                }
                else
                {
                    return SearchHistoryData(node.Next, tK, out rNode);
                }
            }
            rNode = null;
            return false;
        }

    }

}
