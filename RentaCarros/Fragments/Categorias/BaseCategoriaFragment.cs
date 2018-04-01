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
using SQLite;

namespace RentaCarros.Fragments.Categorias
{
    public class BaseCategoriaFragment : Fragment
    {
        private Database<Categoria> _database;

        public BaseCategoriaFragment()
        {
            _database = Database<Categoria>.Instance;
        }

        public int Insertar(Categoria obj)
        {
            return _database.Insert(obj);
        }

        public int Actualizar(Categoria obj)
        {
            return _database.Update(obj);
        }

        public int Eliminar(Categoria obj)
        {
            return _database.Delete(obj);
        }
        public IList<Categoria> Seleccionar()
        {
            return _database.Select().ToList();
        }
        public Categoria Seleccionar(int id)
        {
            var table = _database.Select(id);
            var result = table.FirstOrDefault(t => t.Id == Convert.ToInt32(id));
            return result;
        }
    }
}