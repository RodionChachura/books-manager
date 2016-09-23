using Books.Collections;
using Books.Models;

namespace Books.ViewModels
{
    public class HousesViewModel : ViewModelsAndModelsCollection<HouseViewModel, House>
    {
        public HousesViewModel(HousesCollection models) : base(models)
        {
        }
    }

}
