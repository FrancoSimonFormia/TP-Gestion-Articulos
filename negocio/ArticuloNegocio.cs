﻿using dominio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ArticuloNegocio
    {

        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.settearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, A.IdCategoria, A.Precio, M.Descripcion AS Marca, C.Descripcion AS Categoria, I.ImagenUrl FROM ARTICULOS A, MARCAS M, CATEGORIAS C, IMAGENES I where A.IdMarca = M.Id and A.IdCategoria = C.Id and I.IdArticulo = A.Id");
                //datos.settearConsulta("SELETC A.ID, ");
                //datos.settearConsulta("SELECT * FROM ARTICULOS");
                //datos.settearConsulta("SELECT A.id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, A.IdCategoria, A.Precio, M.Descripcion AS Marca, C.Descripcion AS Categoria FROM ARTICULOS A JOIN MARCAS M ON A.IdMarca = M.Id JOIN CATEGORIAS C ON A.IdCategoria = C.Id");
                //datos.settearConsulta("Select Id, Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio from ARTICULOS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.id = (int)datos.Lector["Id"];
                    aux.codigo = (string)datos.Lector["Codigo"];
                    aux.nombre = (string)datos.Lector["Nombre"];
                    aux.descripcion = (string)datos.Lector["Descripcion"];
                    aux.marcaArticulo = new Marca();
                    //aux.marcaArticulo.id = (int)datos.Lector["IdMarca"];
                    aux.marcaArticulo.descripcion = (string)datos.Lector["Marca"];
                    aux.categoriaArticulo = new Categoria();
                    aux.categoriaArticulo.Descripcion = (string)datos.Lector["Categoria"];
                    //aux.categoriaArticulo.ID = (string)datos.Lector["IdCategoria"];
                    aux.precio = (decimal)datos.Lector["Precio"];
                    aux.imagenes = new List<Imagen>();

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        Imagen ima = new Imagen();
                        ima.Url = (string)datos.Lector["ImagenUrl"];
                        aux.imagenes.Add(ima);
                    }

                    //aux.urlImagen = (string)datos.Lector["ImagenUrl"];
                    
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                return lista;
                //throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }
        
        
       

        public void agregarImagen(int articulo, string url)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.settearConsulta("insert into IMAGENES (IdArticulo, ImagenUrl) values (@IdArticulo, @ImagenUrl)");
                datos.setearParametro("@IdArticulo", articulo);
                datos.setearParametro("@ImagenUrl", url);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                // Manejo adecuado de la excepción, por ejemplo, registro de errores
                Console.WriteLine("Error al agregar imagen: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.settearConsulta("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, Precio = @Precio where Id = @Id");

                datos.setearParametro("@Codigo", articulo.codigo);
                datos.setearParametro("@Nombre", articulo.nombre);
                datos.setearParametro("@Descripcion", articulo.descripcion);
                datos.setearParametro("@IdMarca", articulo.marcaArticulo.id);
                datos.setearParametro("@IdCategoria", articulo.categoriaArticulo.ID);
                datos.setearParametro("@Precio", articulo.precio);
                datos.setearParametro("@Id", articulo.id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                // Manejo adecuado de la excepción, por ejemplo, registro de errores
                Console.WriteLine("Error al modificar artículo: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminarFisica(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.settearConsulta("delete from ARTICULOS where Id = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int UltimoArticuloRegistrado()
        {
            AccesoDatos datos = new AccesoDatos();
            int UltimoIdArticulo = -1;
            try
            {
                datos.settearConsulta("SELECT IDENT_CURRENT('ARTICULOS') AS UltimoID");
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    UltimoIdArticulo = Convert.ToInt32(datos.Lector["UltimoID"]);
                }

                return UltimoIdArticulo+1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Agregar2(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.settearConsulta("Insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) values (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio)" );

                datos.setearParametro("@Codigo", articulo.codigo);
                datos.setearParametro("@Nombre", articulo.nombre);
                datos.setearParametro("@Descripcion", articulo.descripcion);
                datos.setearParametro("@IdMarca", articulo.marcaArticulo.id);
                datos.setearParametro("@IdCategoria", articulo.categoriaArticulo.ID);
                datos.setearParametro("@Precio", articulo.precio);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                // Manejo adecuado de la excepción, por ejemplo, registro de errores
                Console.WriteLine("Error al agregar articulo " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
