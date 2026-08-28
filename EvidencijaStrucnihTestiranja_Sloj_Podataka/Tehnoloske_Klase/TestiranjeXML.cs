using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using EvidencijaStrucnihTestiranja_Sloj_Podataka.Klase_Podataka;

namespace EvidencijaStrucnihTestiranja_Sloj_Podataka.Tehnoloske_Klase
{
    /// <summary>
    /// CRC:
    /// Odgovornost: Serijalizuje podatke o testiranjima u XML format.
    /// Saradnici: Testiranje.
    /// </summary>
    public class TestiranjeXML
    {
        public string KreirajXML(List<Testiranje> testiranja)
        {
            XmlSerializer serializer =
                new XmlSerializer(typeof(List<Testiranje>));

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, testiranja);

                return writer.ToString();
            }
        }
    }
}