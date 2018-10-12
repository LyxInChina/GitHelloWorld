using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HelloWorld.Algorithm
{
    public interface ICache<T, TK, TV>
        where T : IDataItem<TK, TV>
    {
        void Clear();
        void Push(T t);
        bool Pop(ref T t);
        void MakeValue(TK tK, out T t);
    }

    public interface IDataItem<TK, TV>
    {
        TK Key { get; set; }
        TV Value { get; set; }
        IDataItem<TK, TV> Make(TK tK, TV tV);
    }

    public class DataItem<TK, TV> : IDataItem<TK, TV>
        where TK : class
    {
        public DataItem()
        {
        }
        public TK Key { get; set; }
        public TV Value { get; set; }
        public IDataItem<TK, TV> Make(TK tK, TV tV)
        {
            return new DataItem<TK, TV>() { Key = tK, Value = tV };
        }
    }

    public abstract class Cache<T, TK, TV> : ICache<T, TK, TV>
        where T : IDataItem<TK, TV>
    {
        public delegate void MakeValueDelegate<TKD, TVD>(TKD tKd, out TVD tvd);
        public delegate void MakeValueDelegate<TD, TKD, TVD>(TKD tKd, out TD td)
            where TD : IDataItem<TKD, TVD>;
        public MakeValueDelegate<TK, TV> MakeValueCallBack;
        public MakeValueDelegate<T, TK, TV> MakeDataItemCallBack;

        public abstract void Clear();

        public virtual void MakeValue(TK tK, out T t)
        {
            if (MakeDataItemCallBack != null)
            {
                MakeDataItemCallBack(tK, out t);
            }
            else
            {
                t = default(T);
            }
        }
        public abstract bool Pop(ref T t);
        public abstract void Push(T t);

    }

    public static class LruFunction
    {
        public static void Sort<T>(LinkedList<T> link, LinkedListNode<T> rNode)
        {
            if (link == null)
            {
                link = new LinkedList<T>();
            }
            link.Remove(rNode);
            link.AddFirst(rNode);
        }

        public static bool SearchData<T, TK, TV>(LinkedList<T> link, TK tK, out LinkedListNode<T> rNode)
            where T : IDataItem<TK, TV>
        {
            if (link.Count > 0)
            {
                return SearchData<T, TK, TV>(link.First, tK, out rNode);
            }
            else
            {
                rNode = null;
                return false;
            }
        }

        public static bool SearchData<T, TK, TV>(LinkedListNode<T> node, TK tK, out LinkedListNode<T> rNode)
            where T : IDataItem<TK, TV>
        {
            if (node != null)
            {
                if (node.Value.Key.Equals(tK))
                {
                    rNode = node;
                    return true;
                }
                else
                {
                    return SearchData<T, TK, TV>(node.Next, tK, out rNode);
                }
            }
            rNode = null;
            return false;
        }

        public static bool SearchHistoryData<T, TK, TV>(LinkedListNode<Tuple<T, int>> node, TK tK, out LinkedListNode<Tuple<T, int>> rNode)
            where T : IDataItem<TK, TV>
        {
            if (node != null)
            {
                if (node.Value.Item1.Key.Equals(tK))
                {
                    rNode = node;
                    return true;
                }
                else
                {
                    return SearchHistoryData<T, TK, TV>(node.Next, tK, out rNode);
                }
            }
            rNode = null;
            return false;
        }
        public static bool SearchHistoryData<T, TK, TV>(LinkedList<Tuple<T, int>> link, TK tK, out LinkedListNode<Tuple<T, int>> rNode)
            where T : IDataItem<TK, TV>
        {
            if (link.Count > 0)
            {
                return SearchHistoryData<T, TK, TV>(link.First, tK, out rNode);
            }
            else
            {
                rNode = null;
                return false;
            }
        }

        public static bool SearchQueue<T, TK, TV>(Queue<T> queue, TK tK, out T t)
            where T : IDataItem<TK, TV>
        {
            var result = false;
            t = default(T);
            if (queue != null && queue.Count > 0)
            {
                var len = queue.Count;
                for (int i = 0; i < len; i++)
                {
                    var temp = queue.Dequeue();
                    if (temp.Key.Equals(tK))
                    {
                        t = temp;
                        result = true;
                    }
                    else
                    {
                        queue.Enqueue(temp);
                    }
                }
            }
            return result;
        }
    }

    public class LRU<T, TK, TV> : Cache<T, TK, TV>
        where T : IDataItem<TK, TV>
    {
        private LinkedList<T> mLink;
        private object mLockObj = new object();
        public uint Cap { get; private set; }

        public LRU(int capacity)
        {
            System.Diagnostics.Contracts.Contract.Requires(capacity > 0);
            Cap = (uint)capacity;
            mLink = new LinkedList<T>();
        }

        public override void Clear()
        {
            mLink.Clear();
        }

        /// <summary>
        /// 访问数据项
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override bool Pop(ref T t)
        {
            t.Value = default(TV);
            LinkedListNode<T> rNode;
            if (LruFunction.SearchData<T, TK, TV>(mLink, t.Key, out rNode))
            {
                //将查找的节点移动到链表头部
                LruFunction.Sort(mLink, rNode);
                return true;
            }
            else
            {
                //若未找到则添加节点到链表头
                MakeValue(t.Key, out t);
                Push(t);
            }
            return false;
        }

        /// <summary>
        /// 添加数据项
        /// </summary>
        /// <param name="t"></param>
        public override void Push(T t)
        {
            while (mLink.Count > Cap)
            {
                mLink.RemoveLast();
            }
            mLink.AddFirst(t);
        }
    }

    public class LRU_K<T, TK, TV> : Cache<T, TK, TV>
        where T : IDataItem<TK, TV>
    {
        private LinkedList<Tuple<T, int>> mLinkedList_his { get; set; }
        private LinkedList<T> mLink { get; set; }

        public uint K { get; private set; }

        public uint Cap { get; private set; }

        public uint CapHistory { get; private set; }

        public LRU_K(int cap, int capHistory, uint k)
        {
            System.Diagnostics.Contracts.Contract.Requires(cap > 0);
            System.Diagnostics.Contracts.Contract.Requires(capHistory > 0);
            System.Diagnostics.Contracts.Contract.Requires(k > 0);
            Cap = (uint)cap;
            CapHistory = (uint)capHistory;
            K = k;
            mLinkedList_his = new LinkedList<Tuple<T, int>>();
            mLink = new LinkedList<T>();
        }

        public override void Clear()
        {
            mLinkedList_his.Clear();
            mLink.Clear();
        }

        private void PushHistory(T t)
        {
            if (mLinkedList_his.Count >= CapHistory)
            {
                //TODO:删除末尾的节点
                mLinkedList_his.RemoveLast();
                PushHistory(t);
            }
            else
            {
                mLinkedList_his.AddFirst(new Tuple<T, int>(t, 0));
            }
        }


        public override bool Pop(ref T t)
        {
            t.Value = default(TV);
            LinkedListNode<T> rNode;
            if (LruFunction.SearchData<T, TK, TV>(mLink, t.Key, out rNode))
            {
                LruFunction.Sort(mLink, rNode);
            }
            else
            {
                LinkedListNode<Tuple<T, int>> rHisNode;
                if (LruFunction.SearchHistoryData<T, TK, TV>(mLinkedList_his, t.Key, out rHisNode))
                {
                    var k = rHisNode.Value.Item2 + 1;
                    if (k >= K)
                    {
                        mLinkedList_his.Remove(rHisNode);
                        Push(rHisNode.Value.Item1);
                    }
                    else
                    {
                        rHisNode.Value = new Tuple<T, int>(rHisNode.Value.Item1, k);
                        LruFunction.Sort(mLinkedList_his, rHisNode);
                    }
                }
                else
                {
                    MakeValue(t.Key, out t);
                    PushHistory(t);
                    return true;
                }
            }
            return false;
        }

        public override void Push(T t)
        {
            while (mLink.Count > Cap)
            {
                mLink.RemoveLast();
            }
            mLink.AddFirst(t);
        }
    }

    public class Two_Queues<T, TK, TV> : Cache<T, TK, TV>
        where T : IDataItem<TK, TV>
    {
        private Queue<T> mHisQueue { get; set; }
        private LinkedList<T> mLink { get; set; }
        private int Cap { get; set; }
        private int CapHistory { get; set; }
        public Two_Queues(int cap, int capHistory)
        {
            Cap = cap;
            CapHistory = capHistory;
            mHisQueue = new Queue<T>();
            mLink = new LinkedList<T>();
        }

        public override void Clear()
        {
            if (mHisQueue != null)
            {
                mHisQueue.Clear();
            }
            if (mLink != null)
            {
                mLink.Clear();
            }
        }

        public override bool Pop(ref T t)
        {
            t= default(T);
            LinkedListNode<T> rNode;
            T temp;
            if (LruFunction.SearchData<T, TK, TV>(mLink, t.Key, out rNode))
            {
                LruFunction.Sort(mLink,rNode);
            }
            else if (LruFunction.SearchQueue<T, TK, TV>(mHisQueue, t.Key, out temp))
            {
                mHisQueue.Enqueue(temp);
                t = temp;
            }
            else
            {
                MakeValue(t.Key,out t);
                mHisQueue.Enqueue(t);
            }
            return false;
        }

        public override void Push(T t)
        {
            T temp;
            if (LruFunction.SearchQueue<T, TK, TV>(mHisQueue, t.Key, out temp))
            {
                while (mLink.Count > Cap)
                {
                    mLink.RemoveLast();
                }
                mLink.AddFirst(t);
            }
            else
            {
                while (mHisQueue.Count > CapHistory)
                {
                    mHisQueue.Dequeue();
                }
                mHisQueue.Enqueue(t);
            }
        }
    }

}
