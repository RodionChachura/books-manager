using Books.Abstraction;
using Books.Collections;
using Books.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Books.ViewModels;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.ViewModels
{
    public class AuthorsViewModel : ViewModelsAndModelsCollection<AuthorViewModel, Author>
    {
        public AuthorsViewModel(AuthorsCollection models) : base(models)
        {
        }
    }

}
