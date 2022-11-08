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
        public string previewSentence { get; set; }
        private int previewWord = 200;

        public Book(string file)
        {
            sentenceArray = ReadFile(file);

            if (sentence.Length > 2)
            {
                title = sentenceArray[0];
                author = sentenceArray[1];

                StringBuilder sb = new StringBuilder();
                for (int i = 2; i < Math.Min(sentenceArray.Length, previewWord); i++)
                {
                    sb.Append(sentenceArray[i]);
                }
                previewSentence = sb.ToString();
                previewSentence = previewSentence.Replace("\n", "");
            }
        }

        private string[] ReadFile(string file)
        {
            StreamReader sr = new StreamReader(file);
            sentence = sr.ReadToEnd();
            sr.Close();
            sentence = sentence.Replace("\r\n", "\n"); // 全文の改行コードを変更しておく
            return sentence.Split('\n');
        }
        public void SplitAllSentence()
        {
            sentenceArray = sentence.Split('\n');
            // 選択された後は全単語分割する 0・1はタイトル・作者なので無視
            // [0]は空にしたいので[1]から順に代入
            sentenceArray[0] = "";
            for (int i = 2; i < sentenceArray.Length; i++)
            {
                sentenceArray[i - 1] = sentenceArray[i];
            }
            sentenceArray[sentenceArray.Length - 1] = "";
        }
        public String ToString()
        {
            return title;
        }
    }
}
