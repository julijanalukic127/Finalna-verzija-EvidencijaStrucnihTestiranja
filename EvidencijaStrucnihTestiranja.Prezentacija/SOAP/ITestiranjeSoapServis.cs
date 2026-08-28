using System.ServiceModel;
using EvidencijaStrucnihTestiranja_Sloj_Servisa.DTO;

namespace EvidencijaStrucnihTestiranja.Prezentacija.SOAP
{
    /// <summary>
    /// CRC:
    /// Odgovornost: Definiše SOAP operacije za pristup podacima o testiranjima.
    /// Saradnici: TestiranjeSoapServis, TestiranjeDTO.
    /// </summary>
    [ServiceContract]
    public interface ITestiranjeSoapServis
    {
        [OperationContract]
        List<TestiranjeDTO> VratiSvaTestiranja();

        [OperationContract]
        TestiranjeDTO? PronadjiTestiranje(int id);
    }
}