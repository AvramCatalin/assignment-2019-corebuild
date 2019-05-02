using System.Collections.Generic;
using System.Xml.Serialization;

namespace CorebuildAssignment
{
    [XmlRoot("planets")]
    public class Planets
    {
        [XmlElement("planet")]
        public List<Planet> Planet { get; set; }
    }
    public class Planet
    {
        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("id")]
        public byte Id { get; set; }

        [XmlElement("modifiers")]
        public Modifiers Modifiers { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }
    }
    public class Modifiers
    {
        [XmlElement("heroAttackModifier")]
        public sbyte HeroAttackModifier { get; set; }

        [XmlElement("heroHealthModifier")]
        public sbyte HeroHealthModifier { get; set; }

        [XmlElement("villainAttackModifier")]
        public sbyte VillainAttackModifier { get; set; }

        [XmlElement("villainHealthModifier")]
        public sbyte VillainHealthModifier { get; set; }
    }
}
