using Cookbook.Entity.Recipe;
using System;
using Tools.UI.ViewData;

namespace Cookbook.UI.ViewData.Recipe
{
    public class SeasonVD : ViewDataBase
    {
        public Guid Id { get; set; }

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

        private string _code;
        public string Code
        {
            get { return _code; }
            set
            {
                if (_code != value)
                {
                    _code = value;
                    OnPropertyChanged("Code");
                }
            }
        }

        public SeasonVD() : base() { }

        public SeasonVD(Season season) : this()
        {
            SetFromEntity(season);
        }

        public void SetFromEntity(Season season)
        {
            Id = season.Id;
            Name = season.Name;
            Code = season.Code;
        }

        public Season GetEntity()
        {
            var season = new Season();

            season.Id = Id;
            season.Name = Name;
            season.Code = Code;

            return season;
        }
    }
}
