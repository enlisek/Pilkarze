using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_1C.Model
{
    class ListaPilkarzy
    {
        List<Pilkarz> listaPilkarzy { get; } = new List<Pilkarz>();

        public void AddPilkarz(Pilkarz pilkarz)
        {
            if (!listaPilkarzy.Contains(pilkarz))
            {
                listaPilkarzy.Add(pilkarz);
            }
        }
        public void RemovPilkarz(Pilkarz pilkarz)
        {
            if (listaPilkarzy.Contains(pilkarz))
            {
                listaPilkarzy.Remove(pilkarz);
            }
        }

        public string[] PilkarzeToString
        {
            get
            {
                if (listaPilkarzy.Count == 0) throw new Exception("Nie zdefiniowano, żednych operacji");
                string[] res = new string[listaPilkarzy.Count];
                for (int i = 0; i < res.Length; i++)
                    res[i] = listaPilkarzy[i].ToString();
                return res;
            }
        }
    }
}
