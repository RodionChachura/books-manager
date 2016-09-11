using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using Books.Abstraction;

namespace Books.Collections
{
    public class ModelsCollection : Dictionary<string, IModel>
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
