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

using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace DustInTheWind.B64.Business
{
    public class SaveLoadFile
    {
        public static void SaveTextFile(string defaultFileName, string text)
        {
            string filename = AskToSaveTextFile(defaultFileName);

            if (filename == null)
                return;

            File.WriteAllText(filename, text);
        }

        private static string AskToSaveTextFile(string defaultFileName)
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

        public static string LoadTextFile()
        {
            string filename = AskToLoadTextFile();

            return filename == null ? null : File.ReadAllText(filename);
        }

        private static string AskToLoadTextFile()
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                DefaultExt = ".txt",
                Filter = "Text documents|*.txt|All files|*.*"
            };

            bool? result = dlg.ShowDialog();

            return result != true ? null : dlg.FileName;
        }

        public static string GetTextFromFirstFile(IDataObject data)
        {
            string fileName = ExtractFirstFileName(data);
            return fileName == null ? null : File.ReadAllText(fileName);
        }

        public static string ExtractFirstFileName(IDataObject data)
        {
            if (!data.GetDataPresent(DataFormats.FileDrop))
                return null;

            string[] files = (string[])data.GetData(DataFormats.FileDrop);

            return files[0];
        }
    }
}