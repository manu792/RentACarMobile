using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using RentaCarros.Datos;
using RentaCarros.Modelos;

namespace RentaCarros.Fragments.Carros
{
    public class BaseCarroFragment : Fragment
    {
        private Database<Carro> _database;

        public BaseCarroFragment()
        {
            _database = Database<Carro>.Instance;
        }

        public int Insertar(Carro obj)
        {
            return _database.Insert(obj);
        }

        public int Actualizar(Carro obj)
        {
            return _database.Update(obj);
        }

        public int Eliminar(Carro obj)
        {
            return _database.Delete(obj);
        }
        public IList<Carro> Seleccionar()
        {
            return _database.Select().ToList();
        }
        public Carro Seleccionar(string id)
        {
            var table = _database.Select(id);
            var result = table.FirstOrDefault(t => t.Placa == id);
            return result;
        }
    }
}