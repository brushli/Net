<script runat="template">

        //get Columns info by TableName
        public ViewColumnSchemaCollection GetColumnCollectionByTable(ViewSchema table)
        {
            ViewColumnSchemaCollection columns = new ViewColumnSchemaCollection(table.Columns);
            return columns;
        }

        //Get camelcase name,such as Customer,
        public string GetCamelCaseName(string str)
        {
            return str.Substring(0,1).ToUpper()+str.Substring(1);
        }
        
       //Get ,user,private const String USER_FIELD = "User"
        public string GetMemberConstantDeclarationStatement(ColumnSchema column)
        {
            return GetMemberConstantDeclarationStatement("public const String ",column);
        }
        
        //such as public const String USER_TABLE = "User"
        public string GetTableConstantDeclarationStatement(ViewSchema table)
        {
            return GetMemberConstantDeclarationStatement("public const String ",table);    
        }
        //suck as USER_TABLE
        public string GetUpperStatement(ViewSchema table)
        {
            return     table.Name.ToUpper()+"_TABLE";
       }
        //suck as USER_FIELD
        public string GetUpperStatement(ColumnSchema column)
       {
           return column.Name.ToUpper()+"_FIELD";
        }

        // such as USER_TABLE = "User"
        public string GetMemberConstantDeclarationStatement(string protectionLevel, ViewSchema table)
        {
            return protectionLevel+GetUpperStatement(table)+" = "+GetCamelCaseName(table.Name)+"";
        }
       
        //such as USERID_FIELD = "Userid"
        public string GetMemberConstantDeclarationStatement(string protectionLevel,ColumnSchema column)
        {
            return protectionLevel+GetUpperStatement(column)+" = "+GetCamelCaseName(column.Name)+"";
        }
        public string GetTsType(ColumnSchema column)
        {
            switch(column.DataType)
            {                
                case DbType.AnsiString: return "string";
                case DbType.AnsiStringFixedLength: return "string";
                case DbType.Binary: return "any";                    
                case DbType.Boolean: return  "number";
                case DbType.Byte: return "number";
                case DbType.Currency: return "number";
                case DbType.Date: return "Date";
                case DbType.DateTime: return "Date";
                case DbType.Decimal: return "number";
                case DbType.Double: return "number";
                case DbType.Guid: return "any";
                case DbType.Int16: return "number";
                case DbType.Int32: return "number";
                case DbType.Int64: return "number";
                case DbType.Object: return "any";
                case DbType.SByte: return "any";
                case DbType.Single: return "number";
                case DbType.String: return "string";
                case DbType.StringFixedLength: return "string";
                case DbType.Time: return "any";
                case DbType.UInt16: return  "number";
                case DbType.UInt32: return "number";
                case DbType.UInt64: return "number";
                case DbType.VarNumeric: return "number";
            }            
            return null;
        }   
        public string GetCSharpVariableType(ColumnSchema column)
        {
            switch(column.DataType)
            {
                
                case DbType.AnsiString: return "string";
                case DbType.AnsiStringFixedLength: return "string";
                case DbType.Binary: return "byte[]";                    
                case DbType.Boolean: return  column.AllowDBNull?"bool?":"bool";
                case DbType.Byte: return column.AllowDBNull?"int?":"int";
                case DbType.Currency: return "decimal";
                case DbType.Date: return "DataTime";
                case DbType.DateTime: return "DateTime";
                case DbType.Decimal: return "decimal";
                case DbType.Double: return "double";
                case DbType.Guid: return "Guid";
                case DbType.Int16: return "short";
                case DbType.Int32: return column.AllowDBNull?"int?":"int";
                case DbType.Int64: return column.AllowDBNull?"long?":"long";
                case DbType.Object: return "object";
                case DbType.SByte: return "sbyte";
                case DbType.Single: return "float";
                case DbType.String: return "string";
                case DbType.StringFixedLength: return "string";
                case DbType.Time: return "TimeSpan";
                case DbType.UInt16: return  column.AllowDBNull?"bool?":"bool";
                case DbType.UInt32: return "uint";
                case DbType.UInt64: return "ulong";
                case DbType.VarNumeric: return "decimal";
            }            
            return null;
        }        
        public string GetCSharpBaseType(ViewColumnSchema column)
       {
            switch(column.DataType)
           {
                case DbType.AnsiString: return "System.String";
                case DbType.AnsiStringFixedLength: return "System.String";
                case DbType.Binary: return "System.Byte[]";
                case DbType.Boolean: return "System.Boolean";
                case DbType.Byte: return "System.Int32";
                case DbType.Currency: return "System.Decimal";
                case DbType.Date: return "System.DataTime";
                case DbType.DateTime: return "System.DataTime";
                case DbType.Decimal: return "System.Decimal";
                case DbType.Double: return "System.Double";
                case DbType.Guid: return "System.Guid";
                case DbType.Int16: return "System.Int16";
                case DbType.Int32: return "System.Int32";
                case DbType.Int64: return "System.Int64";
                case DbType.Object: return "System.Object";
                case DbType.SByte: return "System.SByte";
                case DbType.Single: return "System.Single";
                case DbType.String: return "System.String";
                case DbType.StringFixedLength: return "System.String";
                case DbType.Time: return "System.TimeSpan";
                case DbType.UInt16: return "System.UInt16";
                case DbType.UInt32: return "System.UInt32";
                case DbType.UInt64: return "System.UInt64";
                case DbType.VarNumeric: return "System.Decimal";
            }
            return null;
        }
</script>

