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
using RentaCarros.Datos;
using RentaCarros.Modelos;

namespace RentaCarros.Fragments.Rentas
{
    public class BaseRentaFragment : Fragment
    {
        private Database<Renta> _database;

        public BaseRentaFragment()
        {
            _database = Database<Renta>.Instance;
        }

        public int Insertar(Renta obj)
        {
            return _database.Insert(obj);
        }

        public int Actualizar(Renta obj)
        {
            return _database.Update(obj);
        }

        public int Eliminar(Renta obj)
        {
            return _database.Delete(obj);
        }
        public IList<Renta> Seleccionar()
        {
            return _database.Select().ToList();
        }
        public Renta Seleccionar(int clienteId, int carroId)
        {
            var table = _database.Select();
            var result = table.FirstOrDefault(t => t.ClienteId == clienteId && t.CarroId == carroId);
            return result;
        }
    }
}