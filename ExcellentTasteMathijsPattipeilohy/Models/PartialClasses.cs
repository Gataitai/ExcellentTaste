using System;
using System.ComponentModel.DataAnnotations;

namespace ExcellentTasteMathijsPattipeilohy.Models
{
    [MetadataType(typeof(reserveringmetadata))]
    public partial class Reservering
    {
    }

    [MetadataType(typeof(bestellingmetadata))]
    public partial class Bestelling
    {
    }

    [MetadataType(typeof(menumetadata))]
    public partial class ConsumptieItem
    {
    }

}