using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Collections.ObjectModel;

namespace SokudokuMaker
{
    // Window1
    public partial class TitleWindow : Page
    {
        //List<Book> bookList = new List<Book>();
        ObservableCollection<Book> bookList = new ObservableCollection<Book>();

        public TitleWindow() {
            // ナビゲーションを削除してメモリ消費を抑える
            //var nav = this.NavigationService;
            //while (nav.CanGoBack) nav.RemoveBackEntry();

            InitializeComponent();
            bookListBox.ItemsSource = bookList;
            String bookFolderPath = Properties.Settings.Default.bookFolderPath;
            ReadFiles(bookFolderPath);
            bookListBox.SelectedIndex = 0; // 初期は一番上を選択状態にしておく
        }

        private void ReadFiles(String bookFolderPath)
        {
            string[] files = System.IO.Directory.GetFiles(bookFolderPath, "*.txt", System.IO.SearchOption.AllDirectories);
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
        private void ClickFileButton(object sender, RoutedEventArgs e)
        {
            var dlg = new CommonOpenFileDialog();
            dlg.IsFolderPicker = true;
            dlg.DefaultDirectory = Application.StartupPath;
            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                bookList = new ObservableCollection<Book>();
                ReadFiles(dlg.FileName);
                Properties.Settings.Default.bookFolderPath = dlg.FileName;
                var titleWindow = new TitleWindow();
                NavigationService.Navigate(titleWindow);
            }
        }
    }
}
