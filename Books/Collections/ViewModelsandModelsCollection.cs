using Books.Abstraction;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Books.Collections
{
    public class ViewModelsAndModelsCollection<TViewModel, TModel> : ObservableCollection<TViewModel> 
        where TViewModel : class, IViewModel, new()
        where TModel : class, IModel
    {
        private readonly ObservableDictionary<string, TModel> models;
        private bool _synchDisabled;

        public ViewModelsAndModelsCollection(ObservableDictionary<string, TModel> models)
        {
            this.models = models;
            models.CollectionChanged += ModelCollectionChanged;

            FetchFromModels();
        }

        public void FetchFromModels()
        {
            // Deactivate change pushing
            _synchDisabled = true;

            // Clear collection
            Clear();

            // Create and add new VM for each model
            foreach (TModel model in models.Values)
                Add(new TViewModel { Model = model });

            // Reactivate change pushing
            _synchDisabled = false;
        }

        private void ModelCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_synchDisabled) return;
            _synchDisabled = true;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (IModel model in e.NewItems.OfType<TModel>())
                        Add(new TViewModel { Model = model });
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (TModel model in e.OldItems.OfType<TModel>())
                        Remove(Items.FirstOrDefault(x => x.Name == model.Name));
                    break;
                
                case NotifyCollectionChangedAction.Reset:
                    Clear();
                    FetchFromModels();
                    break;
            }

            _synchDisabled = false;
        }
    }
}
