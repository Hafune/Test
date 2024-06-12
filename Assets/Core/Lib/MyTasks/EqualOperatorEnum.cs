using System;

namespace Core.Tasks
{
    public enum EqualOperatorEnum
    {
        LessThanOrEqual,
        LessThan,
        GreaterThanOrEqual,
        GreaterThan,
        Equal,
        NotEqual
    }

    public static class EqualOperator
    {
        public static string GetName(EqualOperatorEnum e) => e switch
        {
            EqualOperatorEnum.LessThanOrEqual => "<=",
            EqualOperatorEnum.LessThan => "<",
            EqualOperatorEnum.GreaterThanOrEqual => ">=",
            EqualOperatorEnum.GreaterThan => ">",
            EqualOperatorEnum.Equal => "==",
            EqualOperatorEnum.NotEqual => "!=",
            _ => throw new ArgumentOutOfRangeException(nameof(e), e, null)
        };
    }
}