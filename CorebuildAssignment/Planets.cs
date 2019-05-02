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
        public int Id { get; set; }

        [XmlElement("modifiers")]
        public Modifiers Modifiers { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }
    }
    public class Modifiers
    {
        [XmlElement("heroAttackModifier")]
        public int HeroAttackModifier { get; set; }

        [XmlElement("heroHealthModifier")]
        public int HeroHealthModifier { get; set; }

        [XmlElement("villainAttackModifier")]
        public int VillainAttackModifier { get; set; }

        [XmlElement("villainHealthModifier")]
        public int VillainHealthModifier { get; set; }
    }
}
