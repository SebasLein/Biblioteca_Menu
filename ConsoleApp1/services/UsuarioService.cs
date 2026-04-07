using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace services
{
    public class UsuarioService
    {
        private List<Usuario> usuarios = new List<Usuario>();

        // =========================
        // CRUD BÁSICO
        // =========================

        public void AgregarUsuario(Usuario usuario)
        {
            usuarios.Add(usuario);
        }

        public List<Usuario> ObtenerTodos()
        {
            return usuarios;
        }

        public void EliminarUsuario(Usuario usuario)
        {
            usuarios.Remove(usuario);
        }

        // =========================
        // BÚSQUEDAS
        // =========================

        public Usuario BuscarPorId(int id)
        {
            return usuarios.Find(u => u.Id == id);
        }

        public List<Usuario> BuscarPorNombre(string nombre)
        {
            return usuarios.FindAll(u => u.Nombre.ToLower().Contains(nombre.ToLower()));
        }

        // =========================
        // ORDENACIÓN
        // =========================

        public List<Usuario> OrdenarPorNombre()
        {
            return usuarios.OrderBy(u => u.Nombre).ToList();
        }

        // =========================
        // KPIs
        // =========================

        public int ObtenerTotalUsuarios()
        {
            return usuarios.Count;
        }

        public int ObtenerUsuariosActivos()
        {
            return usuarios.Count(u => u.Activo);
        }

        public int ObtenerUsuariosInactivos()
        {
            return usuarios.Count(u => !u.Activo);
        }
    }
}