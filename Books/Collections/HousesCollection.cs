using Books.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Collections
{
    public class HousesCollection : ObservableDictionary<string, House>
    {
        public HousesCollection() : base()
        {
        }
        public void Add(House house)
        {
            Add(house.Name, house);
        }
        public List<House> ToList()
        {
            return Values.Cast<House>().ToList();
        }
    }
}
