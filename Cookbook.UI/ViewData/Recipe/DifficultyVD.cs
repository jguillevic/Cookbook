using Cookbook.Entity.Recipe;
using System;
using Tools.UI.ViewData;

namespace Cookbook.UI.ViewData.Recipe
{
    public class DifficultyVD : ViewDataBase
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

        public DifficultyVD() : base() { }

        public DifficultyVD(Difficulty difficulty) : this()
        {
            SetFromEntity(difficulty);
        }

        public void SetFromEntity(Difficulty difficulty)
        {
            Id = difficulty.Id;
            Name = difficulty.Name;
            Code = difficulty.Code;
        }

        public Difficulty GetEntity()
        {
            var difficulty = new Difficulty();

            difficulty.Id = Id;
            difficulty.Name = Name;
            difficulty.Code = Code;

            return difficulty;
        }
    }
}
