﻿using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP2
{
    public partial class FrmCargarArticulo : Form
    {
        private Articulo articulo = null;
        public FrmCargarArticulo()
        {
            InitializeComponent();
        }

        private void cargar()
        {
            try
            {
                NegocioMarca accesoMarcas = new NegocioMarca();
                CategoriaNegocio accesoCategorias = new CategoriaNegocio();

                ddlMarca.DataSource = accesoMarcas.listar();
                ddlCategoria.DataSource = accesoCategorias.listar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public FrmCargarArticulo(Articulo arti)
        {
            InitializeComponent();
            this.articulo = arti;
            Text = "Modificar artículo";
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCrearArticulo_Click(object sender, EventArgs e)
        {
            //Articulo nuevo = new Articulo();
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                bool ArticuloNoexistente = false;
                if (articulo == null) { 
                articulo = new Articulo();
                ArticuloNoexistente = true;
                    articulo.id = negocio.UltimoArticuloRegistrado();
                }

                articulo.nombre = txtNombre.Text;
                articulo.codigo = txtCodigo.Text;
                articulo.descripcion = textBoxDescripcion.Text;
                articulo.precio = decimal.Parse(textBoxPrecio.Text);
                articulo.marcaArticulo = new Marca();
                articulo.marcaArticulo.id = ((Marca)ddlMarca.SelectedItem).id;
                articulo.marcaArticulo.descripcion = ((Marca)ddlMarca.SelectedItem).descripcion;
                articulo.categoriaArticulo = new Categoria();
                articulo.categoriaArticulo.Descripcion = ((Categoria)ddlCategoria.SelectedItem).Descripcion;
                articulo.categoriaArticulo.ID = ((Categoria)ddlCategoria.SelectedItem).ID;
             
                articulo.imagenes = new List<Imagen>();

                Imagen ima = new Imagen(articulo.id, textBoxURL.Text);

                articulo.imagenes.Add(ima);                

                if(!ArticuloNoexistente)
                {
                    MessageBox.Show("modificando ....");
                    negocio.modificar(articulo);
                    MessageBox.Show("Articulo modificado correctamente");
                }
                else
                {
                  
                    MessageBox.Show("agregando ....");
                    negocio.agregarImagen(articulo.id, ima.Url);
                    negocio.Agregar2(articulo);
                    MessageBox.Show("Articulo agregado correctamente");
                }

                

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString() + "Error Click Crear Articulo");
            }
            finally
            {
                this.Close();
            }
        }

        private void FrmCargarArticulo_Load(object sender, EventArgs e)
        {

            cargar();

            ddlCategoria.DisplayMember = "Descripcion";
            ddlCategoria.ValueMember = "ID";

            ddlMarca.DisplayMember = "descripcion";
            ddlMarca.ValueMember = "id";

            if (articulo != null)
            {
                textBoxDescripcion.Text = articulo.descripcion;
                textBoxPrecio.Text = articulo.precio.ToString();
                txtCodigo.Text = articulo.codigo;
                txtNombre.Text = articulo.nombre;
                ddlMarca.SelectedValue = articulo.marcaArticulo.id;
                ddlCategoria.SelectedValue = articulo.categoriaArticulo.ID;
              
                cargarImagen(articulo.imagenes[0].Url);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmAgregarMarca nuevaMarca = new FrmAgregarMarca();
            nuevaMarca.ShowDialog();
            cargar();
        }

        private void btnCargarImagen_Click(object sender, EventArgs e)
        {
            cargarImagen(textBoxURL.Text);
        }

        private void cargarImagen(string imagen)
        {


            try
            {
                pbxImagenes.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxImagenes.Load("https://miracomosehace.com/wp-content/uploads/2020/06/error-web.jpg");

            }
        }

        private void buttonAñadirCategoria_Click(object sender, EventArgs e)
        {
            FrmCargarCategoria nuevaCategoria = new FrmCargarCategoria();
            nuevaCategoria.ShowDialog();
            cargar();
        }
    }
}