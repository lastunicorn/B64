using System;
using System.ComponentModel;
using System.Reflection;

namespace DustInTheWind.B64
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly Base64Encoder base64Encoder;
        private bool updateMode;
        private string decodedText;
        private string encodedText;
        private string title;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

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
                decodedText = value;

                if (!updateMode)
                {
                    Update(() =>
                    {
                        EncodedText = base64Encoder.Encode(value);
                    });
                }

                OnPropertyChanged("DecodedText");
            }
        }

        public string EncodedText
        {
            get { return encodedText; }
            set
            {
                encodedText = value;

                if (!updateMode)
                {
                    Update(() =>
                    {
                        try
                        {
                            DecodedText = base64Encoder.Decode(value);
                        }
                        catch (Exception ex)
                        {
                            DecodedText = "error: " + ex.Message;
                        }
                    });
                }

                OnPropertyChanged("EncodedText");
            }
        }

        public MainViewModel()
        {
            base64Encoder = new Base64Encoder();

            Title = GetWindowTitle();
        }

        private static string GetWindowTitle()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            Version version = assembly.GetName().Version;
            return string.Format("Base64 Encoder {0}", version.ToString(2));
        }

        public void Update(Action action)
        {
            updateMode = true;
            try
            {
                action();
            }
            finally
            {
                updateMode = false;
            }
        }
    }
}