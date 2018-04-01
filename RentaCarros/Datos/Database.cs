using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RentaCarros.Modelos;
using SQLite;

namespace RentaCarros.Datos
{
    public class Database<T> where T : new()
    {
        private string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
        private SQLiteConnection conn;
        private static Database<T> _instance;

        public static Database<T> Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Database<T>();
                return _instance;
            }
        }

        private Database()
        {
            BootstrapDatabase();
        }

        public int Insert(T obj)
        {
            return conn.Insert(obj);
        }

        public int Update(T obj)
        {
            return conn.Update(obj);
        }

        public int Delete(T obj)
        {
            return conn.Delete(obj);
        }
        public TableQuery<T> Select()
        {
            return conn.Table<T>();
        }
        public TableQuery<T> Select(string id)
        {
            return conn.Table<T>();
        }
        public TableQuery<T> Select(int id)
        {
            var table = conn.Table<T>();
            return table;
        }

        private void BootstrapDatabase()
        {
            if (!File.Exists(path))
            {
                conn = new SQLiteConnection(path);

                conn.CreateTable<Carro>();
                conn.CreateTable<Cliente>();
                conn.CreateTable<Renta>();
                conn.CreateTable<Categoria>();

                conn.Insert(new Categoria()
                {
                    Id = 1,
                    Descripcion = "Vehiculo liviano",
                    PrecioDia = 150000,
                    PrecioKm = 1000,
                    TarifaDia = 120000,
                    TarifaKm = 2000
                });
                conn.Insert(new Categoria()
                {
                    Id = 2,
                    Descripcion = "Carga liviana",
                    PrecioDia = 150000,
                    PrecioKm = 1000,
                    TarifaDia = 120000,
                    TarifaKm = 2000
                });
                conn.InsertAll(new List<Categoria>()
                {
                    new Categoria()
                    {
                        Id = 1,
                        Descripcion = "Vehiculo liviano",
                        PrecioDia = 150000,
                        PrecioKm = 1000,
                        TarifaDia = 120000,
                        TarifaKm = 2000
                    },
                    new Categoria()
                    {
                        Id = 2,
                        Descripcion = "Carga liviana",
                        PrecioDia = 150000,
                        PrecioKm = 1000,
                        TarifaDia = 120000,
                        TarifaKm = 2000
                    }
                });

                conn.InsertAll(new List<Carro>()
                {
                    new Carro()
                    {
                        Id = 1,
                        CategoriaId = 1,
                        Kilometraje = 4,
                        Placa = "811187"
                    },
                    new Carro()
                    {
                        Id = 2,
                        CategoriaId = 2,
                        Kilometraje = 5,
                        Placa = "MRS872"
                    }
                });

                conn.InsertAll(new List<Cliente>
                {
                    new Cliente()
                    {
                        Id = 1,
                        Cedula = "115190794",
                        Nombre = "Manuel Roman"
                    },
                    new Cliente()
                    {
                        Id = 2,
                        Cedula = "116115487",
                        Nombre = "Ellie Roman"
                    }
                });

                conn.InsertAll(new List<Renta>
                {
                    new Renta()
                    {
                        Id = 1,
                        CarroId = 1,
                        ClienteId = 2,
                        FechaRenta = DateTime.Now,
                        FechaRetorno = DateTime.Now.AddMonths(1),
                        MetrosRenta = 5000,
                        MetrosRetorno = 7000,
                        PrecioDia = 15000,
                        PrecioKm = 3000,
                        TarifaDia = 3500,
                        TarifaKm = 4000
                    }
                });
            }
            else
            {
                conn = new SQLiteConnection(path);

                conn.CreateTable<Carro>();
                conn.CreateTable<Cliente>();
                conn.CreateTable<Renta>();
                conn.CreateTable<Categoria>();
            }
        }
    }
}