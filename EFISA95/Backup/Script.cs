using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFISA95.Backup
{
    [Serializable()]
    public class ScriptField
    {
        public string NameField { get; set; }
        public Type FieldType { get; set; }
        public int? FieldSize { get; set; }
        public bool FieldKey { get; set; }
        public bool FieldNull { get; set; }
    }

    [Serializable()]
    public class ScriptTable
    {
        public string Scheme { get; set; }
        public string NameTable { get; set; }
        public ScriptField[] ScriptField { get; set; }
    }

    public class Scripts
    {

        /// <summary>
        /// Определить тип и размер поля
        /// </summary>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public string GetSQLType(Type type, int? size)
        {
            switch (type.Name)
            {
                case "Int32":
                    return "int";
                case "String":
                    if (size == null) throw new System.ArgumentException("Не задан размер поля (nvarchar)");
                    return "nvarchar(" + size + ")";
                case "DateTime":
                    return "datetime";
                case "Boolean":
                    return "bit";
                case "Double":
                    return "Real";
                default:
                    return null;
            }
        }
        /// <summary>
        /// Вернуть имя ключевого поля
        /// </summary>
        /// <param name="sfs"></param>
        /// <returns></returns>
        public string GetNameKey(ScriptField[] sfs)
        {
            foreach (ScriptField field in sfs)
            {
                if (field.FieldKey) return field.NameField;
            }
            return null;

        }
    }
}
