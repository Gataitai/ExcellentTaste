//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExcellentTasteMathijsPattipeilohy.Models
{
    using System;
    
    public partial class GET_TODAYS_RESERVATIONS_Result
    {
        public string klantNaam { get; set; }
        public string telefoon { get; set; }
        public System.TimeSpan tijd { get; set; }
        public int tafel { get; set; }
        public int aantalPersonen { get; set; }
        public System.DateTime datumToegevoegd { get; set; }
        public int reserveringId { get; set; }
        public int status { get; set; }
        public Nullable<decimal> BonBestellingen { get; set; }
    }
}