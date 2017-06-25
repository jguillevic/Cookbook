using Cookbook.Entity.Recipe;
using System;
using System.Collections.ObjectModel;
using Tools.UI.ViewData;

namespace Cookbook.UI.ViewData.Recipe
{
    public class RecipeFilterVD : ViewDataBase
    {
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

        private int? _preparationTime;
        public int? PreparationTime
        {
            get { return _preparationTime; }
            set
            {
                if (_preparationTime != value)
                {
                    _preparationTime = value;
                    OnPropertyChanged("PreparationTime");
                }
            }
        }

        private int? _cookingTime;
        public int? CookingTime
        {
            get { return _cookingTime; }
            set
            {
                if (_cookingTime != value)
                {
                    _cookingTime = value;
                    OnPropertyChanged("CookingTime");
                }
            }
        }

        public ObservableCollection<Guid> CostIds { get; private set; }
        public ObservableCollection<Guid> DifficultyIds { get; private set; }
        public ObservableCollection<Guid> FeatureIds { get; private set; }
        public ObservableCollection<Guid> RecipeKindIds { get; private set; }
        public ObservableCollection<Guid> SeasonIds { get; private set; }
        
        public RecipeFilterVD(RecipeFilter filter)
        {
            Name = filter.Name;
            PreparationTime = filter.PreparationTime;
            CookingTime = filter.CookingTime;
            CostIds = new ObservableCollection<Guid>(filter.CostIds);
            DifficultyIds = new ObservableCollection<Guid>(filter.DifficultyIds);
            FeatureIds = new ObservableCollection<Guid>(filter.FeatureIds);
            RecipeKindIds = new ObservableCollection<Guid>(filter.RecipeKindIds);
            SeasonIds = new ObservableCollection<Guid>(filter.SeasonIds);
        }

        public RecipeFilter GetEntity()
        {
            var filter = new RecipeFilter();

            filter.Name = Name;
            filter.PreparationTime = PreparationTime;
            filter.CookingTime = CookingTime;
            filter.CostIds.AddRange(CostIds);
            filter.DifficultyIds.AddRange(DifficultyIds);
            filter.FeatureIds.AddRange(FeatureIds);
            filter.RecipeKindIds.AddRange(RecipeKindIds);
            filter.SeasonIds.AddRange(SeasonIds);

            return filter;
        }
    }
}
