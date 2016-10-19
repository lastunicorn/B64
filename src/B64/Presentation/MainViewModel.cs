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
using System.Reflection;
using DustInTheWind.B64.Business;
using DustInTheWind.B64.Presentation.Commands;

namespace DustInTheWind.B64.Presentation
{
    internal class MainViewModel : ViewModelBase
    {
        private string decodedText;
        private string encodedText;
        private string title;

        private volatile bool updateFromBusiness;

        private readonly ApplicationState applicationState;

        public LoadEncodedFileCommand LoadEncodedFileCommand { get; set; }
        public SaveEncodedFileCommand SaveEncodedFileCommand { get; set; }
        public LoadDecodedFileCommand LoadDecodedFileCommand { get; set; }
        public SaveDecodedFileCommand SaveDecodedFileCommand { get; set; }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        public string DecodedText
        {
            get { return decodedText; }
            set
            {
                if (updateFromBusiness)
                {
                    decodedText = value;
                    OnPropertyChanged("DecodedText");
                }
                else
                {
                    applicationState.DecodedText = value;
                }
            }
        }

        public string EncodedText
        {
            get { return encodedText; }
            set
            {
                if (updateFromBusiness)
                {
                    encodedText = value;
                    OnPropertyChanged("EncodedText");
                }
                else
                {
                    applicationState.EncodedText = value;
                }
            }
        }

        public MainViewModel()
        {
            Base64Encoder base64Encoder = new Base64Encoder();

            applicationState = new ApplicationState(base64Encoder);
            applicationState.EncodedTextChanged += HandleEncodedTextChanged;
            applicationState.DecodedTextChanged += HandleApplicationStateDecodedTextChanged;

            LoadEncodedFileCommand = new LoadEncodedFileCommand(applicationState);
            SaveEncodedFileCommand = new SaveEncodedFileCommand(applicationState);
            LoadDecodedFileCommand = new LoadDecodedFileCommand(applicationState);
            SaveDecodedFileCommand = new SaveDecodedFileCommand(applicationState);

            Title = GetWindowTitle();
        }

        private void HandleApplicationStateDecodedTextChanged(object sender, EventArgs eventArgs)
        {
            UpdateFromBusiness(() =>
            {
                DecodedText = applicationState.DecodingError != null
                    ? "error: " + applicationState.DecodingError.Message
                    : applicationState.DecodedText;
            });
        }

        private void HandleEncodedTextChanged(object sender, EventArgs eventArgs)
        {
            UpdateFromBusiness(() =>
            {
                EncodedText = applicationState.EncodingError != null
                    ? "error: " + applicationState.EncodingError.Message
                    : applicationState.EncodedText;
            });
        }

        private static string GetWindowTitle()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            Version version = assembly.GetName().Version;
            return string.Format("Base64 Encoder {0}", version.ToString(2));
        }

        private void UpdateFromBusiness(Action action)
        {
            updateFromBusiness = true;
            try
            {
                action();
            }
            finally
            {
                updateFromBusiness = false;
            }
        }
    }
}