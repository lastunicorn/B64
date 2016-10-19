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
    internal class DecodeViewModel : ViewModelBase
    {
        private readonly ApplicationState applicationState;

        private string decodedText;
        private volatile bool updateFromBusiness;

        public LoadDecodedFileCommand LoadDecodedFileCommand { get; set; }
        public SaveDecodedFileCommand SaveDecodedFileCommand { get; set; }

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

        public DecodeViewModel(ApplicationState applicationState)
        {
            this.applicationState = applicationState;
            applicationState.DecodedTextChanged += HandleApplicationStateDecodedTextChanged;

            LoadDecodedFileCommand = new LoadDecodedFileCommand(applicationState);
            SaveDecodedFileCommand = new SaveDecodedFileCommand(applicationState);
        }

        private void HandleApplicationStateDecodedTextChanged(object sender, EventArgs eventArgs)
        {
            updateFromBusiness = true;
            try
            {
                DecodedText = applicationState.DecodingError != null
                    ? "error: " + applicationState.DecodingError.Message
                    : applicationState.DecodedText;
            }
            finally
            {
                updateFromBusiness = false;
            }
        }

        public void LoadDecodedText(IDataObject data)
        {
            TextFromDataLoader loader = new TextFromDataLoader(data);
            applicationState.LoadDecodedText(loader);
        }
    }
}