using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Importação do namespace
using LinqToSqlBasic.Model;

namespace LinqToSqlBasic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            atualizarGridFuncionarios();
            atualizarListViewFuncionarios();

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            //Os nomes das tabelas viram objetos, e seus campos atributos
            Funcionario novoFuncionario = new Funcionario();
            novoFuncionario.Nome = txtNome.Text;
            novoFuncionario.Sexo = rdoMasculino.Checked ? 'M' : 'F';
            novoFuncionario.Telefone = mtxtTelefone.Text;
            novoFuncionario.DataNascimento = dtpNasc.Value;
            novoFuncionario.Salario = Convert.ToDecimal(txtSalario.Text);
            novoFuncionario.Ativo = chkAtivo.Checked;
            novoFuncionario.UltimaAtualizacao = DateTime.Now;

            try
            {
                FuncionarioDataAcess.Inserir(novoFuncionario);
                MessageBox.Show("Inserido com Sucesso!");
                atualizarGridFuncionarios();
                atualizarListViewFuncionarios();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Não Foi Possivel Inserir Erro Inesperado \r\nDetalhes: " + ex.Message);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string textSearch = txtPesquisa.Text.Trim();

            //se conter nada realiza pesquisa ordenada por nome
            if(textSearch == "")
            {
                dataGridView1.DataSource = FuncionarioDataAcess.ObterFuncionario();
            }
            //se apenas conter numeros pesquisa pelo ID
            else if (textSearch.All(char.IsNumber))
            {
                dataGridView1.DataSource = FuncionarioDataAcess.ObterFuncionario(int.Parse(textSearch));
            }
            //senao pesquiso pelo nome que estiver no campo           
            else {
                dataGridView1.DataSource = FuncionarioDataAcess.ObterFuncionario(textSearch);
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            Funcionario novoFuncionario = new Funcionario();
            Funcionario f = dataGridView1.CurrentRow.DataBoundItem as Funcionario;
            
            novoFuncionario.Id = f.Id;

            novoFuncionario.Nome = txtNome.Text;
            novoFuncionario.Sexo = rdoMasculino.Checked ? 'M' : 'F';
            novoFuncionario.Telefone = mtxtTelefone.Text;
            novoFuncionario.DataNascimento = dtpNasc.Value;
            
            decimal testValor;
            if(decimal.TryParse(txtSalario.Text, out testValor))
                novoFuncionario.Salario = testValor;

            novoFuncionario.Ativo = chkAtivo.Checked;
            novoFuncionario.UltimaAtualizacao = DateTime.Now;
            try
            {
                FuncionarioDataAcess.Alterar(novoFuncionario);
                MessageBox.Show("Funcionarios Alterados com Sucesso!");
                atualizarGridFuncionarios();
                atualizarListViewFuncionarios();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Não Foi Possivel Atualizar Erro Inesperado \r\nDetalhes: " + ex.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count < 1)
            {
                MessageBox.Show("Selecione um funcionario para poder excluir");
                return;
            }

            Funcionario funcionario;
            funcionario = dataGridView1.CurrentRow.DataBoundItem as Funcionario;

            try
            {
                FuncionarioDataAcess.Deletar(funcionario);
                MessageBox.Show("Funcionario Excluido com Sucesso!");
                atualizarGridFuncionarios();
                atualizarListViewFuncionarios();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Não Foi Possivel Atualizar Erro Inesperado \r\nDetalhes: " + ex.Message);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Funcionario func = dataGridView1.CurrentRow.DataBoundItem as Funcionario;

            txtNome.Text = func.Nome;
            mtxtTelefone.Text = func.Telefone;
            dtpNasc.Value = func.DataNascimento.Value;
            txtSalario.Text = func.Salario.ToString();
            if(func.Sexo == 'M')
                rdoMasculino.Checked = true;
            else
                rdoFeminino.Checked = true;
            chkAtivo.Checked = (bool)func.Ativo;
        }
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            int id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            Funcionario func = FuncionarioDataAcess.ObterFuncionario(id);

            txtNome.Text = func.Nome;
            mtxtTelefone.Text = func.Telefone;
            dtpNasc.Value = func.DataNascimento.Value;
            txtSalario.Text = func.Salario.ToString();
            if(func.Sexo == 'M')
                rdoMasculino.Checked = true;
            else
                rdoFeminino.Checked = true;
            chkAtivo.Checked = (bool)func.Ativo;
        }

        private void atualizarGridFuncionarios(){
            dataGridView1.DataSource = FuncionarioDataAcess.ObterFuncionario();
        }

        private void atualizarListViewFuncionarios()
        {
            listView1.Items.Clear();
            List<Funcionario> funcionarios = FuncionarioDataAcess.ObterFuncionario();
            
            for(int i = 0; i<funcionarios.Count; i++)
            {
                listView1.Items.Add(funcionarios[i].Id.ToString());
                listView1.Items[i].SubItems.Add(funcionarios[i].Nome);
                listView1.Items[i].SubItems.Add(funcionarios[i].Sexo.ToString());
                listView1.Items[i].SubItems.Add(funcionarios[i].Telefone);

                if(i%2==0)
                    listView1.Items[i].BackColor = Color.LightGray;
            }            
            //                  ou
            //bool hasColor = false;
            //foreach(Funcionario f in funcionarios)
            //{
            //    ListViewItem item = new ListViewItem(f.Nome);
            //    item.SubItems.Add(f.Sexo.ToString());
            //    item.SubItems.Add(f.Salario.ToString());
            //    listView1.Items.Add(item);

            //    if(hasColor)
            //        item.BackColor = Color.LightPink;
            //    hasColor = !hasColor;
            //}
        }
        
        //private void carregarComboOuListBox(){
        //    LinqToSqlBasic_DataClassesDataContext dt = new LinqToSqlBasic_DataClassesDataContext();
        //    comboBox1.DataSource = (from p in dt.Funcionarios select p.Nome);
        //    listBox1.DataSource =  (from p in dt.Funcionarios select p.Nome);
        //}
    }
}
