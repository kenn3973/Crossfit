using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Windows.Storage;
using Windows.UI.Popups;

namespace Crossfit.ViewModel
{
    public class WodViewModel : INotifyPropertyChanged
    {
        public AddWodCommand AddWodCommand { get; set; }

        public RemoveWodCommand RemoveWodCommand { get; set; }

        public SaveWodCommand SaveWodCommand { get; set; }

        public LoadWodCommand LoadWodCommand { get; set; }

        public Model.WodList Wodliste { get; set; }


        StorageFolder localfolder = null;

        private readonly string filnavn = "JsonText.json";


        private Model.Wod _selectedWod;

        public event PropertyChangedEventHandler PropertyChanged;

        public Model.Wod SelectedWod
        {
            get { return _selectedWod; }
            set { _selectedWod = value;
                OnPropertyChanged(nameof(SelectedWod));
            }
        
        }

        protected virtual void OnPropertyChanged(string SelectedWod)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(SelectedWod));
            }
        }

        public  WodViewModel()
        {
            Wodliste = new Model.WodList();
            SelectedWod = new Model.Wod();
            AddWodCommand = new AddWodCommand(AddNewWod);
            RemoveWodCommand = new RemoveWodCommand(RemoveSelectedWod);
            NewWod = new Model.Wod();
            //AddWodCommand = new RelayCommand(AddNewWod, null);
            SaveWodCommand = new SaveWodCommand(GemDataTilDiskAsync);
            LoadWodCommand = new LoadWodCommand(HentdataFraDiskAsync);

            localfolder = ApplicationData.Current.LocalFolder;
        }

        public async void HentdataFraDiskAsync()
        {
            try
            {
                StorageFile file = await localfolder.GetFileAsync(filnavn);
                string jsonText = await FileIO.ReadTextAsync(file);
                this.Wodliste.Clear();
                Wodliste.IndsætJson(jsonText);
            }
            catch
            {
                MessageDialog messageDialog = new MessageDialog("Ændret filnavn eller du har ikke gemt ?", "Filnavn");
                await messageDialog.ShowAsync();
            }
        }

        /// <summary>
        /// Gemmer json data fra liste i localfolder
        /// </summary>
        public async void GemDataTilDiskAsync()
        {
            string jsonText = this.Wodliste.GetJson();
            StorageFile file = await localfolder.CreateFileAsync(filnavn, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, jsonText);
        }

        public Model.Wod NewWod { get; set; }

        //public RelayCommand AddWodCommand { get; set; }

        public void AddNewWod()
        {
            Wodliste.Add(NewWod);
        }

        public void RemoveSelectedWod()
        {
            Wodliste.Remove(SelectedWod);
        }
    }
}
