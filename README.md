# Query Builder with .net core
      You can generate a sql command from a SQL Json file.

### Basic query

Input
```
{
  "tableName": "Product",
  "selectcolumns": [ "Id", "Name", "Price" ],
  "Filtercolumns": [
    {
      "operator": ">",
      "fieldName": "Price",
      "typeFilter": "AND",
      "fieldValue": "14"
    },
    {
      "operator": "=",
      "fieldName": "Name",
      "typeFilter": "AND",
      "fieldValue": "Test"
    }
  ]
}
```
Output
```
SELECT
      Product.Id,
      Product.Name,
      Product.Price
FROM Product
WHERE Price > (14)
AND Name = (Test)
```

### Basic query with BETWEEN
Input
```
{
  "tableName": "Product",
  "selectcolumns": [ "Id", "Name", "Price" ],
  "Filtercolumns": [
    {
      "operator": "BETWEEN",
      "fieldName": "Price",
      "typeFilter": "AND",
      "fieldValueFirst": "10",
      "fieldValueSecond": "20"
    }
  ]
}
```
Output
```
SELECT
    Product.Id,
    Product.Name,
    Product.Price
FROM Product
WHERE Price BETWEEN 10 AND 20
```

### Basic query with join

Input
```
{
  "tableName": "Product",
  "selectcolumns": [ "Id", "Name", "Price" ],
  "Filtercolumns": [
    {
      "operator": ">",
      "fieldName": "Price",
      "typeFilter": "AND",
      "fieldValue": "14"
    },
    {
      "operator": "=",
      "fieldName": "Name",
      "typeFilter": "AND",
      "fieldValue": "Test"
    },
    {
      "operator": "<",
      "fieldName": "Price",
      "typeFilter": "AND",
      "JoinTable": "HistoryPrice",
      "JoinTableColumn": "LastPrice"
    }
  ],
  "TableJoins": [
    {
      "tableName": "HistoryPrice",
      "typeJoin": "INNER",
      "RerefenceTableFieldName": "Id",
      "FieldName": "ProductId"
    }
  ]
}
```

Output
```
SELECT
    Product.Id,
    Product.Name,
    Product.Price
FROM Product
INNER JOIN HistoryPrice ON Product.Id=HistoryPrice.ProductId
WHERE Price > (14)
AND Name = (Test)
AND Price < (HistoryPrice.LastPrice)
```

### Sub query

Input
```
{
  "tableName": "Product",
  "selectcolumns": [ "Id", "Name", "Price" ],
  "Filtercolumns": [
    {
      "operator": "BETWEEN",
      "fieldName": "Price",
      "typeFilter": "AND",
      "fieldValueFirst": "10",
      "fieldValueSecond": "20"
    },
    {
      "operator": "> ALL",
      "fieldName": "price",
      "typeFilter": "AND",
      "subquery": {
        "tableName": "HistoryPrice",
        "selectcolumns": [ "LastPrice" ],
        "Filtercolumns": [
          {
            "operator": "=",
            "fieldName": "LastPrice",
            "typeFilter": "AND",
            "fieldValue": "100"
          }
        ]
      }
    }
  ]
}
```

Output
```
SELECT
    Product.Id,
    Product.Name,
    Product.Price
FROM Product
WHERE Price BETWEEN 10 AND 20
AND price > ALL (SELECT
                    HistoryPrice.LastPrice
                    FROM HistoryPrice
                    WHERE LastPrice = (100))
```

### Complex query
Input
```
{
  "tableName": "Product",
  "selectcolumns": [ "Id", "Name", "Price" ],
  "Filtercolumns": [
    {
      "operator": ">",
      "fieldName": "Price",
      "typeFilter": "AND",
      "fieldValue": "14"
    },
    {
      "operator": "=",
      "fieldName": "Name",
      "typeFilter": "AND",
      "fieldValue": "Test"
    },
    {
      "operator": "<",
      "fieldName": "Price",
      "typeFilter": "AND",
      "JoinTable": "HistoryPrice",
      "JoinTableColumn": "LastPrice"
    },
    {
      "operator": "BETWEEN",
      "fieldName": "Price",
      "typeFilter": "AND",
      "fieldValueFirst": "10",
      "fieldValueSecond": "20"
    },
    {
      "operator": "> ALL",
      "fieldName": "price",
      "typeFilter": "AND",
      "subquery": {
        "tableName": "HistoryPrice",
        "selectcolumns": [ "LastPrice" ],
        "Filtercolumns": [
          {
            "operator": "=",
            "fieldName": "LastPrice",
            "typeFilter": "AND",
            "fieldValue": "100"
          }
        ]
      }
    }
  ],
  "TableJoins": [
    {
      "tableName": "HistoryPrice",
      "typeJoin": "INNER",
      "RerefenceTableFieldName": "Id",
      "FieldName": "ProductId"
    }
  ]
}
```
Output
```
SELECT
    Product.Id,
    Product.Name,
    Product.Price
FROM Product
INNER JOIN HistoryPrice ON Product.Id=HistoryPrice.ProductId
WHERE Price > (14)
AND Name = (Test)
AND Price < (HistoryPrice.LastPrice)
AND Price BETWEEN 10 AND 20
AND price > ALL (SELECT
                  HistoryPrice.LastPrice
                  FROM HistoryPrice
                  WHERE LastPrice = (100))
```
