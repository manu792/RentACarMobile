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

namespace RentaCarros.Fragments.Rentas
{
    public class RentaAgregarFragment : BaseRentaFragment
    {
        //private EditText _cedula;
        //private EditText _nombre;
        //private Button _btnGuardar;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            FindViews();
            HandleEvents();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return inflater.Inflate(Resource.Layout.ClienteAgregarFragment, container, false);
        }

        private void FindViews()
        {
            //_cedula = View.FindViewById<EditText>(Resource.Id.txtCedula);
            //_nombre = View.FindViewById<EditText>(Resource.Id.txtNombre);
            //_btnGuardar = View.FindViewById<Button>(Resource.Id.btnGuardar);
        }

        private void HandleEvents()
        {
            //_btnGuardar.Click += _btnGuardar_Click;
        }

        private void _btnGuardar_Click(object sender, EventArgs e)
        {
            
        }

        private void LimpiarCampos()
        {
            
        }
    }
}