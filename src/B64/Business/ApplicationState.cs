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

namespace DustInTheWind.B64.Business
{
    internal class ApplicationState
    {
        private readonly Base64Encoder encoder;
        private volatile bool updateMode;
        private string decodedText;
        private string encodedText;

        public event EventHandler DecodedTextChanged;
        public event EventHandler EncodedTextChanged;

        public string DecodedText
        {
            get { return decodedText; }
            set
            {
                decodedText = value;

                OnDecodedTextChanged();

                if (!updateMode)
                    UpdateEncodedText();
            }
        }

        public string EncodedText
        {
            get { return encodedText; }
            set
            {
                encodedText = value;

                OnEncodedTextChanged();

                if (!updateMode)
                    UpdateDecodedText();
            }
        }

        public ApplicationState(Base64Encoder encoder)
        {
            if (encoder == null) throw new ArgumentNullException("encoder");
            this.encoder = encoder;
        }

        private void UpdateEncodedText()
        {
            updateMode = true;

            try
            {
                EncodedText = encoder.Encode(decodedText);
            }
            finally
            {
                updateMode = false;
            }
        }

        private void UpdateDecodedText()
        {
            updateMode = true;
            try
            {
                DecodedText = encoder.Decode(encodedText);
            }
            finally
            {
                updateMode = false;
            }
        }

        protected virtual void OnDecodedTextChanged()
        {
            EventHandler handler = DecodedTextChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnEncodedTextChanged()
        {
            EventHandler handler = EncodedTextChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}
