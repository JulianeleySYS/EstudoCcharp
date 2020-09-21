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

        private void LimparCampos()
        {
            txtCodigo.Text = "";
            txtNome.Text = "";
            txtUsuario.Text = "";
            txtSenha.Text = "";
        }

        private void Carregagrid()
        {
            dgv_listaUsuarios.DataSource = null;
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

        private void HabilitarCampos()
        {
            txtNome.Enabled = true;
            txtUsuario.Enabled = true;
            txtSenha.Enabled = true;
        }

        private void Inicio()
        {
            DesabilitarCampos();
            LimparCampos();
            Carregagrid();
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnEditar.Enabled = false;
        }

        private void Opcoes(string op)
        {
            switch (op)
            {
                case "Novo":
                    LimparCampos();
                    HabilitarCampos();
                    objTabela.Id = 0;
                    break;

                case "Salvar":
                    try
                    {
                        if (txtCodigo.Text != "")
                            objTabela.Id = Convert.ToInt32(txtCodigo.Text);
                        objTabela.Nome = txtNome.Text;
                        objTabela.Usuario = txtUsuario.Text;
                        objTabela.Senha = txtSenha.Text;

                        int x = UsuarioModel.Inserir(objTabela);
                        if (x > 0)
                        {
                            if (txtCodigo.Text == "")
                            {
                                MessageBox.Show("Usuário Gravado com Sucesso!");
                            }
                            else
                            {
                                MessageBox.Show("Usuário Alterado com Sucesso!");
                            }
                            Inicio();
                            Carregagrid();
                        }
                        else
                        {
                            MessageBox.Show("Não Inserido");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro ao Salvar." + ex);
                        throw;
                    }
                    break;

                case "Excluir":
                    try
                    {
                        if (txtCodigo.Text != "")
                            objTabela.Id = Convert.ToInt32(txtCodigo.Text);

                        int x = UsuarioModel.Excluir(objTabela);
                        if (x > 0)
                        {
                            if (txtCodigo.Text == "")
                            {
                                MessageBox.Show("Usuário Excluído com Sucesso!");
                            }
                            Inicio();
                            Carregagrid();
                        }
                        else
                        {
                            MessageBox.Show("Não foi Possivel Excluir este Registro");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro no Banco." + ex);
                        throw;
                    }
                    break;

                case "Editar":
                    HabilitarCampos();
                    break;

            }
        }

        

        private void DesabilitarCampos()
        {
            txtNome.Enabled = false;
            txtUsuario.Enabled = false;
            txtSenha.Enabled = false;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            Opcoes("Novo");
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            btnEditar.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Opcoes("Salvar");
            Inicio();
        }


        private void btnEditar_Click(object sender, EventArgs e)
        {
            Opcoes("Editar");
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            btnEditar.Enabled = false;
        }

        

        private void frmCadUsuario_Load(object sender, EventArgs e)
        {
            Inicio();
        }

        private void dgv_listaUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCodigo.Text = dgv_listaUsuarios.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = dgv_listaUsuarios.CurrentRow.Cells[1].Value.ToString();
            txtUsuario.Text = dgv_listaUsuarios.CurrentRow.Cells[2].Value.ToString();
            txtSenha.Text = dgv_listaUsuarios.CurrentRow.Cells[3].Value.ToString();
            btnNovo.Enabled = false;
            btnSalvar.Enabled = false;
            btnEditar.Enabled = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Inicio();
        }

        private void dgv_listaUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult result = MessageBox.Show("Deseja Excluir este Registro?", "Excluir:", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                Opcoes("Excluir");
            }
        }
    }
}
