using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace Books.Abstraction
{
    public abstract class Models : OrderedDictionary
    {
        public void Add(IModel model)
        {
            Add(model.Name, model);
        }

        public IEnumerable<IModel> GetAll()
        {
            foreach (IModel model in Values)
                yield return model;   
        }
    }
}
