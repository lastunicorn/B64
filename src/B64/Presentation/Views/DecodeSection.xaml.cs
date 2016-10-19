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
using System.Windows.Controls;
using DustInTheWind.B64.Presentation.TextLoaders;
using DustInTheWind.B64.Presentation.ViewModels;

namespace DustInTheWind.B64.Presentation.Views
{
    /// <summary>
    /// Interaction logic for DecodeSection.xaml
    /// </summary>
    public partial class DecodeSection : UserControl
    {
        public DecodeSection()
        {
            InitializeComponent();
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
            TextFromDataLoader loader = new TextFromDataLoader(e.Data);
            string text = loader.GetText();

            if (text != null)
            {
                DecodeViewModel viewModel = DataContext as DecodeViewModel;

                if (viewModel != null)
                    viewModel.DecodedText = text;
            }

            e.Handled = true;
        }
    }
}
