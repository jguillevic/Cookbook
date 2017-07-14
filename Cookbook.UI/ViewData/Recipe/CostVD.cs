﻿using Cookbook.Entity.Recipe;
using System;
using Tools.UI.ViewData;

namespace Cookbook.UI.ViewData.Recipe
{
    public class CostVD : ViewDataBase
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

        public CostVD() : base() { }

        public CostVD(Cost cost) : this()
        {
            SetFromEntity(cost);
        }

        public void SetFromEntity(Cost cost)
        {
            Id = cost.Id;
            Name = cost.Name;
            Code = cost.Code;
        }

        public Cost GetEntity()
        {
            var cost = new Cost();

            cost.Id = Id;
            cost.Name = Name;
            cost.Code = Code;

            return cost;
        }
    }
}
