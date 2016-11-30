using EFRailWay.SAP;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferWagons.SAP
{
    public class References
    {

        private eventID eventID = eventID.TransferWagons_SAP_References;
        private SAPIncomingSupply sapis = new SAPIncomingSupply();

        public References() 
        { 
        
        }

    }
}
