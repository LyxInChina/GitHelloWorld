using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VSX.Model
{
    /*<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" protocolVersion="3" />
    <add key="本地nuget源" value="http://localhost:8080/nuget/" />
    <add key="nuget_mirror" value="http://172.18.44.43:8081/nexus/service/local/nuget/Nuget_Mirror/" />
    <add key="nuget_hosted" value="http://172.18.44.43:8081/nexus/service/local/nuget/Nuget_Hosted_Symbols/" />
  </packageSources>
  <activePackageSource>
    <add key="All" value="(Aggregate source)" />
  </activePackageSource>
  <packageRestore>
    <add key="enabled" value="True" />
    <add key="automatic" value="True" />
  </packageRestore>
  <bindingRedirects>
    <add key="skip" value="False" />
  </bindingRedirects>
  <packageManagement>
    <add key="format" value="0" />
    <add key="disabled" value="False" />
  </packageManagement>
</configuration>*/

    [Serializable]
    [XmlRoot("configuration")]
    public class Nuget
    {
        [XmlElement("packageSource")]
        public AddCollection PkgSource { get; set; }

        [XmlElement("activePackageSource")]
        public AddCollection ActivePkg { get; set; }

        [XmlElement("packageRestore")]
        public AddCollection PkgRestore { get; set; }

        [XmlElement("bindingRedirects")]
        public AddCollection BindingRedic { get; set;}

        [XmlElement("packageManagement")]
        public AddCollection PkgManagement { get; set; }

        [XmlElement("disabledSources")]
        public AddCollection DisabledSources { get; set; }
    }

    [Serializable]
    public class AddCollection
    {
        [XmlElement("add")]
        public List<Add> Adds { get; set; }
    }

    [Serializable]
    public class Add
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }

        [XmlAttribute("protocolVersion")]
        public string ProtocolVersion { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
