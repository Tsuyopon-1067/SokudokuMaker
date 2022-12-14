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
        // 指定パスのフォルダ内のテキストファイル名を取得したらBookインスタンス化して配列に入れる
        private void ReadFiles(String bookFolderPath)
        {
            string[] files = System.IO.Directory.GetFiles(bookFolderPath, "*.txt", System.IO.SearchOption.AllDirectories);
            foreach (string file in files)
            {
                bookList.Add(new Book(file));
            }
        }

        int idx = 0;

        // ItemBoxの要素が選択されたら選択された要素のインデックスを取得してタイトル・本文を表示する
        private void ListboxSelected(object sender, SelectionChangedEventArgs e)
        {
            idx = bookListBox.SelectedIndex;
            TitleTextBlock.Text = bookList[idx].title;
            SentenceTextBlock.Text = bookList[idx].previewSentence;
        }

        // ItemBoxの要素が選択されたら選択された要素のインデックスを取得してタイトル・本文を表示する
        private void ClickButton(object sender, RoutedEventArgs e)
        {
            Book selectedBook = bookList[idx];
            var readWindow = new ReadWindow(selectedBook);
            NavigationService.Navigate(readWindow);
        }
        // フォルダ選択ボタンが押されたら呼び出される
        private void ClickFolderButton(object sender, RoutedEventArgs e)
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
