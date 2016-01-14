using Microsoft.AspNet.Builder;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Hosting;
using Microsoft.Dnx.Runtime;

namespace grole.src
{

    public class Startup
	{
		
		public IConfiguration Configuration { get; set; }
        private readonly IApplicationEnvironment _appEnvironment;
        public Startup()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("Config.json");
            Configuration = builder.Build();
        }
		
		public void ConfigureServices(IServiceCollection Services)
		{
			Services.AddCaching();
            Services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromDays(1);
            });
            Services.AddAuthentication();
            Services.AddSingleton<IConfiguration>(Configuration);
            
            Services.AddTransient<IApplicationEnvironment>();
            //Sobrantes
            Services.AddTransient<grole.src.Persistencia.SobrantesMPersistencia>();
            Services.AddTransient<grole.src.Persistencia.SobrantesDPersistencia>();
            Services.AddTransient<grole.src.Logica.SobrantesMLogica>();
            //ValoresRescate
            Services.AddTransient<grole.src.Persistencia.Valores_RescatePersistencia>();
            Services.AddTransient<grole.src.Logica.Valores_RescateLogica>();
            //MateriaPrima
            Services.AddTransient<grole.src.Persistencia.MateriaPrimaPersistencia>();
            Services.AddTransient<grole.src.Logica.MateriaPrimaLogica>();
            //Canales
            Services.AddTransient<grole.src.Persistencia.CanalesPersistencia>();
            Services.AddTransient<grole.src.Logica.CanalesLogica>();
            //ResumenCortesPersistencia
            Services.AddTransient<grole.src.Persistencia.ResumenCortesPersistencia>();
            //InformacionCaja
            Services.AddTransient<grole.src.Persistencia.InformacionCajaPersistencia>();
            Services.AddTransient<grole.src.Logica.InformacionCajaLogica>();
            //RastreoFolios
            Services.AddTransient<grole.src.Persistencia.RastreoFoliosPersistencia>();
            Services.AddTransient<grole.src.Logica.RastreoFoliosLogica>();
            //Correos
            Services.AddTransient<grole.Models.Mail>();
            //Canales
            Services.AddTransient<grole.src.Persistencia.CanalesPersistencia>();
            Services.AddTransient<grole.src.Logica.CanalesLogica>();
            //Eliminadas
            Services.AddTransient<grole.src.Persistencia.EliminadasPersistencia>();
            Services.AddTransient<grole.src.Logica.EliminadasLogica>();
            //Basculas
            Services.AddTransient<grole.src.Persistencia.BasculasPersistencia>();
            //EstimacionEmpaques
            Services.AddTransient<grole.src.Persistencia.EstimacionEmpaquesDPersistencia>();
            Services.AddTransient<grole.src.Persistencia.EstimacionEmpaquesMPersistencia>();
            Services.AddTransient<grole.src.Logica.EstimacionEmpaquesLogica>();
            //OrdenesProduccion
            Services.AddTransient<grole.src.Persistencia.OrdenesProduccionPersistencia>();
            Services.AddTransient<grole.src.Logica.OrdenesProduccionLogica>();
            //Embarques
            Services.AddTransient<grole.src.Persistencia.EmbarquesPersistencia>();
            Services.AddTransient<grole.src.Logica.EmbarquesLogica>();
            //Cambios tara
            Services.AddTransient<grole.src.Persistencia.CambiosTaraPersistencia>();
            //Salida Inventario
            Services.AddTransient<grole.src.Persistencia.SalidaInventarioPersistencia>();
            //Cortes
            Services.AddTransient<grole.src.Logica.CortesLogica>();
            //Etiquetas
            Services.AddTransient<grole.src.Logica.EtiquetasLogica>();
            //Tarimas
            Services.AddTransient<grole.src.Persistencia.TarimasPersistencia>();
            Services.AddTransient<grole.src.Logica.TarimasLogica>();
            //Destinos
            Services.AddTransient<grole.src.Persistencia.DestinosPersistencia>();
            Services.AddTransient<grole.src.Logica.DestinosLogica>();
            //Clase orden produccion
            Services.AddTransient<grole.src.Persistencia.ClaseOrdenProduccionPersistencia>();
            Services.AddTransient<grole.src.Logica.ClaseOrdenProduccionLogica>();
            //Clasificacion cortes
            Services.AddTransient<grole.src.Persistencia.ClasificacionCortesPersistencia>();
            Services.AddTransient<grole.src.Logica.ClasificacionCortesLogica>();
            //Empresas
            Services.AddTransient<grole.src.Persistencia.EmpresasPersistencia>();
            //Configuraciones
            Services.AddTransient<grole.src.Logica.ConfiguracionesLogica>();
            //Empaques
            Services.AddTransient<grole.src.Persistencia.EmpaquesPersistencia>();
            Services.AddTransient<grole.src.Logica.EmpaquesLogica>();
            //Lotes no inventariables
            Services.AddTransient<grole.src.Persistencia.LotesNoInventariablesPersistencia>();
            Services.AddTransient<grole.src.Logica.LotesNoInventariablesLogica>();
            //Usuarios
            Services.AddTransient<grole.src.Persistencia.UsuariosPersistencia>();
            Services.AddTransient<grole.src.Logica.UsuariosLogica>();
            //Accounts
            Services.AddTransient<grole.src.Persistencia.AccountsPersistencia>();
            Services.AddTransient<grole.src.Logica.AccountsLogica>();
            //Lineas
            Services.AddTransient<grole.src.Persistencia.LineasPersistencia>();
            Services.AddTransient<grole.src.Logica.LineasLogica>();
            //Cajas
            Services.AddTransient<grole.src.Persistencia.CajasPersistencia>();
            Services.AddTransient<grole.src.Logica.CajasLogica>();
            //Cámaras
            Services.AddTransient<grole.src.Persistencia.CamarasPersistencia>();
            Services.AddTransient<grole.src.Logica.CamarasLogica>();
            //Conexiones
            Services.AddTransient<grole.src.Persistencia.Conexiones>();
            //Productos
            Services.AddTransient<grole.src.Persistencia.ProductosPersistencia>();
            Services.AddTransient<grole.src.Logica.ProductosLogica>();
            //Granjas
            Services.AddTransient<grole.src.Persistencia.GranjasPersistencia>();
            Services.AddTransient<grole.src.Logica.GranjasLogica>();
            //Productos
            Services.AddTransient<grole.src.Persistencia.ProductosPersistencia>();
			Services.AddTransient<grole.src.Logica.ProductosLogica>();
			//UsuarioBascula
			Services.AddTransient<grole.src.Persistencia.UsuarioBasculaPersistencia>();
			Services.AddTransient<grole.src.Logica.UsuarioBasculaLogica>();
			//Clases
			Services.AddTransient<grole.src.Persistencia.ClasesPersistencia>();
			Services.AddTransient<grole.src.Logica.ClasesLogica>();
			//Clientes Grole
			Services.AddTransient<grole.src.Persistencia.ClientesGrolePersistencia>();
			Services.AddTransient<grole.src.Logica.ClientesGroleLogica>();
			//Costos Maquila
			Services.AddTransient<grole.src.Persistencia.CostosMaquilaPersistencia>();
			Services.AddTransient<grole.src.Logica.CostosMaquilaLogica>();
            //Categorias
            Services.AddTransient<grole.src.Persistencia.CategoriasPersistencia>();
            Services.AddTransient<grole.src.Logica.CategoriasLogica>();
			
            //Recetas
            Services.AddTransient<grole.src.Persistencia.RecetasPersistencia>();
            Services.AddTransient<grole.src.Logica.RecetasLogica>();
            Services.AddMvc();
        }
		
		public void Configure(IApplicationBuilder app)
		{
            app.UseIISPlatformHandler();
            app.UseSession();
            app.UseDeveloperExceptionPage();
            
            app.UseCookieAuthentication(options => 
			{
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
				options.AutomaticAuthenticate = true;
				options.LoginPath = "/Account/Login";
			});
			app.UseStaticFiles();
			app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");

                // Uncomment the following line to add a route for porting Web API 2 controllers.
                // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });
        }
        
         public static void Main(string[] args) 
         { 
             var application = new WebApplicationBuilder() 
                 .UseConfiguration(WebApplicationConfiguration.GetDefault(args)) 
                 .UseStartup<Startup>() 
                 .Build(); 

             application.Run();
         } 
		
	}
	
}