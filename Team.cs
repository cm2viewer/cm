using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace cm
{

    [StructLayout(LayoutKind.Explicit)]
    internal struct TeamData
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

        [FieldOffset(216)]
        public ushort shortlist;

        [FieldOffset(218)]
        public unsafe fixed ushort p1[32];

        [FieldOffset(297)]
        public unsafe fixed ushort p2[22];

        [FieldOffset(467)]
        public double season_ticket;

        [FieldOffset(475)]
        public double gate_receipt;

        [FieldOffset(483)]
        public double tv_prize;

        [FieldOffset(499)]
        public double player_sales;

        [FieldOffset(507)]
        public double other_income;

        [FieldOffset(515)]
        public double player_wages;

        [FieldOffset(523)]
        public double player_bonus;

        [FieldOffset(531)]
        public double player_purchase;

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
        public int id
        {
            get;
            set;
        }

        public string name
        {
            get;
            set;
        }

        public string cn
        {
            get;
            set;
        }
        public byte EEC { get; set; }
        public string division { get; set; }
        public string stadium { get; set; }
        public byte pop
        {
            get;
            set;
        }

        public byte flex
        {
            get;
            set;
        }

        public byte supp
        {
            get;
            set;
        }

        public ushort cap
        {
            get;
            set;
        }

        public ushort seat
        {
            get;
            set;
        }

        public int val
        {
            get;
            set;
        }

        public int player_sales { get; set; }

        public ushort shortlist
        {
            get;
            set;
        }

        public string p1
        {
            get;
            set;
        }

        public string p2
        {
            get;
            set;
        }
    }



}
