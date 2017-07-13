using Icl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace WcfService1
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ClassA : IServiceA
    {
        public string GetDataA(int value)
        {
            return value.ToString() + "_ok";
        }

        public CompositeType GetDataUsingDataContractA(CompositeType composite)
        {
            return new CompositeType() { BoolValue = !composite.BoolValue, StringValue = composite.StringValue.GetHashCode().ToString() };
        }
    }
}