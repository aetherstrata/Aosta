// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq.Expressions;

namespace Aosta.Common.Extensions;

public static class ExpressionExtensions
{
    public static MemberExpression? GetMemberExpression(this Expression expression)
    {
        return expression switch
        {
            MemberExpression memberExpression => memberExpression,
            LambdaExpression lambdaExpression => lambdaExpression.Body switch
            {
                MemberExpression body => body,
                UnaryExpression unaryExpression => ((MemberExpression)unaryExpression.Operand),
                _ => null,
            },
            _ => null
        };
    }
}
