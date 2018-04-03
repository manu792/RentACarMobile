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
using Newtonsoft.Json;
using RentaCarros.Adapters;
using RentaCarros.Fragments.Carros;
using RentaCarros.Fragments.Categorias;
using RentaCarros.Fragments.Rentas;
using RentaCarros.Modelos;

namespace RentaCarros
{
    public class RentaSeleccionarCarroActivity : BaseRentaFragment
    {
        private EditText _kilometraje;
        private EditText _categoria;
        private ListView _listView;
        private EditText _buscar;
        private Button _btnCancelar;
        private Button _btnContinuar;
        private Cliente _cliente;
        private IList<Carro> carros;
        private IList<Carro> filteredList;
        private Carro _carroSeleccionado;
        private BaseCarroFragment _baseCarro;
        private BaseCategoriaFragment _baseCategorias;

        public RentaSeleccionarCarroActivity()
        {
            _baseCarro = new BaseCarroFragment();
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

            ObtenerCliente();

            FindViews();
            EventHandlers();

            ActualizarCarros();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return inflater.Inflate(Resource.Layout.RentaSeleccionarCarro, container, false);
        }

        private void FindViews()
        {
            _kilometraje = View.FindViewById<EditText>(Resource.Id.kilometraje);
            _categoria = View.FindViewById<EditText>(Resource.Id.categoria);
            _listView = View.FindViewById<ListView>(Resource.Id.carrosListView);
            _buscar = View.FindViewById<EditText>(Resource.Id.buscar);
            _btnCancelar = View.FindViewById<Button>(Resource.Id.btnCancelar);
            _btnContinuar = View.FindViewById<Button>(Resource.Id.btnGuardar);
        }

        private void EventHandlers()
        {
            _buscar.TextChanged += _buscar_TextChanged;
            _listView.ItemClick += _listView_ItemClick;
            _btnCancelar.Click += _btnCancelar_Click;
            _btnContinuar.Click += _btnContinuar_Click;
        }

        private void _btnContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                var count = Insertar(new Modelos.Renta()
                {
                    CarroId = _carroSeleccionado.Id,
                    ClienteId = _cliente.Id
                    //FechaRenta = fechaRenta.Text,
                    //FechaRetorno = fechaRetorno.Text,
                    //MetrosRenta = metrosRenta.Text,
                    //MetrosRetorno = metrosRetorno.Text,
                    //PrecioDia = precioDia.Text,
                    //PrecioKm = precioKm.Text,
                    //TarifaDia = tarifaDia.Text,
                    //TarifaKm = tarifaKm.Text
                });

                Toast.MakeText(this.Activity, count > 0 ? "Renta agregada correctamemnte"
                        : "Hubo un problema al agregar la renta. Intente de nuevo mas tarde", ToastLength.Short)
                        .Show();

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this.Activity, ex.Message, ToastLength.Long)
                        .Show();
            }
        }

        private void _btnCancelar_Click(object sender, EventArgs e)
        {
            this.Activity.Finish();
        }

        private void _listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            _carroSeleccionado = filteredList[e.Position];

            var categoria = _baseCategorias.Seleccionar(_carroSeleccionado.CategoriaId);
            _categoria.Text = categoria.Descripcion;
            _kilometraje.Text = _carroSeleccionado.Kilometraje.ToString();
        }

        private void _buscar_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var searchTerm = _buscar.Text;
            int km = 0;

            if (int.TryParse(searchTerm, out km))
                filteredList = carros.Where(c => c.Placa.Contains(searchTerm) || c.Kilometraje == km).ToList();
            else
                filteredList = carros.Where(c => c.Placa.Contains(searchTerm)).ToList();

            var filteredAdapter = new CarroListAdapter(this.Activity, filteredList);
            _listView.Adapter = filteredAdapter;
        }

        private void ObtenerCliente()
        {
            // Obtengo la informacion del cliente y la deserializo
            var clienteTexto = Arguments.GetString("cliente");
            _cliente = JsonConvert.DeserializeObject<Cliente>(clienteTexto);
        }

        private void ActualizarCarros()
        {
            carros = _baseCarro.Seleccionar();
            filteredList = carros;
            _carroSeleccionado = null;
            _listView.Adapter = new CarroListAdapter(this.Activity, filteredList);
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            _categoria.Text = string.Empty;
            _kilometraje.Text = string.Empty;
        }
    }
}