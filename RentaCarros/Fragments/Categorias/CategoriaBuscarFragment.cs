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
using RentaCarros.Modelos;

namespace RentaCarros.Fragments.Categorias
{
    public class CategoriaBuscarFragment : BaseCategoriaFragment
    {
        private EditText _descripcion;
        private EditText _precioKm;
        private EditText _precioDia;
        private EditText _tarifaDia;
        private EditText _tarifaKm;
        private Button _btnActualizar;
        private Button _btnEliminar;
        private Spinner _categoriasDropDown;
        private Categoria _categoriaSeleccionada;

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
            
            return inflater.Inflate(Resource.Layout.CategoriaBuscarFragment, container, false);
        }

        private void FindViews()
        {
            _descripcion = View.FindViewById<EditText>(Resource.Id.txtDescripcion);
            _precioKm = View.FindViewById<EditText>(Resource.Id.txtPrecioKm);
            _precioDia = View.FindViewById<EditText>(Resource.Id.txtPrecioDia);
            _tarifaDia = View.FindViewById<EditText>(Resource.Id.txtTarifaDia);
            _tarifaKm = View.FindViewById<EditText>(Resource.Id.txtTarifaKm);
            _btnActualizar = View.FindViewById<Button>(Resource.Id.btnActualizar);
            _btnEliminar = View.FindViewById<Button>(Resource.Id.btnEliminar);
            _categoriasDropDown = View.FindViewById<Spinner>(Resource.Id.categoriasDropDown);
        }

        private void CargarCategorias()
        {
            var array = Seleccionar();

            var adapter = new ArrayAdapter<Categoria>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, array);
            _categoriasDropDown.Adapter = adapter;
        }

        private void HandleEvents()
        {
            _btnActualizar.Click += _btnActualizar_Click;
            _btnEliminar.Click += _btnEliminar_Click;
            _categoriasDropDown.ItemSelected += _categoriasDropDown_ItemSelected;
        }

        private void _categoriasDropDown_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = (Spinner)sender;
            _categoriaSeleccionada = ((ArrayAdapter<Categoria>)spinner.Adapter).GetItem(e.Position);

            Buscar(_categoriaSeleccionada.Id);
        }

        private void _btnEliminar_Click(object sender, EventArgs e)
        {
            var count = Eliminar(new Categoria()
            {
                Id = _categoriaSeleccionada.Id,
                Descripcion = _descripcion.Text,
                PrecioKm = Convert.ToDouble(_precioKm.Text),
                PrecioDia = Convert.ToDouble(_precioDia.Text),
                TarifaDia = Convert.ToDouble(_tarifaDia.Text),
                TarifaKm = Convert.ToDouble(_tarifaKm.Text)
            });

            if (count > 0)
            {
                Toast.MakeText(this.Activity, "Categoria eliminada correctamente.", ToastLength.Short)
                    .Show();

                _descripcion.Text = string.Empty;
                _precioKm.Text = string.Empty;
                _precioDia.Text = string.Empty;
                _tarifaDia.Text = string.Empty;
                _tarifaKm.Text = string.Empty;
            }
            else
                Toast.MakeText(this.Activity, "Hubo un problema al tratar de eliminar la categoria. Intente de nuevo mas tarde",
                    ToastLength.Short)
                    .Show();
        }

        private void _btnActualizar_Click(object sender, EventArgs e)
        {
            var count = Actualizar(new Categoria()
            {
                 Id = _categoriaSeleccionada.Id,
                 Descripcion = _descripcion.Text,
                 PrecioKm = Convert.ToDouble(_precioKm.Text),
                 PrecioDia = Convert.ToDouble(_precioDia.Text),
                 TarifaDia = Convert.ToDouble(_tarifaDia.Text),
                 TarifaKm = Convert.ToDouble(_tarifaKm.Text)
            });

            if (count > 0)
                Toast.MakeText(this.Activity, "Categoria modificada correctamente.", ToastLength.Short)
                    .Show();
            else
                Toast.MakeText(this.Activity, "Hubo un problema al tratar de modificar la categoria. Intente de nuevo mas tarde", 
                    ToastLength.Short)
                    .Show();
        }

        private void Buscar(int id)
        {
            var categoria = Seleccionar(id);

            if (categoria != null)
            {
                _descripcion.Text = categoria.Descripcion;
                _precioKm.Text = categoria.PrecioKm.ToString();
                _precioDia.Text = categoria.PrecioDia.ToString();
                _tarifaDia.Text = categoria.TarifaDia.ToString();
                _tarifaKm.Text = categoria.TarifaKm.ToString();
            }
            else
                Toast.MakeText(this.Activity, "No existe la categoria especificada", ToastLength.Long)
                    .Show();
        }
    }
}