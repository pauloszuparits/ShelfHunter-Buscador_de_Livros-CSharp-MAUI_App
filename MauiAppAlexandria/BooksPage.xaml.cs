namespace MauiAppAlexandria {
    public partial class BooksPage : ContentPage
    {
        private List<Book> books;
        private int currentIndex;

        public BooksPage(List<Book> books)
        {
            InitializeComponent();
            this.books = books;
            this.currentIndex = 0;

            DisplayBookDetails(books[currentIndex]);
        }

        private void DisplayBookDetails(Book book)
        {

            TitleLabel.Text = book.Title.Length > 50 ? book.Title.Substring(0, 50) + "..." : book.Title;
            AuthorsLabel.Text = book.Authors;
            PublisherLabel.Text = book.Publisher;     
            DescriptionLabel.Text =book.Description;
            PublishedYear.Text = book.PublishedYear;

            if (book.Pages == 0)
            {
                PagesLabel.Text = "Informação não disponível";
            }
            else
            {
                PagesLabel.Text = book.Pages.ToString();
            }
            

            if (!string.IsNullOrEmpty(book.Thumbnail))
            {
                BookThumbnail.Source = ImageSource.FromUri(new Uri(book.Thumbnail));
            }
            else
            {
                BookThumbnail.Source = ImageSource.FromFile("noimg.jpg"); 
            }

        }

        private void OnPreviousBookClicked(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                DisplayBookDetails(books[currentIndex]);
            }
        }

        private void OnNextBookClicked(object sender, EventArgs e)
        {
            if (currentIndex < books.Count - 1)
            {
                currentIndex++;
                DisplayBookDetails(books[currentIndex]);
            }
        }
    }
}



