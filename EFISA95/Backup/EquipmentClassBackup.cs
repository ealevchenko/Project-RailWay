using EFISA95.Abstract;
using EFISA95.Concrete;
using EFISA95.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFISA95.Backup
{
    public class EquipmentClassBackup : BackupTable<EquipmentClass> 
    {
        
        private IEquipmentClassRepository rep_EquipmentClass;

        public EquipmentClassBackup() : base() 
        {
            this.rep_EquipmentClass = new EFEquipmentClassRepository();
        }

        public EquipmentClassBackup(IEquipmentClassRepository rep_EquipmentClass) : base() 
        {
            this.rep_EquipmentClass = rep_EquipmentClass;
        }

        public override void DeclareTable()
        {
            this.ScriptTable = new ScriptTable()
            {
                Scheme = "dbo",
                NameTable = "EquipmentClass",
                ScriptField = new ScriptField[]
                {
                    new ScriptField() { NameField="ID", FieldType=typeof(int), FieldSize=null, FieldKey=true, FieldNull=false},
                    new ScriptField() { NameField="Description", FieldType=typeof(string), FieldSize=50, FieldKey=false, FieldNull=true},
                    new ScriptField() { NameField="EquipmentLevel", FieldType=typeof(string), FieldSize=50, FieldKey=false, FieldNull=true},
                    new ScriptField() { NameField="HierarchyScope", FieldType=typeof(int), FieldSize=null, FieldKey=false, FieldNull=true},
                    new ScriptField() { NameField="ParentID", FieldType=typeof(int), FieldSize=null, FieldKey=false, FieldNull=true},
                }
            };
        }

        public override StringBuilder SetTable(EquipmentClass ec, ChangeID cid)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-------------------------------------------------------------------------------------------------");
            sb.AppendLine("--> Присвоим значения.");
            //sb.AppendLine(String.Format("set @ID ={0};", ec.ID.ToString()));
            if (cid.ID == null)
            { sb.AppendLine(String.Format("set @ID ={0};", ec.ID.ToString())); }
            else { sb.AppendLine(String.Format("set @ID ={0};", cid.ID.ToString())); }
            sb.AppendLine(String.Format("set @Description = {0};", String.IsNullOrWhiteSpace(ec.Description) ? "Null" : "N'" + ec.Description + "'"));
            sb.AppendLine(String.Format("set @EquipmentLevel = {0};", String.IsNullOrWhiteSpace(ec.EquipmentLevel) ? "Null" : "N'" + ec.EquipmentLevel + "'"));
            sb.AppendLine(String.Format("set @HierarchyScope = {0};", ec.HierarchyScope == null ? "Null" : ec.HierarchyScope.ToString()));
            //sb.AppendLine(String.Format("set @ParentID = {0};", ec.ParentID == null ? "Null" : ec.ParentID.ToString()));            
            if (cid.ID == null)
            { sb.AppendLine(String.Format("set @ParentID = {0};", ec.ParentID == null ? "Null" : ec.ParentID.ToString())); }
            else { sb.AppendLine(String.Format("set @ParentID = {0};", cid.IDParent == null ? "Null" : cid.IDParent.ToString())); }


            return sb;
        }

        public override IQueryable<EquipmentClass> GetChild(EquipmentClass ec)
        {

            return this.rep_EquipmentClass.EquipmentClass.Where(c => c.ParentID == ec.ID);
        }

        public override IQueryable<EquipmentClass> GetRoot(int id)
        {

            return this.rep_EquipmentClass.EquipmentClass.Where(c => c.ID == id);
        }

        public override IQueryable<EquipmentClass> GetRootParentID(int? parentid)
        {

            return this.rep_EquipmentClass.EquipmentClass.Where(c => c.ParentID == parentid);
        }

    }
}
