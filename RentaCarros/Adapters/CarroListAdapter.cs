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
using RentaCarros.Fragments.Categorias;
using RentaCarros.Modelos;

namespace RentaCarros.Adapters
{
    public class CarroListAdapter : BaseAdapter<Carro>
    {
        IList<Carro> items;
        Activity context;
        BaseCategoriaFragment _baseCategoria;

        public CarroListAdapter(Activity context, IList<Carro> items) : base()
        {
            this.context = context;
            this.items = items;
            this._baseCategoria = new BaseCategoriaFragment();
        }

        public override int Count
        {
            get
            {
                return items.Count;
            }
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Carro this[int position] => items[position];

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            var categoria = _baseCategoria.Seleccionar(item.CategoriaId);

            if (convertView == null)
                convertView = context.LayoutInflater.Inflate(Resource.Layout.CarroRowView, null);
            
            convertView.FindViewById<TextView>(Resource.Id.placa).Text = $"Placa: {item.Placa}";
            convertView.FindViewById<TextView>(Resource.Id.categoria).Text = $"Categoria: {categoria.Descripcion}";
            convertView.FindViewById<TextView>(Resource.Id.kilometraje).Text = $"Km: {item.Kilometraje.ToString()}";

            return convertView;
        }
    }
}