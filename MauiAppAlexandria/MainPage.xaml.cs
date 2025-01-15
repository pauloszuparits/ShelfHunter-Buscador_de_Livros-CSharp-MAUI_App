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
            string bookAuthor = BookAuthorEntry.Text;

            string url = returnUrl(bookName, bookAuthor);
            if (url == null)
            {
                return;
            }

            try
            {
                
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode(); 

                string responseBody = await response.Content.ReadAsStringAsync();

                
                using (JsonDocument doc = JsonDocument.Parse(responseBody))
                {

                    if (doc.RootElement.TryGetProperty("items", out var items) && items.ValueKind == JsonValueKind.Array)
                    {
                        if (items.GetArrayLength() > 0)
                        {
                            var firstBook = returnJsonElementIfExists("volumeInfo", items[0]);
                            if (firstBook.ValueKind == JsonValueKind.Undefined)
                            {
                                await DisplayAlert("Resultado da Pesquisa", "Nenhum dado encontrado para esse livro/autor", "OK");
                                return;
                            }

                            string title = returnFieldIfExists("title", firstBook) ?? "Título não disponível";
                            string authors = string.Join(", ", returnFieldArrayIfExists("authors", firstBook)) ?? "Autores não disponíveis";
                            string description = returnFieldIfExists("description", firstBook) ?? "Descrição não disponível";
                            string publisher = returnFieldIfExists("publisher", firstBook) ?? "Editora não disponível";
                            string thumbnail = string.Empty;

                            var imageLinks = returnJsonElementIfExists("imageLinks", firstBook);
                            if (imageLinks.ValueKind != JsonValueKind.Undefined)
                            {
                                thumbnail = returnFieldIfExists("thumbnail", imageLinks) ?? "Imagem não disponível";
                            }


                            await Navigation.PushAsync(new BookDetailsPage(title, authors, description, publisher, thumbnail));

                        }
                        else
                        {
                           await  DisplayAlert("Resultado da Pesquisa", "Nenhum livro/autor encontrado.", "OK");
                        }
                    }
                    else {
                        await DisplayAlert("Resultado da Pesquisa", "Nenhum livro/autor encontrado.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro ao pesquisar: {ex.Message}", "OK");
            }
        }

        private string? returnUrl(string bookName, string bookAuthor)
        {
            if (string.IsNullOrWhiteSpace(bookName) && string.IsNullOrWhiteSpace(bookAuthor))
            {
                DisplayAlert("Erro", "Por favor, digite o nome de um livro ou autor.", "OK");
                return null;
            }

            string url = "https://www.googleapis.com/books/v1/volumes?q=";

            if (!string.IsNullOrWhiteSpace(bookName))
            {
                url += Uri.EscapeDataString(bookName);
            }

            if (!string.IsNullOrWhiteSpace(bookAuthor))
            {
                url += "+inauthor:" + Uri.EscapeDataString(bookAuthor);
            }
            return url;
        }

        private JsonElement returnJsonElementIfExists(string fieldName, JsonElement json)
        {
            if (json.TryGetProperty(fieldName, out var jsonElement))
            {
                return jsonElement;
            }
            return default; 
        }

        private string? returnFieldIfExists(string fieldName, JsonElement json)
        {
            if (json.TryGetProperty(fieldName, out var jsonUrl))
            {
                return jsonUrl.GetString();
            }
            return null; 
        }

        private IEnumerable<string> returnFieldArrayIfExists(string fieldName, JsonElement json)
        {
            if (json.TryGetProperty(fieldName, out var jsonUrl) && jsonUrl.ValueKind == JsonValueKind.Array)
            {
                return jsonUrl.EnumerateArray().Select(x => x.GetString()).Where(x => !string.IsNullOrEmpty(x));
            }
            return Enumerable.Empty<string>(); 
        }
    }
}
