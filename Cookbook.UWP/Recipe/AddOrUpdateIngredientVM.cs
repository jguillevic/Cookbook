using Cookbook.UWP.Common;
using Cookbook.UWP.IngredientServiceReference;
using System;

namespace Cookbook.UWP.Recipe
{
    public class AddOrUpdateIngredientVM : ViewModelBase
    {
        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public AddOrUpdateIngredientVM(Ingredient ingredient)
        {
            Id = ingredient.Id;
            Name = ingredient.Name;
        }

        public Ingredient GetIngredient()
        {
            return new Ingredient { Id = Id, Name = Name };
        }
    }
}
