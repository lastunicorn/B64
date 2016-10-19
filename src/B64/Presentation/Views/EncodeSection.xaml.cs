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
using DustInTheWind.B64.Presentation.ViewModels;

namespace DustInTheWind.B64.Presentation.Views
{
    /// <summary>
    /// Interaction logic for EncodeSection.xaml
    /// </summary>
    public partial class EncodeSection : UserControl
    {
        public EncodeSection()
        {
            InitializeComponent();
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
            EncodeViewModel viewModel = DataContext as EncodeViewModel;

            if (viewModel != null)
                viewModel.LoadEncodedText(e.Data);

            e.Handled = true;
        }
    }
}
