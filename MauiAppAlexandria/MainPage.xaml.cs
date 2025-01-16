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
            
            SearchButton.IsVisible = false;
            LoadingIndicator.IsRunning = true;
            LoadingIndicator.IsVisible = true;

            string bookName = BookNameEntry.Text;
            string bookAuthor = BookAuthorEntry.Text;
            string publisherName = BookPublisherEntry.Text;

            string url = returnUrl(bookName, bookAuthor, publisherName);

            if (url == null)
            {
                await DisplayAlert("Erro", "Por favor, insira pelo menos um critério de pesquisa.", "OK");
                
                SearchButton.IsVisible = true;
                LoadingIndicator.IsRunning = false;
                LoadingIndicator.IsVisible = false;
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
                        List<Book> books = new List<Book>();

                        foreach (var item in items.EnumerateArray())
                        {
                            var volumeInfo = returnJsonElementIfExists("volumeInfo", item);

                            if (volumeInfo.ValueKind == JsonValueKind.Undefined)
                            {
                                continue;
                            }

                            books.Add(new Book
                            {
                                Title = returnFieldIfExists("title", volumeInfo) ?? "Título não disponível",
                                Authors = string.Join(", ", returnFieldArrayIfExists("authors", volumeInfo)),
                                Description = returnFieldIfExists("description", volumeInfo) ?? "Descrição não disponível",
                                Publisher = returnFieldIfExists("publisher", volumeInfo) ?? "Editora não disponível",
                                Thumbnail = returnFieldIfExists("thumbnail", returnJsonElementIfExists("imageLinks", volumeInfo)) ?? string.Empty,
                                Pages = returnFieldIntIfExists("pageCount", volumeInfo),
                                PublishedYear = returnFieldIfExists("publishedDate", volumeInfo) != null
                                                ? returnFieldIfExists("publishedDate", volumeInfo).Split('-')[0]
                                                : "Ano de publicação não disponível"
                            });
                        }

                        await Navigation.PushAsync(new BooksPage(books));
                    }
                    else
                    {
                        await DisplayAlert("Resultado da Pesquisa", "Nenhum livro/autor encontrado.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro ao pesquisar: {ex.StackTrace}", "OK");
            }
            finally
            {
                SearchButton.IsVisible = true;
                LoadingIndicator.IsRunning = false;
                LoadingIndicator.IsVisible = false;
            }
        }




        private string? returnUrl(string bookName, string bookAuthor, string publisherName)
        {
            if (string.IsNullOrWhiteSpace(bookName) && string.IsNullOrWhiteSpace(bookAuthor) && string.IsNullOrWhiteSpace(publisherName))
            {
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

            if (!string.IsNullOrWhiteSpace(publisherName))
            {
                url += "+inpublisher:" + Uri.EscapeDataString(publisherName);
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

        private string returnFieldIfExists(string fieldName, JsonElement json)
        {
            try
            {
                if (json.TryGetProperty(fieldName, out var jsonUrl))
                {
                    return jsonUrl.GetString();
                }

                return null;
            }
            catch (Exception ex) {
                return null;
            }
        }

        private int returnFieldIntIfExists(string fieldName, JsonElement json)
        {
            try
            {
                if (json.TryGetProperty(fieldName, out var jsonUrl))
                {
                    return jsonUrl.GetInt32();
                }

                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
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
