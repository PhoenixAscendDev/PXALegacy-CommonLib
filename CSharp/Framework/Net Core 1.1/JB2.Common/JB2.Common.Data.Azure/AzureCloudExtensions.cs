using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

namespace JB2.Common.Extensions
{
    public static class CloudExtensions
    // code source - http://www.codeproject.com/Tips/790554/StartsWith-comparison-for-searching-in-Azure-Table
    {
        public static IEnumerable<TElement> StartsWith<TElement>
        (this CloudTable table, string partitionKey, string searchStr, 
        string columnName = "RowKey",int recordLimit = 1000, bool decrypt = false) where TElement : ITableEntity, new()
        {
            if (string.IsNullOrEmpty(searchStr)) return null;



            char lastChar = searchStr[searchStr.Length - 1];
            char nextLastChar = (char)((int)lastChar + 1);
            string nextSearchStr = searchStr.Substring(0, searchStr.Length - 1) + nextLastChar;
            string prefixCondition = TableQuery.CombineFilters(
                TableQuery.GenerateFilterCondition(columnName, QueryComparisons.GreaterThanOrEqual, searchStr),
                TableOperators.And,
                TableQuery.GenerateFilterCondition(columnName, QueryComparisons.LessThan, nextSearchStr)
                );

            string filterString = TableQuery.CombineFilters(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey),
                TableOperators.And,
                prefixCondition
                );
            var query = new TableQuery<TElement>().Where(filterString);
            return table.ExecuteQuery<TElement>(query).Take(recordLimit);
        }
    }
}
