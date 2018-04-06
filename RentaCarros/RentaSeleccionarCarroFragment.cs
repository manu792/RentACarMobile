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
using RentaCarros.Fragments;
using RentaCarros.Fragments.Carros;
using RentaCarros.Fragments.Categorias;
using RentaCarros.Fragments.Rentas;
using RentaCarros.Modelos;

namespace RentaCarros
{
    public class RentaSeleccionarCarroFragment : BaseRentaFragment
    {
        private EditText _kilometraje;
        private EditText _categoria;
        private TextView _txtFechaRenta;
        private TextView _txtFechaRetorno;
        private EditText _txtMetrosRenta;
        private EditText _txtMetrosRetorno;
        private TextView _txtPrecioDia;
        private TextView _txtPrecioKm;
        private TextView _txtTarifaDia;
        private TextView _txtTarifaKm;
        private ListView _listView;
        private EditText _buscar;
        private Button _btnCancelar;
        private Button _btnContinuar;
        private Button _btnFechaRenta;
        private Button _btnFechaRetorno;
        private Cliente _cliente;
        private IList<Carro> carros;
        private IList<Carro> filteredList;
        private Carro _carroSeleccionado;
        private BaseCarroFragment _baseCarro;
        private BaseCategoriaFragment _baseCategorias;

        public RentaSeleccionarCarroFragment()
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
            _txtFechaRenta = View.FindViewById<TextView>(Resource.Id.txtFechaRenta);
            _txtFechaRetorno = View.FindViewById<TextView>(Resource.Id.txtFechaRetorno);
            _txtMetrosRenta = View.FindViewById<EditText>(Resource.Id.metrosRenta);
            _txtMetrosRetorno = View.FindViewById<EditText>(Resource.Id.metrosRetorno);
            _txtPrecioDia = View.FindViewById<EditText>(Resource.Id.precioDia);
            _txtPrecioKm = View.FindViewById<EditText>(Resource.Id.precioKm);
            _txtTarifaDia = View.FindViewById<EditText>(Resource.Id.tarifaDia);
            _txtTarifaKm = View.FindViewById<EditText>(Resource.Id.tarifaKm);
            _listView = View.FindViewById<ListView>(Resource.Id.carrosListView);
            _buscar = View.FindViewById<EditText>(Resource.Id.buscar);
            _btnCancelar = View.FindViewById<Button>(Resource.Id.btnCancelar);
            _btnContinuar = View.FindViewById<Button>(Resource.Id.btnGuardar);
            _btnFechaRenta = View.FindViewById<Button>(Resource.Id.btnFechaRenta);
            _btnFechaRetorno = View.FindViewById<Button>(Resource.Id.btnFechaRetorno);
        }

        private void EventHandlers()
        {
            _buscar.TextChanged += _buscar_TextChanged;
            _listView.ItemClick += _listView_ItemClick;
            _btnCancelar.Click += _btnCancelar_Click;
            _btnContinuar.Click += _btnContinuar_Click;
            _btnFechaRenta.Click += _btnFechaRenta_Click;
            _btnFechaRetorno.Click += _btnFechaRetorno_Click;
        }

        private void _btnFechaRetorno_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                _txtFechaRetorno.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void _btnFechaRenta_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                _txtFechaRenta.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void _btnContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                var count = Insertar(new Modelos.Renta()
                {
                    CarroId = _carroSeleccionado.Placa,
                    ClienteId = _cliente.Cedula,
                    FechaRenta = Convert.ToDateTime(_txtFechaRenta.Text),
                    FechaRetorno = Convert.ToDateTime(_txtFechaRetorno.Text),
                    MetrosRenta = Convert.ToInt32(_txtMetrosRenta.Text),
                    MetrosRetorno = Convert.ToInt32(_txtMetrosRetorno.Text),
                    PrecioDia = Convert.ToDouble(_txtPrecioDia.Text),
                    PrecioKm = Convert.ToDouble(_txtPrecioKm.Text),
                    TarifaDia = Convert.ToDouble(_txtTarifaDia.Text),
                    TarifaKm = Convert.ToDouble(_txtTarifaKm.Text)
                });

                Toast.MakeText(this.Activity, count > 0 ? "Renta agregada correctamemnte"
                        : "Hubo un problema al agregar la renta. Intente de nuevo mas tarde", ToastLength.Short)
                        .Show();

                RegresarAMain();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this.Activity, ex.Message, ToastLength.Long)
                        .Show();
            }
        }

        private void _btnCancelar_Click(object sender, EventArgs e)
        {
            RegresarAMain();
        }

        private void RegresarAMain()
        {
            this.Activity.Finish();
        }

        private void _listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            _carroSeleccionado = filteredList[e.Position];

            var categoria = _baseCategorias.Seleccionar(_carroSeleccionado.CategoriaId);
            _categoria.Text = categoria.Descripcion;
            _kilometraje.Text = _carroSeleccionado.Kilometraje.ToString();
            _txtPrecioDia.Text = categoria.PrecioDia.ToString();
            _txtPrecioKm.Text = categoria.PrecioKm.ToString();
            _txtTarifaDia.Text = categoria.TarifaDia.ToString();
            _txtTarifaKm.Text = categoria.TarifaKm.ToString();
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
            var rentas = Seleccionar();

            carros = carros.Where(c => !rentas
                .Select(r => r.CarroId)
                .Contains(c.Placa)
            ).ToList();

            filteredList = carros;
            _carroSeleccionado = null;
            _listView.Adapter = new CarroListAdapter(this.Activity, filteredList);

            ConfigurarAlturaListView();

            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            _categoria.Text = string.Empty;
            _kilometraje.Text = string.Empty;
            _txtPrecioDia.Text = string.Empty;
            _txtPrecioKm.Text = string.Empty;
            _txtTarifaDia.Text = string.Empty;
            _txtTarifaKm.Text = string.Empty;
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