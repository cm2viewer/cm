using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace cm
{

    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct TeamData
    {
        [FieldOffset(28)]
        public ushort cn;
        [FieldOffset(34)]
        public byte x;
        [FieldOffset(35)]
        public byte y;
        [FieldOffset(36)]
        public byte EEC;
        [FieldOffset(37)]
        public ushort manager;
        [FieldOffset(40)]
        public byte unknown; //increase after end of month
        [FieldOffset(60)]
        public byte pop;
        [FieldOffset(61)]
        public byte flex;
        [FieldOffset(77)]
        public byte supp;
        [FieldOffset(69)]
        public ushort cap;
        [FieldOffset(73)]
        public ushort seat;
        [FieldOffset(87)]
        public double start_balance;
        [FieldOffset(103)]
        public byte division;
        [FieldOffset(104)]
        public byte prev_division;
        [FieldOffset(105)]
        public byte prev_division_position;

        [FieldOffset(218)]
        public unsafe fixed ushort squad[32];
        [FieldOffset(288)]
        public ushort numofplayers;
        [FieldOffset(290)]
        public byte reputation;     //club current reputation, determine 'big club release' contract, etc'
        [FieldOffset(377)]          //377-457
        public unsafe fixed byte shortlist[80];    //byte 1 & 2 (player id) byte3,4,5 unknown, assume max 16 shortlist

        [FieldOffset(467)]
        public double season_ticket;
        [FieldOffset(475)]
        public double gate_receipt;
        [FieldOffset(483)]
        public double tv_prize;
        [FieldOffset(499)]
        public double player_sales; //amount of money seems to effect big club release clause for player below 150 potential
        [FieldOffset(507)]
        public double other_income;
        [FieldOffset(515)]
        public double player_wages;
        [FieldOffset(523)]
        public double player_bonus;
        [FieldOffset(531)]
        public double player_purchase;
        [FieldOffset(535)]
        public double unknown7; 
        [FieldOffset(539)]
        public double other_cost;

        [FieldOffset(787)]
        public byte competition_year;
        [FieldOffset(789)]
        public byte competition_division;
        [FieldOffset(790)]
        public byte competition_pos;
        [FieldOffset(791)]
        public byte competition_play;
        [FieldOffset(792)]
        public byte competition_win;
    }


    public class Team
    {
        public int id { get; set; }
        public string name { get; set; }
        public string cn { get; set; }
        public string loc { get; set; }
        public byte EEC { get; set; }
        public string division { get; set; }
        public string stadium { get; set; }
        public byte pop { get; set; }
        public byte flex { get; set; }
        public byte supp { get; set; }
        public byte rep { get; set; }
        public ushort cap { get; set; }
        public ushort seat { get; set; }
        public int balance { get; set; }
        public int player_sales { get; set; }
        public string shortlist { get; set; }
        public string squad { get; set; }

    }
}
