using System;
using System.Windows;
using System.Windows.Controls;
using System.Timers;

namespace SokudokuMaker
{
    // Window2
    public partial class ReadWindow : Page
    {
        private Book book;
        private string[] sentenceArray;
        private TextBlock[] readText = new TextBlock[3];
        private int nowWord = 1;
        private int wpm = 300;
        private int timerSpeed = 100;
        private bool isStart = false; // タイマー稼働中はtrue
        private Timer timer;
        public ReadWindow(Book book)
        {
            InitializeComponent();

            this.book = book;
            book.SplitAllSentence();
            sentenceArray = book.sentenceArray;
            TitleText.Text = book.title;

            readText[0] = ReadText0;
            readText[1] = ReadText1;
            readText[2] = ReadText2;
            SetReadText();
            RenewWpm();

            NowWordSlider.Maximum = sentenceArray.Length-1;

            timer = new Timer();
            timer.Elapsed += Tick;
            StartStopTimer();
        }

        // sentenceArrayは1スタート
        private void SetReadText()
        {
            this.Dispatcher.Invoke(() => {
                if (sentenceArray != null)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        readText[i].Text = sentenceArray[nowWord + i - 1];
                    }
                }
                if (NowWordTextBox != null)
                {
                    NowWordTextBox.Text = nowWord.ToString();
                }
            });
        }
        private void RenewNowWord()
        {
            this.Dispatcher.Invoke(() => {
                if (NowWordTextBox != null) NowWordTextBox.Text = nowWord.ToString();
                if (NowWordSlider != null) NowWordSlider.Value = nowWord;
                if (sentenceArray != null)
                {
                    if (nowWord >= sentenceArray.Length - 1) nowWord = sentenceArray.Length - 2;
                    else if (nowWord < 1) nowWord = 1;
                }
            });
            
            SetReadText();
        }
        private void RenewWpm()
        {
            if (WpmTextBox != null) WpmTextBox.Text = wpm.ToString();
            if (WpmSlider != null) WpmSlider.Value = wpm;
            StartStopTimer();
        }
        private void StartStopTimer()
        {
            timerSpeed = (int)(60000 / wpm);

            if (timer != null)
            {
                timer.Interval = timerSpeed;
                if (isStart)
                {
                    timer.Start();
                }
                else
                {
                    timer.Stop();
                }
            }
        }

        private void ClickNowWordDown(object sender, RoutedEventArgs e)
        {
            --nowWord;
            RenewNowWord();
        }

        private void ClickWpmDown(object sender, RoutedEventArgs e)
        {
            --wpm;
            RenewWpm();
        }

        private void ClickNowWordUp(object sender, RoutedEventArgs e)
        {
            ++nowWord;
            RenewNowWord();
        }
        void Tick(object sender, ElapsedEventArgs e)
        {
            ++nowWord;
            RenewNowWord();
        }

        private void ClickWpmUp(object sender, RoutedEventArgs e)
        {
            ++wpm;
            RenewWpm();
        }

        private void NowWordSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            nowWord = (int)NowWordSlider.Value;
            RenewNowWord();
        }

        private void WpmSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            wpm = (int)WpmSlider.Value;
            RenewWpm();
        }

        private void ClickStartButton(object sender, RoutedEventArgs e)
        {
            isStart = !isStart;
            StartStopTimer();
        }

        private void WpmTextBoxChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(WpmTextBox.Text, out wpm)) ;
            RenewWpm();
        }

        private void NowWordTextBoxChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(NowWordTextBox.Text, out nowWord)) ;
            RenewNowWord();
        }
    }
}
