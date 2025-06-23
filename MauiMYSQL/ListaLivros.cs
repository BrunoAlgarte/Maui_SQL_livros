using MauiMYSQL.Models;

namespace MauiMYSQL;

public partial class ListaLivros : ContentPage
{
    Conecta banco = new Conecta();
    Livros livros = new Livros();

    public ListaLivros()
    {
        InitializeComponent();
        CarregarLista();
    }

    private void CarregarLista()
    {
        if (banco.Conexao())
        {
            lblStatus.Text = "Banco conectado com Sucesso!";

            if (livros.Livros_Consulta())
            {
                // Evita setar ItemsSource como null
                lstLivros.ItemsSource = livros.listaLivros?.ToList();
            }
        }
        else
        {
            lblStatus.Text = banco.conexao_status;
        }
    }

    private void btnAdicionar(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtTitulo.Text) ||
            string.IsNullOrWhiteSpace(txtAutor.Text) ||
            string.IsNullOrWhiteSpace(txtLido.Text))
        {
            MostrarAlerta("Atenção", "Preencha todos os campos!");
            return;
        }

        if (!bool.TryParse(txtLido.Text, out bool lido))
        {
            MostrarAlerta("Atenção", "O campo 'Lido' deve ser 'true' ou 'false'.");
            return;
        }

        bool sucesso = livros.Livros_Add(txtTitulo.Text, txtAutor.Text, lido);

        if (sucesso)
        {
            MostrarAlerta("Sucesso", "Livro adicionado com sucesso!");

            txtTitulo.Text = "";
            txtAutor.Text = "";
            txtLido.Text = "";

            CarregarLista();
        }
        else
        {
            MostrarAlerta("Erro", "Erro ao adicionar livro.");
        }
    }

    private void MostrarAlerta(string titulo, string mensagem)
    {
        Application.Current.MainPage.DisplayAlert(titulo, mensagem, "OK");
    }
}
