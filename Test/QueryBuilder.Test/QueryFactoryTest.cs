using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QueryBuilder.Test
{
    public class QueryFactoryTest
    {
        private readonly Mock<QueryFactory> mockQueryFactory;
        private readonly BaseColumn baseColumnMock;
        private readonly ReaderQuery subQueryMock;

        public QueryFactoryTest()
        {
            baseColumnMock = new BaseColumn { FieldName = "Column", TypeFilter = "AND" };
            mockQueryFactory = new Mock<QueryFactory>();
            subQueryMock = new ReaderQuery { TableName = "tableSubQuery", FilterColumns = new List<ReaderFilterColumns> { new ReaderFilterColumns { FieldName = "collumn", Operator = ">" } } };
        }
        [Theory]
        [InlineData(">", "10")]
        [InlineData("=", "14")]
        public void BasicFilterColumnTest(string aOperator, string value)
        {
            var filters = new List<ReaderFilterColumns> { new ReaderFilterColumns { FieldName = baseColumnMock.FieldName, TypeFilter = baseColumnMock.TypeFilter, Operator = aOperator, FieldValue = value } };
            var queryFactory = mockQueryFactory.Object.MakeFilterColumns(filters);
            var generateCommand = queryFactory.FirstOrDefault().FilterCommand();
            Assert.Equal($"{baseColumnMock.FieldName} {aOperator} ({value})", generateCommand);
        }

        [Theory]
        [InlineData(">", "table", "ColumnId")]
        [InlineData("=", "table", "ColumnId")]
        public void BasicFilterWithTableJoinColumnTest(string aOperator, string joinTable, string joinTableColumn)
        {
            var filters = new List<ReaderFilterColumns> { new ReaderFilterColumns { FieldName = baseColumnMock.FieldName, TypeFilter = baseColumnMock.TypeFilter, Operator = aOperator, JoinTable = joinTable, JoinTableColumn = joinTableColumn } };
            var queryFactory = mockQueryFactory.Object.MakeFilterColumns(filters);
            var generateCommand = queryFactory.FirstOrDefault().FilterCommand();
            Assert.Equal($"{baseColumnMock.FieldName} {aOperator} ({joinTable}.{joinTableColumn})", generateCommand);
        }

        [Theory]
        [InlineData("BETWEEN", "10", "40")]
        public void BetweenFilterColumnTest(string aOperator, string fieldValueFirst, string fieldValueSecond)
        {
            var filters = new List<ReaderFilterColumns> { new ReaderFilterColumns { FieldName = baseColumnMock.FieldName, TypeFilter = baseColumnMock.TypeFilter, Operator = aOperator, FieldValueFirst = fieldValueFirst, FieldValueSecond = fieldValueSecond } };
            var queryFactory = mockQueryFactory.Object.MakeFilterColumns(filters);
            var generateCommand = queryFactory.FirstOrDefault().FilterCommand();
            Assert.Equal($"{baseColumnMock.FieldName} {aOperator} {fieldValueFirst} AND {fieldValueSecond}", generateCommand);
        }

        [Theory]
        [InlineData("> ANY")]
        [InlineData("= ALL")]
        [InlineData("<= SOME")]
        public void SubQueryFilterColumnTest(string aOperator)
        {
            var filters = new List<ReaderFilterColumns> { new ReaderFilterColumns { FieldName = baseColumnMock.FieldName, TypeFilter = baseColumnMock.TypeFilter, Operator = aOperator, SubQuery = subQueryMock } };
            var queryFactory = mockQueryFactory.Object.MakeFilterColumns(filters);
            var generateCommand = queryFactory.FirstOrDefault().FilterCommand();
            var subQueryFactory = new QueryFactory(subQueryMock);
            Assert.Equal($"{baseColumnMock.FieldName} {aOperator} ({subQueryFactory.MakeQuery()})", generateCommand);
        }
        [Theory]
        [InlineData("EXISTS")]
        public void SubQueryExistsFilterColumnTest(string aOperator)
        {           
            var filters = new List<ReaderFilterColumns> { new ReaderFilterColumns { FieldName = baseColumnMock.FieldName, TypeFilter = baseColumnMock.TypeFilter, Operator = aOperator, SubQuery = subQueryMock } };
            var queryFactory = mockQueryFactory.Object.MakeFilterColumns(filters);
            var generateCommand = queryFactory.FirstOrDefault().FilterCommand();
            var subQueryFactory = new QueryFactory(subQueryMock);
            Assert.Equal($"{aOperator} ({subQueryFactory.MakeQuery()})", generateCommand);
        }
    }
}
