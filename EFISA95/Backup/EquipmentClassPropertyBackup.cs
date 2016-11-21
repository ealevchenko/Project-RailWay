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
    public class EquipmentClassPropertyBackup : BackupTable<EquipmentClassProperty>
    {
        private IEquipmentClassPropertyRepository rep_EquipmentClassProperty;

        public EquipmentClassPropertyBackup() : base() 
        {
            this.rep_EquipmentClassProperty = new EFEquipmentClassPropertyRepository();
        }

        public EquipmentClassPropertyBackup(IEquipmentClassPropertyRepository rep_EquipmentClassProperty) : base() 
        {
            this.rep_EquipmentClassProperty = rep_EquipmentClassProperty;
        }

        public override void DeclareTable()
        {
            this.ScriptTable = new ScriptTable()
            {
                Scheme = "dbo",
                NameTable = "EquipmentClassProperty",
                ScriptField = new ScriptField[]
                {
                    new ScriptField() { NameField="ID", FieldType=typeof(int), FieldSize=null, FieldKey=true, FieldNull=false},
                    new ScriptField() { NameField="Description", FieldType=typeof(string), FieldSize=50, FieldKey=false, FieldNull=true},
                    new ScriptField() { NameField="Value", FieldType=typeof(string), FieldSize=50, FieldKey=false, FieldNull=true},
                    new ScriptField() { NameField="EquipmentClassProperty", FieldType=typeof(int), FieldSize=null, FieldKey=false, FieldNull=true},
                    new ScriptField() { NameField="EquipmentCapabilityTestSpecification", FieldType=typeof(string), FieldSize=50, FieldKey=false, FieldNull=true},
                    new ScriptField() { NameField="EquipmentClassID", FieldType=typeof(int), FieldSize=null, FieldKey=false, FieldNull=true},
                    new ScriptField() { NameField="UnitID", FieldType=typeof(int), FieldSize=null, FieldKey=false, FieldNull=true},
                }
            };
        }

        public override StringBuilder SetTable(EquipmentClassProperty ecp, ChangeID cid)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-------------------------------------------------------------------------------------------------");
            sb.AppendLine("--> Присвоим значения.");
            if (cid.ID == null)
            { sb.AppendLine(String.Format("set @ID ={0};", ecp.ID.ToString())); }
            else { sb.AppendLine(String.Format("set @ID ={0};", cid.ID.ToString())); }
            //sb.AppendLine(String.Format("set @ID ={0};", ecp.ID.ToString()));
            sb.AppendLine(String.Format("set @Description = {0};", String.IsNullOrWhiteSpace(ecp.Description) ? "Null" : "N'" + ecp.Description + "'"));
            sb.AppendLine(String.Format("set @Value = {0};", String.IsNullOrWhiteSpace(ecp.Value) ? "Null" : "N'" + ecp.Value + "'"));
            //sb.AppendLine(String.Format("set @EquipmentClassProperty = {0};", ecp.EquipmentClassProperty1 == null ? "Null" : ecp.EquipmentClassProperty1.ToString()));
            if (cid.ID == null)
            { sb.AppendLine(String.Format("set @EquipmentClassProperty = {0};", ecp.EquipmentClassProperty1 == null ? "Null" : ecp.EquipmentClassProperty1.ToString())); }
            else { sb.AppendLine(String.Format("set @EquipmentClassProperty = {0};", cid.IDParent == null ? "Null" : cid.IDParent.ToString())); }
            
            sb.AppendLine(String.Format("set @EquipmentCapabilityTestSpecification = {0};", String.IsNullOrWhiteSpace(ecp.EquipmentCapabilityTestSpecification) ? "Null" : "N'" + ecp.EquipmentCapabilityTestSpecification + "'"));
            sb.AppendLine(String.Format("set @EquipmentClassID = {0};", ecp.EquipmentClassID == null ? "Null" : ecp.EquipmentClassID.ToString()));
            sb.AppendLine(String.Format("set @UnitID = {0};", ecp.UnitID == null ? "Null" : ecp.UnitID.ToString()));
            return sb;
        }

        public override IQueryable<EquipmentClassProperty> GetChild(EquipmentClassProperty ecp)
        {

            return this.rep_EquipmentClassProperty.EquipmentClassProperty.Where(c => c.EquipmentClassProperty1 == ecp.ID);
        }

        public override IQueryable<EquipmentClassProperty> GetRoot(int id)
        {

            return this.rep_EquipmentClassProperty.EquipmentClassProperty.Where(c => c.ID == id);
        }

        public override IQueryable<EquipmentClassProperty> GetRootParentID(int? parentid)
        {

            return this.rep_EquipmentClassProperty.EquipmentClassProperty.Where(c => c.EquipmentClassProperty1 == parentid);
        }

        public override IQueryable<EquipmentClassProperty> GetRootOwner(int? idOwner) 
        {

            return this.rep_EquipmentClassProperty.EquipmentClassProperty.Where(c => c.EquipmentClassID == idOwner);        
        }
    }
}
