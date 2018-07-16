using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VSX.Model
{
    /*
<?xml version="1.0" encoding="utf-8"?>
<module>
  <id>client.LibN</id>
  <version>0.2.5-b1</version>
  <libraries>
    <library>
      <id>Lib</id>
      <version>0.2.5-b1</version>
      <scope>compile</scope>
    </library>
    <library>
      <id>NO</id>
      <version>0.2.5-b1</version>
      <scope>compile</scope>
    </library>
  </libraries>
  <dependencies>
    <dependency>
      <id>EntityFramework</id>
      <version>6.0.0</version>
    </dependency>
    <dependency>
      <id>xap.mw.core</id>
      <version>0.2.5-b1</version>
    </dependency>
    <dependency>
      <id>AForge</id>
      <version>2.2.5</version>
    </dependency>
  </dependencies>
</module>*/
    
    [Serializable]
    [XmlRoot("module")]
    public class Module
    {
        [XmlElement("id")]
        public string ID { get; set; }

        [XmlElement("version")]
        public string Version { get; set; }

        [XmlElement("libraries")]
        public LibraryCollection Librars { get; set; }

        [XmlElement("dependencies")]
        public DenpencyCollection Denpencies { get; set; }
    }

    [Serializable]
    public class LibraryCollection
    {
        [XmlElement("library")]
        public List<Library> Libraries { get; set; }
    }

    [Serializable]
    public class DenpencyCollection
    {
        [XmlElement("dependency")]
        public List<Dependency> Dependencies { get; set; }
    }

    [Serializable]
    public class Library
    {
        [XmlElement("id")]
        public string ID { get; set; }

        [XmlElement("version")]
        public string Version { get; set; }

        [XmlElement("scope")]
        public Scope Scope { get; set; }
    }

    [Serializable]
    public class Dependency
    {
        [XmlElement("id")]
        public string ID { get; set; }

        [XmlElement("version")]
        public string Version { get; set; }
    }

    public enum Scope
    {
        Runtime = 0,
        Compile = 1,
    }
}
