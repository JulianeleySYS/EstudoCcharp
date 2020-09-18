using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace Sistema.DAO
{
    class Conexao
    {

        string string_conexao = @"Data Source=SMC-10-26-5525\SQLEXPRESS;Initial Catalog=bancomvc;Integrated Security=True";
        string string_conexao2 = @"Data Source=DESKTOP-C0L5C87;Initial Catalog=bancomcv;Integrated Security=True";
        private SqlConnection _conexao = null;

        public Conexao()
        {
            this._conexao = new SqlConnection();
            this._conexao.ConnectionString = string_conexao2;
        }

        public SqlConnection Conectar()
        {
            if (_conexao.State == ConnectionState.Closed)
            {
                this._conexao.Open();
            }
            return _conexao;
        }

        public void Desconectar()
        {
            if (_conexao.State == ConnectionState.Open)
            {
                this._conexao.Close();
            }
        }


    }
}
