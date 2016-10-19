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

using System;
using System.IO;
using DustInTheWind.B64.Business;
using Microsoft.Win32;

namespace DustInTheWind.B64.Presentation.TextLoaders
{
    internal class TextFromFileSaver : ISaver
    {
        private readonly string defaultFileName;

        public TextFromFileSaver(string defaultFileName)
        {
            if (defaultFileName == null) throw new ArgumentNullException("defaultFileName");

            this.defaultFileName = defaultFileName;
        }

        public void Save(string text)
        {
            if (text == null) throw new ArgumentNullException("text");

            string filename = AskToSaveTextFile();

            if (filename == null)
                return;

            File.WriteAllText(filename, text);
        }

        private string AskToSaveTextFile()
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                FileName = defaultFileName,
                DefaultExt = ".txt",
                Filter = "Text documents|*.txt"
            };

            bool? result = dlg.ShowDialog();

            return result != true ? null : dlg.FileName;
        }
    }
}