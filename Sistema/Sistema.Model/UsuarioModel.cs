﻿using System;
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
            return new UsuarioDAO().Insere_Altera(objTabela);
        }

        public List<Tb_Usuario> Buscar()
        {
            return new UsuarioDAO().Listar();
        }

        public Tb_Usuario Login(Tb_Usuario objTabela)
        {
            return new UsuarioDAO().PesquisaUsuarioLogin(objTabela);
        }

        public static int Excluir(Tb_Usuario objTabela)
        {
            return new UsuarioDAO().Excluir(objTabela);
        }

        public List<Tb_Usuario> Pesquisar(string pesquisa)
        {
            return new UsuarioDAO().Pesquisar(pesquisa);
        }
    }
}
