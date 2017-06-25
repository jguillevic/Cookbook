using Cookbook.Entity.Recipe;
using System;
using Tools.UI.ViewData;

namespace Cookbook.UI.ViewData.Recipe
{
    public class MeasureVD : ViewDataBase
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

        public MeasureVD(Measure measure)
        {
            Id = measure.Id;
            Name = measure.Name;
            Code = measure.Code;
        }

        public Measure GetEntity()
        {
            var measure = new Measure();

            measure.Id = Id;
            measure.Name = Name;
            measure.Code = Code;

            return measure;
        }
    }
}
