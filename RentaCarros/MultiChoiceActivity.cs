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
using RentaCarros.Modelos;

namespace RentaCarros
{
    [Activity(Label = "MultiChoiceActivity")]
    public class MultiChoiceActivity : Activity
    {
        private ListView listView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.MultiView);

            listView = FindViewById<ListView>(Resource.Id.listViewTest);
            listView.ItemClick += ListView_ItemClick;


            var carros = new List<Carro>()
            {
                new Carro()
                {
                    Id = 1,
                    CategoriaId = 1,
                    Kilometraje = 407,
                    Placa = "MRS289"
                },
                new Carro()
                {
                    Id = 2,
                    CategoriaId = 2,
                    Kilometraje = 105300,
                    Placa = "811187"
                },
                new Carro()
                {
                    Id = 3,
                    CategoriaId = 3,
                    Kilometraje = 260125,
                    Placa = "1234567"
                }
            };
            var array = carros.ToArray<Carro>();
            var adapter = new ArrayAdapter<Carro>(this, Android.Resource.Layout.SimpleListItemMultipleChoice, array);
            listView.Adapter = adapter;
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            SparseBooleanArray checkeds = listView.CheckedItemPositions;
            int num_selected = 0;
            for (int i = 0; i < checkeds.Size(); i++)
            {
                if (checkeds.ValueAt(i)) {
                    num_selected++;
                    int key = checkeds.KeyAt(i);
                    bool value = checkeds.Get(key);
                    if (value)
                    {
                        //
                    }
                }
            }
            }
    }
}