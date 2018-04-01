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

namespace RentaCarros.Fragments.Clientes
{
    public class BaseClienteFragment : Fragment
    {
        private Database<Cliente> _database;

        public BaseClienteFragment()
        {
            _database = Database<Cliente>.Instance;
        }

        public int Insertar(Cliente obj)
        {
            return _database.Insert(obj);
        }

        public int Actualizar(Cliente obj)
        {
            return _database.Update(obj);
        }

        public int Eliminar(Cliente obj)
        {
            return _database.Delete(obj);
        }
        public IList<Cliente> Seleccionar()
        {
            return _database.Select().ToList();
        }
        public Cliente Seleccionar(string id)
        {
            var table = _database.Select(id);
            var result = table.FirstOrDefault(t => t.Cedula == id);
            return result;
        }
    }
}