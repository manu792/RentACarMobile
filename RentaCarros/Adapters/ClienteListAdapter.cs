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
using RentaCarros.Modelos;

namespace RentaCarros.Adapters
{
    public class ClienteListAdapter : BaseAdapter<Cliente>
    {
        IList<Cliente> items;
        Activity context;

        public ClienteListAdapter(Activity context, IList<Cliente> items) : base()
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

        public override Cliente this[int position] => items[position];

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            if (convertView == null)
                convertView = context.LayoutInflater.Inflate(Resource.Layout.ClienteRowView, null);

            convertView.FindViewById<TextView>(Resource.Id.cedula).Text = item.Cedula;
            convertView.FindViewById<TextView>(Resource.Id.nombre).Text = item.Nombre;

            return convertView;
        }
    }
}