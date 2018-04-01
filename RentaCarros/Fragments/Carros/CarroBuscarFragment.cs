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
        private Spinner _categoriasDropDown;
        private Button _btnBuscar;
        private Button _btnActualizar;
        private Button _btnEliminar;
        private Categoria _categoriaSeleccionada;

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
            _categoriasDropDown = View.FindViewById<Spinner>(Resource.Id.categoriasDropDown);
            _btnBuscar = View.FindViewById<Button>(Resource.Id.btnCarroBuscar);
            _btnActualizar = View.FindViewById<Button>(Resource.Id.btnCarroActualizar);
            _btnEliminar = View.FindViewById<Button>(Resource.Id.btnCarroEliminar);
        }

        private void HandleEvents()
        {
            _btnBuscar.Click += _btnBuscar_Click;
            _btnActualizar.Click += _btnActualizar_Click;
            _btnEliminar.Click += _btnEliminar_Click;
            _categoriasDropDown.ItemSelected += _categoriasDropDown_ItemSelected;
        }

        private void _categoriasDropDown_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = (Spinner)sender;
            _categoriaSeleccionada = ((ArrayAdapter<Categoria>)spinner.Adapter).GetItem(e.Position);
        }

        private void _btnEliminar_Click(object sender, EventArgs e)
        {
            var count = Eliminar(new Carro()
            {
                CategoriaId = _categoriaSeleccionada.Id,
                Placa = _placa.Text,
                Kilometraje = Convert.ToInt32(_kilometraje.Text)
            });

            if (count > 0)
            {
                Toast.MakeText(this.Activity, "Se ha eliminado el carro correctamente.", ToastLength.Long)
                    .Show();

                _categoriasDropDown.SetSelection(0);
                _kilometraje.Text = string.Empty;
            }
            else
                Toast.MakeText(this.Activity, "Hubo un problema al tratar de eliminar el carro. Intente de nuevo mas tarde.",
                    ToastLength.Long).Show();
        }

        private void _btnActualizar_Click(object sender, EventArgs e)
        {
            var count = Actualizar(new Carro()
            {
                CategoriaId = _categoriaSeleccionada.Id,
                Placa = _placa.Text,
                Kilometraje = Convert.ToInt32(_kilometraje.Text)
            });   

            if(count > 0)
                Toast.MakeText(this.Activity, "Se ha modificado el carro correctamente.", ToastLength.Long)
                    .Show();
            else
                Toast.MakeText(this.Activity, "Hubo un problema al tratar de modificar el carro. Intente de nuevo mas tarde.", 
                    ToastLength.Long).Show();
        }

        private void _btnBuscar_Click(object sender, EventArgs e)
        {
            var carro = Seleccionar(_placa.Text);
            if(carro != null)
            {
                var categoria = _baseCategorias.Seleccionar(carro.CategoriaId);
                _categoriasDropDown.SetSelection(LookForIndex(categoria.Id));
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
            _categoriasDropDown.Adapter = adapter;
        }

        private int LookForIndex(int id)
        {
            var adapter = ((ArrayAdapter<Categoria>)_categoriasDropDown.Adapter);

            for (int i = 0; i < adapter.Count; i++)
            {
                var item = adapter.GetItem(i);
                if (item.Id == id)
                    return i;
            }

            return -1;
        }
    }
}