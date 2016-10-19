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
using System.Windows.Input;
using DustInTheWind.B64.Business;
using DustInTheWind.B64.Presentation.TextLoaders;

namespace DustInTheWind.B64.Presentation.Commands
{
    internal class SaveEncodedFileCommand : ICommand
    {
        private readonly ApplicationState applicationState;

        public event EventHandler CanExecuteChanged;

        public SaveEncodedFileCommand(ApplicationState applicationState)
        {
            if (applicationState == null) throw new ArgumentNullException("applicationState");

            this.applicationState = applicationState;
            this.applicationState.EncodedTextChanged += HandleApplicationStateEncodedTextChanged;
        }

        private void HandleApplicationStateEncodedTextChanged(object sender, EventArgs eventArgs)
        {
            OnCanExecuteChanged();
        }

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(applicationState.EncodedText);
        }

        public void Execute(object parameter)
        {
            TextFromFileSaver saver = new TextFromFileSaver("Encoded");
            applicationState.SaveEncodedText(saver);
        }

        protected virtual void OnCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}