using Gestion_Rendimiento_Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Data
{
   public  class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
           : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        public virtual DbSet<Evaluador> Evaluador { get; set; }
        public virtual DbSet<Oficina> Oficina { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Variable> Variable { get; set; }
        public virtual DbSet<EvaluadorConsulta> EvaluadorConsulta { get; set; }
        public virtual DbSet<RendimientoConsulta> RendimientoConsulta { get; set; }
        public virtual DbSet<Evaluado> Evaluado { get; set; }
        public virtual DbSet<Proyecto> Proyecto { get; set; }
        public virtual DbSet<Proyecto_Detalle> Proyecto_Detalle { get; set; }
        public virtual DbSet<TipoGestionRendimiento> TipoGestionRendimiento { get; set; }
        public virtual DbSet<ConfiguracionRendimiento> ConfiguracionRendimiento { get; set; }
        public virtual DbSet<ConfiguracionDetalle> ConfiguracionDetalle { get; set; }
        public virtual DbSet<ProyectoSeguimiento> ProyectoSeguimiento { get; set; }
        public virtual DbSet<NivelSeguimiento> NivelSeguimiento { get; set; }
        public virtual DbSet<ProyectoSeguimientoConsulta> ProyectoSeguimientoConsulta { get; set; }
        public virtual DbSet<Proyecto_Detalle_Sub> Proyecto_Detalle_Sub { get; set; }
        public virtual DbSet<ReporteRendimiento> ReporteRendimiento { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var esquema = Gestion_Rendimiento_Common.Constantes.EsquemaBD;
            modelBuilder.HasDefaultSchema(esquema);
            //   modelBuilder.Entity<SaldoMigrado>().Property(b => b.SALDO_APERTURA).HasPrecision(18, 2);

            modelBuilder.Entity<Variable>()
              .HasKey(c => new { c.SISTEMA, c.CAMPO, c.VALOR });


        }

    }
}
