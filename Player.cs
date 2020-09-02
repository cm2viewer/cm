using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace cm
{ 
    [StructLayout(LayoutKind.Explicit)]
    internal struct Data
    {
        [FieldOffset(8)]
        public ushort cn;
        [FieldOffset(10)]
        public byte caps;
        [FieldOffset(15)]
        public ushort club;
        [FieldOffset(20)]
        public byte dd;
        [FieldOffset(21)]
        public byte mm;
        [FieldOffset(22)]
        public byte yy;
        [FieldOffset(23)]
        public ushort position;
        [FieldOffset(25)]
        public byte side;
        [FieldOffset(26)]
        public ushort ability;
        [FieldOffset(28)]
        public ushort potential;
        [FieldOffset(30)]
        public ushort rep;
        [FieldOffset(32)]
        public ushort unknown1;
        [FieldOffset(34)]
        public ushort talent;
        [FieldOffset(36)]
        public ushort unknown2;
        [FieldOffset(38)]
        public ushort unknown3;
        [FieldOffset(68)]
        public double wage;
        [FieldOffset(122)]
        public double price;
        [FieldOffset(138)]
        public byte status;
        [FieldOffset(234)]
        public long skill1;
        [FieldOffset(242)]
        public long skill2;
        [FieldOffset(250)]
        public long skill3;
        [FieldOffset(267)]
        public byte domapps;
        [FieldOffset(268)]
        public byte domgoals;
        [FieldOffset(269)]
        public byte domasst;
        [FieldOffset(270)]
        public ushort domrate;
        [FieldOffset(276)]
        public byte euapps;
        [FieldOffset(277)]
        public byte eugoals;
        [FieldOffset(278)]
        public byte euasst;
        [FieldOffset(279)]
        public ushort eurate;
    }

    public class Player
    {
        public string Name { get; set; }
        public int age { get; set; }
        public string cn { get; set; }
        public string pos { get; set; }
        public string alt { get; set; }
        public ushort abi { get; set; }
        public ushort pot { get; set; }
        public int DDM_pot { get; set; }
        public int FC_pot { get; set; }
        public ushort rep { get; set; }
        public ushort tal { get; set; }
        public byte Big { get; set; }
        public byte Chr { get; set; }
        public byte Mor { get; set; }
        public byte Con { get; set; }
        public byte Agg { get; set; }
        public string TRF { get; set; }
        public int price { get; set; }
        public int wage { get; set; }
        public double rating { get; set; }
        public string club { get; set; }
        public string club_cn { get; set; }
        public string division { get; set; }
        public string fgn { get; set; }
        public byte caps { get; set; }

        public byte Adap { get; set; }
        public byte Cre { get; set; }
        public byte Det { get; set; }
        public byte Dir { get; set; }
        public byte Dri { get; set; }
        public byte Fla { get; set; }
        public byte Hea { get; set; }
        public byte Inf { get; set; }
        public byte Inj { get; set; }
        public byte Off { get; set; }
        public byte Pac { get; set; }
        public byte Pas { get; set; }
        public byte Posi { get; set; }
        public byte Set { get; set; }
        public byte Sho { get; set; }
        public byte Sta { get; set; }
        public byte Str { get; set; }
        public byte Tac { get; set; }
        public byte Tec { get; set; }

        public int apps { get; set; }    
        public int goal { get; set; }
        public int asst { get; set; }
        public int GK { get; set; }
        public int SW { get; set; }
        public int D { get; set; }
        public int DM { get; set; }
        public int M { get; set; }
        public int AM { get; set; }
        public int S { get; set; }
        public int L { get; set; }
        public int C { get; set; }
        public int R { get; set; }

        public int teamval { get; set; }
        public string sell { get; set; }
    }
   
    public class Playerxx
    {
        public string Name;
        public int age;

        public string pos;
        public string alt;
        public ushort abi;
        public ushort pot;
        public int DDM_pot;
        public int FC_pot;
        public ushort rep;
        public ushort tal;
        public byte Abil;
        public byte Big;
        public byte Chr;
        public byte Con;
        public string TRF;
        public int teamval;
        public string sell;
        public int price;
        public int wage;
        public double rate;
        public string cn;
        public string club;
        public byte caps;
        public byte Adap;
        public byte Agg;
        public byte Cre;
        public byte Det;
        public byte Dir;
        public byte Dri;
        public byte Fla;
        public byte Hea;
        public byte Inf;
        public byte Inj;
        public byte Off;
        public byte Pac;
        public byte Pas;
        public byte Posi;
        public byte Set;
        public byte Sho;
        public byte Sta;
        public byte Str;
        public byte Tac;
        public byte Tec;
        public int apps;
        public int goal;
        public int asst;
        
        public byte GK;
        public byte SW;
        public byte D;
        public byte DM;
        public byte M;
        public byte AM;
        public byte S;
        public byte L;
        public byte C;
        public byte R;
    }

}
