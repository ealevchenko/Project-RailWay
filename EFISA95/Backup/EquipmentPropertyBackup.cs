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
    public class EquipmentPropertyBackup : BackupTable<EquipmentProperty>
    {
        private IEquipmentPropertyRepository rep_EquipmentProperty;
        
        public EquipmentPropertyBackup() : base() 
        {
            this.rep_EquipmentProperty = new EFEquipmentPropertyRepository();
        }

        public EquipmentPropertyBackup(IEquipmentPropertyRepository rep_EquipmentProperty) : base() 
        {
            this.rep_EquipmentProperty = rep_EquipmentProperty;
        }

        public override void DeclareTable()
        {
            this.ScriptTable = new ScriptTable()
            {
                Scheme = "dbo",
                NameTable = "EquipmentProperty",
                ScriptField = new ScriptField[]
                {
                    new ScriptField() { NameField="ID", FieldType=typeof(int), FieldSize=null, FieldKey=true, FieldNull=false},
                    new ScriptField() { NameField="Description", FieldType=typeof(string), FieldSize=50, FieldKey=false, FieldNull=true},
                    new ScriptField() { NameField="Value", FieldType=typeof(string), FieldSize=50, FieldKey=false, FieldNull=true},
                    new ScriptField() { NameField="EquipmentProperty", FieldType=typeof(int), FieldSize=null, FieldKey=false, FieldNull=true},
                    new ScriptField() { NameField="EquipmentCapabilityTestSpecification", FieldType=typeof(string), FieldSize=50, FieldKey=false, FieldNull=true},
                    new ScriptField() { NameField="TestResult", FieldType=typeof(string), FieldSize=50, FieldKey=false, FieldNull=true},
                    new ScriptField() { NameField="EquipmentID", FieldType=typeof(int), FieldSize=null, FieldKey=false, FieldNull=true},
                    new ScriptField() { NameField="ClassPropertyID", FieldType=typeof(int), FieldSize=null, FieldKey=false, FieldNull=true},
                    new ScriptField() { NameField="UnitID", FieldType=typeof(int), FieldSize=null, FieldKey=false, FieldNull=true},
                }
            };
        }

        public override StringBuilder SetTable(EquipmentProperty ep, ChangeID cid)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-------------------------------------------------------------------------------------------------");
            sb.AppendLine("--> Присвоим значения.");
            if (cid.ID == null)
            { sb.AppendLine(String.Format("set @ID ={0};", ep.ID.ToString())); }
            else { sb.AppendLine(String.Format("set @ID ={0};", cid.ID.ToString())); }

            sb.AppendLine(String.Format("set @Description = {0};", String.IsNullOrWhiteSpace(ep.Description) ? "Null" : "N'" + ep.Description + "'"));
            sb.AppendLine(String.Format("set @Value = {0};", String.IsNullOrWhiteSpace(ep.Value) ? "Null" : "N'" + ep.Value + "'"));
            if (cid.ID == null)
            { sb.AppendLine(String.Format("set @EquipmentProperty = {0};", ep.EquipmentProperty1 == null ? "Null" : ep.EquipmentProperty1.ToString())); }
            else { sb.AppendLine(String.Format("set @EquipmentProperty = {0};", cid.IDParent == null ? "Null" : cid.IDParent.ToString())); }
            sb.AppendLine(String.Format("set @EquipmentCapabilityTestSpecification = {0};", String.IsNullOrWhiteSpace(ep.EquipmentCapabilityTestSpecification) ? "Null" : "N'" + ep.EquipmentCapabilityTestSpecification + "'"));
            sb.AppendLine(String.Format("set @TestResult = {0};", String.IsNullOrWhiteSpace(ep.TestResult) ? "Null" : "N'" + ep.TestResult + "'"));
            sb.AppendLine(String.Format("set @EquipmentID = {0};", ep.EquipmentID == null ? "Null" : ep.EquipmentID.ToString()));
            sb.AppendLine(String.Format("set @ClassPropertyID = {0};", ep.ClassPropertyID == null ? "Null" : ep.ClassPropertyID.ToString()));
            sb.AppendLine(String.Format("set @UnitID = {0};", ep.UnitID == null ? "Null" : ep.UnitID.ToString()));
            return sb;
        }

        public override IQueryable<EquipmentProperty> GetChild(EquipmentProperty ecp)
        {
            return this.rep_EquipmentProperty.EquipmentProperty.Where(c => c.EquipmentProperty1 == ecp.ID);
        }

        public override IQueryable<EquipmentProperty> GetRoot(int id)
        {
            return this.rep_EquipmentProperty.EquipmentProperty.Where(c => c.ID == id);
        }

        public override IQueryable<EquipmentProperty> GetRootParentID(int? parentid)
        {
            return this.rep_EquipmentProperty.EquipmentProperty.Where(c => c.EquipmentProperty1 == parentid);
        }

        public override IQueryable<EquipmentProperty> GetRootOwner(int? idOwner) 
        {
            return this.rep_EquipmentProperty.EquipmentProperty.Where(c => c.EquipmentID == idOwner);        
        }
    }
}
