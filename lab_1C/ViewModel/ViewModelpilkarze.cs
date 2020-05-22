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
    using System.Windows;
    using System.Runtime.InteropServices.ComTypes;

    internal class ViewModelpilkarze: ViewModelBase
    {
        private string imie;
        private string nazwisko;
        private uint wiek;
        private uint waga;
        private Pilkarz selectedPilkarz = null;
        private Pilkarz result;
        private ListaPilkarzy listapilkarzy=new ListaPilkarzy();
        private List<string> listapilkarzyStr;



        public ViewModelpilkarze()
        {
            listapilkarzy.WczytajPilkarzy();
            Clear();

        }

        public List<Pilkarz> Lista
        {
            get 
            {
                List<Pilkarz> lista = new List<Pilkarz>();
                foreach (var item in listapilkarzy.Pilkarze)
                {
                    lista.Add(item);
                }
                
                return lista; 
            }
        }


        public Pilkarz SelectedPilkarz
        {
            get { return selectedPilkarz; }
            set
            {
                selectedPilkarz = value;
                if (selectedPilkarz != null)
                {
                    Imie = selectedPilkarz.Imie;
                    Nazwisko = selectedPilkarz.Nazwisko;
                    Wiek = selectedPilkarz.Wiek;
                    Waga = selectedPilkarz.Waga;
                }
            }
        }

        public void Clear()
        {
            Imie = "";
            Nazwisko = "";
            Wiek = 30;
            Waga = 70;
            Result = new Pilkarz(Imie, Nazwisko, Wiek, Waga);
        }

       

        public string Imie 
        {
            get { return imie; }
            set 
            { 
                imie = value;
                onPropertyChanged(nameof(Imie));
            } 
        }
        public string Nazwisko
        {
            get { return nazwisko; }
            set
            {
                nazwisko = value;
                onPropertyChanged(nameof(Nazwisko));
            }
        }
        public uint Wiek
        {
            get { return wiek; }
            set
            {
                wiek = value;
                onPropertyChanged(nameof(Wiek));
            }
        }
        public uint Waga
        {
            get { return waga; }
            set
            {
                waga = value;
                onPropertyChanged(nameof(Waga));
            }
        }


        public Pilkarz Result
        {
            get
            {
                return result;
            }
            set
            {
                result = value;
                onPropertyChanged(nameof(Result));
            }
        }



        private ICommand _pilkarzAdd = null;

        public ICommand PilkarzAdd
        {
            get
            {
                if (_pilkarzAdd == null)
                {
                    _pilkarzAdd = new RelayCommand(
                        arg => {
                            if (!string.IsNullOrEmpty(Imie) && !string.IsNullOrEmpty(Nazwisko))
                            {
                                Result.Imie = Imie;
                                Result.Nazwisko = Nazwisko;
                                Result.Waga = Waga;
                                Result.Wiek = Wiek;
                                if(!listapilkarzy.CzyIstniejePilkarz(Result))
                                {
                                    listapilkarzy.AddPilkarz(Result);
                                    onPropertyChanged(nameof(Lista));
                                    Clear();
                                }
                                else
                                {
                                    var dialog = MessageBox.Show($"{Result.ToString()} już jest na liście {Environment.NewLine} Czy wyczyścić formularz?", "Uwaga", MessageBoxButton.OKCancel);
                                    if (dialog == MessageBoxResult.OK)
                                    {
                                        Clear();
                                    }
                                }
                            }
                            else
                            {
                                var dialog = MessageBox.Show($"Pola Imię i Nazwisko nie mogą być puste", "Uwaga", MessageBoxButton.OK);
                            }

                        },
                        arg => (true)
                        );
                }
                return _pilkarzAdd;
            }
        }

        private ICommand _pilkarzRemov = null;

        public ICommand PilkarzRemov
        {
            get
            {
                if (_pilkarzRemov == null)
                {
                    _pilkarzRemov = new RelayCommand(
                        arg => {
                            listapilkarzy.RemovPilkarz(SelectedPilkarz);
                            SelectedPilkarz = null;
                            onPropertyChanged(nameof(Lista));
                            Clear();
                        },
                        arg => (listapilkarzy.CzyIstniejePilkarz(new Pilkarz(Imie, Nazwisko, Wiek, Waga)) )
                        );
                }
                return _pilkarzRemov;
            }
        }

        private ICommand _pilkarzEdit = null;

        public ICommand PilkarzEdit
        {
            get
            {
                if (_pilkarzEdit == null)
                {
                    _pilkarzEdit = new RelayCommand(
                        arg => {
                            if (!listapilkarzy.CzyIstniejePilkarz(new Pilkarz(Imie, Nazwisko, Wiek, Waga)))
                            {
                                SelectedPilkarz.ChangePilkarz(new Pilkarz(Imie, Nazwisko, Wiek, Waga));
                                onPropertyChanged(nameof(Lista));
                            }
                            else
                            {
                                var dialog = MessageBox.Show("Dane nie zostały zmienione", "Edycja", MessageBoxButton.OK);
                            }
                            
                        },
                        arg => (!string.IsNullOrEmpty(Imie) 
                                && !string.IsNullOrEmpty(Nazwisko)
                                && SelectedPilkarz!=null)
                        );
                }
                return _pilkarzEdit;
            }
        }



        private ICommand _save = null;

        public ICommand Save
        {
            get
            {
                if (_save == null)
                {
                    _save = new RelayCommand(
                        arg => {
                            listapilkarzy.ZapiszPilkarzy();
                        },
                        arg => (true)
                        );
                }
                return _save;
            }
        }



    }
}
