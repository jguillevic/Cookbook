using Cookbook.Entity.Recipe;
using System;
using System.Collections.ObjectModel;
using Tools.UI.Common;
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

        public ObservableRangeCollection<Guid> CostIds { get; private set; }
        public ObservableRangeCollection<Guid> DifficultyIds { get; private set; }
        public ObservableRangeCollection<Guid> FeatureIds { get; private set; }
        public ObservableRangeCollection<Guid> RecipeKindIds { get; private set; }
        public ObservableRangeCollection<Guid> SeasonIds { get; private set; }
        
        public RecipeFilterVD(RecipeFilter filter)
        {
            Name = filter.Name;
            PreparationTime = filter.PreparationTime;
            CookingTime = filter.CookingTime;
            CostIds = new ObservableRangeCollection<Guid>(filter.CostIds);
            DifficultyIds = new ObservableRangeCollection<Guid>(filter.DifficultyIds);
            FeatureIds = new ObservableRangeCollection<Guid>(filter.FeatureIds);
            RecipeKindIds = new ObservableRangeCollection<Guid>(filter.RecipeKindIds);
            SeasonIds = new ObservableRangeCollection<Guid>(filter.SeasonIds);
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
