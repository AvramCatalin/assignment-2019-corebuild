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
        public int Attack { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("health")]
        public int Health { get; set; }

        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("isVillain")]
        public bool IsVillain { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

    }
}
