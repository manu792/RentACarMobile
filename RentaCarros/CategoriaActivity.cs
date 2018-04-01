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
using RentaCarros.Fragments.Categorias;

namespace RentaCarros
{
    [Activity(Label = "Categorias")]
    public class CategoriaActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Categoria);

            // Add tabs
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            ActionBar.SetIcon(Resource.Drawable.icon);
            ActionBar.SetDisplayShowHomeEnabled(true);
            ActionBar.DisplayOptions = ActionBarDisplayOptions.HomeAsUp | ActionBarDisplayOptions.ShowTitle | ActionBarDisplayOptions.ShowHome;
            

            AddTab("Agregar", 0, new CategoriasAgregarFragment());
            AddTab("Buscar", 0, new CategoriaBuscarFragment());
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    var intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                    break;
                default:
                    break;
            }
            return base.OnOptionsItemSelected(item);
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