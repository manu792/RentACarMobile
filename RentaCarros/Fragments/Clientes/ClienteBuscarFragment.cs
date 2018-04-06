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
using RentaCarros.Modelos;

namespace RentaCarros.Fragments.Clientes
{
    public class ClienteBuscarFragment : BaseClienteFragment
    {
        private ListView _listView;
        private IList<Cliente> _clientes;
        private IList<Cliente> _filteredList;
        private Cliente _clienteSeleccionado;
        private Button _btnActualizar;
        private Button _btnEliminar;
        private EditText _buscar;
        private EditText _nombre;


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

            return inflater.Inflate(Resource.Layout.ClienteBuscarFragment, container, false);
        }

        private void FindViews()
        {
            _buscar = View.FindViewById<EditText>(Resource.Id.buscar);
            _nombre = View.FindViewById<EditText>(Resource.Id.txtNombre);
            _listView = View.FindViewById<ListView>(Resource.Id.clientesListView);
            _btnActualizar = View.FindViewById<Button>(Resource.Id.btnActualizar);
            _btnEliminar = View.FindViewById<Button>(Resource.Id.btnEliminar);
        }

        private void HandleEvents()
        {
            _buscar.TextChanged += _buscar_TextChanged;
            _listView.ItemClick += _listView_ItemClick;
            _btnActualizar.Click += _btnActualizar_Click;
            _btnEliminar.Click += _btnEliminar_Click;
        }

        private void _btnEliminar_Click(object sender, EventArgs e)
        {
            if (_clienteSeleccionado != null)
            {
                var count = Eliminar(new Cliente()
                {
                    Cedula = _clienteSeleccionado.Cedula,
                    Nombre = _nombre.Text
                });

                if (count > 0)
                {
                    Toast.MakeText(this.Activity, "Se ha eliminado el cliente correctamente.", ToastLength.Long)
                        .Show();

                    LimpiarCampos();
                }
                else
                    Toast.MakeText(this.Activity, "Hubo un problema al tratar de eliminar el cliente. Intente de nuevo mas tarde.",
                        ToastLength.Long).Show();

                ActualizarClientes();
            }
        }

        private void _btnActualizar_Click(object sender, EventArgs e)
        {
            if (_clienteSeleccionado != null)
            {
                var count = Actualizar(new Cliente()
                {
                    Cedula = _clienteSeleccionado.Cedula,
                    Nombre = _nombre.Text
                });

                if (count > 0)
                    Toast.MakeText(this.Activity, "Se ha modificado el cliente correctamente.", ToastLength.Long)
                        .Show();
                else
                    Toast.MakeText(this.Activity, "Hubo un problema al tratar de modificar el cliente. Intente de nuevo mas tarde.",
                        ToastLength.Long).Show();

                ActualizarClientes();
            }
        }

        private void _listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            _clienteSeleccionado = _filteredList[e.Position];
            _nombre.Text = _clienteSeleccionado.Nombre;
        }

        private void _buscar_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var searchTerm = _buscar.Text;

            _filteredList = _clientes.Where(c => c.Cedula.Contains(searchTerm) || c.Nombre.Contains(searchTerm)).ToList();

            var filteredAdapter = new ClienteListAdapter(this.Activity, _filteredList);
            _listView.Adapter = filteredAdapter;
        }

        private void ActualizarClientes()
        {
            _clientes = Seleccionar();
            _filteredList = _clientes;
            _clienteSeleccionado = null;
            _listView.Adapter = new ClienteListAdapter(this.Activity, _filteredList);

            ConfigurarAlturaListView();

            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            _listView.SetSelection(0);
            _nombre.Text = string.Empty;
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