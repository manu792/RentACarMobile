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
using RentaCarros.Datos;
using RentaCarros.Modelos;

namespace RentaCarros.Fragments.Categorias
{
    public class CategoriasAgregarFragment : BaseCategoriaFragment
    {
        private EditText _descripcion;
        private EditText _precioKm;
        private EditText _precioDia;
        private EditText _tarifaDia;
        private EditText _tarifaKm;
        private Button _btnGuardar;

        public CategoriasAgregarFragment()
        {
        }

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

            return inflater.Inflate(Resource.Layout.CategoriasAgregarFragment, container, false);
        }

        private void FindViews()
        {
            _descripcion = View.FindViewById<EditText>(Resource.Id.txtDescripcion);
            _precioKm = View.FindViewById<EditText>(Resource.Id.txtPrecioKm);
            _precioDia = View.FindViewById<EditText>(Resource.Id.txtPrecioDia);
            _tarifaDia = View.FindViewById<EditText>(Resource.Id.txtTarifaDia);
            _tarifaKm = View.FindViewById<EditText>(Resource.Id.txtTarifaKm);
            _btnGuardar = View.FindViewById<Button>(Resource.Id.btnGuardar);
        }

        private void HandleEvents()
        {
            _btnGuardar.Click += _btnGuardar_Click;
        }

        private void _btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var id = Insertar(new Modelos.Categoria()
                {
                    Descripcion = _descripcion.Text,
                    PrecioDia = Convert.ToDouble(_precioDia.Text),
                    PrecioKm = Convert.ToDouble(_precioKm.Text),
                    TarifaDia = Convert.ToDouble(_tarifaDia.Text),
                    TarifaKm = Convert.ToDouble(_tarifaKm.Text)
                });

                Toast.MakeText(this.Activity, id != 0 ? "Categoria agregada correctamemnte"
                        : "Hubo un problema al agregar la categoria. Intente de nuevo mas tarde", ToastLength.Short)
                        .Show();

                LimpiarCampos();
            }
            catch(Exception ex)
            {
                Toast.MakeText(this.Activity, ex.Message, ToastLength.Long)
                        .Show();
            }
        }

        private void LimpiarCampos()
        {
            _descripcion.Text = string.Empty;
            _precioKm.Text = string.Empty;
            _precioDia.Text = string.Empty;
            _tarifaDia.Text = string.Empty;
            _tarifaKm.Text = string.Empty;
        }
    }
}