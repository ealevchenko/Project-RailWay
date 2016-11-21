using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conLibrary
{
    class Test_ModelISA_Backup
    {
    }
    //// Модели;
    //EquipmentBackup eb = new EquipmentBackup();
    //EquipmentPropertyBackup ep = new EquipmentPropertyBackup();

    //eb.NameFile = "Equipment";
    //ep.NameFile = "EquipmentProperty";
    //ResultScript rs_e = eb.CreateIUScriptInID(10500);
    //List<ResultScript> list_rs_ep = new List<ResultScript>();
    //StringBuilder sb_ep = new StringBuilder();
    //bool primary = true;
    //foreach (ResultIDScript rids in rs_e.ListResultID)
    //{
    //    ResultScript rs_ep = ep.CreateIUScriptInOwner(rids.ID);
    //    if (primary) { sb_ep.Append(rs_ep.ScriptDeclare); primary = false; }
    //    sb_ep.Append(rs_ep.ScriptOperation);
    //    list_rs_ep.Add(rs_ep);
    //}
    //Backup bc = new Backup();
    //bc.SaveScript(rs_e, eb.NameFile);
    //bc.SaveScript(sb_ep, ep.NameFile);


    //EFEquipmentRepository eq = new EFEquipmentRepository();
    //foreach (Equipment eqel in eq.Equipment)
    //{
    //    Console.WriteLine("Equipment.ID: {0}, Equipment.Description: {1}", eqel.ID, eqel.Description);
    //}
}
