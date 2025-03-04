using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Bitacora.Models;
using Bitacora.Auth;

namespace Bitacora.Data
{
	public class BitacoraDb : IdentityDbContext<AutenticacionUsuario, IdentityRole<int>, int>
	{
		public BitacoraDb(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<AutenticacionUsuario>().ToTable("UsuariosAutenticados");

			// Agrego la relación entre AutenticaciónUsuario y el modelo Usuario.
			builder.Entity<ModeloUsuario>()
				.HasOne(u => u.PerfilDeAutenticacion)
				.WithOne(a => a.Usuario)
				.HasForeignKey<ModeloUsuario>(u => u.AutenticacionId);

			// Agrego la relación entre una Bitacora y varios Posts.
			builder.Entity<ModeloBitacora>()
				.HasMany(b => b.PostsRealizados)
				.WithOne(p => p.BitacoraAsociada)
				.HasForeignKey(p => p.BitacoraId);
		}

		public DbSet<ModeloUsuario> Usuarios { get; set; }
		public DbSet<ModeloBitacora> Bitacoras { get; set; }
		public DbSet<ModeloPost> Posts { get; set; }

	}
}
