// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq.Expressions;
using System.Text;

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

    public static string GetPropertyName(this LambdaExpression expression)
    {
        var member = expression.GetMemberExpression();
        var path = new StringBuilder();

        while (member != null)
        {
            if (path.Length > 0) path.Prepend('.');

            path.Prepend(member.Member.Name);

            member = member.Expression?.GetMemberExpression();
        }

        return path.ToString();
    }
}
