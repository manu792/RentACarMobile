﻿using Android.App;
using Android.Widget;
using Android.OS;
using RentaCarros.Modelos;
using System.Collections.Generic;
using System;
using System.IO;
using SQLite;
using System.Linq;
using RentaCarros.Datos;

namespace RentaCarros
{
    [Activity(Label = "Renta de Carros Roman SA", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private Button categoriasBtn;
        private Button carrosBtn;
        private Button clientesBtn;
        private Button rentasBtn;

        public MainActivity()
        {
            
        }
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Init();
            SetEventHandlers();
        }
        private void Init()
        {
            categoriasBtn = FindViewById<Button>(Resource.Id.categoriaBtn);
            carrosBtn = FindViewById<Button>(Resource.Id.carroBtn);
            clientesBtn = FindViewById<Button>(Resource.Id.clienteBtn);
            rentasBtn = FindViewById<Button>(Resource.Id.rentaBtn);
        }
        private void SetEventHandlers()
        {
            categoriasBtn.Click += CategoriasBtn_Click;
            carrosBtn.Click += CarrosBtn_Click;
        }

        private void CarrosBtn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(CarroActivity));
        }

        private void CategoriasBtn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(CategoriaActivity));
        }
    }
}
