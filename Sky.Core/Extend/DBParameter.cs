using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace System
{
    /// <summary>
    /// 可序列化参数
    /// </summary>
    [Serializable]
    //[Obsolete("该类已经过时，请使用具体的数据参数类,如SqlParameter和OracleParameter")]
    public class DBParameter : IDataParameter, IDbDataParameter
    {
        public DBParameter()
        {
            Direction = ParameterDirection.Input;
            DbType = System.Data.DbType.String;
        }
        public DBParameter(string parameterName)
            : this()
        {
            ParameterName = parameterName;
        }

        public DBParameter(string parameterName, object value)
            : this()
        {
            ParameterName = parameterName;
            Value = value;


        }
        public DBParameter(string parameterName, DbType dbType)
            : this()
        {
            ParameterName = parameterName;
            DbType = dbType;
        }

        public DBParameter(string columnName, string parameterName, object value)
            : this(parameterName, value)
        {
            SourceColumn = columnName;
        }

        public string SourceColumn { get; set; }

        public object _Value;
        public object Value
        {
            get { return _Value; }
            set
            {
                if (value != null && value.GetType() == typeof(Guid))
                {
                    DbType = DbType.Guid;
                }
                _Value = value;
            }
        }


        public string ParameterName { get; set; }
        public ParameterDirection Direction { get; set; }
        public DbType DbType { get; set; }
        public DataRowVersion SourceVersion { get; set; }
        public int Size { get; set; }
        public bool IsNullable
        {
            get { return true; }
        }

        public byte Precision
        {
            get;
            set;
        }

        public byte Scale
        {
            get;
            set;
        }
    }
}
