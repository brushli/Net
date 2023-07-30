<script runat="template">

public string GetStringForPascalCase(string name)
{
    if(string.IsNullOrEmpty(name)) return string.Empty;
    string fileName = string.Empty;
    int position = name.IndexOf("_");
    if (position > 0)
    {
          fileName = StringUtil.ToPascalCase(name);
    }
    else
    {
         fileName = GetFirstCharToUpper(name.ToLower());
    } 
   return fileName ;
}
public string GetFirstCharToUpper(string name)
{
	if(string.IsNullOrEmpty(name)) return string.Empty;
	
	return name.Substring(0, 1).ToUpper() + name.Substring(1, name.Length - 1);
}
public string GetFirstCharToLower(string name)
{
	if(string.IsNullOrEmpty(name)) return string.Empty;
	
	return name.Substring(0, 1).ToLower() + name.Substring(1, name.Length - 1);
}
public string GetFormatName(string name, bool isFormat)
{
	if(!isFormat) return name;
	
	string fileName = name.Substring(name.IndexOf("_") + 1);
	
	if(fileName.EndsWith("ses")) fileName = fileName.Substring(0, fileName.LastIndexOf("es"));
	
	if(fileName.EndsWith("ies")) fileName = fileName.Substring(0, fileName.LastIndexOf("ies")) + "y";
	
	if(fileName.EndsWith("s")) fileName = fileName.Substring(0, fileName.Length - 1);
	
	return GetFirstCharToUpper(fileName);
}
public string GetListName(string name)
{
	return name + "List";
}
public string GetEntityName(string tableName, bool isFormat)
{
	tableName = GetFormatName(tableName, isFormat);
	
	return tableName + "Entity";
}
public string GetByName(string name, ColumnSchemaCollection columns)
{
	string names = string.Empty;
	
	if(columns.Count == 0) return names;
	
	for(int i = 0; i < columns.Count; i++)
	{
		if(i == 0) 
		{
			names += columns[i].Name;
		}
		else
		{
			names += "And" + columns[i].Name;
		}
	}
	
	if(name.EndsWith("]"))
	{
		return name.Substring(0, name.Length - 1) + "By" + names + "]";
	}
	
	return name + "By" + names;
}
public string GetFormatProName(string tableName, string operationName, bool isFormat, bool isList)
{
	tableName = GetFormatName(tableName, isFormat);
	
	string tableNameList = tableName;
	
	if(isList) tableNameList = GetListName(tableName);
	
	return string.Format("[dbo].[UP_{0}_{1}]", tableName, GetFirstCharToUpper(operationName) + tableNameList);
}
public string GetFormatFunctionName(string tableName, string operationName, bool isFormat, bool isList)
{
	tableName = GetFormatName(tableName, isFormat);
		
	if(isList) tableName = GetListName(tableName);
	
	return GetFirstCharToUpper(operationName) + tableName;
}

public string cleanstring(string source)
{
  return source.Replace((char)10, (char)32).Replace("\n", " ");
}

</script>