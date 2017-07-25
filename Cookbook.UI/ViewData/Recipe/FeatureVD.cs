using Cookbook.Entity.Recipe;
using System;
using Tools.UI.ViewData;

namespace Cookbook.UI.ViewData.Recipe
{
    public class FeatureVD : ViewDataBase
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

        public FeatureVD() : base() { }

        public FeatureVD(Feature feature) : this()
        {
            SetFromEntity(feature);
        }

        public void SetFromEntity(Feature feature)
        {
            Id = feature.Id;
            Name = feature.Name;
            Code = feature.Code;
        }

        public Feature GetEntity()
        {
            var feature = new Feature();

            feature.Id = Id;
            feature.Name = Name;
            feature.Code = Code;

            return feature;
        }
    }
}
