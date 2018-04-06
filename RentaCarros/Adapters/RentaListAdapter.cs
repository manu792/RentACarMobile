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
using RentaCarros.Fragments.Clientes;
using RentaCarros.Modelos;

namespace RentaCarros.Adapters
{
    public class RentaListAdapter : BaseAdapter<RentaCliente>
    {
        IList<RentaCliente> items;
        Activity context;

        public RentaListAdapter(Activity context, IList<RentaCliente> items) : base()
        {
            this.context = context;
            this.items = items;
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

        public override RentaCliente this[int position] => items[position];

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            if (convertView == null)
                convertView = context.LayoutInflater.Inflate(Resource.Layout.RentaRowView, null);

            convertView.FindViewById<TextView>(Resource.Id.id).Text = item.Id.ToString();
            convertView.FindViewById<TextView>(Resource.Id.clienteNombre).Text = item.ClienteNombre;
            convertView.FindViewById<TextView>(Resource.Id.clienteCedula).Text = item.ClienteId;
            convertView.FindViewById<TextView>(Resource.Id.carroPlaca).Text = item.CarroId;

            return convertView;
        }
    }
}