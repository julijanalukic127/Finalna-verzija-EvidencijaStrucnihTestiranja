using EvidencijaStrucnihTestiranja_Sloj_Servisa.DTO;
using EvidencijaStrucnihTestiranja_Sloj_Servisa.Interfejsi;
using EvidencijaStrucnihTestiranja_Sloj_Servisa.Maperi;

namespace EvidencijaStrucnihTestiranja.Prezentacija.SOAP
{
    /// <summary>
    /// CRC:
    /// Odgovornost: Realizuje SOAP servis za čitanje podataka o testiranjima.
    /// Saradnici: ITestiranjeServis, TestiranjeMapper, TestiranjeDTO.
    /// </summary>
    public class TestiranjeSoapServis : ITestiranjeSoapServis
    {
        private readonly ITestiranjeServis testiranjeServis;

        public TestiranjeSoapServis(ITestiranjeServis testiranjeServis)
        {
            this.testiranjeServis = testiranjeServis;
        }

        public List<TestiranjeDTO> VratiSvaTestiranja()
        {
            var testiranja = testiranjeServis.VratiSvaTestiranja();

            return TestiranjeMapper.UListuDTO(testiranja);
        }

        public TestiranjeDTO? PronadjiTestiranje(int id)
        {
            var testiranje = testiranjeServis.PronadjiTestiranje(id);

            if (testiranje == null)
            {
                return null;
            }

            return TestiranjeMapper.UDTO(testiranje);
        }
    }
}