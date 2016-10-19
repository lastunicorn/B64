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

namespace DustInTheWind.B64.Presentation.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private string title;

        private volatile bool updateFromBusiness;

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        public EncodeViewModel EncodeViewModel { get; set; }
        public DecodeViewModel DecodeViewModel { get; set; }

        public MainViewModel()
        {
            Base64Encoder base64Encoder = new Base64Encoder();
            ApplicationState applicationState = new ApplicationState(base64Encoder);

            EncodeViewModel = new EncodeViewModel(applicationState);
            DecodeViewModel = new DecodeViewModel(applicationState);

            Title = GetWindowTitle();
        }

        private static string GetWindowTitle()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            Version version = assembly.GetName().Version;
            return string.Format("Base64 Encoder {0}", version.ToString(2));
        }
    }
}