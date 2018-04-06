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
using RentaCarros.Adapters;
using RentaCarros.Modelos;

namespace RentaCarros.Fragments.Rentas
{
    public class RentaBuscarFragment : BaseRentaFragment
    {
        private ListView _listView;
        private IList<RentaCliente> _rentas;
        private IList<RentaCliente> _filteredList;
        private RentaCliente _rentaSeleccionada;
        private Button _btnActualizar;
        private Button _btnEliminar;
        private Button _fechaRenta;
        private TextView _txtFechaRenta;
        private Button _fechaRetorno;
        private TextView _txtFechaRetorno;
        private EditText _metrosRenta;
        private EditText _metrosRetorno;
        private EditText _precioDia;
        private EditText _precioKm;
        private EditText _tarifaDia;
        private EditText _tarifaKm;
        private EditText _buscar;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return inflater.Inflate(Resource.Layout.RentaBuscarFragment, container, false);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            FindViews();
            HandleEvents();
            ActualizarClientes();
        }

        private void FindViews()
        {
            _buscar = View.FindViewById<EditText>(Resource.Id.buscar);
            _listView = View.FindViewById<ListView>(Resource.Id.rentasListView);
            _btnActualizar = View.FindViewById<Button>(Resource.Id.btnActualizar);
            _btnEliminar = View.FindViewById<Button>(Resource.Id.btnEliminar);
            _fechaRenta = View.FindViewById<Button>(Resource.Id.btnFechaRenta);
            _txtFechaRenta = View.FindViewById<TextView>(Resource.Id.txtFechaRenta);
            _fechaRetorno = View.FindViewById<Button>(Resource.Id.btnFechaRetorno);
            _txtFechaRetorno = View.FindViewById<TextView>(Resource.Id.txtFechaRetorno);
            _metrosRenta = View.FindViewById<EditText>(Resource.Id.metrosRenta);
            _metrosRetorno = View.FindViewById<EditText>(Resource.Id.metrosRetorno);
            _precioDia = View.FindViewById<EditText>(Resource.Id.precioDia);
            _precioKm = View.FindViewById<EditText>(Resource.Id.precioKm);
            _tarifaDia = View.FindViewById<EditText>(Resource.Id.tarifaDia);
            _tarifaKm = View.FindViewById<EditText>(Resource.Id.tarifaKm);
    }

        private void HandleEvents()
        {
            _buscar.TextChanged += _buscar_TextChanged;
            _listView.ItemClick += _listView_ItemClick;
            _btnActualizar.Click += _btnActualizar_Click;
            _btnEliminar.Click += _btnEliminar_Click;
            _fechaRenta.Click += _fechaRenta_Click;
            _fechaRetorno.Click += _fechaRetorno_Click;
        }

        private void _fechaRetorno_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                _txtFechaRetorno.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void _fechaRenta_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                _txtFechaRenta.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void _btnEliminar_Click(object sender, EventArgs e)
        {
            if (_rentaSeleccionada != null)
            {
                var count = Eliminar(new Renta()
                {
                    Id = _rentaSeleccionada.Id,
                    CarroId = _rentaSeleccionada.CarroId,
                    ClienteId = _rentaSeleccionada.ClienteId,
                    FechaRenta = Convert.ToDateTime(_txtFechaRenta.Text),
                    FechaRetorno = Convert.ToDateTime(_txtFechaRetorno.Text),
                    MetrosRenta = Convert.ToInt32(_metrosRenta.Text),
                    MetrosRetorno = Convert.ToInt32(_metrosRetorno.Text),
                    PrecioDia = Convert.ToDouble(_precioDia.Text),
                    PrecioKm = Convert.ToDouble(_precioKm.Text),
                    TarifaDia = Convert.ToDouble(_tarifaDia.Text),
                    TarifaKm = Convert.ToDouble(_tarifaKm.Text)
                });

                if (count > 0)
                {
                    Toast.MakeText(this.Activity, "Se ha eliminado la renta correctamente.", ToastLength.Long)
                        .Show();

                    LimpiarCampos();
                }
                else
                    Toast.MakeText(this.Activity, "Hubo un problema al tratar de eliminar la renta. Intente de nuevo mas tarde.",
                        ToastLength.Long).Show();

                ActualizarClientes();
            }
        }

        private void _btnActualizar_Click(object sender, EventArgs e)
        {
            if (_rentaSeleccionada != null)
            {
                var count = Actualizar(new Renta()
                {
                    Id = _rentaSeleccionada.Id,
                    CarroId = _rentaSeleccionada.CarroId,
                    ClienteId = _rentaSeleccionada.ClienteId,
                    FechaRenta = Convert.ToDateTime(_txtFechaRenta.Text),
                    FechaRetorno = Convert.ToDateTime(_txtFechaRetorno.Text),
                    MetrosRenta = Convert.ToInt32(_metrosRenta.Text),
                    MetrosRetorno = Convert.ToInt32(_metrosRetorno.Text),
                    PrecioDia = Convert.ToDouble(_precioDia.Text),
                    PrecioKm = Convert.ToDouble(_precioKm.Text),
                    TarifaDia = Convert.ToDouble(_tarifaDia.Text),
                    TarifaKm = Convert.ToDouble(_tarifaKm.Text)
                });

                if (count > 0)
                    Toast.MakeText(this.Activity, "Se ha modificado la renta correctamente.", ToastLength.Long)
                        .Show();
                else
                    Toast.MakeText(this.Activity, "Hubo un problema al tratar de modificar la renta. Intente de nuevo mas tarde.",
                        ToastLength.Long).Show();

                ActualizarClientes();
            }
        }

        private void _listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            _rentaSeleccionada = _filteredList[e.Position];

            _txtFechaRenta.Text = _rentaSeleccionada.FechaRenta.ToLongDateString();
            _txtFechaRetorno.Text = _rentaSeleccionada.FechaRetorno.ToLongDateString();
            _metrosRenta.Text = _rentaSeleccionada.MetrosRenta.ToString();
            _metrosRetorno.Text = _rentaSeleccionada.MetrosRetorno.ToString();
            _precioDia.Text = _rentaSeleccionada.PrecioDia.ToString();
            _precioKm.Text = _rentaSeleccionada.PrecioKm.ToString();
            _tarifaDia.Text = _rentaSeleccionada.TarifaDia.ToString();
            _tarifaKm.Text = _rentaSeleccionada.TarifaKm.ToString();
        }

        private void _buscar_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var searchTerm = _buscar.Text;

            _filteredList = _rentas.Where(c => c.ClienteNombre.Contains(searchTerm) 
                || c.ClienteId.Contains(searchTerm) 
                || c.CarroId.Contains(searchTerm)).ToList();

            var filteredAdapter = new RentaListAdapter(this.Activity, _filteredList);
            _listView.Adapter = filteredAdapter;
        }

        private void ActualizarClientes()
        {
            _rentas = Seleccionar();
            _filteredList = _rentas;
            _rentaSeleccionada = null;
            _listView.Adapter = new RentaListAdapter(this.Activity, _filteredList);

            ConfigurarAlturaListView();

            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            _txtFechaRenta.Text = string.Empty;
            _txtFechaRetorno.Text = string.Empty;
            _metrosRenta.Text = string.Empty;
            _metrosRetorno.Text = string.Empty;
            _precioDia.Text = string.Empty;
            _precioKm.Text = string.Empty;
            _tarifaDia.Text = string.Empty;
            _tarifaKm.Text = string.Empty;
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