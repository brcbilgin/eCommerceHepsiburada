using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Application.Interfaces
{
    public static class IEnumerableExtentions
    {
        public static IEnumerable<T> AddBaseValidationMessages<T>(this IEnumerable<T> baseResults, List<T> results)
                where T : ValidationResult
        {
            if (baseResults.Any())
            {
                results.AddRange(baseResults);
            }

            return results;
        }
    }
}
