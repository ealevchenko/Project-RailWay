using EFRailWay.Abstract;
using EFRailWay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete
{
    public class EFTypeValueRepository : EFRepository, ITypeValueRepository
    {
        /// <summary>
        /// 
        /// </summary>
        public IQueryable<TypeValue> TypeValue
        {
            get { return context.TypeValue; }
        }
        /// <summary>
        /// Добавить или править
        /// </summary>
        /// <param name="TypeValue"></param>
        /// <returns></returns>
        public int SaveTypeValue(TypeValue TypeValue)
        {
            TypeValue dbEntry;
                dbEntry = context_edit.TypeValue.Find(TypeValue.IDTypeValue);
                if (dbEntry != null)
                {
                    dbEntry.TypeValue1 = TypeValue.TypeValue1;
                }
                else 
                {
                    dbEntry = new TypeValue()
                    {
                        IDTypeValue = TypeValue.IDTypeValue,
                        TypeValue1 = TypeValue.TypeValue1
                    };
                    context_edit.TypeValue.Add(dbEntry);
                }
            try
            {
                context_edit.SaveChanges();
            }
            catch (Exception e)
            {
                return -1;
            }
            return dbEntry.IDTypeValue;
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="IDTypeValue"></param>
        /// <returns></returns>
        public TypeValue DeleteTypeValue(int IDTypeValue)
        {
            TypeValue dbEntry = context_edit.TypeValue.Find(IDTypeValue);
            if (dbEntry != null)
            {
                context_edit.TypeValue.Remove(dbEntry);
                context_edit.SaveChanges();
            }
            return dbEntry;
        }
    }
}
