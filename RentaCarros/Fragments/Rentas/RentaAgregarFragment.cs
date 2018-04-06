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
using RentaCarros.Fragments.Clientes;
using RentaCarros.Modelos;

namespace RentaCarros.Fragments.Rentas
{
    public class RentaAgregarFragment : BaseRentaFragment
    {
        private EditText _buscar;
        private EditText _nombre;
        private Button _btnCancelar;
        private Button _btnContinuar;
        private ListView _listView;
        private Cliente _clienteSeleccionado;
        private IList<Cliente> _clientes;
        private IList<Cliente> _filteredList;
        private BaseClienteFragment _baseCliente;
        private BaseCarroFragment _baseCarro;
        private BaseRentaFragment _baseRenta;

        public RentaAgregarFragment()
        {
            _baseCliente = new BaseClienteFragment();
            _baseCarro = new BaseCarroFragment();
            _baseRenta = new BaseRentaFragment();
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
            ActualizarClientes();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return inflater.Inflate(Resource.Layout.RentaAgregarFragment, container, false);
        }

        private void FindViews()
        {
            _buscar = View.FindViewById<EditText>(Resource.Id.buscar);
            _nombre = View.FindViewById<EditText>(Resource.Id.txtNombre);
            _btnCancelar = View.FindViewById<Button>(Resource.Id.btnCancelar);
            _btnContinuar = View.FindViewById<Button>(Resource.Id.btnContinuar);
            _listView = View.FindViewById<ListView>(Resource.Id.clientesListView);
        }

        private void HandleEvents()
        {
            _buscar.TextChanged += _buscar_TextChanged;
            _btnCancelar.Click += _btnCancelar_Click;
            _btnContinuar.Click += _btnContinuar_Click;
            _listView.ItemClick += _listView_ItemClick;
        }

        private void _buscar_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var searchTerm = _buscar.Text;

            _filteredList = _clientes.Where(c => c.Cedula.Contains(searchTerm) || c.Nombre.Contains(searchTerm)).ToList();

            var filteredAdapter = new ClienteListAdapter(this.Activity, _filteredList);
            _listView.Adapter = filteredAdapter;
        }

        private void _listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            _clienteSeleccionado = _filteredList[e.Position];
            _nombre.Text = _clienteSeleccionado.Nombre;
        }

        private void _btnContinuar_Click(object sender, EventArgs e)
        {
            if (HayCarrosDisponibles())
            {
                var renta = new RentaSeleccionarCarroFragment();

                var bundle = new Bundle();
                bundle.PutString("cliente", JsonConvert.SerializeObject(_clienteSeleccionado));

                var transaction = FragmentManager.BeginTransaction();
                transaction.Replace(Resource.Id.fragmentContainer, renta, "renta");
                transaction.Commit();
                renta.Arguments = bundle;
            }
            else
                Toast.MakeText(this.Activity, 
                    "No hay carros disponibles para rentar. Intente despues de agregar un nuevo carro", 
                    ToastLength.Long).Show();
        }

        private bool HayCarrosDisponibles()
        {
            var carros = _baseCarro.Seleccionar();
            var rentas = _baseRenta.Seleccionar();

            return carros.Where(c => !rentas
                .Select(r => r.CarroId)
                .Contains(c.Placa)
            ).ToList().Count > 0;
        }

        private void _btnCancelar_Click(object sender, EventArgs e)
        {
            this.Activity.Finish();
        }

        private void LimpiarCampos()
        {
            _nombre.Text = string.Empty;
        }

        private void ActualizarClientes()
        {
            _clientes = _baseCliente.Seleccionar();
            _filteredList = _clientes;
            _clienteSeleccionado = null;
            _listView.Adapter = new ClienteListAdapter(this.Activity, _filteredList);

            ConfigurarAlturaListView();

            LimpiarCampos();
        }

        private void ConfigurarAlturaListView()
        {
            var listAdapter = _listView.Adapter;
            if (listAdapter == null)
                return;

            int desiredWidth = View.MeasureSpec.MakeMeasureSpec(_listView.Width, MeasureSpecMode.Unspecified);
            int totalHeight = 0;
            View view = null;
            for (int i = 0; i < listAdapter.Count; i++)
            {
                view = listAdapter.GetView(i, view, _listView);
                if (i == 0)
                    view.LayoutParameters = (new ViewGroup.LayoutParams(desiredWidth, WindowManagerLayoutParams.WrapContent));

                view.Measure(desiredWidth, (int)MeasureSpecMode.Unspecified);
                totalHeight += view.MeasuredHeight;
            }
            ViewGroup.LayoutParams prm = _listView.LayoutParameters;
            prm.Height = totalHeight + (_listView.DividerHeight * (listAdapter.Count - 1));
            _listView.LayoutParameters = prm;
        }
    }
}