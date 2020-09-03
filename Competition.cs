using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace cm
{

    [StructLayout(LayoutKind.Explicit)]
    internal struct CompetitionData
    {
        [FieldOffset(85)]
        public byte numOfTeams;
        [FieldOffset(88)]
        public unsafe fixed ushort teams[25];

    }

    public class Competition
    {
        public int id { get; set; }
        public string name1 { get; set; }
        public string name2 { get; set; }
        public string name3 { get; set; }
        public string name4 { get; set; }
        public string name5 { get; set; }
        public int numTeams { get; set; }
        public string teams { get; set; }
        public List<int> teamsID { get; set; }

        public Competition()
        {
            teamsID = new List<int>();
        }
    }
}
