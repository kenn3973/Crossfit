using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Crossfit.ViewModel
{
    public class WodViewModel : INotifyPropertyChanged
    {
        public AddWodCommand AddWodCommand { get; set; }

        public RemoveWodCommand RemoveWodCommand { get; set; }

        public Model.WodList Wodliste { get; set; }

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
