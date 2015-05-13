using System.Windows;
using System.Windows.Controls;

namespace DustInTheWind.B64
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();

            viewModel = new MainViewModel();
            DataContext = viewModel;
        }

        private void TextBoxDecoded_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.DecodedText = TextBoxDecoded.Text;
        }

        private void TextBoxEncoded_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.EncodedText = TextBoxEncoded.Text;
        }

        private void ButtonSaveDecoded_OnClick(object sender, RoutedEventArgs e)
        {
            SaveLoadFile.SaveTextFile("Decoded", TextBoxDecoded.Text);
        }

        private void ButtonSaveEncoded_OnClick(object sender, RoutedEventArgs e)
        {
            SaveLoadFile.SaveTextFile("Encoded", TextBoxEncoded.Text);
        }

        private void ButtonLoadDecoded_OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.DecodedText = SaveLoadFile.LoadTextFile();
        }

        private void ButtonLoadEncoded_OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.EncodedText = SaveLoadFile.LoadTextFile();
        }

        private void TextBoxDecoded_OnPreviewDragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }

        private void TextBoxDecoded_OnPreviewDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }

        private void TextBoxDecoded_OnDrop(object sender, DragEventArgs e)
        {
            string text = SaveLoadFile.GetTextFromFirstFile(e.Data);

            if (text != null)
                viewModel.DecodedText = text;

            e.Handled = true;
        }

        private void TextBoxEncoded_OnPreviewDragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }

        private void TextBoxEncoded_OnPreviewDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }

        private void TextBoxEncoded_OnDrop(object sender, DragEventArgs e)
        {
            string text = SaveLoadFile.GetTextFromFirstFile(e.Data);

            if (text != null)
                viewModel.EncodedText = text;

            e.Handled = true;
        }
    }
}
