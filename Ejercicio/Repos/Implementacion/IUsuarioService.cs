using Microsoft.EntityFrameworkCore;
using ProyectoLogin.Models;

namespace ProyectoLogin.Servicios.Implementacion
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuario(string correo, string clave);
        Task<Usuario> SaveUsuario(Usuario modelo);

    }
    public class UsuarioService : IUsuarioService
    {
        private readonly Context _dbContext;
        public UsuarioService(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuario> GetUsuario(string correo, string clave)
        {
            Usuario usuario_encontrado = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo && u.Clave == clave);
            return usuario_encontrado;
        }

        public async Task<Usuario> SaveUsuario(Usuario modelo)
        {
            _dbContext.Usuarios.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return modelo;
        }
    }
}
