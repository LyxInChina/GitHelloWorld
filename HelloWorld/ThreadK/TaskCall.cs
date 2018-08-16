using System;
using System.Threading;
using System.Threading.Tasks;

namespace HelloWorld.ThreadK
{
    public class TaskCall
    {
        private static readonly bool _IsLoadStubAssembly = false;
        private static readonly object _loadStubAssemblytLock = new object();

        private static readonly Task _loadStubTask;
        private static readonly Thread _loadStubThread = new Thread(Do_Work);
        private static readonly CancellationTokenSource tokenSource = new CancellationTokenSource();

        private static readonly Action _action;

        private static void Do_Work()
        {
            try
            {
                _action?.Invoke();
            }
            catch (Exception)
            {

            }
        }

        public static void DoWorkAsync<T>(Action<T> action)
        {

        }

    }

    public interface AsyncCall
    {
        void RunAsync(Action action, Action callBack);
        void RunAsync(Action action);
        void RunAsync(Action action, object param);
    }

}
