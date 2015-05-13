using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace DustInTheWind.B64
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