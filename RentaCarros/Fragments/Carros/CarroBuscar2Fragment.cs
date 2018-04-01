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
using RentaCarros.Adapters;
using RentaCarros.Fragments.Categorias;
using RentaCarros.Modelos;

namespace RentaCarros.Fragments.Carros
{
    public class CarroBuscar2Fragment : BaseCarroFragment
    {
        private ListView _listView;
        private IList<Carro> carros;
        private EditText _buscar;
        private Spinner _categoriasDropDown;
        private Button _btnActualizar;
        private Button _btnEliminar;
        private Categoria _categoriaSeleccionada;
        private EditText _kilometraje;
        private Carro _carroSeleccionado;
        private BaseCategoriaFragment _baseCategorias;
        private IList<Carro> filteredList;

        public CarroBuscar2Fragment()
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
            HandleEvents();
            CargarCategorias();

            ActualizarCarros();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return inflater.Inflate(Resource.Layout.CarroBuscar2Fragment, container, false);
        }

        private void FindViews()
        {
            _listView = View.FindViewById<ListView>(Resource.Id.carrosListView);
            _kilometraje = View.FindViewById<EditText>(Resource.Id.txtKilometraje);
            _buscar = View.FindViewById<EditText>(Resource.Id.buscar);
            _categoriasDropDown = View.FindViewById<Spinner>(Resource.Id.categoriasDropDown);
            _btnActualizar = View.FindViewById<Button>(Resource.Id.btnCarroActualizar);
            _btnEliminar = View.FindViewById<Button>(Resource.Id.btnCarroEliminar);
        }

        private void HandleEvents()
        {
            _listView.ItemClick += ListView_ItemClick;
            _buscar.TextChanged += _buscar_TextChanged;
            _categoriasDropDown.ItemSelected += _categoriasDropDown_ItemSelected;
            _btnActualizar.Click += _btnActualizar_Click;
            _btnEliminar.Click += _btnEliminar_Click;
        }

        private void CargarCategorias()
        {
            var array = _baseCategorias.Seleccionar();

            var adapter = new ArrayAdapter<Categoria>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, array);
            _categoriasDropDown.Adapter = adapter;
        }

        private void _btnEliminar_Click(object sender, EventArgs e)
        {
            if(_carroSeleccionado != null)
            {
                var count = Eliminar(new Carro()
                {
                    CategoriaId = _categoriaSeleccionada.Id,
                    Placa = _carroSeleccionado.Placa,
                    Kilometraje = Convert.ToInt32(_kilometraje.Text)
                });

                if (count > 0)
                {
                    Toast.MakeText(this.Activity, "Se ha eliminado el carro correctamente.", ToastLength.Long)
                        .Show();

                    LimpiarCampos();
                }
                else
                    Toast.MakeText(this.Activity, "Hubo un problema al tratar de eliminar el carro. Intente de nuevo mas tarde.",
                        ToastLength.Long).Show();

                ActualizarCarros();
            }
        }

        private void _btnActualizar_Click(object sender, EventArgs e)
        {
            if(_carroSeleccionado != null)
            {
                var count = Actualizar(new Carro()
                {
                    CategoriaId = _categoriaSeleccionada.Id,
                    Placa = _carroSeleccionado.Placa,
                    Kilometraje = Convert.ToInt32(_kilometraje.Text)
                });

                if (count > 0)
                    Toast.MakeText(this.Activity, "Se ha modificado el carro correctamente.", ToastLength.Long)
                        .Show();
                else
                    Toast.MakeText(this.Activity, "Hubo un problema al tratar de modificar el carro. Intente de nuevo mas tarde.",
                        ToastLength.Long).Show();

                ActualizarCarros();
            }
        }

        private void _categoriasDropDown_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = (Spinner)sender;
            _categoriaSeleccionada = ((ArrayAdapter<Categoria>)spinner.Adapter).GetItem(e.Position);
        }

        private void _buscar_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var searchTerm = _buscar.Text;
            int km = 0;

            if(int.TryParse(searchTerm, out km))
                filteredList = carros.Where(c => c.Placa.Contains(searchTerm) || c.Kilometraje == km).ToList();
            else
                filteredList = carros.Where(c => c.Placa.Contains(searchTerm)).ToList();

            var filteredAdapter = new CarroListAdapter(this.Activity, filteredList);
            _listView.Adapter = filteredAdapter;
        }

        protected void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            _carroSeleccionado = filteredList[e.Position];

            var categoria = _baseCategorias.Seleccionar(_carroSeleccionado.CategoriaId);
            _categoriasDropDown.SetSelection(LookForIndex(categoria.Id));
            _kilometraje.Text = _carroSeleccionado.Kilometraje.ToString();
        }

        private void ActualizarCarros()
        {
            carros = Seleccionar();
            filteredList = carros;
            _carroSeleccionado = null;
            _listView.Adapter = new CarroListAdapter(this.Activity, filteredList);
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            _listView.SetSelection(0);
            _kilometraje.Text = string.Empty;
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