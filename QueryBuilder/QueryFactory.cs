using QueryBuilder.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueryBuilder
{
    public class QueryFactory
    {
        public Query Query { get; set; }

        public QueryFactory()
        {
            Query = new Query();
        }

        public QueryFactory(ReaderQuery query)
        {
            Query = new Query { TableName = query.TableName, SelectColumns = query.SelectColumns, TableJoins = query.TableJoins, FilterColumns = new List<BaseColumn>() };
            Query.FilterColumns = MakeFilterColumns(query.FilterColumns);
        }
        public List<BaseColumn> MakeFilterColumns(List<ReaderFilterColumns> filterColumns)
        {
            var filterColumnsFormated = new List<BaseColumn>();
            if (filterColumns != null)
            {
                foreach (var filter in filterColumns)
                {
                    if (Parameters.BasicOperators.Contains(filter.Operator.ToUpper()) && string.IsNullOrWhiteSpace(filter.JoinTableColumn))
                    {
                        filterColumnsFormated.Add(new BasicFilterColumn(filter));
                    }
                    else if (Parameters.BasicOperators.Contains(filter.Operator.ToUpper()) && !string.IsNullOrWhiteSpace(filter.JoinTableColumn))
                    {
                        filterColumnsFormated.Add(new BasicFilterWithTableJoinColumn(filter));
                    }
                    else if (filter.Operator == "BETWEEN")
                    {
                        filterColumnsFormated.Add(new BasicBetweenFilterColumn(filter));
                    }
                    else if (filter.Operator == "EXISTS")
                    {
                        filterColumnsFormated.Add(new SubQueryExistsFilterColumn(filter));
                    }
                    else if (Parameters.SubQueryOperators.Contains(filter.Operator.ToUpper()))
                    {
                        filterColumnsFormated.Add(new SubQueryFilterColumn(filter));
                    }
                }
            }
            return filterColumnsFormated;
        }
        public StringBuilder MakeQuery()
        {
            var sqlOutPut = new StringBuilder("SELECT");
            sqlOutPut.AppendLine();
            sqlOutPut.Append(PrintSelectColums());
            sqlOutPut.AppendLine();
            sqlOutPut.AppendLine($"FROM {Query.TableName}");
            sqlOutPut.Append(PrintTableJoins());
            sqlOutPut.Append(PrintFilterColumns());
            return sqlOutPut;
        }
        private StringBuilder PrintSelectColums()
        {
            var selectColumnsQuery = new StringBuilder();

            if (Query.SelectColumns != null)
            {
                foreach (var col in Query.SelectColumns)
                {
                    selectColumnsQuery.AppendLine($"{Query.TableName}.{col},");
                }

                selectColumnsQuery.Length -= 3;
            }

            return selectColumnsQuery;
        }

        private StringBuilder PrintFilterColumns()
        {
            var query = new StringBuilder();
            bool first = true;
            if (Query.FilterColumns != null)
            {
                foreach (var item in Query.FilterColumns)
                {
                    if (!first)
                    {
                        query.AppendLine($"{item.TypeFilter} {item.FilterCommand()}");
                    }
                    else
                    {
                        first = false;
                        query.AppendLine($"WHERE {item.FilterCommand()}");
                    }

                }
            }

            return query;
        }
        private StringBuilder PrintTableJoins()
        {
            var query = new StringBuilder();

            if (Query.TableJoins != null)
            {
                foreach (var join in Query.TableJoins)
                {
                    query.AppendLine($"{join.TypeJoin} JOIN {join.TableName} ON {Query.TableName}.{join.RerefenceTableFieldName}={join.TableName}.{join.FieldName}");
                }

            }

            return query;
        }
        public void PrintQuery()
        {
            Console.WriteLine(MakeQuery());
        }
    }
}
