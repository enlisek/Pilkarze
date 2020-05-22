using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_1C.Model
{
    class ListaPilkarzy
    {
        private List<Pilkarz> pilkarze= new List<Pilkarz>();

        string plikArchiwizacji = "archiwum.txt";

        public void AddPilkarz(Pilkarz pilkarz)
        {
            if (!pilkarze.Contains(pilkarz))
            {
                pilkarze.Add(pilkarz);
            }
        }
        public void RemovPilkarz(Pilkarz pilkarz)
        {
            if (pilkarze.Contains(pilkarz))
            {
                pilkarze.Remove(pilkarz);
            }
        }
        public List<Pilkarz> Pilkarze 
        {
            get 
            { 
                return pilkarze; 
            } 

        }

        public List<string> PilkarzeToString
        {
            get
            {
                if (pilkarze.Count == 0) throw new Exception("Nie zdefiniowano, żednych operacji");
                List<string> res = new List<string>();
                for (int i = 0; i < pilkarze.Count(); i++)
                    res.Add(pilkarze[i].ToString());
                return res;
            }
        }
        public void WczytajPilkarzy()
        {   
            var pilkarzezpliku = Archiwizacja.CzytajPilkarzyZPliku(plikArchiwizacji);
            if (pilkarzezpliku != null)
                foreach (var p in pilkarzezpliku)
                {
                    pilkarze.Add(p);
                }
        }

        public bool CzyIstniejePilkarz(Pilkarz pilkarz)
        {

            foreach (var p in Pilkarze)
            {
                var p1 = p as Pilkarz;
                if (p1.isTheSame(pilkarz))
                {
                    return true;

                }
            }
            return false;
        }

        public void ZapiszPilkarzy()
        {
            int n = Pilkarze.Count;

            if (n > 0)
            {
                Pilkarz[] pilkarzetab = new Pilkarz[n];
                int index = 0;
                foreach (var o in Pilkarze)
                {
                    pilkarzetab[index++] = o as Pilkarz;
                }
                Archiwizacja.ZapisPilkarzyDoPliku(plikArchiwizacji, pilkarzetab);
            }
            else
            {
                Pilkarz[] pilkarzetab = null;
                Archiwizacja.ZapisPilkarzyDoPliku(plikArchiwizacji, pilkarzetab);
            }


        }
    }
}
