using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace lab_1C.ViewModel
{
    using Model;
    using BaseClass;


    internal class Calculating: ViewModelBase
    {

        public ListaPilkarzy listapilkarzy = new Model.ListaPilkarzy();

        public Calculating()
        {
            Pilkarz[] pilkarze = Archiwizacja.CzytajPilkarzyZPliku(plikArchiwizacji);
            for (int i = 0; i < pilkarze.Length; i++)
            {
                listapilkarzy.AddPilkarz(pilkarze[i]);
            }
        }

        private string plikArchiwizacji = "archiwum.txt";
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public uint Wiek { get; set; }
        public uint Waga { get; set; }

        public Pilkarz selectedPilkarz { get; set; }


        private Pilkarz result = null;
        public Pilkarz Result
        {
            get
            {
                return result;
            }
            set
            {
                result = value;
                //metoda wywołuącą zdarzenia PropertyChange
                // metoda odziedziczona po klasie ModelViewBase
                // "informuje" widok, że zmianie uległ wynik
                //dzięki temu zkatualizuje się widok
                onPropertyChanged(nameof(Result));
            }
        }



        private ICommand _calculateAdd = null;

        public ICommand CalculateAdd
        {
            get
            {
                if (_calculateAdd == null)
                {
                    _calculateAdd = new RelayCommand(
                        arg => { Result = new Pilkarz(Imie, Nazwisko, Wiek, Waga);
                            listapilkarzy.AddPilkarz(Result); },
                        arg => ((Imie != null) && (Nazwisko != null))
                        );
                }
                return _calculateAdd;
            }
        }

        private ICommand _calculateRemov = null;

        public ICommand CalculateRemov
        {
            get
            {
                if (_calculateAdd == null && selectedPilkarz!=null)
                {
                    _calculateAdd = new RelayCommand(
                        arg => {
                            listapilkarzy.RemovPilkarz(selectedPilkarz);
                        },
                        arg => ((Imie != null) && (Nazwisko != null))
                        );
                }
                return _calculateAdd;
            }
        }

    }
}
