﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.Entidades;
using System.Data;
using System.Data.SqlClient;

namespace Sistema.DAO
{
    public class UsuarioDAO
    {
        public int Insere_Altera(Tb_Usuario objTabela)
        {
            Conexao con = new Conexao();
            SqlCommand cmd = new SqlCommand();
            int result = -1;

            if (objTabela.Id <= 0)
            {
                cmd.CommandText = "INSERT INTO usuarios ([nome],[usuario],[senha]) VALUES (@nome, @usuario, @senha)";
            }
            else
            {
                cmd.CommandText = "Update usuarios set nome=@nome, usuario=@usuario, senha=@senha where id = @codigo";
                cmd.Parameters.AddWithValue("@codigo", objTabela.Id);
            }

            cmd.Parameters.AddWithValue("@nome", objTabela.Nome);
            cmd.Parameters.AddWithValue("@usuario", objTabela.Usuario);
            cmd.Parameters.AddWithValue("@senha", objTabela.Senha);

            try
            {
                cmd.Connection = con.Conectar();
                result = cmd.ExecuteNonQuery();
                con.Desconectar();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return result;
        }

        public List<Tb_Usuario> Pesquisar(string pesquisa)
        {
            Conexao con = new Conexao();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "Select * From usuarios where nome like @nome";
            cmd.Parameters.AddWithValue("@nome", pesquisa + "%"); 
            List<Tb_Usuario> lista = new List<Tb_Usuario>();
            SqlDataReader dr;
            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                Tb_Usuario usuario;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        usuario = new Tb_Usuario();
                        usuario.Id = Convert.ToInt32(dr["id"]);
                        usuario.Nome = Convert.ToString(dr["nome"]);
                        usuario.Usuario = Convert.ToString(dr["usuario"]);
                        usuario.Senha = Convert.ToString(dr["senha"]);
                        lista.Add(usuario);
                    }
                }
                con.Desconectar();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return lista;
        }

        public int Excluir(Tb_Usuario objTabela)
        {
            Conexao con = new Conexao();
            SqlCommand cmd = new SqlCommand();
            int result = -1;

            if (objTabela.Id > 0)
            {
                cmd.CommandText = "Delete From usuarios where id = @codigo";
                cmd.Parameters.AddWithValue("@codigo", objTabela.Id);
            }

            try
            {
                cmd.Connection = con.Conectar();
                result = cmd.ExecuteNonQuery();
                con.Desconectar();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return result;
        }

        public Tb_Usuario PesquisaUsuarioLogin(Tb_Usuario objTabela)
        {
            Conexao con = new Conexao();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "Select * From usuarios where usuario=@usuario and senha=@senha";

            cmd.Parameters.AddWithValue("@usuario", objTabela.Usuario);
            cmd.Parameters.AddWithValue("@senha", objTabela.Senha);

            Tb_Usuario u = null;
            SqlDataReader dr;
            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        u = new Tb_Usuario();
                        u.Id = Convert.ToInt32(dr["id"]);
                        u.Nome = Convert.ToString(dr["nome"]);
                        u.Usuario = Convert.ToString(dr["usuario"]);
                        u.Senha = Convert.ToString(dr["senha"]);
                    }
                }
                con.Desconectar();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return u;
        }

        public int PesquisaUsuario(Tb_Usuario objTabela)
        {
            Conexao con = new Conexao();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "Select * From usuarios where usuario=@usuario";
            cmd.Parameters.AddWithValue("@usuario", objTabela.Usuario);

            SqlDataReader dr;
            int result = -1;
            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    result = 1;
                }
                con.Desconectar();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return result;
        }

        public List<Tb_Usuario> Listar()
        {
            Conexao con = new Conexao();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "Select * From usuarios order by id desc";
            List<Tb_Usuario> lista = new List<Tb_Usuario>();
            SqlDataReader dr;
            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                Tb_Usuario usuario;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        usuario = new Tb_Usuario();
                        usuario.Id = Convert.ToInt32(dr["id"]);
                        usuario.Nome = Convert.ToString(dr["nome"]);
                        usuario.Usuario = Convert.ToString(dr["usuario"]);
                        usuario.Senha = Convert.ToString(dr["senha"]);
                        lista.Add(usuario);
                    }
                }
                con.Desconectar();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return lista;
        }
    }
}
