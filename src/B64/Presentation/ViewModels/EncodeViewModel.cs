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
using System.Windows;
using DustInTheWind.B64.Business;
using DustInTheWind.B64.Presentation.Commands;
using DustInTheWind.B64.Presentation.TextLoaders;

namespace DustInTheWind.B64.Presentation.ViewModels
{
    internal class EncodeViewModel : ViewModelBase
    {
        private readonly ApplicationState applicationState;

        private string encodedText;
        private volatile bool updateFromBusiness;

        public LoadEncodedFileCommand LoadEncodedFileCommand { get; set; }
        public SaveEncodedFileCommand SaveEncodedFileCommand { get; set; }

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

        public EncodeViewModel(ApplicationState applicationState)
        {
            this.applicationState = applicationState;
            applicationState.EncodedTextChanged += HandleEncodedTextChanged;

            LoadEncodedFileCommand = new LoadEncodedFileCommand(applicationState);
            SaveEncodedFileCommand = new SaveEncodedFileCommand(applicationState);
        }

        private void HandleEncodedTextChanged(object sender, EventArgs eventArgs)
        {
            updateFromBusiness = true;
            try
            {
                EncodedText = applicationState.EncodingError != null
                    ? "error: " + applicationState.EncodingError.Message
                    : applicationState.EncodedText;
            }
            finally
            {
                updateFromBusiness = false;
            }
        }

        public void LoadEncodedText(IDataObject data)
        {
            TextFromDataLoader loader = new TextFromDataLoader(data);
            applicationState.LoadEncodedText(loader);
        }
    }
}