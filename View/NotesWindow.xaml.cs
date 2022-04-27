using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyEvernote.View
{
    /// <summary>
    /// Lógica interna para NoteWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        SpeechRecognitionEngine recognizer;

        public NotesWindow()
        {
            InitializeComponent();
            try
            {
                var currentCulture = (from r in SpeechRecognitionEngine.InstalledRecognizers()
                                      where r.Culture.Equals(Thread.CurrentThread.CurrentCulture)
                                      select r).FirstOrDefault();

                recognizer = new SpeechRecognitionEngine();

                GrammarBuilder builder = new GrammarBuilder();
                builder.AppendDictation();
                Grammar grammaer = new Grammar(builder);

                recognizer.LoadGrammar(grammaer);
                recognizer.SetInputToDefaultAudioDevice();

                recognizer.SpeechRecognized += Recognizer_SpeechRecognized;
            }
            catch (Exception)
            {
                // Do nothing, My PC is with PT-BR language and there is no recognizer to it, so I can't text this block for now.
            }
        }

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            var recognizedText = e.Result.Text;
            contentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(recognizedText)));
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void contentRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int amountOfCharacters = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd).Text.Length;

            statusTextBlock.Text = $"Document Length {amountOfCharacters} characters";
        }

        private void boldButton_Click(object sender, RoutedEventArgs e)
        {
            contentRichTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
        }

        private bool isRecognizing = false;
        private void SpeechButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isRecognizing)
                {
                    recognizer.RecognizeAsync(RecognizeMode.Multiple);
                    isRecognizing = true;
                }
                else
                {
                    recognizer.RecognizeAsyncStop();
                    isRecognizing = false;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
