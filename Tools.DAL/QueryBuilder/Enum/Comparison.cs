namespace Tools.DAL.QueryBuilder.Enum
{
    /// <summary>
    /// Représente les opérateurs de comparaison pour les clauses WHERE, HAVING et JOIN.
    /// </summary>
    public enum Comparison
    {
        Equals,
        NotEquals,
        Like,
        NotLike,
        GreaterThan,
        GreaterOrEquals,
        LessThan,
        LessOrEquals,
        In,
        NotIn
    }
}