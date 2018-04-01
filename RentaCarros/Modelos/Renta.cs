using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RentaCarros.Modelos
{
    public class Renta
    {
        public int Id { get; set; }
        public int CarroId { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaRenta { get; set; }
        public DateTime FechaRetorno { get; set; }
        public int MetrosRenta { get; set; }
        public int MetrosRetorno { get; set; }
        public double PrecioKm { get; set; }
        public double PrecioDia { get; set; }
        public double TarifaDia { get; set; }
        public double TarifaKm { get; set; }
    }
}