namespace EvidencijaStrucnihTestiranja.Prezentacija.Models
{
    /// <summary>
    /// CRC:
    /// Odgovornost: Čuva podatke potrebne za prikaz informacija o grešci.
    /// Saradnici: PocetnaController, Error pogled.
    /// </summary>
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
