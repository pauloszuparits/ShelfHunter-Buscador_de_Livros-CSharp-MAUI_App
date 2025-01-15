using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace MauiAppAlexandria
{
    public partial class MainPage : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnSearchBookClicked(object sender, EventArgs e)
        {
            string bookName = BookNameEntry.Text;

            // Exibe o nome do livro pesquisado
            DisplayAlert("Livro Pesquisado", $"Você pesquisou: {bookName}", "OK");

            // Construa a URL da requisição, substituindo espaços por "+"
            string url = $"https://www.googleapis.com/books/v1/volumes?q={Uri.EscapeDataString(bookName)}";

            try
            {
                // Faz a requisição para a API do Google Books
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Garante que a resposta foi bem-sucedida

                string responseBody = await response.Content.ReadAsStringAsync();

                // Parse a resposta JSON usando System.Text.Json
                using (JsonDocument doc = JsonDocument.Parse(responseBody))
                {
                    // Acessa os itens da resposta JSON
                    var items = doc.RootElement.GetProperty("items");

                    if (items.GetArrayLength() > 0)
                    {
                        var firstBook = items[0].GetProperty("volumeInfo");

                        // Extrai as informações do primeiro livro
                        string title = firstBook.GetProperty("title").GetString();
                        string authors = string.Join(", ", firstBook.GetProperty("authors").EnumerateArray());
                        string description = firstBook.GetProperty("description").GetString();
                        string publisher = firstBook.GetProperty("publisher").GetString();
                        string thumbnail = firstBook.GetProperty("imageLinks").GetProperty("thumbnail").GetString();

                        // Exibe as informações do livro
                        DisplayAlert("Resultado da Pesquisa",
                            $"Título: {title}\n" +
                            $"Autores: {authors}\n" +
                            $"Descrição: {description}\n" +
                            $"Editora: {publisher}\n" +
                            $"Thumbnail: {thumbnail}",
                            "OK");
                    }
                    else
                    {
                        DisplayAlert("Resultado da Pesquisa", "Nenhum livro encontrado.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", $"Erro ao pesquisar: {ex.Message}", "OK");
            }
        }
    }
}
