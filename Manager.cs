using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace cm
{

    [StructLayout(LayoutKind.Explicit)]
    internal struct MData
    {
        [FieldOffset(8)]
        public ushort cn;

        [FieldOffset(12)]
        public ushort club;

        [FieldOffset(22)]
        public byte year;

        [FieldOffset(24)]
        public byte rep;
    }


    // WindowsFormsApp1.Manager
    public class Manager
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

        public string club
        {
            get;
            set;
        }
        public int clubID { get; set; }

        public byte rep
        {
            get;
            set;
        }

        public byte yr
        {
            get;
            set;
        }
    }

}
