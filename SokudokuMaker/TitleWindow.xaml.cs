using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Diagnostics;

namespace SokudokuMaker
{
    // Window1
    public partial class TitleWindow : Page
    {
        List<Book> bookList = new List<Book>();
        public TitleWindow()
        {
            InitializeComponent();
            bookListBox.ItemsSource = bookList;
            ReadFiles();
            bookListBox.SelectedIndex = 0; // 初期は一番上を選択状態にしておく
        }

        private void ReadFiles()
        {
            string[] files = System.IO.Directory.GetFiles(@"books", "*.txt", System.IO.SearchOption.AllDirectories);
            foreach (string file in files)
            {
                bookList.Add(new Book(file));
            }
        }

        int idx = 0;
        private void ItemboxSelected(object sender, SelectionChangedEventArgs e)
        {
            idx = bookListBox.SelectedIndex;
            TitleTextBlock.Text = bookList[idx].title;
            SentenceTextBlock.Text = bookList[idx].previewSentence;
        }

        private void ClickButton(object sender, RoutedEventArgs e)
        {
            Book selectedBook = bookList[idx];
            var readWindow = new ReadWindow(selectedBook);
            NavigationService.Navigate(readWindow);
        }
    }
}
