<script runat="template">
public IndexSchemaCollection GetUniqueKeyIndexs(TableSchema table)
{	
	IndexSchemaCollection indexs = new IndexSchemaCollection(0);
	
	foreach(IndexSchema index in table.Indexes)
	{
		if(index.IsUnique && !index.IsPrimaryKey)
		{
			indexs.Add(index);
		}		
	}
	
	return indexs;
}
public ColumnSchemaCollection GetCreateColumns(TableSchema table)
{
	ColumnSchemaCollection cc = new ColumnSchemaCollection(0);
	
	for(int i = 0; i < table.Columns.Count; i++)
	{
		ColumnSchema column = table.Columns[i];
		
		if(bool.Parse(column.ExtendedProperties["CS_IsIdentity"].Value.ToString())) continue;
		
		cc.Add(column);
	}
	
	return cc;
}
public ColumnSchemaCollection GetPrimaryKeys(TableSchema table)
{
	ColumnSchemaCollection cc = new ColumnSchemaCollection(0);
	
	if(table.HasPrimaryKey)
	{
		foreach(MemberColumnSchema column in table.PrimaryKey.MemberColumns)
		{
			cc.Add(column as ColumnSchema);
		}
	}
	
	return cc;
}
public ColumnSchemaCollection GetUniqueKeys(IndexSchema index)
{
	ColumnSchemaCollection cc = new ColumnSchemaCollection(0);
	
	foreach(MemberColumnSchema member in index.MemberColumns)
	{
		cc.Add(member as ColumnSchema);
	}
	
	return cc;
}
public string GetSqlParameterString(ColumnSchema column)
{
	string type = GetSqlType(column);
	
	string param = string.Format("@{0} {1}", column.Name, type);
	
	switch(type.ToLower())
	{	
		case "binary":
		case "char":
		case "nchar":
		case "nvarchar":
		case "varbinary":
		case "varchar":
			param += "(" + column.Size + ")";
			break;
		case "decima":
		case "numeric":
			param += "(" + column.Precision + "," + column.Scale + ")";
			break;			
		default:
			break;
	}
	
	return param;
}
public string GetSqlParameterDefault(ColumnSchema column)
{
	if(column.AllowDBNull) return " = NULL";
	
	string dbType = GetSqlType(column).ToLower();
	
	if(string.Compare(dbType, "UniqueIdentifier", true) == 0) return " = NULL";
	
	if(dbType.IndexOf("datetime") >= 0) return " = NULL";
	
	string defaultValue = column.ExtendedProperties["CS_Default"].Value.ToString();
	
	defaultValue = defaultValue.Replace("(", "").Replace(")", "");
	
	if(string.IsNullOrEmpty(defaultValue)) return string.Empty;
	
	switch(dbType)
	{
		case "int":
		case "bit":
		case "char":
		case "varchar":
		case "nchar":
		case "nvarchar":
			return " = " + defaultValue;
	}
	
	return string.Empty;
}
public string GetCSharpType(ColumnSchema column)
{
	switch (column.NativeType)
	{
		case "bigint":
			return "long";
		case "binary":
			return "byte[]";
		case "bit":
			return "bool";
		case "char":
			return "string";
		case "date":
			return "DateTime";
		case "datetime":
			return "DateTime";
		case "datetime2":
			return "DateTime";
		case "datetimeoffset":
			return "DateTimeOffset";
		case "decimal":
			return "decimal";
		case "float":
			return "double";
		case "image":
			return "byte[]";
		case "int":
			return "int";
		case "money":
			return "decimal";
		case "nchar":
			return "string";
		case "ntext":
			return "string";
		case "numeric":
			return "decimal";
		case "nvarchar":
			return "string";
		case "real":
			return "Single";
		case "rowversion":
			return "byte[]";
		case "smalldatetime":
			return "DateTime";
		case "smallint":
			return "short";
		case "smallmoney":
			return "decimal";
		case "sql_variant":
			return "object";
		case "text":
			return "string";
		case "time":
			return "TimeSpan";
		case "timestamp":
			return "byte[]";
		case "tinyint":
			return "byte";
		case "uniqueidentifier":
			return "Guid";
		case "varbinary":
			return "byte[]";
		case "varchar":
			return "string";
		case "xml":
			return "System.Data.SqlTypes.SqlXml";
		default:
			return "Error";
	}
}
public string GetSqlType(ColumnSchema column)
{
	switch (column.NativeType)
	{
		case "varchar":
		case "nvarchar":
		case "varbinary":
			if(column.Size == -1) return column.NativeType + "(MAX)";
			return column.NativeType;
		default:
			return column.NativeType;
	}
}
public string GetSqlDbType(ColumnSchema column)
{
	switch (column.NativeType)
	{
		case "bigint":
			return "SqlDbType.BigInt";
		case "binary":
			return "SqlDbType.VarBinary";
		case "bit":
			return "SqlDbType.Bit";
		case "char":
			return "SqlDbType.Char";
		case "date":
			return "SqlDbType.Date";
		case "datetime":
			return "SqlDbType.DateTime";
		case "datetime2":
			return "SqlDbType.DateTime2";
		case "datetimeoffset":
			return "SqlDbType.DateTimeOffset";
		case "decimal":
			return "SqlDbType.Decimal";
		case "float":
			return "SqlDbType.Float";
		case "image":
			return "SqlDbType.Binary";
		case "int":
			return "SqlDbType.Int";
		case "money":
			return "SqlDbType.Money";
		case "nchar":
			return "SqlDbType.NChar";
		case "ntext":
			return "SqlDbType.NText";
		case "numeric":
			return "SqlDbType.Decimal";
		case "nvarchar":
			return "SqlDbType.NVarChar";
		case "real":
			return "SqlDbType.Real";
		case "rowversion":
			return "SqlDbType.Timestamp";
		case "smalldatetime":
			return "SqlDbType.DateTime";
		case "smallint":
			return "SqlDbType.SmallInt";
		case "smallmoney":
			return "SqlDbType.SmallMoney";
		case "sql_variant":
			return "SqlDbType.Variant";
		case "text":
			return "SqlDbType.Text";
		case "time":
			return "SqlDbType.Time";
		case "timestamp":
			return "SqlDbType.Timestamp";
		case "tinyint":
			return "SqlDbType.TinyInt";
		case "uniqueidentifier":
			return "SqlDbType.UniqueIdentifier";
		case "varbinary":
			return "SqlDbType.VarBinary";
		case "varchar":
			return "SqlDbType.VarChar";
		case "xml":
			return "SqlDbType.Xml";
		default:
			return "Error";
	}
}
public string GetReaderMethod(ColumnSchema column)
{	
	switch (column.NativeType)
	{
		case "bigint":
			return "GetInt64({0})";
		case "binary":
			return "GetSqlBinary({0}).Value";
		case "bit":
			return "GetBoolean({0})";
		case "char":
			return "GetString({0})";
		case "date":
			return "GetDateTime({0})";
		case "datetime":
			return "GetDateTime({0})";
		case "datetime2":
			return "GetDateTime({0})";
		case "datetimeoffset":
			return "GetDateTimeOffset({0})";
		case "decimal":
			return "GetDecimal({0})";
		case "float":
			return "GetDouble({0})";
		case "image":
			return "GetSqlBinary({0}).Value";
		case "int":
			return "GetInt32({0})";
		case "money":
			return "GetDecimal({0})";
		case "nchar":
			return "GetString({0})";
		case "ntext":
			return "GetString({0})";
		case "numeric":
			return "GetDecimal({0})";
		case "nvarchar":
			return "GetString({0})";
		case "real":
			return "GetFloat({0})";
		case "rowversion":
			return "GetSqlBinary({0}).Value";
		case "smalldatetime":
			return "GetDateTime({0})";
		case "smallint":
			return "GetInt16({0})";
		case "smallmoney":
			return "GetDecimal({0})";
		case "sql_variant":
			return "GetValue({0})";
		case "text":
			return "GetString({0})";
		case "time":
			return "GetDateTime({0})";
		case "timestamp":
			return "GetSqlBinary({0}).Value";
		case "tinyint":
			return "GetByte({0})";
		case "uniqueidentifier":
			return "GetGuid({0})";
		case "varbinary":
			return "GetSqlBinary({0}).Value";
		case "varchar":
			return "GetString({0})";
		case "xml":
			return "GetSqlXml({0})";
		default:
			return "Error";
	}
}
</script>