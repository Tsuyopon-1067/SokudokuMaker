using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        }

        private void ReadFiles()
        {
            string[] files = System.IO.Directory.GetFiles(@"books", "*.txt", System.IO.SearchOption.AllDirectories);
            foreach (string file in files)
            {
                bookList.Add(new Book(file));
            }
        }


        private void ItemboxSelected(object sender, SelectionChangedEventArgs e)
        {
            int idx = bookListBox.SelectedIndex;
            TitleTextBlock.Text = bookList[idx].title;
            SentenceTextBlock.Text = bookList[idx].sentence;
        }

        private void ClickButton(object sender, RoutedEventArgs e)
        {
            var page2 = new ReadWindow();
            NavigationService.Navigate(page2);
        }
    }
}
