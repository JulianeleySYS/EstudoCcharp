using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema.Entidades;
using Sistema.Model;

namespace Sistema.View
{
    public partial class frmCadUsuario : Form
    {
        Tb_Usuario objTabela;

        public frmCadUsuario()
        {
            InitializeComponent();
            objTabela = new Tb_Usuario();
        }

        private void Opcoes(string op)
        {
            switch (op)
            {
                case "Novo":
                    HabilitarCampos();
                    LimparCampos();
                    break;

                case "Salvar":
                    try
                    {
                        objTabela.Nome = txtNome.Text;
                        objTabela.Usuario = txtUsuario.Text;
                        objTabela.Senha = txtSenha.Text;

                        int x = UsuarioModel.Inserir(objTabela);
                        if (x > 0)
                        {
                            MessageBox.Show(String.Format("Usuário \"{0}\" foi inserido!", txtNome.Text));
                            DesabilitarCampos();
                            Carregagrid();
                        }
                        else
                        {
                            MessageBox.Show("Não Inserido");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro ao Salvar."+ex);
                        throw;
                    }
                    break;

                case "Excluir":
                    break;

                case "Editar":
                    HabilitarCampos();
                    break;

            }
        }

        private void HabilitarCampos()
        {
            txtNome.Enabled = true;
            txtUsuario.Enabled = true;
            txtSenha.Enabled = true;
        }

        private void DesabilitarCampos()
        {
            txtNome.Enabled = false;
            txtUsuario.Enabled = false;
            txtSenha.Enabled = false;
        }

        private void LimparCampos()
        {
            txtNome.Text = "";
            txtUsuario.Text = "";
            txtSenha.Text = "";
        }



        private void btnNovo_Click(object sender, EventArgs e)
        {
            Opcoes("Novo");
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Opcoes("Salvar");
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Opcoes("Excluir");
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Opcoes("Editar");
        }

        private void Carregagrid()
        {
            try
            {
                List<Tb_Usuario> lista = new List<Tb_Usuario>();
                lista = new UsuarioModel().Listar();
                dgv_listaUsuarios.AutoGenerateColumns = false;
                dgv_listaUsuarios.DataSource = lista;
            }
            catch (Exception)
            {

            }
        }

        private void frmCadUsuario_Load(object sender, EventArgs e)
        {
            Carregagrid();
        }
    }
}
