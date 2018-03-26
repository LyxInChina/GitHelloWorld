using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Base
{
    public class BaseKnowledge
    {
        /// <summary>
        /// 编译器条件编译
        /// </summary>
        [Conditional(conditionString: "Release")]
        static void Fun1()
        {
        }
        
        public interface ITest
        {
            string S { get; set; }
            Delegate MDelegate { get; set; }
            void Func1();
            
        }

        public abstract class BClass
        {
            public virtual string S { get; set; }
            public abstract string AS { get; set; }
        }

    }
}

/*  Base Knowledge
 *  
 *  对象深复制
 *  
 *  序列化
 *  
 *  JSON格式化
 *  
 *  依赖注入和控制反转
 * 
 * 
 */
