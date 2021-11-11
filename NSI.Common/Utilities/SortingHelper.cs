using NSI.Common.Collation;
using NSI.Common.DataContracts.Enumerations;
using NSI.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace NSI.Common.Utilities
{
    public static class SortingHelper
    {
        /// <summary>
        /// Orders source by provided property and sort type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Source to apply ordering to</param>
        /// <param name="property">Property to order by</param>
        /// <param name="methodName">Order type</param>
        /// <returns><see cref="IOrderedQueryable"/></returns>
        public static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            List<string> properties = property.Split('.').ToList();
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;

            foreach (string prop in properties)
            {
                PropertyInfo propertyInfo = type.GetProperty(prop);

                if (propertyInfo == null)
                {
                    throw new ArgumentException(ExceptionMessages.PropertyDoesNotExist);
                }

                expr = Expression.Property(expr, propertyInfo);
                type = propertyInfo.PropertyType;
            }

            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);

            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });

            return (IOrderedQueryable<T>)result;
        }

        /// <summary>
        /// Orders source by provided SortCriteria and sort functor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">Source to apply ordering to</param>
        /// <param name="rule">Sort rule</param>
        /// <param name="sortFunctor">Sort functor</param>
        public static IOrderedQueryable<T> ApplySortRule<T>(IOrderedQueryable<T> data, SortCriteria rule, Func<string, Expression<Func<T, object>>> sortFunctor)
        {
            if (rule == null || string.IsNullOrWhiteSpace(rule.Column))
            {
                return data;
            }

            Expression<Func<T, object>> fnc = sortFunctor(rule.Column);

            if (fnc == null)
            {
                throw new ArgumentException("fnc");
            }

            switch (rule.Order)
            {
                case SortOrder.Descending:
                    return data.ThenByDescending(fnc);
                case SortOrder.Ascending:
                default:
                    return data.ThenBy(fnc);
            }
        }
    }
}
