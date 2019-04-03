using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.GDI
{

    /// <summary>
    /// From DevExpress v18.1 
    /// DevExpress.Utils.v18.1
    /// 用于使用GDI+或者DirectX来 提供方法绘制界面
    /// 并且提供Pens Brushes Fonts的存储
    /// </summary>
    public class GraphicsCache
    {
        private Graphics _graphics { get; set; }

        private HashSet<Pen> pens { get; set; }
        private HashSet<Font> fonts { get; set; }
        private HashSet<Brush> brushes { get; set; }


        [ThreadStatic]
        private static ImageAttributes disabledImageAttr;

        public GraphicsCache(Graphics g)
        {
            _graphics = g;
        }

        /*
         计算Pen Hash
         */


    }

}
