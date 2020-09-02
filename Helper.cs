// WindowsFormsApp1.Helper
using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

public static class Helper
{
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

   
}
