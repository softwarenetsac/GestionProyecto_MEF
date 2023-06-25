using System;
using System.Linq;
using System.Linq.Expressions;

namespace Gestion_Rendimiento_Common
{
    public  static class Helper
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> lista, string columnName, bool isAscending = true)
        {
           
            if (String.IsNullOrEmpty(columnName))
            {
                return lista;
            }

            ParameterExpression parameter = Expression.Parameter(lista.ElementType, "");

            MemberExpression property = Expression.Property(parameter, columnName);
            LambdaExpression lambda = Expression.Lambda(property, parameter);

            string methodName = isAscending ? "OrderBy" : "OrderByDescending";

            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                  new Type[] { lista.ElementType, property.Type },
                                  lista.Expression, Expression.Quote(lambda));

            return lista.Provider.CreateQuery<T>(methodCallExpression);
        }
    }
}
