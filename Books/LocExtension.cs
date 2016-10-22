using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Windows.Data;

namespace Books
{
    public class TranslationSource : INotifyPropertyChanged
    {
        private static readonly TranslationSource instance = new TranslationSource();
        public static TranslationSource Instance
        {
            get { return instance; }
        }

        private readonly ResourceManager resManager = Properties.Resources.ResourceManager;
        private CultureInfo currentCulture = new CultureInfo("en-US");

        public string this[string key]
        {
            get { return resManager.GetString(key, currentCulture); }
        }

        public CultureInfo CurrentCulture
        {
            get { return currentCulture; }
            set
            {
                if (currentCulture != value)
                {
                    currentCulture = value;
                    if(PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(string.Empty));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class LocExtension : Binding
    {
        public LocExtension(string name)
            : base("[" + name + "]")
        {
            Mode = BindingMode.OneWay;
            Source = TranslationSource.Instance;
        }
    }
}