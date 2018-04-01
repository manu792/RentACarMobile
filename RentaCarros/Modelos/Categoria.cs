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
using SQLite;

namespace RentaCarros.Modelos
{
    public class Categoria
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public double PrecioKm { get; set; }
        public double PrecioDia { get; set; }
        public double TarifaDia { get; set; }
        public double TarifaKm { get; set; }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}