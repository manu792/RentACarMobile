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
using RentaCarros.Fragments.Categorias;
using RentaCarros.Modelos;

namespace RentaCarros.Fragments.Carros
{
    public class CarroBuscarFragment : BaseCarroFragment
    {
        private BaseCategoriaFragment _baseCategorias;
        private EditText _placa;
        private EditText _kilometraje;
        private EditText _categoria;
        private Button _btnBuscar;
        private Button _btnActualizar;
        private Button _btnEliminar;

        public CarroBuscarFragment()
        {
            _baseCategorias = new BaseCategoriaFragment();
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
            CargarCategorias();
            HandleEvents();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return inflater.Inflate(Resource.Layout.CarroBuscarFragment, container, false);
        }

        private void FindViews()
        {
            _placa = View.FindViewById<EditText>(Resource.Id.txtPlaca);
            _kilometraje = View.FindViewById<EditText>(Resource.Id.txtKilometraje);
            _categoria = View.FindViewById<EditText>(Resource.Id.txtCategoria);
            _btnBuscar = View.FindViewById<Button>(Resource.Id.btnCarroBuscar);
            _btnActualizar = View.FindViewById<Button>(Resource.Id.btnCarroActualizar);
            _btnEliminar = View.FindViewById<Button>(Resource.Id.btnCarroEliminar);
        }

        private void HandleEvents()
        {
            _btnBuscar.Click += _btnBuscar_Click;
        }

        private void _btnBuscar_Click(object sender, EventArgs e)
        {
            var carro = Seleccionar(_placa.Text);
            if (carro != null)
            {
                _categoria.Text = carro.CategoriaId.ToString();
                _kilometraje.Text = carro.Kilometraje.ToString();
            }
            else
                Toast.MakeText(this.Activity, "El carro que trata de buscar no existe. Verifique que el numero de placa sea el correcto", ToastLength.Long)
                    .Show();
        }

        private void CargarCategorias()
        {
            var array = _baseCategorias.Seleccionar();

            var adapter = new ArrayAdapter<Categoria>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, array);
            //_categoriasDropDown.Adapter = adapter;
        }
    }
}