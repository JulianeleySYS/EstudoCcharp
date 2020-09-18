using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.DAO;
using Sistema.Entidades;

namespace Sistema.Model
{
    public class UsuarioModel
    {
        public static int Inserir(Tb_Usuario objTabela)
        {
            return new UsuarioDAO().Inserir(objTabela);
        }

        public List<Tb_Usuario> Listar()
        {
            return new UsuarioDAO().Listar();
        }

        public Tb_Usuario Login(Tb_Usuario objTabela)
        {
            return new UsuarioDAO().PesquisaUsuario(objTabela);
        }
    }
}
