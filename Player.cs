using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace cm
{ 
    [StructLayout(LayoutKind.Explicit,Size = 360)]
    public unsafe struct Data
    {
        [FieldOffset(8)]
        public ushort cn;
        [FieldOffset(10)]
        public ushort caps;
        [FieldOffset(12)]
        public ushort caps_goals;
        [FieldOffset(14)]
        public byte unknown;
        [FieldOffset(15)]
        public ushort club;
        //byte 17,18 always FFFF
        [FieldOffset(19)]
        public sbyte play;   //how much player is played on first team, determine contract duration
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
        public short rep;
        [FieldOffset(32)]
        public ushort unknown1;
        [FieldOffset(34)]
        public short talent;
        [FieldOffset(36)]
        public ushort unknown2;
        [FieldOffset(38)]
        public ushort unknown3;
        [FieldOffset(68)]  
        public double wage;     // byte 68-75
        [FieldOffset(76)]
        public byte joindate_dd;
        [FieldOffset(77)]
        public byte joindate_mm;
        [FieldOffset(78)]
        public byte joindate_yy;
        //byte 76.77.78 join date 14 01 62 -> 14 Januari 1998
        //byte 79.80.81 contract expire date 07 06 66 -> 7 June 102+1900
        //byte 82 win bonus
        //byte 83 goal bonus
        //byte 84 promotion bonus
        //byte 85 domestic trophy bonus
        //byte 86 european trophy bonus
        //byte 87 big club release
        //byte 88 relegation release
        //byte 89 non-promotion release
        //byte 90 free transfer on expire
        //byte 91 management job offer
        //byte 92 out of contract
        [FieldOffset(87)]
        public byte bcr;        //big-club release clause

        //byte 93-121 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
        [FieldOffset(122)]
        public double price;
        [FieldOffset(138)]
        public byte status;
        [FieldOffset(139)]
        public byte interested; //how many club interested
        [FieldOffset(140)]
        public byte workpermit;

        //byte 141-190 mix of 00 and 01
        [FieldOffset(191)]
        public ushort loan_to_club;
        [FieldOffset(193)]
        public byte recall_on_loan;
        [FieldOffset(194)]
        public byte loan_status;
        [FieldOffset(195)]
        public byte transfer_status;    //1E when just returned from loan //04 negotiating contract //05 offer accepted
        [FieldOffset(196)]
        public byte fixprice;       //fix selling price 1 - true, 0 - price according to performance
        [FieldOffset(197)]
        public byte physicalcondition;
        [FieldOffset(198)]
        public short recovery_percentage;   //the final_calculated_physicalcondition = physicalcondition * recovery_percentage/100
        //byte 200 00 or 01
        [FieldOffset(201)]
        public ushort injury_type;
        [FieldOffset(203)]
        public ushort injury_length;

        //byte 204-209 00 00 01 00 00 00
        [FieldOffset(210)]
        public byte domestic_ban;
        [FieldOffset(211)]
        public byte domestic_ban_start_dd;
        [FieldOffset(212)]
        public byte domestic_ban_start_mm;
        [FieldOffset(213)]
        public byte domestic_ban_start_yy;
        //byte 214-218 00 00 00 00 00
        [FieldOffset(219)]
        public byte european_ban;
        //byte 220-227 00 00 00 00 00 00 00 00
        [FieldOffset(228)]
        public byte international_ban;
        //byte 229-233 00 00 00 00 00
        [FieldOffset(234)]
        public fixed byte skill[24];    //byte 234-257
        //byte 258-266 1E 09 09 DF 00 00 04 00
        [FieldOffset(267)]
        public byte domapps;
        [FieldOffset(268)]
        public byte domgoals;
        [FieldOffset(269)]
        public byte domasst;
        [FieldOffset(270)]
        public ushort domrate;
        [FieldOffset(272)]
        public byte domestic_mom;
        [FieldOffset(273)]
        public byte domestic_yellowcard;
        [FieldOffset(274)]
        public byte domestic_redcard;
        [FieldOffset(275)]
        public byte domestic_discipline;

        [FieldOffset(276)]
        public byte eu_apps;
        [FieldOffset(277)]
        public byte eu_goals;
        [FieldOffset(278)]
        public byte eu_asst;
        [FieldOffset(279)]
        public ushort eu_rating;
        [FieldOffset(281)]
        public byte eu_mom;
        [FieldOffset(282)]
        public byte eu_yellowcard;
        [FieldOffset(283)]
        public byte eu_redcard;

        [FieldOffset(284)]
        public byte international_apps;
        [FieldOffset(285)]
        public byte international_goals;
        [FieldOffset(286)]
        public byte international_asst;
        [FieldOffset(287)]
        public ushort international_rating;
        [FieldOffset(289)]
        public byte international_mom;
        [FieldOffset(290)]
        public byte international_yellowcard;
        [FieldOffset(291)]
        public byte international_redcard;

    }

    public class Player
    {
        public static string[] skillName = 
            {"Agg","Big","Chr","Mor","Con","Cre","Det","Dir","Dri","Fla","Hea","Inf","Inj","Adap",
            "Off","Pac","Pas","Posi","Set","Sho","Sta","Str","Tac","Tec" };
        public static string[] skillLongName =
            {"Aggression","Big Oscassion","Character","Morale","Consistency",
            "Creativity","Determination","Dirtyness","Dribbling","Flair","Heading",
            "Influence","Injury Prone","Adaptibility",
            "Off the ball","Pace","Passing","Positioning","Set Pieces","Shooting",
            "Stamina","Strength","Tackling","Technique" };
        public string Name { get; set; }

        public int age { get; set; }
        public string cn { get; set; }
        
        public int days { get; set; }   //number of days since join date
        public int mon { get; set; }
        public string avail { get; set; }   //available for sell

        public string pos { get; set; }
        public string alt { get; set; }
        public ushort abi { get; set; }
        public ushort pot { get; set; }
        public short rep { get; set; }
        public short tal { get; set; }
        public int play { get; set; }
        public byte Mor { get; set; }
        //public byte[] playerskill { get; set; }
        public int skill { get; set; }
        public double avg { get; set; }
        public byte Big { get; set; }
        public byte Con { get; set; }
        public byte Agg { get; set; }


        public byte Chr { get; set; }
        public string TRF { get; set; }
        public byte CLBREP { get; set; }

        public int buy { get; set; }
        public int price { get; set; }
        public double rating { get; set; }
        public string bcr { get; set; }
        public string club { get; set; }
        public int DDM { get; set; }
        public int FC { get; set; }
        public string club_cn { get; set; }
        public string div { get; set; }
        public int wage { get; set; }

        public string fgn { get; set; }
        public ushort caps { get; set; }

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
        public byte GK { get; set; }
        public byte SW { get; set; }
        public byte D { get; set; }
        public byte DM { get; set; }
        public byte M { get; set; }
        public byte AM { get; set; }
        public byte S { get; set; }
        public byte L { get; set; }
        public byte C { get; set; }
        public byte R { get; set; }

        public int teamval { get; set; }
        public string sell { get; set; }
        public int injury { get; set; }
        public byte phy { get; set; }
        public double recovery { get; set; }
        //public int ID { get; set; }
        public DateTime join { get; set; }
        public int clbpop { get; set; } //club popularity, determine the price threshold for player available for sale
    }

}
