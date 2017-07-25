using Cookbook.Crawler.Recipe;
using Cookbook.Entity.Recipe;
using Cookbook.Rule.Recipe;
using Cookbook.ServiceClient.Recipe;
using Cookbook.UI.ViewData.Recipe;
using System.Collections.Generic;
using System.Globalization;
using Tools.UI.Command;
using Tools.UI.Common;
using Tools.UI.ViewModel;

namespace Cookbook.UI.ViewModel.Recipe
{
    public class RecipeCrawlerVM : PageViewModel
    {
        private RecipeCrawler _recipeCrawler;

        private int _recipeNumber;
        public int RecipeNumber
        {
            get { return _recipeNumber; }
            set
            {
                if (_recipeNumber != value)
                {
                    _recipeNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _currentRecipeIndex;
        public int CurrentRecipeIndex
        {
            get { return _currentRecipeIndex; }
            set
            {
                if (_currentRecipeIndex != value)
                {
                    _currentRecipeIndex = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _crawlingStatus;
        public string CrawlingStatus
        {
            get { return _crawlingStatus; }
            set
            {
                if (_crawlingStatus != value)
                {
                    _crawlingStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _notRecognizedText;
        public string NotRecognizedText
        {
            get { return _notRecognizedText; }
            set
            {
                if (_notRecognizedText != value)
                {
                    _notRecognizedText = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _alternativeName;
        public string AlternativeName
        {
            get { return _alternativeName; }
            set
            {
                if (_alternativeName != value)
                {
                    _alternativeName = value;
                    OnPropertyChanged();
                }
            }
        }

        private MeasureVD _newMeasure;
        public MeasureVD NewMeasure
        {
            get { return _newMeasure; }
            private set
            {
                if (_newMeasure != value)
                {
                    _newMeasure = value;
                    OnPropertyChanged();
                }
            }
        }

        private IngredientVD _newIngredient;
        public IngredientVD NewIngredient
        {
            get { return _newIngredient; }
            private set
            {
                if (_newIngredient != value)
                {
                    _newIngredient = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableRangeCollection<MeasureVD> Measures { get; set; }
        public ObservableRangeCollection<IngredientVD> Ingredients { get; set; }

        public DelegateCommand CrawlRecipeCommand { get; set; }
        public DelegateCommand GoToListRecipesCommand { get; set; }
        public DelegateCommand ResumeCrawlCommand { get; set; }

        public RecipeCrawlerVM() : base()
        {
            _recipeCrawler = new RecipeCrawler();
            _recipeCrawler.CrawlStartedAction = CrawlStarted;
            _recipeCrawler.CrawlFinishedAction = CrawlFinished;
            _recipeCrawler.CrawlRecipeStartedAction = CrawlRecipeStarted;
            _recipeCrawler.MeasureNotRecognizedAction = MeasureNotRecognized;
            _recipeCrawler.IngredientNotRecognizedAction = IngredientNotRecognized;

            CurrentRecipeIndex = 0;
            RecipeNumber = 10;
            NotRecognizedText = string.Empty;
            AlternativeName = string.Empty;

            Measures = new ObservableRangeCollection<MeasureVD>();
            Ingredients = new ObservableRangeCollection<IngredientVD>();

            CrawlRecipeCommand = new DelegateCommand(CrawlRecipeCommandExecute, CrawlRecipeCommandCanExecute);
            GoToListRecipesCommand = new DelegateCommand(GoToListRecipesCommandExecute, GoToListRecipesCommandCanExecute);
            ResumeCrawlCommand = new DelegateCommand(ResumeCrawlCommandExecute, ResumeCrawlCommandCanExecute);
        }

        private async void CrawlRecipeCommandExecute(object obj)
        {
            if (RecipeNumber > 0)
            {
                UpdateCrawlRecipeAndGoToListRecipesCommandCanExecute();

                Measures.Clear();
                var measures = await MeasureServiceClient.LoadAsync();
                var measuresVD = new List<MeasureVD>();
                measures.ForEach(item => measuresVD.Add(new MeasureVD(item)));
                Measures.AddRange(measuresVD);

                Ingredients.Clear();
                var ingredients = await IngredientServiceClient.LoadAsync();
                var ingredientsVD = new List<IngredientVD>();
                ingredients.ForEach(item => ingredientsVD.Add(new IngredientVD(item)));
                Ingredients.AddRange(ingredientsVD);

                var recipes = await _recipeCrawler.CrawlAsync(
                    RecipeNumber
                    , measures
                    , ingredients);

                await RecipeServiceClient.AddAsync(recipes);
            }
        }

        private bool CrawlRecipeCommandCanExecute(object obj)
        {
            return !_recipeCrawler.IsCrawling;
        }

        private void UpdateCrawlRecipeAndGoToListRecipesCommandCanExecute()
        {
            CrawlRecipeCommand.OnCanExecuteChanged();
            GoToListRecipesCommand.OnCanExecuteChanged();
        }

        private void UpdateResumeCrawlCommandCanExecute()
        {
            ResumeCrawlCommand.OnCanExecuteChanged();
        }

        private async void GoToListRecipesCommandExecute(object obj)
        {
            await Setter.SetCurrentViewModelAsync(new ListRecipesVM());
        }

        private bool GoToListRecipesCommandCanExecute(object obj)
        {
            return !_recipeCrawler.IsCrawling;
        }

        private void ResumeCrawlCommandExecute(object obj)
        {
            _recipeCrawler.ResumeCrawl();
            UpdateResumeCrawlCommandCanExecute();
        }

        private bool ResumeCrawlCommandCanExecute(object obj)
        {
            return _recipeCrawler.IsCrawling && _recipeCrawler.IsPaused;
        }

        private void CrawlStarted()
        {
            UpdateCrawlRecipeAndGoToListRecipesCommandCanExecute();
        }

        private void CrawlFinished()
        {
            CurrentRecipeIndex = 0;
            CrawlingStatus = "Crawl des recettes terminé";
            UpdateCrawlRecipeAndGoToListRecipesCommandCanExecute();
        }

        private void CrawlRecipeStarted(int index)
        {
            CurrentRecipeIndex = index;
            CrawlingStatus = string.Format("En cours de crawl. Recettes : {0}/{1}", CurrentRecipeIndex, RecipeNumber);
        }

        private void MeasureNotRecognized(string measureName, IList<Measure> measures)
        {
            UpdateResumeCrawlCommandCanExecute();

            NotRecognizedText = string.Format(CultureInfo.CurrentCulture, "Mesure non reconnu : {0}", measureName);
            
            AlternativeName = string.Empty;
            NewMeasure = new MeasureVD(MeasureRule.GetDefault());
        }

        private void IngredientNotRecognized(string ingredientName, IList<Ingredient> ingredients)
        {
            UpdateResumeCrawlCommandCanExecute();

            NotRecognizedText = string.Format(CultureInfo.CurrentCulture, "Ingrédient non reconnu : {0}", ingredientName);
            
            AlternativeName = string.Empty;
            NewIngredient = new IngredientVD(IngredientRule.GetDefault());
        }
    }
}
