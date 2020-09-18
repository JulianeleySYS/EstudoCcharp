using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema.Model;
using Sistema.Entidades;

namespace Sistema.View
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtUsuario.Text == "")
            {
                MessageBox.Show("Preencha o Campo Usuári");
                txtUsuario.Focus();
            }
            else
            {
                if(txtSenha.Text == "")
                {
                    MessageBox.Show("Preencha o Campo Senha");
                    txtSenha.Focus();
                }
                else
                {
                    Tb_Usuario objTabela = new Tb_Usuario();
                    objTabela.Usuario = txtUsuario.Text;
                    objTabela.Senha = txtSenha.Text;

                    objTabela = new UsuarioModel().Login(objTabela);

                    if (objTabela == null)
                    {
                        lblMensagem.Text = "Usuário ou Senha Inválido!";
                        lblMensagem.ForeColor = Color.Red;
                    }
                    else
                    {
                        this.Hide(); //oculta o formulario que esta aberto no momento

                        frmCadUsuario form = new frmCadUsuario();
                        form.FormClosed += (s, args) => this.Close(); //fecha o formulario atual que esta escondido para que ele não volte a tela

                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.Show(); //exibe o novo formulario que no caso é o de cadastro de usuario
                        
                        

                    }
                }
            }
        }
    }
}
