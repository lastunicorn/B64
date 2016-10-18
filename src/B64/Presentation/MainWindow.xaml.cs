// B64
// Copyright (C) 2016 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Windows;
using DustInTheWind.B64.Business;

namespace DustInTheWind.B64.Presentation
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
