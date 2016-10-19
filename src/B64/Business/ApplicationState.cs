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
        private volatile bool isInternalUpdate;
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

                if (!isInternalUpdate)
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

                if (!isInternalUpdate)
                    UpdateDecodedText();
            }
        }

        public Exception EncodingError { get; private set; }
        public Exception DecodingError { get; private set; }

        public ApplicationState(Base64Encoder encoder)
        {
            if (encoder == null) throw new ArgumentNullException("encoder");
            this.encoder = encoder;
        }

        private void UpdateEncodedText()
        {
            isInternalUpdate = true;

            try
            {
                EncodingError = null;
                EncodedText = encoder.Encode(decodedText);
            }
            catch (Exception ex)
            {
                EncodingError = ex;
                EncodedText = null;
            }
            finally
            {
                isInternalUpdate = false;
            }
        }

        private void UpdateDecodedText()
        {
            isInternalUpdate = true;

            try
            {
                DecodingError = null;
                DecodedText = encoder.Decode(encodedText);
            }
            catch (Exception ex)
            {
                DecodingError = ex;
                DecodedText = null;
            }
            finally
            {
                isInternalUpdate = false;
            }
        }

        public void SaveEncodedText(ISaver saver)
        {
            if (saver == null) throw new ArgumentNullException("saver");

            saver.Save(encodedText);
        }

        public void LoadEncodedText(ILoader loader)
        {
            if (loader == null) throw new ArgumentNullException("loader");

            string text = loader.Load();

            if (text != null)
                EncodedText = text;
        }

        public void SaveDecodedText(ISaver saver)
        {
            if (saver == null) throw new ArgumentNullException("saver");

            saver.Save(decodedText);
        }

        public void LoadDecodedText(ILoader loader)
        {
            if (loader == null) throw new ArgumentNullException("loader");

            string text = loader.Load();

            if (text != null)
                DecodedText = text;
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
