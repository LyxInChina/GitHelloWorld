using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VSX.Model
{
    /*
 <project>
  <artifactId>Lib</artifactId>
  <version>0.2.5-b1</version>
  <ProjectReference>
    <RName>NO</RName>
  </ProjectReference>
</project>
*/

    [Serializable]
    [XmlRoot("project")]
    public class Project
    {
        [XmlElement("artifactId")]
        public string ArtifactId { get; set; }

        [XmlElement("version")]
        public string Version { get; set; }

        [XmlElement("ProjectReference")]
        public ProjectReference References { get; set; }
    }

    [Serializable]
    public class ProjectReference
    {
         [XmlElement("RName")]
         public List<RName> RNames { get; set; }

    }

    public class RName
    {
        [XmlText]
        public string Value { get; set; }
    }


}
