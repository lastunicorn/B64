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
using System.Windows;

namespace DustInTheWind.B64.Presentation.TextLoaders
{
    public class TextFromDataLoader
    {
        private readonly IDataObject data;

        public TextFromDataLoader(IDataObject data)
        {
            if (data == null) throw new ArgumentNullException("data");
            this.data = data;
        }

        public string GetText()
        {
            string fileName = ExtractFirstFileName();
            return fileName == null ? null : File.ReadAllText(fileName);
        }

        private string ExtractFirstFileName()
        {
            if (!data.GetDataPresent(DataFormats.FileDrop))
                return null;

            string[] files = (string[])data.GetData(DataFormats.FileDrop);

            return files[0];
        }
    }
}