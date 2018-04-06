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
using RentaCarros.Fragments.Clientes;
using RentaCarros.Modelos;

namespace RentaCarros.Fragments.Rentas
{
    public class BaseRentaFragment : Fragment
    {
        private Database<Renta> _database;
        private BaseClienteFragment _baseCliente;

        public BaseRentaFragment()
        {
            _database = Database<Renta>.Instance;
            _baseCliente = new BaseClienteFragment();
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
        public IList<RentaCliente> Seleccionar()
        {
            var rentas = _database.Select().ToList();
            return rentas.Select(r => new RentaCliente()
            {
                Id = r.Id,
                CarroId = r.CarroId,
                ClienteId = r.ClienteId,
                ClienteNombre = _baseCliente.Seleccionar(r.ClienteId) != null ? _baseCliente.Seleccionar(r.ClienteId).Nombre : "Not Found",
                FechaRenta = r.FechaRenta,
                FechaRetorno = r.FechaRetorno,
                MetrosRenta = r.MetrosRenta,
                MetrosRetorno = r.MetrosRetorno,
                PrecioDia = r.PrecioDia,
                PrecioKm = r.PrecioKm,
                TarifaDia = r.TarifaDia,
                TarifaKm = r.TarifaKm
            }).ToList();
        }
        public Renta Seleccionar(string clienteId, string carroId)
        {
            var table = _database.Select();
            var result = table.FirstOrDefault(t => t.ClienteId == clienteId && t.CarroId == carroId);
            return result;
        }
        public IList<Renta> Seleccionar(string clienteId)
        {
            var table = _database.Select();
            var result = table.Where(t => t.ClienteId == clienteId).ToList();
            return result;
        }
    }
}