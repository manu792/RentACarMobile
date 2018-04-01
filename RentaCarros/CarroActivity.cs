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
using RentaCarros.Fragments.Carros;

namespace RentaCarros
{
    [Activity(Label = "Carros")]
    public class CarroActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Carro);

            // Add tabs
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            AddTab("Agregar", 0, new CarroAgregarFragment());
            AddTab("Buscar", 0, new CarroBuscarFragment());
        }

        private void AddTab(string text, int iconId, Fragment view)
        {
            var tab = ActionBar.NewTab();
            tab.SetText(text);
            //tab.SetIcon(iconId);

            tab.TabSelected += (sender, e) =>
            {
                var fragment = FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);
                e.FragmentTransaction.Add(Resource.Id.fragmentContainer, view);
            };

            tab.TabUnselected += (sender, e) =>
            {
                e.FragmentTransaction.Remove(view);
            };

            ActionBar.AddTab(tab);
        }
    }
}