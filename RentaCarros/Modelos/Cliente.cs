﻿using System;
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
    public class Cliente
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
    }
}