using EvidencijaStrucnihTestiranja_Sloj_Podataka.Klase_Podataka;

namespace EvidencijaStrucnihTestiranja_Sloj_Poslovne_Logike
{
    /// <summary>
    /// CRC:
    /// Odgovornost: Primena poslovnih pravila i provera podataka zaposlenih.
    /// Saradnici: Zaposleni.
    /// </summary>
    public class ZaposleniLogika
    {
        public bool ProveriJMBG(string jmbg)
        {
            return !string.IsNullOrWhiteSpace(jmbg)
                   && jmbg.Length == 13
                   && jmbg.All(char.IsDigit);
        }
        public bool ProveriPodatkeZaposlenog(Zaposleni zaposleni)
        {
            if (zaposleni == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(zaposleni.Ime) ||
                string.IsNullOrWhiteSpace(zaposleni.Prezime) ||
                string.IsNullOrWhiteSpace(zaposleni.RadnoMesto) ||
                string.IsNullOrWhiteSpace(zaposleni.Email))
            {
                return false;
            }

            if(!ProveriJMBG(zaposleni.JMBG))
{
                return false;
            }

            if (!ProveriEmail(zaposleni.Email))
            {
                return false;
            }

            return true;

        }
        public bool ProveriEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            return email.Contains("@") && email.Contains(".");
        }
    }


}