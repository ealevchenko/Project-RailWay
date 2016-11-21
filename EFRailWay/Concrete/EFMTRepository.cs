using EFRailWay.Abstract;
using EFRailWay.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete
{
    public class EFMTRepository : EFRepository, IMTRepository
    {

        public IQueryable<MTSostav> MTSostav
        {
            get { return context.MTSostav; }
        }
        public IQueryable<MTList> MTList
        {
            get { return context.MTList; }
        }
        public IQueryable<MTConsignee> MTConsignee
        {
            get { return context.MTConsignee; }
        }

        /// <summary>
        /// Добавить или править состав
        /// </summary>
        /// <param name="mtsostav"></param>
        /// <returns></returns>
        public int SaveMTSostav(MTSostav mtsostav)
        {
            MTSostav dbEntry;
            if (mtsostav.IDMTSostav == 0)
            {
                dbEntry = new MTSostav()
                {
                    FileName= mtsostav.FileName, 
                    CompositionIndex= mtsostav.CompositionIndex,
                    DateTime= mtsostav.DateTime,
                    Operation= mtsostav.Operation,
                    Create= mtsostav.Create,
                    Close= mtsostav.Close,
                    ParentID = mtsostav.ParentID
                };
                context_edit.MTSostav.Add(dbEntry);
            }
            else
            {
                dbEntry = context_edit.MTSostav.Find(mtsostav.IDMTSostav);
                if (dbEntry != null)
                {
                    dbEntry.FileName= mtsostav.FileName; 
                    dbEntry.CompositionIndex= mtsostav.CompositionIndex;
                    dbEntry.DateTime= mtsostav.DateTime;
                    dbEntry.Operation= mtsostav.Operation;
                    dbEntry.Create= mtsostav.Create;
                    dbEntry.Close= mtsostav.Close;
                    dbEntry.ParentID = mtsostav.ParentID;
                }
            }
            try
            {
                context_edit.SaveChanges();
            }
            catch (Exception e)
            {
                return -1;
            }
            return dbEntry.IDMTSostav;
        }
        /// <summary>
        /// Удалить состав
        /// </summary>
        /// <param name="IDMTSostav"></param>
        /// <returns></returns>
        public MTSostav DeleteMTSostav(int IDMTSostav)
        {
            MTSostav dbEntry = context_edit.MTSostav.Find(IDMTSostav);
            if (dbEntry != null)
            {
                context_edit.MTSostav.Remove(dbEntry);
                context_edit.SaveChanges();
            }
            return dbEntry;
        }
        /// <summary>
        /// Добавить или править 
        /// </summary>
        /// <param name="mtlist"></param>
        /// <returns></returns>
        public int SaveMTList(MTList mtlist)
        {
            MTList dbEntry;
            if (mtlist.IDMTList == 0)
            {
                dbEntry = new MTList()
                {
                    IDMTList=mtlist.IDMTList,
                    IDMTSostav=mtlist.IDMTSostav,
                    Position=mtlist.Position,
                    CarriageNumber=mtlist.CarriageNumber,
                    CountryCode=mtlist.CountryCode,
                    Weight=mtlist.Weight,
                    IDCargo=mtlist.IDCargo,
                    Cargo=mtlist.Cargo,
                    IDStation=mtlist.IDStation,
                    Station=mtlist.Station,
                    Consignee=mtlist.Consignee,
                    Operation=mtlist.Operation,
                    CompositionIndex=mtlist.CompositionIndex,
                    DateOperation=mtlist.DateOperation,
                    TrainNumber=mtlist.TrainNumber,
                    NaturList=mtlist.NaturList
                };
                context_edit.MTList.Add(dbEntry);
            }
            else
            {
                dbEntry = context_edit.MTList.Find(mtlist.IDMTList);
                if (dbEntry != null)
                {
                    dbEntry.IDMTList = mtlist.IDMTList;
                    dbEntry.IDMTSostav = mtlist.IDMTSostav;
                    dbEntry.Position = mtlist.Position;
                    dbEntry.CarriageNumber = mtlist.CarriageNumber;
                    dbEntry.CountryCode = mtlist.CountryCode;
                    dbEntry.Weight = mtlist.Weight;
                    dbEntry.IDCargo = mtlist.IDCargo;
                    dbEntry.Cargo = mtlist.Cargo;
                    dbEntry.IDStation = mtlist.IDStation;
                    dbEntry.Station = mtlist.Station;
                    dbEntry.Consignee = mtlist.Consignee;
                    dbEntry.Operation = mtlist.Operation;
                    dbEntry.CompositionIndex = mtlist.CompositionIndex;
                    dbEntry.DateOperation = mtlist.DateOperation;
                    dbEntry.TrainNumber = mtlist.TrainNumber;
                    dbEntry.NaturList = mtlist.NaturList;
                }
            }
            try
            {
                context_edit.SaveChanges();
            }
            catch (Exception e)
            {
                return -1;
            }
            return dbEntry.IDMTList;
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="IDMTList"></param>
        /// <returns></returns>
        public MTList DeleteMTList(int IDMTList)
        {
            MTList dbEntry = context_edit.MTList.Find(IDMTList);
            if (dbEntry != null)
            {
                context_edit.MTList.Remove(dbEntry);
                context_edit.SaveChanges();
            }
            return dbEntry;
        }
        /// <summary>
        ///  Добавить или править 
        /// </summary>
        /// <param name="MTConsignee"></param>
        /// <returns></returns>
        public int SaveMTConsignee(MTConsignee MTConsignee)
        {
            MTConsignee dbEntry;
            dbEntry = context_edit.MTConsignee.Find(MTConsignee.Code);
            if (dbEntry == null)
            {
                dbEntry = new MTConsignee()
                {
                    Code = MTConsignee.Code,
                    CodeDescription = MTConsignee.CodeDescription,
                    Consignee = MTConsignee.Consignee,
                };
                context_edit.MTConsignee.Add(dbEntry);
            }
            else { 
                    dbEntry.Code = MTConsignee.Code;
                    dbEntry.CodeDescription = MTConsignee.CodeDescription;
                    dbEntry.Consignee = MTConsignee.Consignee;           
            }
            try
            {
                context_edit.SaveChanges();
            }
            catch (Exception e)
            {
                return -1;
            }
            return dbEntry.Code;            
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public MTConsignee DeleteMTConsignee(int Code)
        {
            MTConsignee dbEntry = context_edit.MTConsignee.Find(Code);
            if (dbEntry != null)
            {
                context_edit.MTConsignee.Remove(dbEntry);
                context_edit.SaveChanges();
            }
            return dbEntry;
        }

    }
}
