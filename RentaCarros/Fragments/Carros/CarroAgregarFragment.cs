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
    public class CarroAgregarFragment : BaseCarroFragment
    {
        private BaseCategoriaFragment _baseCategoria;
        private Spinner _categoriasDropDown;
        private EditText _placa;
        private EditText _kilometraje;
        private Button _btnGuardar;
        private Categoria _categoriaSeleccionada;

        public CarroAgregarFragment()
        {
            _baseCategoria = new BaseCategoriaFragment();
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
            CargarCategorias();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return inflater.Inflate(Resource.Layout.CarrosAgregarFragment, container, false);
        }

        private void FindViews()
        {
            _categoriasDropDown = View.FindViewById<Spinner>(Resource.Id.categoriasDropDown);
            _placa = View.FindViewById<EditText>(Resource.Id.txtPlaca);
            _kilometraje = View.FindViewById<EditText>(Resource.Id.txtKilometraje);
            _btnGuardar = View.FindViewById<Button>(Resource.Id.btnCarroGuardar);
        }

        private void HandleEvents()
        {
            _categoriasDropDown.ItemSelected += _categoriasDropDown_ItemSelected;
            _btnGuardar.Click += _btnGuardar_Click;
        }

        private void _categoriasDropDown_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = (Spinner)sender;
            _categoriaSeleccionada = ((ArrayAdapter<Categoria>)spinner.Adapter).GetItem(e.Position);
        }

        private void _btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var count = Insertar(new Carro()
                {
                    CategoriaId = _categoriaSeleccionada.Id,
                    Placa = _placa.Text,
                    Kilometraje = Convert.ToInt32(_kilometraje.Text)
                });

                if (count > 0)
                {
                    Toast.MakeText(this.Activity, "Carro agregado correctamente", ToastLength.Short)
                        .Show();

                    _categoriasDropDown.SetSelection(0);
                    _placa.Text = string.Empty;
                    _kilometraje.Text = string.Empty;
                }
                else
                    Toast.MakeText(this.Activity, "Hubo un problema al tratar de agregar el carro. Intente de nuevo mas tarde",
                        ToastLength.Long).Show();
            }
            catch(Exception ex)
            {
                Toast.MakeText(this.Activity, ex.Message, ToastLength.Long).Show();
            }
        }

        private void CargarCategorias()
        {
            var array = _baseCategoria.Seleccionar();

            var adapter = new ArrayAdapter<Categoria>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, array);
            _categoriasDropDown.Adapter = adapter;
        }
    }
}