using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;

namespace SokudokuMaker
{
    public class Book
    {
        public string title { get; }
        public string author { get; }
        public string[] sentenceArray { get; private set; }
        public string sentence { get; private set; }
        private const int previewWord = 300;

        public Book(string file)
        {
            string[] _sentence = ReadFile(file);

            if (_sentence.Length > 2)
            {
                title = _sentence[0];
                author = _sentence[1];
                title = title.TrimEnd();
                author = author.TrimEnd();
            }
            // ファイル読み込み段階ではpreviewWord語以下に抑える
            for (int i = 2; i < Math.Min(_sentence.Length, previewWord); i++)
            {
                _sentence[i - 2] = _sentence[i];
            }
        }

        private string[] ReadFile(string file)
        {
            StreamReader sr = new StreamReader(file);
            sentence = sr.ReadToEnd();
            sr.Close();
            return sentence.Split('\n');
        }
        public void SplitAllSentence()
        {
            sentenceArray = sentence.Split('\n');
            // 選択された後は全単語分割する
            for (int i = 2; i < sentenceArray.Length; i++)
            {
                sentenceArray[i - 2] = sentenceArray[i];
            }
        }

    }
}
