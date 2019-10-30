using Dz4.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dz4.DataAccess
{
    public class Repository<T> : IDisposable where T : Entity
    {
        private readonly DbProviderFactory providerFactory;
        private readonly DbConnection connection;
        private readonly string rootPath = "Dz4.Domain.";
        private static Dictionary<Type, DbType> typeMap;
        private List<DbParameter> sqlParameters;
        private Assembly assembly;

        public Repository(string connectionString, string providerName)
        {
            sqlParameters = new List<DbParameter>();
            SqlHelper();
            providerFactory = DbProviderFactories.GetFactory(providerName);
            connection = providerFactory.CreateConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
        }

        public void Add(T element)
        {
            var name = typeof(T).Name.ToString();
            GetProperty(element, name);

            var commandTmp = $"Insert Into {name}s (";
            commandTmp = CreateQuery(commandTmp);
            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = commandTmp;
                command.Parameters.AddRange(sqlParameters.ToArray());
                command.ExecuteNonQuery();
            }
            sqlParameters = new List<DbParameter>();
        }

        public void Delete(Guid Id)
        {
            var name = typeof(T).Name.ToString();
            Type type = typeof(T);
            StringBuilder stringBuilder = new StringBuilder();

            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.Date.ToString("yyyy-MM-dd HH:mm:ss");

            var commandTmp = $"Update [{name}s] set DeleteDate = '{sqlFormattedDate}' where Id = '{Id}';";
            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = commandTmp;
                command.ExecuteNonQuery();
            }
        }

        public void Update(T element)
        {
            var name = typeof(T).Name.ToString();
            GetProperty(element, name);

            var commandTmp = $"Update {name}s Set ";
            for (int i = 0; i < sqlParameters.Count; i++)
            {
                commandTmp += sqlParameters[i].Value is null ? "" : $"[{sqlParameters[i].ParameterName.Substring(1)}] = {sqlParameters[i].ParameterName}";
                if (i != sqlParameters.IndexOf(sqlParameters.Last())) commandTmp += ",";
            }
            commandTmp += $" Where Id = '{element.Id}';";

            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = commandTmp;
                command.ExecuteNonQuery();
            }
        }

        public ICollection<T> GetAll()
        {
            List<T> table = new List<T>();
            var name = typeof(T).Name.ToString();
            var commandSQL = $"Select * From {name}s";
            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = commandSQL;
                DbDataReader sqlDataReader = command.ExecuteReader();

                string json = $"[";
                while (sqlDataReader.Read())
                {
                    json += "{";
                    for (int i = 0; i < sqlDataReader.FieldCount; i++)
                    {
                        json += $@" {sqlDataReader.GetName(i)} : {(sqlDataReader.GetValue(i) is null ? "null" : $@"""{sqlDataReader.GetValue(i)}""")}";
                        if (i != sqlDataReader.FieldCount - 1)
                        {
                            json += ",";
                        }
                    }
                    json += "},";
                }
                json += "]";
                json = json.Replace(@"""""", "null");
                var format = "dd/MM/yyyy HH:mm:ss";
                var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };
                table = JsonConvert.DeserializeObject<List<T>>(json, dateTimeConverter);
            }
            return table;
        }


        private void GetProperty(T element, string name)
        {
            assembly = typeof(T).Assembly;
            var type = assembly.GetType(rootPath + name);

            foreach (var member in type.GetMembers())
            {
                if (member is PropertyInfo)
                {
                    var property = member as PropertyInfo;
                    AddParametr(property, element);
                }
            }
        }

        private void AddParametr(PropertyInfo property, T element)
        {
            var dbType = GetDbType(property.PropertyType);

            if (property.DeclaringType.ToString().Contains("Entity"))
            {
                if (!property.PropertyType.ToString().Contains("Nullable"))
                {
                    sqlParameters.Insert(sqlParameters.IndexOf(sqlParameters.First()),
                    CreateObjectSqlParametr(property, element, dbType));
                }
            }
            else
            {
                if (!property.PropertyType.ToString().Contains("Nullable"))
                {
                    sqlParameters.Add(CreateObjectSqlParametr(property, element, dbType));
                }
            }
        }

        private string CreateQuery(string command)
        {
            for (int i = 0; i < sqlParameters.Count; i++)
            {
                if (sqlParameters[i].IsNullable != true)
                {
                    command += $"[{sqlParameters[i].ParameterName.Substring(1)}]";
                    if (i != sqlParameters.IndexOf(sqlParameters.Last())) command += ",";
                    else command += ")";
                }
            }
            command += "values(";
            for (int i = 0; i < sqlParameters.Count; i++)
            {
                if (sqlParameters[i].IsNullable != true)
                {
                    command += $"{sqlParameters[i]}";
                    if (i != sqlParameters.IndexOf(sqlParameters.Last())) command += ",";
                    else command += ");";
                }
            }

            return command;
        }

        private DbParameter CreateObjectSqlParametr(PropertyInfo property, T element, DbType dbType)
        {
            var parametr = providerFactory.CreateParameter();

            parametr.ParameterName = $"@{property.Name}";
            parametr.Value = property.GetValue(element);
            parametr.DbType = dbType;
            return parametr;
        }

        private void SqlHelper()
        {
            typeMap = new Dictionary<Type, DbType>();

            typeMap[typeof(string)] = DbType.String;
            typeMap[typeof(char[])] = DbType.String;
            typeMap[typeof(short)] = DbType.Int16;
            typeMap[typeof(int)] = DbType.Int32;
            typeMap[typeof(long)] = DbType.Int64;
            typeMap[typeof(bool)] = DbType.Binary;
            typeMap[typeof(DateTime)] = DbType.DateTime;
            typeMap[typeof(DateTimeOffset)] = DbType.DateTimeOffset;
            typeMap[typeof(decimal)] = DbType.Decimal;
            typeMap[typeof(double)] = DbType.Double;
            typeMap[typeof(TimeSpan)] = DbType.Time;
            typeMap[typeof(Guid)] = DbType.Guid;
        }

        private DbType GetDbType(Type giveType)
        {
            giveType = Nullable.GetUnderlyingType(giveType) ?? giveType;

            if (typeMap.ContainsKey(giveType))
            {
                return typeMap[giveType];
            }

            throw new ArgumentException($"{giveType.FullName} is not a supported .NET class");
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}
