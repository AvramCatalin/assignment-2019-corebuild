using System.Collections.Generic;
using System.Xml.Serialization;

namespace CorebuildAssignment
{
    [XmlRoot("characters")]
    public class Characters
    {
        [XmlElement("character")]
        public List<Character> Character { get; set; }
    }
    public class Character
    {
        [XmlElement("attack")]
        public short Attack { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("health")]
        public short Health { get; set; }

        [XmlElement("id")]
        public byte Id { get; set; }

        [XmlElement("isVillain")]
        public bool IsVillain { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

    }
}
