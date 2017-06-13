using Cookbook.UWP.RecipeServiceReference;
using System.Collections.Generic;

namespace Cookbook.UWP.Recipe
{
    public class VisualDifficulty
    {
        public Difficulty Difficulty { get; set; }

        public string Name { get; set; }

        public static readonly VisualDifficulty None = new VisualDifficulty { Difficulty = Difficulty.None, Name = string.Empty };
        public static readonly VisualDifficulty VeryEasy = new VisualDifficulty { Difficulty = Difficulty.VeryEasy, Name = "Très facile" };
        public static readonly VisualDifficulty Easy = new VisualDifficulty { Difficulty = Difficulty.Easy, Name = "Facile" };
        public static readonly VisualDifficulty Medium = new VisualDifficulty { Difficulty = Difficulty.Medium, Name = "Moyenne" };
        public static readonly VisualDifficulty Difficult = new VisualDifficulty { Difficulty = Difficulty.Difficult, Name = "Difficile" };
        public static readonly VisualDifficulty VeryDifficult = new VisualDifficulty { Difficulty = Difficulty.VeryDifficult, Name = "Très difficile" };
    }

    public class DifficultyDataProvider
    {
        public static List<VisualDifficulty> Difficulties { get; set; }

        static DifficultyDataProvider()
        {
            Difficulties = new List<VisualDifficulty>();
            Difficulties.Add(VisualDifficulty.None);
            Difficulties.Add(VisualDifficulty.VeryEasy);
            Difficulties.Add(VisualDifficulty.Easy);
            Difficulties.Add(VisualDifficulty.Medium);
            Difficulties.Add(VisualDifficulty.Difficult);
            Difficulties.Add(VisualDifficulty.VeryDifficult);
        }

        public static VisualDifficulty GetVisualDifficulty(Difficulty difficulty)
        {
            var visualDifficulty = VisualDifficulty.None;

            switch (difficulty)
            {
                case Difficulty.None:
                    visualDifficulty = VisualDifficulty.None;
                    break;
                case Difficulty.VeryEasy:
                    visualDifficulty = VisualDifficulty.VeryEasy;
                    break;
                case Difficulty.Easy:
                    visualDifficulty = VisualDifficulty.Easy;
                    break;
                case Difficulty.Medium:
                    visualDifficulty = VisualDifficulty.Medium;
                    break;
                case Difficulty.Difficult:
                    visualDifficulty = VisualDifficulty.Difficult;
                    break;
                case Difficulty.VeryDifficult:
                    visualDifficulty = VisualDifficulty.VeryDifficult;
                    break;
                default:
                    break;
            }

            return visualDifficulty;
        }
    }
}
