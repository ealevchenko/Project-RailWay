
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
    public class EquipmentBackup : BackupTable<Equipment>
    {
        private IEquipmentRepository rep_Equipment;

        public EquipmentBackup() : base() 
        {
            this.rep_Equipment = new EFEquipmentRepository();
        }

        public EquipmentBackup(IEquipmentRepository rep_Equipment) : base() 
        {
            this.rep_Equipment = rep_Equipment;
        }

        public override void DeclareTable()
        {
            this.ScriptTable = new ScriptTable()
            {
                Scheme = "dbo",
                NameTable = "Equipment",
                ScriptField = new ScriptField[]
                {
                    new ScriptField() { NameField="ID", FieldType=typeof(int), FieldSize=null, FieldKey=true, FieldNull=false},
                    new ScriptField() { NameField="Description", FieldType=typeof(string), FieldSize=50, FieldKey=false, FieldNull=true},
                    new ScriptField() { NameField="EquipmentLevel", FieldType=typeof(string), FieldSize=50, FieldKey=false, FieldNull=true},
                    new ScriptField() { NameField="Equipment", FieldType=typeof(int), FieldSize=null, FieldKey=false, FieldNull=true},
                    new ScriptField() { NameField="HierarchyScope", FieldType=typeof(int), FieldSize=null, FieldKey=false, FieldNull=true},
                    new ScriptField() { NameField="EquipmentClassID", FieldType=typeof(int), FieldSize=null, FieldKey=false, FieldNull=true}
                }
            };
        }

        public override StringBuilder SetTable(Equipment e, ChangeID cid)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-------------------------------------------------------------------------------------------------");
            sb.AppendLine("--> Присвоим значения.");
            if (cid.ID == null) 
                { sb.AppendLine(String.Format("set @ID ={0};", e.ID.ToString())); }
            else { sb.AppendLine(String.Format("set @ID ={0};", cid.ID.ToString())); }

            sb.AppendLine(String.Format("set @Description = {0};", String.IsNullOrWhiteSpace(e.Description) ? "Null" : "N'" + e.Description + "'"));
            sb.AppendLine(String.Format("set @EquipmentLevel = {0};", String.IsNullOrWhiteSpace(e.EquipmentLevel) ? "Null" : "N'" + e.EquipmentLevel + "'"));
            //sb.AppendLine(String.Format("set @Equipment = {0};", e.Equipment1 == null ? "Null" : e.Equipment1.ToString()));
            if (cid.ID == null)
            { sb.AppendLine(String.Format("set @Equipment = {0};", e.Equipment1 == null ? "Null" : e.Equipment1.ToString())); }
            else { sb.AppendLine(String.Format("set @Equipment = {0};", cid.IDParent == null ? "Null" : cid.IDParent.ToString())); }

            sb.AppendLine(String.Format("set @HierarchyScope = {0};", e.HierarchyScope == null ? "Null" : e.HierarchyScope.ToString()));
            sb.AppendLine(String.Format("set @EquipmentClassID = {0};", e.EquipmentClassID == null ? "Null" : e.EquipmentClassID.ToString()));
            return sb;
        }

        public override IQueryable<Equipment> GetChild(Equipment e)
        {
            return this.rep_Equipment.Equipment.Where(c => c.Equipment1 == e.ID);
        }

        public override IQueryable<Equipment> GetRoot(int id)
        {
            return this.rep_Equipment.Equipment.Where(c => c.ID == id);
        }

        public override IQueryable<Equipment> GetRootParentID(int? parentid)
        {
            return this.rep_Equipment.Equipment.Where(c => c.Equipment1 == parentid);
        }

        public override IQueryable<Equipment> GetRootOwner(int? idOwner)
        {
            return this.rep_Equipment.Equipment.Where(c => c.EquipmentClassID == idOwner);
        }
    }
}
