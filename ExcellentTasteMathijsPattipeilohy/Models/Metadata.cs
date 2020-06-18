using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExcellentTasteMathijsPattipeilohy.Models
{
    public class reserveringmetadata
    {
        [Required(ErrorMessage = "vul datum reservering in")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Reserverings Datum")]
        public DateTime datum { get; set; }

        [DisplayName("Aankomst Tijd")]
        public TimeSpan tijd { get; set; }

        [DisplayName("Tafel")]
        public int tafel { get; set; }

        [DisplayName("Aantal Personen")]
        public int aantalPersonen { get; set; }

        [DisplayName("Status Klant")]
        public int status { get; set; }

        [DisplayName("Status Klant")]
        public int datumToegevoegd { get; set; }

        [DisplayName("Bon Datum")]
        public int bonDatum { get; set; }

        [DisplayName("Betalingswijze")]
        public int betalingswijze { get; set; }

        [DisplayName("Bon Totaal")]
        public int bonTotaal { get; set; }
    }

    public class bestellingmetadata
    {
        public int bestellingId { get; set; }

        public int reserveringId { get; set; }

        public string consumptieItemCode { get; set; }

        public int aantal { get; set; }

        public Nullable<System.DateTime> dateTimeBereidingConsumptie { get; set; }

        public Nullable<decimal> prijs { get; set; }

        public Nullable<decimal> totaal { get; set; }

        public Nullable<bool> isKlaar { get; set; }
    }

    public class menumetadata
    {
        public string consumptieItemCode { get; set; }

        public string consumptieGroepCode { get; set; }

        public string consumptieItemNaam { get; set; }

        [DataType(DataType.Currency)]
        public decimal prijs { get; set; }
    }
}
