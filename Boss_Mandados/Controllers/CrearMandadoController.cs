﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Boss_Mandados.Models;

namespace Boss_Mandados.Controllers
{
    public class CrearMandadoController : Controller
    {
        private RepartidoresEntities db_repartidores = new RepartidoresEntities();
        private UsuariosEntities db_usuarios = new UsuariosEntities();
        private ServiciosEntities db_servicios = new ServiciosEntities();
        private MandadosEntities db_mandados = new MandadosEntities();
        private MandadosRutasEntities db_rutas = new MandadosRutasEntities();
        private MandadosEstadosEntities db_estados = new MandadosEstadosEntities();
        private ClientesEntities db_clientes = new ClientesEntities();

        public struct Repartidor
        {
            public int id;
            public string nombre;
        }

        public struct Servicio
        {
            public int id;
            public string nombre;
        }

        public struct Estado
        {
            public int id;
            public string nombre;
        }

        // GET: CrearMandado
        public ActionResult Index()
        {
            //Repartidores
            List<Repartidor> repartidores = new List<Repartidor>();
            var repartidores_db = db_repartidores.manboss_repartidores.ToList();
            foreach (var repartidor in repartidores_db)
            {
                Repartidor aux = new Repartidor();
                aux.nombre = db_usuarios.manboss_usuarios.Where(x => x.id == repartidor.repartidor).Select(x => x.nombre).FirstOrDefault();
                aux.id = repartidor.repartidor;
                repartidores.Add(aux);
            }
            ViewBag.repartidores = repartidores;
            //Servicios
            List<Servicio> servicios = new List<Servicio>();
            var servicios_db = db_servicios.manboss_servicios.ToList();
            foreach (var servicio in servicios_db)
            {
                Servicio aux = new Servicio();
                aux.id = servicio.id;
                aux.nombre = servicio.nombre;
                servicios.Add(aux);
            }
            ViewBag.servicios = servicios;
            //Estados
            List<Estado> estados = new List<Estado>();
            var estados_db = db_estados.manboss_mandados_estados.ToList();
            foreach (var estado in estados_db)
            {
                Estado aux = new Estado();
                aux.id = estado.id;
                aux.nombre = estado.nombre;
                estados.Add(aux);
            }
            ViewBag.estados = estados;
            ViewBag.rutas = Session["rutas_mandados"];
            return View();
        }

        [HttpPost]
        public ActionResult Agregar_Ruta(int servicio, float latitud, float longitud, string comentarios)
        {
            Ruta aux = new Ruta();
            aux.id_servicio = servicio;
            aux.servicio = db_servicios.manboss_servicios.Where(x => x.id == servicio).Select(x => x.nombre).FirstOrDefault();
            aux.latitud = latitud;
            aux.longitud = longitud;
            aux.comentarios = comentarios;
            List<Ruta> rutas = (List<Ruta>)Session["rutas_mandados"];
            rutas.Add(aux);
            Session["rutas_mandados"] = rutas;
            ViewBag.rutas = Session["rutas_mandados"];
            return Content("exito");
        }

        [HttpPost]
        public ActionResult Agregar_Mandado(string nombre, string correo, string telefono, float total)
        {
            //Crear Cliente
            manboss_clientes nuevo_cliente = new manboss_clientes();
            nuevo_cliente.nombre = nombre;
            nuevo_cliente.correo = correo;
            nuevo_cliente.telefono = telefono;
            db_clientes.manboss_clientes.Add(nuevo_cliente);
            db_clientes.SaveChanges();
            int cliente_id = nuevo_cliente.id;
            //Crear Mandado
            manboss_mandados nuevo_mandado = new manboss_mandados();
            nuevo_mandado.estado = 1;
            nuevo_mandado.cliente = cliente_id;
            nuevo_mandado.total = total;
            nuevo_mandado.fecha = DateTime.Now;
            nuevo_mandado.tipo_pago = 0;
            nuevo_mandado.cuenta_pendiente = 0;
            db_mandados.manboss_mandados.Add(nuevo_mandado);
            db_mandados.SaveChanges();
            int mandado_id = nuevo_mandado.id;
            //Crear Rutas del Mandado
            List<Ruta> rutas = (List<Ruta>)Session["rutas_mandados"];
            foreach (var ruta in rutas)
            {
                manboss_mandados_rutas nueva_ruta = new manboss_mandados_rutas();
                nueva_ruta.mandado = mandado_id;
                nueva_ruta.servicio = ruta.id_servicio;
                nueva_ruta.latitud = ruta.latitud;
                nueva_ruta.longitud = ruta.longitud;
                nueva_ruta.comentarios = ruta.comentarios;
                db_rutas.manboss_mandados_rutas.Add(nueva_ruta);
                db_rutas.SaveChanges();
            }
            return Content("exito");
        }
    }
}