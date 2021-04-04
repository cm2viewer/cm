// WindowsFormsApp1.Helper
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;


public static class Helper
{
    [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
    private static unsafe extern void CopyMemory(void* dest, void* src, int count);

    public static T ReadStruct2<T>(this BinaryReader reader, int numbytes) where T : struct
    {
        int size = numbytes;// Marshal.SizeOf(typeof(T));
        byte[] array = new byte[size];
        reader.Read(array, 0, array.Length);

        IntPtr ptr = Marshal.AllocHGlobal(size);

        Marshal.Copy(array, 0, ptr, size);

        var str = (T)Marshal.PtrToStructure(ptr, typeof(T));
        Marshal.FreeHGlobal(ptr);

        return str;
    }

    //GCHandle.Alloc will fail if the struct has non-blittable data, e.g. an array
    public static T ReadStruct<T>(this BinaryReader reader, int numbytes) where T : struct
    {
        byte[] array = new byte[numbytes];
        reader.Read(array, 0, array.Length);
        GCHandle gCHandle = default(GCHandle);
        try
        {
            gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
            return (T)Marshal.PtrToStructure(gCHandle.AddrOfPinnedObject(), typeof(T));
        }
        finally
        {
            if (gCHandle.IsAllocated)
            {
                gCHandle.Free();
            }
        }
    }

    public static T BytesToStruct<T>(this byte[] b) where T : struct
    {
        GCHandle gCHandle = default(GCHandle);
        try
        {
            gCHandle = GCHandle.Alloc(b, GCHandleType.Pinned);
            return (T)Marshal.PtrToStructure(gCHandle.AddrOfPinnedObject(), typeof(T));
        }
        finally
        {
            if (gCHandle.IsAllocated)
            {
                gCHandle.Free();
            }
        }
    }
    public static byte[] getBytes(this cm.Data d) 
    {
        int size = Marshal.SizeOf(d);
        byte[] arr = new byte[size];

        IntPtr ptr = Marshal.AllocHGlobal(size);
        Marshal.StructureToPtr(d, ptr, true);
        Marshal.Copy(ptr, arr, 0, size);
        Marshal.FreeHGlobal(ptr);
        return arr;
    }

    public static string ReadName(this BinaryReader reader, bool haslastname = true)
    {
        string name;
        int n = reader.ReadUInt16();
        name = Encoding.UTF7.GetString(reader.ReadBytes(n));
        if (haslastname)
        {
            n = reader.ReadUInt16();
            name += " " + Encoding.UTF7.GetString(reader.ReadBytes(n));
        }
        return name.Trim();
    }

    public static void WriteName(this BinaryWriter writer, string name)
    {
        string[] names = name.Split(' ');
        writer.Write((UInt16)names[0].Length);
        writer.Write(names[0]);
        if (names.Length >1)
        {
            writer.Write((UInt16)names[1].Length);
            //writer.Write(names[]);
        }
    }

    public static DialogResult InputBox(string title, string promptText, ref string value)
    {
        Form form = new Form();
        Label label = new Label();
        TextBox textBox = new TextBox();
        Button button = new Button();
        Button button2 = new Button();
        form.Text = title;
        label.Text = promptText;
        textBox.Text = value;
        button.Text = "OK";
        button2.Text = "Cancel";
        button.DialogResult = DialogResult.OK;
        button2.DialogResult = DialogResult.Cancel;
        label.SetBounds(9, 20, 372, 13);
        textBox.SetBounds(12, 36, 372, 20);
        button.SetBounds(228, 72, 75, 23);
        button2.SetBounds(309, 72, 75, 23);
        label.AutoSize = true;
        textBox.Anchor |= AnchorStyles.Right;
        button.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
        button2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
        form.ClientSize = new Size(396, 107);
        form.Controls.AddRange(new Control[4]
        {
            label,
            textBox,
            button,
            button2
        });
        form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
        form.FormBorderStyle = FormBorderStyle.FixedDialog;
        form.StartPosition = FormStartPosition.CenterScreen;
        form.MinimizeBox = false;
        form.MaximizeBox = false;
        form.AcceptButton = button;
        form.CancelButton = button2;
        DialogResult result = form.ShowDialog();
        value = textBox.Text;
        return result;
    }

    public static DataTable BuildDataTable<T>(IList<T> lst, string customcol = "", int defaultvalue = 0)
    {
        DataTable dataTable = CreateTable<T>(customcol,defaultvalue);
        Type typeFromHandle = typeof(T);
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeFromHandle);
        foreach (T item in lst)
        {
            DataRow dataRow = dataTable.NewRow();
            foreach (PropertyDescriptor item2 in properties)
            {
                dataRow[item2.Name] = item2.GetValue(item);
            }
            dataTable.Rows.Add(dataRow);
        }
        return dataTable;
    }

    private static DataTable CreateTable<T>(string customcol = "", int defaultvalue = 0)
    {
        Type typeFromHandle = typeof(T);
        DataTable dataTable = new DataTable(typeFromHandle.Name);
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeFromHandle);
        if (!customcol.Equals(""))
        {
            DataColumn dc = dataTable.Columns.Add(customcol, typeof(Int32));
            dc.DefaultValue = defaultvalue;
        }
        foreach (PropertyDescriptor item in properties)
        {
            dataTable.Columns.Add(item.Name, item.PropertyType);
        }
        return dataTable;
    }
}
