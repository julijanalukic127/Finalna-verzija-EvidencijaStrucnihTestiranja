using EvidencijaStrucnihTestiranja_Sloj_Podataka.Klase_Podataka;


namespace EvidencijaStrucnihTestiranja_Sloj_Servisa.Interfejsi
{
    /// <summary>
    /// CRC:
    /// Odgovornost: Definiše operacije servisnog sloja za rad sa testiranjima.
    /// Saradnici: TestiranjeServis, Testiranje.
    /// </summary>
    public interface ITestiranjeServis
    {
        bool DodajTestiranje(Testiranje novoTestiranje);

        List<Testiranje> VratiSvaTestiranja();

        Testiranje? PronadjiTestiranje(int id);

        bool IzmeniTestiranje(int id, Testiranje izmenjenoTestiranje);

        bool ObrisiTestiranje(int id);
    }
}