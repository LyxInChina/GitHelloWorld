using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Base
{
    [TestClass]
    public class TypeConverter_Test
    {
        [TestInitialize]
        public void Init()
        {
            //1.注册转换器和转换类型
            TypeDescriptor.AddAttributes(typeof(MyView),new TypeConverterAttribute(typeof(MyViewConverter)));
        }

        [TestMethod]
        public void Main_Test()
        {
            var typeConvert = TypeDescriptor.GetConverter(typeof(MyView));
            Assert.IsTrue(typeConvert.GetType() == typeof(MyViewConverter));
            var myView = new MyView();
            var yourView = new YourView();
        }
    }

    [ComVisible(true)]
    [TypeConverter(typeof(MyViewConverter))]
    public class MyView
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
    }

    [ComVisible(true)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class YourView
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
    }

    public class MyViewConverter: TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if(sourceType == typeof(int))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if(value.GetType() == typeof(int))
            {
                return new MyView()
                {
                    Age = (int)(value)
                };
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(MyView))
            {
                var view = (MyView)value;
                return view;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        //public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        //{
        //    return base.GetProperties(context, value, attributes);
        //}

        //public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        //{
        //    return base.GetStandardValues(context);
        //}

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return base.GetPropertiesSupported(context);
        }

    }

}
