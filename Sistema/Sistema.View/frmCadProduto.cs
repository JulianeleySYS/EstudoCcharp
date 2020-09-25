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
    public partial class frmCadProduto : Form
    {
        Tb_Produtos objTabela;

        public frmCadProduto()
        {
            InitializeComponent();
            objTabela = new Tb_Produtos();
        }

        private void frmCadProduto_Load(object sender, EventArgs e)
        {

        }

        private void LimparCampos()
        {
            txtPesquisa.Text = "";
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
                lista = new UsuarioModel().Buscar();
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
            txtPesquisa.Enabled = true;
            txtPesquisa.Focus();
            txtPesquisa.Select();
        }

        private void Opcoes(string op)
        {
            switch (op)
            {
                case "Novo":
                    LimparCampos();
                    HabilitarCampos();
                    txtPesquisa.Enabled = false;
                    txtNome.Focus();
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
                            MessageBox.Show("Não foi possível cadastrar este Usuário, \nverifique os Campos se por acaso estão Correto!");
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

                case "Buscar":
                    dgv_listaUsuarios.DataSource = null;
                    try
                    {
                        List<Tb_Usuario> lista = new List<Tb_Usuario>();
                        lista = new UsuarioModel().Pesquisar(txtPesquisa.Text);
                        dgv_listaUsuarios.AutoGenerateColumns = false;
                        dgv_listaUsuarios.DataSource = lista;
                    }
                    catch (Exception)
                    {

                    }
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
            if (txtNome.Text == "")
            {
                MessageBox.Show("É necessário preencher o Campo Nome!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
            }
            else
            {
                if (txtUsuario.Text == "")
                {
                    MessageBox.Show("É necessário preencher o Campo Usuário!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsuario.Focus();
                }
                else
                {
                    if (txtSenha.Text == "")
                    {
                        MessageBox.Show("É necessário preencher o Campo Senha!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSenha.Focus();
                    }
                    else
                    {
                        Opcoes("Salvar");
                        Inicio();
                    }
                }
            }

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
            DesabilitarCampos();
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtPesquisa.Text != "")
            {
                Opcoes("Buscar");
            }
            else
            {
                MessageBox.Show("Não Existe nenhum valor para realizar a Pesquisa");
            }
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            if (txtPesquisa.Text == "")
            {
                Carregagrid();
            }
            else
            {
                Opcoes("Buscar");

            }
        }
    }
}
}
