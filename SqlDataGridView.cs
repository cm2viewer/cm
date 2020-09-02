//source: https://www.codeproject.com/Articles/20990/DataGridView-with-integrated-filtering-sorting-and
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

public class SqlDataGridView : DataGridView
{
    private class SearchTextbox : ToolStripTextBox
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.WParam.ToInt32() == 13)
            {
                base.Visible = false;
                return true;
            }
            bool ProcessCmdKey = default(bool);
            return ProcessCmdKey;
        }
    }

    private const int WM_KEYDOWN = 256;
    private const int WM_KEYUP = 257;
    private const int WM_CHAR = 258;

    private int iColumn_Clicked = 0;
    private string Combobox_Columns = "";
    private string Defaultvalue_Columns = "";
    private string Default_Values = "";

    //private SqlConnection cn;
    //private string s_SQL = "";
    //private string sSQL = "";
    //private string sqlKeys = "";
    //private SqlCommand cmd;
    private BindingSource Binding_Source;
    //private string Connection_String = "";
    //private DataRowChangeEventArgs _e;
    private Rectangle drRegion;
    private int Header_Selected = -1;


    //menu
    private ContextMenuStrip conMenu;
    private ToolStripMenuItem contoolYes;
    private ToolStripMenuItem contoolNo;
    private ToolStripMenuItem contoolYesNo;
    private ToolStripMenuItem ContoolValue;
    private ToolStripMenuItem ContoolDate;
    private ToolStripMenuItem conTooltext;

    //filter menu
    private SearchTextbox SearchValueGreater;
    private SearchTextbox SearchValueSmaller;
    private SearchTextbox SearchValueEqual;
    private SearchTextbox SearchTextboxLike;
    private SearchTextbox SearchDateGreater;
    private SearchTextbox SearchDateSmaller;
    private SearchTextbox SearchDateEqual;
    private ToolStripMenuItem ToolFilternull;
    private ToolStripMenuItem ToolFilternotnull;
    private ToolStripMenuItem ToolFilterNone;

    //sort menu
    private ToolStripMenuItem SortAsc;
    private ToolStripMenuItem SortDesc;
    private ToolStripMenuItem SortNone;

    public static ContentAlignment anyRight = ContentAlignment.BottomRight | ContentAlignment.MiddleRight | ContentAlignment.TopRight; //(System.Drawing.ContentAlignment)1092;
    public static ContentAlignment anyTop = ContentAlignment.TopRight | ContentAlignment.TopCenter | ContentAlignment.TopLeft;// (System.Drawing.ContentAlignment)7;
    public static ContentAlignment anyBottom = ContentAlignment.BottomRight | ContentAlignment.BottomCenter | ContentAlignment.BottomLeft;// (System.Drawing.ContentAlignment)1792;
    public static ContentAlignment anyCenter = ContentAlignment.BottomCenter | ContentAlignment.MiddleCenter | ContentAlignment.TopCenter;// (System.Drawing.ContentAlignment)546;
    public static ContentAlignment anyMiddle = ContentAlignment.MiddleRight | ContentAlignment.MiddleCenter | ContentAlignment.MiddleLeft;// (System.Drawing.ContentAlignment)112;

    private Dictionary<int, string> Filter_Array = new Dictionary<int, string>();
    private Dictionary<int, string> Sort_Array = new Dictionary<int, string>();


    //ok
    public SqlDataGridView()
    {

        base.BackgroundColor = Color.White;
        base.ColumnHeadersHeight = 23;
        base.AllowUserToOrderColumns = true;
        base.EditMode = DataGridViewEditMode.EditOnEnter;
        base.MultiSelect = false;
        base.RowHeadersVisible = false;
        conMenu = new ContextMenuStrip();
        conMenu.AutoSize = true;
        SortAsc = new ToolStripMenuItem();
        SortAsc.AutoSize = true;
        SortAsc.Text = "Sort : ASC";
        conMenu.Items.Add(SortAsc);
        SortDesc = new ToolStripMenuItem();
        SortDesc.AutoSize = true;
        SortDesc.Text = "Sort : DESC";
        conMenu.Items.Add(SortDesc);
        SortNone = new ToolStripMenuItem();
        SortNone.AutoSize = true;
        SortNone.Text = "Sort : NONE";
        conMenu.Items.Add(SortNone);
        conTooltext = new ToolStripMenuItem();
        conTooltext.AutoSize = true;
        conTooltext.Text = "Filter on text containing";
        SearchTextboxLike = new SearchTextbox();
        SearchTextboxLike.AutoSize = true;
        SearchTextboxLike.BorderStyle = BorderStyle.FixedSingle;
        SearchTextboxLike.ForeColor = Color.FromArgb(21, 66, 139);
        conTooltext.DropDownItems.Add(SearchTextboxLike);
        conMenu.Items.Add(conTooltext);
        contoolYesNo = new ToolStripMenuItem();
        contoolYesNo.AutoSize = true;
        contoolYesNo.Text = "Filter on True/False : ";
        contoolYes = new ToolStripMenuItem();
        contoolYes.Text = "Yes";
        contoolYesNo.DropDownItems.Add(contoolYes);
        contoolNo = new ToolStripMenuItem();
        contoolNo.Text = "No";
        contoolYesNo.DropDownItems.Add(contoolNo);
        conMenu.Items.Add(contoolYesNo);
        ContoolValue = new ToolStripMenuItem();
        ContoolValue.AutoSize = true;
        ContoolValue.Text = "Filter on value";

        ToolStripMenuItem item = new ToolStripMenuItem
        {
            Text = ">"
        };
        SearchValueGreater = new SearchTextbox();
        SearchValueGreater.AutoSize = true;
        SearchValueGreater.BorderStyle = BorderStyle.FixedSingle;
        SearchValueGreater.ForeColor = Color.FromArgb(21, 66, 139);
        item.DropDownItems.Add(SearchValueGreater);
        ContoolValue.DropDownItems.Add(item);
        item = new ToolStripMenuItem
        {
            Text = "<"
        };
        SearchValueSmaller = new SearchTextbox();
        SearchValueSmaller.AutoSize = true;
        SearchValueSmaller.BorderStyle = BorderStyle.FixedSingle;
        SearchValueSmaller.ForeColor = Color.FromArgb(21, 66, 139);
        item.DropDownItems.Add(SearchValueSmaller);
        ContoolValue.DropDownItems.Add(item);
        item = new ToolStripMenuItem
        {
            Text = "="
        };
        SearchValueEqual = new SearchTextbox();
        SearchValueEqual.AutoSize = true;
        SearchValueEqual.BorderStyle = BorderStyle.FixedSingle;
        SearchValueEqual.ForeColor = Color.FromArgb(21, 66, 139);
        item.DropDownItems.Add(SearchValueEqual);
        ContoolValue.DropDownItems.Add(item);
        conMenu.Items.Add(ContoolValue);
        ContoolDate = new ToolStripMenuItem();
        ContoolDate.AutoSize = true;
        ContoolDate.Text = "Filter on date";
        item = new ToolStripMenuItem
        {
            Text = ">"
        };
        SearchDateGreater = new SearchTextbox();
        SearchDateGreater.AutoSize = true;
        SearchDateGreater.BorderStyle = BorderStyle.FixedSingle;
        SearchDateGreater.ForeColor = Color.FromArgb(21, 66, 139);
        item.DropDownItems.Add(SearchDateGreater);
        ContoolDate.DropDownItems.Add(item);
        item = new ToolStripMenuItem
        {
            Text = "<"
        };
        SearchDateSmaller = new SearchTextbox();
        SearchDateSmaller.AutoSize = true;
        SearchDateSmaller.BorderStyle = BorderStyle.FixedSingle;
        SearchDateSmaller.ForeColor = Color.FromArgb(21, 66, 139);
        item.DropDownItems.Add(SearchDateSmaller);
        ContoolDate.DropDownItems.Add(item);
        item = new ToolStripMenuItem
        {
            Text = "="
        };
        SearchDateEqual = new SearchTextbox();
        SearchDateEqual.AutoSize = true;
        SearchDateEqual.BorderStyle = BorderStyle.FixedSingle;
        SearchDateEqual.ForeColor = Color.FromArgb(21, 66, 139);
        item.DropDownItems.Add(SearchDateEqual);
        ContoolDate.DropDownItems.Add(item);
        conMenu.Items.Add(ContoolDate);
        ToolFilternull = new ToolStripMenuItem();
        ToolFilternull.AutoSize = true;
        ToolFilternull.Text = "Filter null values";
        conMenu.Items.Add(ToolFilternull);
        ToolFilternotnull = new ToolStripMenuItem();
        ToolFilternotnull.AutoSize = true;
        ToolFilternotnull.Text = "Filter non-null values";
        conMenu.Items.Add(ToolFilternotnull);
        ToolFilterNone = new ToolStripMenuItem();
        ToolFilterNone.AutoSize = true;
        ToolFilterNone.Text = "Reset";
        conMenu.Items.Add(ToolFilterNone);
        DoubleBuffered = true;
        AddEvents();

    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        try
        {
            if (msg.WParam.ToInt32() == 13)
            {
                SendKeys.Send("{TAB}");
                return true;
            }
        }
        catch
        {
        }
        return false;
    }

    //ok
    protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
    {
        try
        {
            if (e.Button == MouseButtons.Right)
            {
                base.Rows[e.RowIndex].Selected = true;
                iColumn_Clicked = e.ColumnIndex;
            }
            base.OnCellMouseDown(e);
        }
        catch
        {

        }
    }

    //ok
    protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
    {
        try
        {
            if (!Combobox_Columns.Equals(""))
            {
                string[] sTmp = Combobox_Columns.Split(';');
                if (Array.IndexOf(sTmp, base.Columns[e.ColumnIndex].DataPropertyName) > -1)
                {
                    base.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                }
            }
        }
        catch { }
    }

    //ok
    protected override void OnDefaultValuesNeeded(DataGridViewRowEventArgs e)
    {
        try
        {
            if (!Defaultvalue_Columns.Equals(""))
            {
                string[] sTmp = Defaultvalue_Columns.Split(';');
                string[] sValues = Default_Values.Split(';');

                for (int i = 0; i <= sTmp.Length - 1; i++)
                {
                    string text = base.Columns[sTmp[i]].ValueType.ToString();
                    switch (base.Columns[sTmp[i]].ValueType.ToString())
                    {
                        case "System.String":
                            e.Row.Cells[sTmp[i]].Value = sValues[i];
                            break;
                        case "System.Byte":
                        case "System.Int16":
                        case "System.UInt16":
                        case "System.Int32":
                        case "System.UInt32":
                            e.Row.Cells[sTmp[i]].Value = int.Parse(sValues[i]);
                            break;
                        case "System.DateTime":
                            e.Row.Cells[sTmp[i]].Value = DateTime.Parse(sValues[i]);
                            break;
                        case "System.Double":
                            e.Row.Cells[sTmp[i]].Value = Double.Parse(sValues[i]);
                            break;
                        case "System.Boolean":
                            e.Row.Cells[sTmp[i]].Value = Boolean.Parse(sValues[i]);
                            break;
                    }
                }
            }
        }
        catch { }
    }


    //ok
    private void Draw_Background(Graphics g, Image obj, Rectangle r, int index)
    {
        if (obj == null)
        {
            return;
        }
        int oWidth = obj.Width;
        Rectangle lr = Rectangle.FromLTRB(0, 0, 0, 0);
        int x = (index - 1) * oWidth;
        int y = 0;
        int x2 = r.Left;
        int y2 = r.Top;
        if (r.Height > obj.Height && r.Width <= oWidth)
        {
            Rectangle r2 = new Rectangle(x, y, oWidth, lr.Top);
            Rectangle r3 = new Rectangle(x2, y2, r.Width, lr.Top);
            g.DrawImage(obj, r3, r2, GraphicsUnit.Pixel);
            r2 = new Rectangle(x, y + lr.Top, oWidth, obj.Height - (lr.Top - lr.Bottom));
            r3 = new Rectangle(x2, y2 + lr.Top, r.Width, r.Height - (lr.Top - lr.Bottom));
            if (lr.Top + lr.Bottom == 0)
            {
                r2.Height--;
            }
            g.DrawImage(obj, r3, r2, GraphicsUnit.Pixel);
            r2 = new Rectangle(x, y + (obj.Height - lr.Bottom), oWidth, lr.Bottom);
            r3 = new Rectangle(x2, y2 + (r.Height - lr.Bottom), r.Width, lr.Bottom);
            g.DrawImage(obj, r3, r2, GraphicsUnit.Pixel);
        }
        else if (r.Height <= obj.Height && r.Width > oWidth)
        {
            Rectangle r2 = new Rectangle(x, y, lr.Left, obj.Height);
            Rectangle r3 = new Rectangle(x2, y2, lr.Left, r.Height);
            g.DrawImage(obj, r3, r2, GraphicsUnit.Pixel);
            r2 = new Rectangle(x + lr.Left, y, oWidth - (lr.Left - lr.Right), obj.Height);
            r3 = new Rectangle(x2 + lr.Left, y2, r.Width - (lr.Left - lr.Right), r.Height);
            g.DrawImage(obj, r3, r2, GraphicsUnit.Pixel);
            r2 = new Rectangle(x + (oWidth - lr.Right), y, lr.Right, obj.Height);
            r3 = new Rectangle(x2 + (r.Width - lr.Right), y2, lr.Right, r.Height);
            g.DrawImage(obj, r3, r2, GraphicsUnit.Pixel);
        }
        else if (r.Height <= obj.Height && r.Width <= oWidth)
        {
            g.DrawImage(srcRect: new Rectangle((index - 1) * oWidth, 0, oWidth, obj.Height - 1), image: obj, destRect: new Rectangle(x2, y2, r.Width, r.Height), srcUnit: GraphicsUnit.Pixel);
        }
        else if (r.Height > obj.Height && r.Width > oWidth)
        {
            Rectangle r2 = new Rectangle(x, y, lr.Left, lr.Top);
            Rectangle r3 = new Rectangle(x2, y2, lr.Left, lr.Top);
            g.DrawImage(obj, r3, r2, GraphicsUnit.Pixel);
            r2 = new Rectangle(x, y + (obj.Height - lr.Bottom), lr.Left, lr.Bottom);
            r3 = new Rectangle(x2, y2 + (r.Height - lr.Bottom), lr.Left, lr.Bottom);
            g.DrawImage(obj, r3, r2, GraphicsUnit.Pixel);
            r2 = new Rectangle(x, y + lr.Top, lr.Left, obj.Height - (lr.Top - lr.Bottom));
            r3 = new Rectangle(x2, y2 + lr.Top, lr.Left, r.Height - (lr.Top - lr.Bottom));
            g.DrawImage(obj, r3, r2, GraphicsUnit.Pixel);
            r2 = new Rectangle(x + lr.Left, y, oWidth - (lr.Left - lr.Right), lr.Top);
            r3 = new Rectangle(x2 + lr.Left, y2, r.Width - (lr.Left - lr.Right), lr.Top);
            g.DrawImage(obj, r3, r2, GraphicsUnit.Pixel);
            r2 = new Rectangle(x + (oWidth - lr.Right), y, lr.Right, lr.Top);
            r3 = new Rectangle(x2 + (r.Width - lr.Right), y2, lr.Right, lr.Top);
            g.DrawImage(obj, r3, r2, GraphicsUnit.Pixel);
            r2 = new Rectangle(x + (oWidth - lr.Right), y + lr.Top, lr.Right, obj.Height - (lr.Top - lr.Bottom));
            r3 = new Rectangle(x2 + (r.Width - lr.Right), y2 + lr.Top, lr.Right, r.Height - (lr.Top - lr.Bottom));
            g.DrawImage(obj, r3, r2, GraphicsUnit.Pixel);
            r2 = new Rectangle(x + (oWidth - lr.Right), y + (obj.Height - lr.Bottom), lr.Right, lr.Bottom);
            r3 = new Rectangle(x2 + (r.Width - lr.Right), y2 + (r.Height - lr.Bottom), lr.Right, lr.Bottom);
            g.DrawImage(obj, r3, r2, GraphicsUnit.Pixel);
            r2 = new Rectangle(x + lr.Left, y + (obj.Height - lr.Bottom), oWidth - (lr.Left - lr.Right), lr.Bottom);
            r3 = new Rectangle(x2 + lr.Left, y2 + (r.Height - lr.Bottom), r.Width - (lr.Left - lr.Right), lr.Bottom);
            g.DrawImage(obj, r3, r2, GraphicsUnit.Pixel);
            r2 = new Rectangle(x + lr.Left, y + lr.Top, oWidth - (lr.Left - lr.Right), obj.Height - (lr.Top - lr.Bottom));
            r3 = new Rectangle(x2 + lr.Left, y2 + lr.Top, r.Width - (lr.Left - lr.Right), r.Height - (lr.Top - lr.Bottom));
            g.DrawImage(obj, r3, r2, GraphicsUnit.Pixel);
        }
    }

    //ok
    private Rectangle HAlignWithin(Size alignThis, Rectangle withinThis, System.Drawing.ContentAlignment align)
    {
        if ((align & anyRight) != 0)
        {
            withinThis.X += withinThis.Width - alignThis.Width;
        }
        else if ((align & anyCenter) != 0)
        {
            withinThis.X = (int)Math.Round((double)withinThis.X + (double)(withinThis.Width - alignThis.Width + 1) / 2.0);
        }
        withinThis.Width = alignThis.Width;
        return withinThis;
    }

    //ok
    private Rectangle VAlignWithin(Size alignThis, Rectangle withinThis, System.Drawing.ContentAlignment align)
    {
        if ((align & anyBottom) != 0)
        {
            withinThis.Y += withinThis.Height - alignThis.Height;
        }
        else if ((align & anyMiddle) != 0)
        {
            withinThis.Y = (int)Math.Round((double)withinThis.Y + (double)(withinThis.Height - alignThis.Height + 1) / 2.0);
        }
        withinThis.Height = alignThis.Height;
        return withinThis;
    }

    //ok
    protected override void OnCellMouseEnter(DataGridViewCellEventArgs e)
    {
        if (e.RowIndex == -1)
        {
            Header_Selected = e.ColumnIndex;
        }
        base.OnCellMouseEnter(e);
    }

    //ok
    protected override void OnCellMouseLeave(DataGridViewCellEventArgs e)
    {
        if (e.RowIndex == -1)
        {
            Header_Selected = -1;
        }
        base.OnCellMouseLeave(e);
    }

    protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
    {
        try
        {
            if (e.RowIndex != -1)
            {
                return;
            }
            if (e.ColumnIndex == Header_Selected)
            {
                Image img = Base64ToImage(imgDGHeaderRollOver);
                Draw_Background(e.Graphics, img, e.CellBounds, 1);
                StringFormat format1 = new StringFormat();
                format1.HotkeyPrefix = HotkeyPrefix.Show;
                SizeF ef1 = e.Graphics.MeasureString(e.Value.ToString().ToUpper(), Font, new SizeF(e.CellBounds.Width, e.CellBounds.Height), format1);
                Size txts2 = Size.Empty;
                if (e.Graphics.MeasureString(e.Value.ToString().ToUpper(), Font).Width + 30f > (float)base.Columns[e.ColumnIndex].Width)
                {
                    base.Columns[e.ColumnIndex].Width = (int)Math.Round(e.Graphics.MeasureString(e.Value.ToString().ToUpper(), Font).Width + 30f);
                }
                txts2 = Size.Ceiling(ef1);
                e.CellBounds.Inflate(-4, -4);
                Rectangle normalRegion = new Rectangle(e.CellBounds.Location.X + 1, e.CellBounds.Location.Y + 1, e.CellBounds.Size.Width - 22, e.CellBounds.Size.Height - 2);
                normalRegion = HAlignWithin(txts2, normalRegion, System.Drawing.ContentAlignment.MiddleCenter);
                normalRegion = VAlignWithin(txts2, normalRegion, System.Drawing.ContentAlignment.MiddleCenter);
                format1 = new StringFormat();
                format1.HotkeyPrefix = HotkeyPrefix.Show;
                Brush brush3 = new SolidBrush(Color.FromArgb(21, 66, 139));
                e.Graphics.DrawString(e.Value.ToString().ToUpper(), Font, brush3, normalRegion, format1);
                brush3.Dispose();
                Rectangle reCombobox_Columnsrder3 = new Rectangle(e.CellBounds.X - 1, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height - 1);
                e.Graphics.DrawRectangle(Pens.LightSlateGray, reCombobox_Columnsrder3);
                drRegion = new Rectangle(e.CellBounds.Location.X + e.CellBounds.Size.Width - 23, e.CellBounds.Location.Y, 23, 23);
                ComboBoxRenderer.DrawDropDownButton(e.Graphics, drRegion, System.Windows.Forms.VisualStyles.ComboBoxState.Normal);
                e.Handled = true;
            }
            else if (Sort_Array != null)
            {
                //                    if (Array.IndexOf(Sort_Array, e.ColumnIndex) > -1)
                if (Sort_Array.ContainsKey(e.ColumnIndex))
                {
                    Image img3 = Base64ToImage(imgDGHeaderRollOver);
                    Draw_Background(e.Graphics, img3, e.CellBounds, 1);
                    StringFormat format4 = new StringFormat();
                    format4.HotkeyPrefix = HotkeyPrefix.Show;
                    SizeF ef3 = e.Graphics.MeasureString(e.Value.ToString().ToUpper(), Font, new SizeF(e.CellBounds.Width, e.CellBounds.Height), format4);
                    Size txts4 = Size.Empty;
                    if (e.Graphics.MeasureString(e.Value.ToString().ToUpper(), Font).Width + 15f > (float)base.Columns[e.ColumnIndex].Width)
                    {
                        base.Columns[e.ColumnIndex].Width = (int)Math.Round(e.Graphics.MeasureString(e.Value.ToString().ToUpper(), Font).Width + 15f);
                    }
                    txts4 = Size.Ceiling(ef3);
                    e.CellBounds.Inflate(-4, -4);
                    Rectangle normalRegion2 = new Rectangle(e.CellBounds.Location.X + 1, e.CellBounds.Location.Y + 1, e.CellBounds.Size.Width - 11, e.CellBounds.Size.Height - 2);
                    normalRegion2 = HAlignWithin(txts4, normalRegion2, System.Drawing.ContentAlignment.MiddleCenter);
                    normalRegion2 = VAlignWithin(txts4, normalRegion2, System.Drawing.ContentAlignment.MiddleCenter);
                    format4 = new StringFormat();
                    format4.HotkeyPrefix = HotkeyPrefix.Show;
                    Brush brush6 = new SolidBrush(Color.FromArgb(21, 66, 139));
                    e.Graphics.DrawString(e.Value.ToString().ToUpper(), Font, brush6, normalRegion2, format4);
                    brush6.Dispose();
                    Rectangle reCombobox_Columnsrder5 = new Rectangle(e.CellBounds.X - 1, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height - 1);
                    e.Graphics.DrawRectangle(Pens.LightSlateGray, reCombobox_Columnsrder5);
                    Rectangle sortRegion = new Rectangle(e.CellBounds.Location.X + e.CellBounds.Size.Width - 10, e.CellBounds.Location.Y, 9, 9);
                    string[] sSort = Binding_Source.Sort.Split(',');
                    int upperBound = sSort.GetUpperBound(0);
                    for (int i = 0; i <= upperBound; i++)
                    {
                        if (sSort[i].Contains(base.Columns[e.ColumnIndex].DataPropertyName))
                        {
                            img3 = ((!sSort[i].Contains("DESC")) ? Base64ToImage(imgArrowUp) : Base64ToImage(imgArrowDown));
                            break;
                        }
                    }
                    Draw_Background(e.Graphics, img3, sortRegion, 1);
                    e.Handled = true;
                }
                else
                {
                    Image img6 = Base64ToImage(imgDGHeaderNormal);
                    Draw_Background(e.Graphics, img6, e.CellBounds, 1);
                    StringFormat format6 = new StringFormat();
                    format6.HotkeyPrefix = HotkeyPrefix.Show;
                    SizeF ef6 = e.Graphics.MeasureString(e.Value.ToString().ToUpper(), Font, new SizeF(e.CellBounds.Width, e.CellBounds.Height), format6);
                    Size txts6 = Size.Empty;
                    if (e.Graphics.MeasureString(e.Value.ToString().ToUpper(), Font).Width + 30f > (float)base.Columns[e.ColumnIndex].Width)
                    {
                        base.Columns[e.ColumnIndex].Width = (int)Math.Round(e.Graphics.MeasureString(e.Value.ToString().ToUpper(), Font).Width + 30f);
                    }
                    txts6 = Size.Ceiling(ef6);
                    e.CellBounds.Inflate(-4, -4);
                    Rectangle txtr4 = e.CellBounds;
                    txtr4 = HAlignWithin(txts6, txtr4, System.Drawing.ContentAlignment.MiddleCenter);
                    txtr4 = VAlignWithin(txts6, txtr4, System.Drawing.ContentAlignment.MiddleCenter);
                    format6 = new StringFormat();
                    format6.HotkeyPrefix = HotkeyPrefix.Show;
                    Brush brush7 = new SolidBrush(Color.FromArgb(21, 66, 139));
                    e.Graphics.DrawString(e.Value.ToString().ToUpper(), Font, brush7, txtr4, format6);
                    brush7.Dispose();
                    Rectangle reCombobox_Columnsrder6 = new Rectangle(e.CellBounds.X - 1, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height - 1);
                    e.Graphics.DrawRectangle(Pens.LightSlateGray, reCombobox_Columnsrder6);
                    e.Handled = true;
                }
            }
            else if (Filter_Array != null)
            {
                if (Filter_Array.ContainsKey(e.ColumnIndex))
                //    if (Array.IndexOf(Filter_Array, e.ColumnIndex) > -1)
                {
                    Image img5 = Base64ToImage(imgDGHeaderRollOver);
                    Draw_Background(e.Graphics, img5, e.CellBounds, 1);
                    StringFormat format5 = new StringFormat();
                    format5.HotkeyPrefix = HotkeyPrefix.Show;
                    SizeF ef5 = e.Graphics.MeasureString(e.Value.ToString().ToUpper(), Font, new SizeF(e.CellBounds.Width, e.CellBounds.Height), format5);
                    Size txts5 = Size.Empty;
                    if (e.Graphics.MeasureString(e.Value.ToString().ToUpper(), Font).Width + 30f > (float)base.Columns[e.ColumnIndex].Width)
                    {
                        base.Columns[e.ColumnIndex].Width = (int)Math.Round(e.Graphics.MeasureString(e.Value.ToString().ToUpper(), Font).Width + 30f);
                    }
                    txts5 = Size.Ceiling(ef5);
                    e.CellBounds.Inflate(-4, -4);
                    Rectangle txtr3 = e.CellBounds;
                    txtr3 = HAlignWithin(txts5, txtr3, System.Drawing.ContentAlignment.MiddleCenter);
                    txtr3 = VAlignWithin(txts5, txtr3, System.Drawing.ContentAlignment.MiddleCenter);
                    format5 = new StringFormat();
                    format5.HotkeyPrefix = HotkeyPrefix.Show;
                    Brush brush5 = new SolidBrush(Color.FromArgb(21, 66, 139));
                    e.Graphics.DrawString(e.Value.ToString().ToUpper(), Font, brush5, txtr3, format5);
                    brush5.Dispose();
                    Rectangle reCombobox_Columnsrder4 = new Rectangle(e.CellBounds.X - 1, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height - 1);
                    e.Graphics.DrawRectangle(Pens.LightSlateGray, reCombobox_Columnsrder4);
                    e.Handled = true;
                }
                else
                {
                    Image img4 = Base64ToImage(imgDGHeaderNormal);
                    Draw_Background(e.Graphics, img4, e.CellBounds, 1);
                    StringFormat format3 = new StringFormat();
                    format3.HotkeyPrefix = HotkeyPrefix.Show;
                    SizeF ef4 = e.Graphics.MeasureString(e.Value.ToString().ToUpper(), Font, new SizeF(e.CellBounds.Width, e.CellBounds.Height), format3);
                    Size txts3 = Size.Empty;
                    if (e.Graphics.MeasureString(e.Value.ToString().ToUpper(), Font).Width + 30f > (float)base.Columns[e.ColumnIndex].Width)
                    {
                        base.Columns[e.ColumnIndex].Width = (int)Math.Round(e.Graphics.MeasureString(e.Value.ToString().ToUpper(), Font).Width + 30f);
                    }
                    txts3 = Size.Ceiling(ef4);
                    e.CellBounds.Inflate(-4, -4);
                    Rectangle txtr2 = e.CellBounds;
                    txtr2 = HAlignWithin(txts3, txtr2, System.Drawing.ContentAlignment.MiddleCenter);
                    txtr2 = VAlignWithin(txts3, txtr2, System.Drawing.ContentAlignment.MiddleCenter);
                    format3 = new StringFormat();
                    format3.HotkeyPrefix = HotkeyPrefix.Show;
                    Brush brush4 = new SolidBrush(Color.FromArgb(21, 66, 139));
                    e.Graphics.DrawString(e.Value.ToString().ToUpper(), Font, brush4, txtr2, format3);
                    brush4.Dispose();
                    Rectangle reCombobox_Columnsrder2 = new Rectangle(e.CellBounds.X - 1, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height - 1);
                    e.Graphics.DrawRectangle(Pens.LightSlateGray, reCombobox_Columnsrder2);
                    e.Handled = true;
                }
            }
            else
            {
                Image img2 = Base64ToImage(imgDGHeaderNormal);
                Draw_Background(e.Graphics, img2, e.CellBounds, 1);
                StringFormat format2 = new StringFormat();
                format2.HotkeyPrefix = HotkeyPrefix.Show;
                SizeF ef2 = e.Graphics.MeasureString(e.Value.ToString().ToUpper(), Font, new SizeF(e.CellBounds.Width, e.CellBounds.Height), format2);
                Size txts = Size.Empty;
                if (e.Graphics.MeasureString(e.Value.ToString().ToUpper(), Font).Width + 30f > (float)base.Columns[e.ColumnIndex].Width)
                {
                    base.Columns[e.ColumnIndex].Width = (int)Math.Round(e.Graphics.MeasureString(e.Value.ToString().ToUpper(), Font).Width + 30f);
                }
                txts = Size.Ceiling(ef2);
                e.CellBounds.Inflate(-4, -4);
                Rectangle txtr = e.CellBounds;
                txtr = HAlignWithin(txts, txtr, System.Drawing.ContentAlignment.MiddleCenter);
                txtr = VAlignWithin(txts, txtr, System.Drawing.ContentAlignment.MiddleCenter);
                format2 = new StringFormat();
                format2.HotkeyPrefix = HotkeyPrefix.Show;
                Brush brush2 = new SolidBrush(Color.FromArgb(21, 66, 139));
                e.Graphics.DrawString(e.Value.ToString().ToUpper(), Font, brush2, txtr, format2);
                brush2.Dispose();
                Rectangle reCombobox_Columnsrder = new Rectangle(e.CellBounds.X - 1, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height - 1);
                e.Graphics.DrawRectangle(Pens.LightSlateGray, reCombobox_Columnsrder);
                e.Handled = true;
            }
        }
        catch { }
    }

    //ok
    protected override void OnColumnHeaderMouseClick(DataGridViewCellMouseEventArgs e)
    {
        Rectangle rec = new Rectangle(new Point(GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Width - 23, 0), drRegion.Size);
        if (rec.Contains(e.Location))
        {
            iColumn_Clicked = e.ColumnIndex;
            conMenu.Show(this, new Point(GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).X + e.Location.X, GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Y));
            Header_Selected = -1;
        }
    }

    //ok
    public void Bind_to_Bindingsource(BindingSource bs)
    {
        try
        {
            Binding_Source = bs;
            base.DataSource = Binding_Source;
            base.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            foreach (DataGridViewColumn dc in this.Columns)
            {
                switch (GetValueType(dc.ValueType))
                {
                    case "Int":
                        DataGridViewCellStyle cs = new DataGridViewCellStyle();
                        cs.Alignment = DataGridViewContentAlignment.MiddleRight;
                        if (!dc.HeaderText.Equals("ID")) cs.Format = "N0";
                        dc.DefaultCellStyle = cs;
                        break;
                    case "Double":
                        DataGridViewCellStyle cs2 = new DataGridViewCellStyle();
                        cs2.Alignment = DataGridViewContentAlignment.MiddleRight;
                        cs2.Format = "N2";
                        dc.DefaultCellStyle = cs2;
                        break;
                    case "DateTime":
                        DataGridViewCellStyle cs3 = new DataGridViewCellStyle();
                        cs3.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dc.DefaultCellStyle = cs3;
                        break;
                }
                dc.Name = dc.DataPropertyName;
            }
        }
        catch
        {
            Binding_Source = null;
            base.DataSource = null;
        }
    }

    public static string GetValueType(Type type)
    {
        switch (type.ToString())
        {
            case "System.Boolean":
                return "Boolean";

            case "System.Double":
            case "System.Decimal":
                return "Double";

            case "System.Byte":
            case "System.Int16":
            case "System.UInt16":
            case "System.Int32":
            case "System.UInt32":
                return "Int";

            case "System.DateTime":
                return "DateTime";

            case "System.String":
                return "String";

        }
        return "";
    }
    //ok
    private void conMenu_Opening(object sender, CancelEventArgs e)
    {
        switch (GetValueType(base.Columns[iColumn_Clicked].ValueType))
        {
            case "Boolean":
                conTooltext.Visible = false;
                ContoolValue.Visible = false;
                contoolYesNo.Visible = true;
                ContoolDate.Visible = false;
                break;
            case "Double":
            case "Int":
                SearchValueGreater.Visible = true;
                SearchValueSmaller.Visible = true;
                conTooltext.Visible = false;
                ContoolValue.Visible = true;
                contoolYesNo.Visible = false;
                ContoolDate.Visible = false;
                break;
            case "DateTime":
                SearchDateGreater.Visible = true;
                SearchDateSmaller.Visible = true;
                conTooltext.Visible = false;
                ContoolValue.Visible = false;
                contoolYesNo.Visible = false;
                ContoolDate.Visible = true;
                break;
            default:
                SearchTextboxLike.Visible = true;
                conTooltext.Visible = true;
                ContoolValue.Visible = false;
                contoolYesNo.Visible = false;
                ContoolDate.Visible = false;
                break;
        }
    }

    //ok
    private void Apply_Filter(string controlestring, string waardestring, bool IsString, bool blLike)
    {
        string sFilter = "";
        if (IsString)
        {
            if (blLike)
                sFilter = controlestring + "'%" + waardestring + "%'";
            else
                sFilter = controlestring + "'" + waardestring + "'";
        }
        else
            sFilter = controlestring + waardestring;
        if (Filter_Array.ContainsKey(iColumn_Clicked))
            Filter_Array[iColumn_Clicked] = sFilter;
        else
            Filter_Array.Add(iColumn_Clicked, sFilter);

        Binding_Source.Filter = String.Join(" AND ", Filter_Array.Select(d => d.Value).ToArray());
        //MessageBox.Show(Binding_Source.Filter);
    } 
    
    private void Sort_Click(object sender, EventArgs e)
    {
        string sortby = (string)sender;
        try
        {
            switch (sortby)
            {
                case "ASC":
                case "DESC":
                    sortby = sortby.Equals("ASC") ? "" : " DESC";
                    if (!Sort_Array.ContainsKey(iColumn_Clicked))
                        Sort_Array.Add(iColumn_Clicked, "[" + base.Columns[iColumn_Clicked].DataPropertyName + "]" + sortby);
                    else
                        Sort_Array[iColumn_Clicked] = "[" + base.Columns[iColumn_Clicked].DataPropertyName + "]" + sortby;
                    break;
                case "NONE":
                    Sort_Array.Remove(iColumn_Clicked);
                    break;
            }

            Binding_Source.Sort = String.Join(", ", Sort_Array.Select(d => d.Value).ToArray());

        }
        catch { }

    }
    private void AddEvents()
    {
        conMenu.Opening += conMenu_Opening;

        contoolYes.Click += Filter_Changed;
        contoolNo.Click += Filter_Changed;
        SearchTextboxLike.VisibleChanged += Filter_Changed;
        SearchValueSmaller.VisibleChanged += Filter_Changed;// SearchValueSmaller_VisibleChanged;
        SearchValueGreater.VisibleChanged += Filter_Changed;//SearchValueGreater_VisibleChanged;
        SearchValueEqual.VisibleChanged += Filter_Changed;//SearchValueEqual_VisibleChanged;
        SearchDateGreater.VisibleChanged += Filter_Changed;//SearchDateGreater_VisibleChanged;
        SearchDateSmaller.VisibleChanged += Filter_Changed;// SearchDateSmaller_VisibleChanged;
        SearchDateEqual.VisibleChanged += Filter_Changed;//SearchDateEqual_VisibleChanged;

        ToolFilternull.Click += Filter_Changed;// ToolFilternull_Click;
        ToolFilternotnull.Click += Filter_Changed;//ToolFilternotnull_Click;
        ToolFilterNone.Click += Filter_Changed;//ToolFilterNone_Click;

        SortAsc.Click += (object sender, EventArgs e) => Sort_Click("ASC",e);
        SortNone.Click += (object sender, EventArgs e) => Sort_Click("NONE", e);
        SortDesc.Click += (object sender, EventArgs e) => Sort_Click("DESC", e);
    }
    private void Filter_Changed(object sender, EventArgs e)
    {
        string sendertxt = "";
        if (sender.GetType() == typeof(ToolStripMenuItem))
            sendertxt = ((ToolStripMenuItem)sender).Text;
        else if (sender.GetType() == typeof(SearchTextbox))
        {
            sendertxt = ((SearchTextbox)sender).OwnerItem.Text;
            if (((SearchTextbox)sender).OwnerItem.OwnerItem != null)
            {
                if (((SearchTextbox)sender).OwnerItem.OwnerItem.Text.Equals("Filter on date"))
                    sendertxt += "d";
            }
        }
        //MessageBox.Show(sendertxt);
        switch (sendertxt)
        {
            case "Yes":
                Apply_Filter("[" + base.Columns[iColumn_Clicked].DataPropertyName + "]=", "True", false, false);
                break;
            case "No":
                Apply_Filter("[" + base.Columns[iColumn_Clicked].DataPropertyName + "]=", "False", false, false);
                break;
            case ">d":
                if (!SearchDateGreater.Visible && !SearchDateGreater.Text.Equals(""))
                {
                    if (DateTime.TryParse(SearchDateGreater.Text, out DateTime dteZoek))
                    {
                        Apply_Filter("[" + base.Columns[iColumn_Clicked].DataPropertyName + "] > ", $"#{dteZoek:M/dd/yyyy}#", IsString: false, blLike: false);
                    }
                    conMenu.Close();
                    SearchDateGreater.Text = "";
                }
                break;
            case "<d":
                if (!SearchDateSmaller.Visible && !SearchDateGreater.Text.Equals(""))
                {
                    if (DateTime.TryParse(SearchDateSmaller.Text, out DateTime dteZoek))
                    {
                        Apply_Filter("[" + base.Columns[iColumn_Clicked].DataPropertyName + "] < ", $"#{dteZoek:M/dd/yyyy}#", IsString: false, blLike: false);
                    }
                    conMenu.Close();
                    SearchDateSmaller.Text = "";
                }
                break;
            case "=d":
                if (!SearchDateEqual.Visible && !SearchDateEqual.Equals(""))
                {
                    Apply_Filter("[" + base.Columns[iColumn_Clicked].DataPropertyName + "] = ", SearchDateEqual.Text, IsString: false, blLike: false);
                    conMenu.Close();
                    SearchDateEqual.Text = "";
                }
                break;
            case "<":
                if (!SearchValueSmaller.Visible && !SearchValueSmaller.Text.Equals(""))
                {
                    Apply_Filter("[" + base.Columns[iColumn_Clicked].DataPropertyName + "] < ", SearchValueSmaller.Text.Replace(",", "."), IsString: false, blLike: false);
                    conMenu.Close();
                    SearchValueSmaller.Text = "";
                }
                break;
            case ">":
                if (!SearchValueGreater.Visible && !SearchValueGreater.Text.Equals(""))
                {
                    Apply_Filter("[" + base.Columns[iColumn_Clicked].DataPropertyName + "] > ", SearchValueGreater.Text.Replace(",", "."), IsString: false, blLike: false);
                    conMenu.Close();
                    SearchValueGreater.Text = "";
                }
                break;
            case "=":
                if (!SearchValueEqual.Visible && !SearchValueEqual.Equals(""))
                {
                    Apply_Filter("[" + base.Columns[iColumn_Clicked].DataPropertyName + "] = ", SearchValueEqual.Text.Replace(",", "."), IsString: false, blLike: false);
                    conMenu.Close();
                    SearchValueEqual.Text = "";
                }
                break;
            case "Filter on text containing":
                if (!SearchTextboxLike.Visible && !SearchTextboxLike.Text.Equals(""))
                {
                    Apply_Filter("[" + base.Columns[iColumn_Clicked].DataPropertyName + "] LIKE ", SearchTextboxLike.Text, true, true);
                    conMenu.Close();
                    SearchTextboxLike.Text = "";
                }
                break;
            case "Filter null values":
                Apply_Filter("[" + base.Columns[iColumn_Clicked].DataPropertyName + "] ", "IS NULL", false, false);
                break;
            case "Filter non-null values":
                Apply_Filter("[" + base.Columns[iColumn_Clicked].DataPropertyName + "] ", "IS NOT NULL", false, false);
                break;
            case "Reset":
                Binding_Source.Filter = "";
                Binding_Source.Sort = "";
                Sort_Array.Clear();
                Filter_Array.Clear();
                Header_Selected = -1;
                break;
        }
    }

    /*
//ok
public void SaveDatatoDatabase(string cnString, DataRowChangeEventArgs e, string naamTabel)
{
try
{
    s_SQL = "";
    sSQL = "";
    Connection_String = cnString;
    _e = e;
    int i = 0;
    cn = new SqlConnection(Connection_String);
    cn.Open();
    DataColumn[] dca = null;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    cmd = new SqlCommand("SELECT TOP 1 PERCENT * FROM [" + naamTabel + "]", cn);
    da.SelectCommand = cmd;
    da.Fill(ds);
    da.FillSchema(dt, SchemaType.Source);
    string[] dgColumns = null;
    for (int x = 0; x <= base.Columns.Count - 1; x++)
    {
        Array.Resize(ref dgColumns, x + 1);
        dgColumns[x] = base.Columns[x].DataPropertyName.ToLower();
    }


    dca = dt.PrimaryKey;
    if (e.Row.RowState == DataRowState.Added)
    {
        foreach (DataColumn dc in dt.Columns)
        {
            if (Array.IndexOf(dgColumns, dc.ColumnName.ToLower()) > -1)
            {
                if (e.Row[dc.ColumnName] != DBNull.Value)
                {
                    if (dc.AutoIncrement == false)
                    {
                        if (s_SQL.Equals(""))
                            s_SQL = "INSERT INTO [" + naamTabel + "] ([" + dc.ColumnName + "]";
                        else
                            s_SQL = s_SQL + ", [" + dc.ColumnName + "]";
                        switch (dc.DataType.ToString())
                        {
                            case "System.String":
                                if (s_SQL.Equals(""))
                                    sSQL = " SELECT '" + e.Row[dc.ColumnName] + "' as expr" + i;
                                else
                                    sSQL = sSQL + ", '" + e.Row[dc.ColumnName] + "' as expr" + i;
                                break;
                            case "System.Boolean":
                                if (s_SQL.Equals(""))
                                    sSQL = " SELECT " + ((bool)e.Row[dc.ColumnName] ? 1 : 0) + " as expr" + i;
                                else
                                    sSQL = sSQL + ", " + ((bool)e.Row[dc.ColumnName] ? 1 : 0) + " as expr" + i;

                                break;
                            case "System.DateTime":
                                DateTime dteTmp = DateTime.Parse(e.Row[dc.ColumnName].ToString());
                                if (s_SQL.Equals(""))
                                    sSQL = " SELECT CONVERT(Datetime, '" + dteTmp.Year + "/" + dteTmp.Month + "/" + dteTmp.Day + "', 101) as expr" + i;
                                else
                                    sSQL = sSQL + ", CONVERT(Datetime, '" + dteTmp.Year + "/" + dteTmp.Month + "/" + dteTmp.Day + "', 101) as expr" + i;
                                break;
                            case "System.Double":
                                if (s_SQL.Equals(""))
                                    sSQL = " SELECT " + e.Row[dc.ColumnName].ToString().Replace(",", ".") + " as expr" + i;
                                else
                                    sSQL = sSQL + ", " + e.Row[dc.ColumnName].ToString().Replace(",", ".") + " as expr" + i;
                                break;
                            case "System.Int16":
                            case "System.Byte":
                            case "System.UInt16":
                                if (s_SQL.Equals(""))
                                    sSQL = " SELECT " + e.Row[dc.ColumnName] + " as expr" + i;
                                else
                                    sSQL = sSQL + ", " + e.Row[dc.ColumnName] + " as expr" + i;
                                break;
                            case "System.Int32":
                            case "System.UInt32":
                                if (s_SQL.Equals(""))
                                    sSQL = " SELECT " + e.Row[dc.ColumnName] + " as expr" + i;
                                else
                                    sSQL = sSQL + ", " + e.Row[dc.ColumnName] + " as expr" + i;
                                break;
                        }
                        i++;
                    }
                }
            }
        }

        da.Dispose();
        if (!s_SQL.Equals(""))
        {
            s_SQL = s_SQL + ") " + sSQL;
            cmd = new SqlCommand(s_SQL, cn);
            cmd.ExecuteNonQuery();
            e.Row.AcceptChanges();
        }
    }
    else if (e.Row.RowState == DataRowState.Modified)
    {
        foreach (DataColumn dc in dt.Columns)
        {
            if (Array.IndexOf(dgColumns, dc.ColumnName.ToLower()) > -1)
            {
                //If Not IsDBNull(e.Row.Item(dc.ColumnName, DataRowVersion.Current)) Then
                if (e.Row[dc.ColumnName, DataRowVersion.Current] != DBNull.Value)
                {
                    switch (dc.DataType.ToString())
                    {
                        case "System.String":
                            if (s_SQL.Equals(""))
                                sSQL = "UPDATE [" + naamTabel + "] SET [" + dc.ColumnName + "]='" + e.Row[dc.ColumnName, DataRowVersion.Current] + "'";
                            else
                                sSQL = sSQL + ", [" + dc.ColumnName + "]='" + e.Row[dc.ColumnName, DataRowVersion.Current] + "'";
                            if (Array.IndexOf(dca, dc) > -1)
                            {
                                if (s_SQL.Equals(""))
                                    s_SQL = " WHERE ([" + dc.ColumnName + "]='" + e.Row[dc.ColumnName, DataRowVersion.Original] + "') ";
                                else
                                    s_SQL = s_SQL + "AND ([" + dc.ColumnName + "]='" + e.Row[dc.ColumnName, DataRowVersion.Original] + "') ";
                            }
                            break;
                        case "System.Boolean":
                            if (s_SQL.Equals(""))
                                sSQL = "UPDATE [" + naamTabel + "] SET [" + dc.ColumnName + "]=" + ((bool)e.Row[dc.ColumnName, DataRowVersion.Current] == true ? 1 : 0);
                            else
                                sSQL = sSQL + ", [" + dc.ColumnName + "]=" + ((bool)e.Row[dc.ColumnName, DataRowVersion.Current] == true ? 1 : 0);
                            break;
                        case "System.DateTime":
                            DateTime dteTmp = DateTime.Parse(e.Row[dc.ColumnName].ToString());
                            String sdteTmp = "CONVERT(Datetime, '" + dteTmp.ToString("yyyy/MM/dd") + "', 101)";
                            if (s_SQL.Equals(""))
                                sSQL = "UPDATE [" + naamTabel + "] SET [" + dc.ColumnName + "]= " + sdteTmp;
                            else
                                sSQL = sSQL + ", [" + dc.ColumnName + "]= " + sdteTmp;
                            if (Array.IndexOf(dca, dc) > -1)
                            {
                                if (s_SQL.Equals(""))
                                    s_SQL = " WHERE ([" + dc.ColumnName + "]=" + sdteTmp;
                                else
                                    s_SQL = s_SQL + "AND ([" + dc.ColumnName + "]=" + sdteTmp;
                            }
                            break;
                        case "System.Double":
                            if (s_SQL.Equals(""))
                                sSQL = "UPDATE [" + naamTabel + "] SET [" + dc.ColumnName + "]=" + e.Row[dc.ColumnName, DataRowVersion.Current].ToString().Replace(",", ".");
                            else
                                sSQL = sSQL + ", [" + dc.ColumnName + "]=" + e.Row[dc.ColumnName, DataRowVersion.Current].ToString().Replace(",", ".");
                            break;
                        case "System.Int16":
                        case "System.Byte":
                        case "System.UInt16":
                        case "System.Int32":
                        case "System.UInt32":
                            if (s_SQL.Equals(""))
                                sSQL = "UPDATE [" + naamTabel + "] SET [" + dc.ColumnName + "]=" + e.Row[dc.ColumnName, DataRowVersion.Current];
                            else
                                sSQL = sSQL + ", [" + dc.ColumnName + "]=" + e.Row[dc.ColumnName, DataRowVersion.Current];
                            if (Array.IndexOf(dca, dc) > -1)
                            {
                                if (s_SQL.Equals(""))
                                    s_SQL = " WHERE ([" + dc.ColumnName + "]=" + e.Row[dc.ColumnName, DataRowVersion.Original] + ") ";
                                else
                                    s_SQL = s_SQL + "AND ([" + dc.ColumnName + "]=" + e.Row[dc.ColumnName, DataRowVersion.Original] + ") ";
                            }
                            break;
                    }
                }
            }
        }

        da.Dispose();
        if (!s_SQL.Equals(""))
        {
            s_SQL = sSQL + s_SQL;
            cmd = new SqlCommand(s_SQL, cn);
            cmd.ExecuteNonQuery();
            e.Row.AcceptChanges();
        }
    }
    cn.Close();
}
catch { }
}


    /*
    //ok
    private void ToolFilternull_Click(object sender, EventArgs e)
    {
        try
        {
            Apply_Filter("[" + base.Columns[iColumn_Clicked].DataPropertyName + "] ", "IS NULL", IsString: false, blLike: false);
        }
        catch { }
    }
    //ok
    private void SearchValueSmaller_VisibleChanged(object sender, EventArgs e)
    {

        try
        {
            if (!SearchValueSmaller.Visible && !SearchValueSmaller.Text.Equals(""))
            {
                Apply_Filter("[" + base.Columns[iColumn_Clicked].DataPropertyName + "] < ", SearchValueSmaller.Text.Replace(",", "."), IsString: false, blLike: false);
                conMenu.Close();
                SearchValueSmaller.Text = "";
            }
        }
        catch { }
    }

    //ok
    private void SearchValueGreater_VisibleChanged(object sender, EventArgs e)
    {
        try
        {
            MessageBox.Show(">>>");
            if (!SearchValueGreater.Visible && !SearchValueGreater.Text.Equals(""))
            {
                Apply_Filter("[" + base.Columns[iColumn_Clicked].DataPropertyName + "] > ", SearchValueGreater.Text.Replace(",", "."), IsString: false, blLike: false);
                conMenu.Close();
                SearchValueGreater.Text = "";
            }
        }
        catch { }
    }
    private void SearchTextbox1_VisibleChanged(object sender, EventArgs e)
    {
        try
        {
            if (!SearchTextboxLike.Visible && !SearchTextboxLike.Text.Equals(""))
            {
                Apply_Filter("[" + base.Columns[iColumn_Clicked].DataPropertyName + "] LIKE ", SearchTextboxLike.Text, true, true);
                conMenu.Close();
                SearchTextboxLike.Text = "";
            }
        }
        catch { }
    }
    //ok
    private void SearchDateGreater_VisibleChanged(object sender, EventArgs e)
    {

        try
        {
            if (!SearchDateGreater.Visible && !SearchDateGreater.Text.Equals(""))
            {
                if (DateTime.TryParse(SearchDateGreater.Text, out DateTime dteZoek))
                {
                    Apply_Filter("[" + base.Columns[iColumn_Clicked].DataPropertyName + "] > ", $"#{dteZoek:M/dd/yyyy}#", IsString: false, blLike: false);
                }
                conMenu.Close();
                SearchDateGreater.Text = "";
            }
        }
        catch { }
    }

    //ok
    private void SearchDateSmaller_VisibleChanged(object sender, EventArgs e)
    {

        try
        {
            if (!SearchDateSmaller.Visible && !SearchDateGreater.Text.Equals(""))
            {
                if (DateTime.TryParse(SearchDateSmaller.Text, out DateTime dteZoek))
                {
                    Apply_Filter("[" + base.Columns[iColumn_Clicked].DataPropertyName + "] < ", $"#{dteZoek:M/dd/yyyy}#", IsString: false, blLike: false);
                }
                conMenu.Close();
                SearchDateSmaller.Text = "";
            }
        }
        catch { }
    }

    //ok
    private void ToolFilternotnull_Click(object sender, EventArgs e)
    {
        try
        {
            Apply_Filter("[" + base.Columns[iColumn_Clicked].DataPropertyName + "] ", "IS NOT NULL", IsString: false, blLike: false);
        }
        catch { }
    }








    private void SearchValueEqual_VisibleChanged(object sender, EventArgs e)
    {
        try
        {
            if (!SearchValueEqual.Visible && !SearchValueEqual.Equals(""))
            {
                Apply_Filter("[" + base.Columns[iColumn_Clicked].DataPropertyName + "] = ", SearchValueEqual.Text.Replace(",", "."), IsString: false, blLike: false);
                conMenu.Close();
                SearchValueEqual.Text = "";
            }
        }
        catch { }
    }



    private void SearchDateEqual_VisibleChanged(object sender, EventArgs e)
    {
        try
        {
            if (!SearchDateEqual.Visible && !SearchDateEqual.Equals(""))
            {
                Apply_Filter("[" + base.Columns[iColumn_Clicked].DataPropertyName + "] = ", SearchDateEqual.Text, IsString: false, blLike: false);
                conMenu.Close();
                SearchDateEqual.Text = "";
            }
        }
        catch { }
    }

    private void ToolFilterNone_Click(object sender, EventArgs e)
    {
        Binding_Source.Filter = "";
        Sort_Array.Clear();
        Filter_Array.Clear();
        Header_Selected = -1;
    }

*/
    private const string imgDGHeaderRollOver = "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";
    private const string imgDGHeaderNormal = "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";
    private const string imgDGHeaderSorted = "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";
    private const string imgArrowDown = "iVBORw0KGgoAAAANSUhEUgAAAAkAAAAJCAIAAABv85FHAAAABGdBTUEAALGOfPtRkwAAACBjSFJNAACHDwAAjA8AAP1SAACBQAAAfXkAAOmLAAA85QAAGcxzPIV3AAAKOWlDQ1BQaG90b3Nob3AgSUNDIHByb2ZpbGUAAEjHnZZ3VFTXFofPvXd6oc0wAlKG3rvAANJ7k15FYZgZYCgDDjM0sSGiAhFFRJoiSFDEgNFQJFZEsRAUVLAHJAgoMRhFVCxvRtaLrqy89/Ly++Osb+2z97n77L3PWhcAkqcvl5cGSwGQyhPwgzyc6RGRUXTsAIABHmCAKQBMVka6X7B7CBDJy82FniFyAl8EAfB6WLwCcNPQM4BOB/+fpFnpfIHomAARm7M5GSwRF4g4JUuQLrbPipgalyxmGCVmvihBEcuJOWGRDT77LLKjmNmpPLaIxTmns1PZYu4V8bZMIUfEiK+ICzO5nCwR3xKxRoowlSviN+LYVA4zAwAUSWwXcFiJIjYRMYkfEuQi4uUA4EgJX3HcVyzgZAvEl3JJS8/hcxMSBXQdli7d1NqaQffkZKVwBALDACYrmcln013SUtOZvBwAFu/8WTLi2tJFRbY0tba0NDQzMv2qUP91829K3NtFehn4uWcQrf+L7a/80hoAYMyJarPziy2uCoDOLQDI3fti0zgAgKSobx3Xv7oPTTwviQJBuo2xcVZWlhGXwzISF/QP/U+Hv6GvvmckPu6P8tBdOfFMYYqALq4bKy0lTcinZ6QzWRy64Z+H+B8H/nUeBkGceA6fwxNFhImmjMtLELWbx+YKuGk8Opf3n5r4D8P+pMW5FonS+BFQY4yA1HUqQH7tBygKESDR+8Vd/6NvvvgwIH554SqTi3P/7zf9Z8Gl4iWDm/A5ziUohM4S8jMX98TPEqABAUgCKpAHykAd6ABDYAasgC1wBG7AG/iDEBAJVgMWSASpgA+yQB7YBApBMdgJ9oBqUAcaQTNoBcdBJzgFzoNL4Bq4AW6D+2AUTIBnYBa8BgsQBGEhMkSB5CEVSBPSh8wgBmQPuUG+UBAUCcVCCRAPEkJ50GaoGCqDqqF6qBn6HjoJnYeuQIPQXWgMmoZ+h97BCEyCqbASrAUbwwzYCfaBQ+BVcAK8Bs6FC+AdcCXcAB+FO+Dz8DX4NjwKP4PnEIAQERqiihgiDMQF8UeikHiEj6xHipAKpAFpRbqRPuQmMorMIG9RGBQFRUcZomxRnqhQFAu1BrUeVYKqRh1GdaB6UTdRY6hZ1Ec0Ga2I1kfboL3QEegEdBa6EF2BbkK3oy+ib6Mn0K8xGAwNo42xwnhiIjFJmLWYEsw+TBvmHGYQM46Zw2Kx8lh9rB3WH8vECrCF2CrsUexZ7BB2AvsGR8Sp4Mxw7rgoHA+Xj6vAHcGdwQ3hJnELeCm8Jt4G749n43PwpfhGfDf+On4Cv0CQJmgT7AghhCTCJkIloZVwkfCA8JJIJKoRrYmBRC5xI7GSeIx4mThGfEuSIemRXEjRJCFpB+kQ6RzpLuklmUzWIjuSo8gC8g5yM/kC+RH5jQRFwkjCS4ItsUGiRqJDYkjiuSReUlPSSXK1ZK5kheQJyeuSM1J4KS0pFymm1HqpGqmTUiNSc9IUaVNpf+lU6RLpI9JXpKdksDJaMm4ybJkCmYMyF2TGKQhFneJCYVE2UxopFykTVAxVm+pFTaIWU7+jDlBnZWVkl8mGyWbL1sielh2lITQtmhcthVZKO04bpr1borTEaQlnyfYlrUuGlszLLZVzlOPIFcm1yd2WeydPl3eTT5bfJd8p/1ABpaCnEKiQpbBf4aLCzFLqUtulrKVFS48vvacIK+opBimuVTyo2K84p6Ss5KGUrlSldEFpRpmm7KicpFyufEZ5WoWiYq/CVSlXOavylC5Ld6Kn0CvpvfRZVUVVT1Whar3qgOqCmrZaqFq+WpvaQ3WCOkM9Xr1cvUd9VkNFw08jT6NF454mXpOhmai5V7NPc15LWytca6tWp9aUtpy2l3audov2Ax2yjoPOGp0GnVu6GF2GbrLuPt0berCehV6iXo3edX1Y31Kfq79Pf9AAbWBtwDNoMBgxJBk6GWYathiOGdGMfI3yjTqNnhtrGEcZ7zLuM/5oYmGSYtJoct9UxtTbNN+02/R3Mz0zllmN2S1zsrm7+QbzLvMXy/SXcZbtX3bHgmLhZ7HVosfig6WVJd+y1XLaSsMq1qrWaoRBZQQwShiXrdHWztYbrE9Zv7WxtBHYHLf5zdbQNtn2iO3Ucu3lnOWNy8ft1OyYdvV2o/Z0+1j7A/ajDqoOTIcGh8eO6o5sxybHSSddpySno07PnU2c+c7tzvMuNi7rXM65Iq4erkWuA24ybqFu1W6P3NXcE9xb3Gc9LDzWepzzRHv6eO7yHPFS8mJ5NXvNelt5r/Pu9SH5BPtU+zz21fPl+3b7wX7efrv9HqzQXMFb0ekP/L38d/s/DNAOWBPwYyAmMCCwJvBJkGlQXlBfMCU4JvhI8OsQ55DSkPuhOqHC0J4wybDosOaw+XDX8LLw0QjjiHUR1yIVIrmRXVHYqLCopqi5lW4r96yciLaILoweXqW9KnvVldUKq1NWn46RjGHGnIhFx4bHHol9z/RnNjDn4rziauNmWS6svaxnbEd2OXuaY8cp40zG28WXxU8l2CXsTphOdEisSJzhunCruS+SPJPqkuaT/ZMPJX9KCU9pS8Wlxqae5Mnwknm9acpp2WmD6frphemja2zW7Fkzy/fhN2VAGasyugRU0c9Uv1BHuEU4lmmfWZP5Jiss60S2dDYvuz9HL2d7zmSue+63a1FrWWt78lTzNuWNrXNaV78eWh+3vmeD+oaCDRMbPTYe3kTYlLzpp3yT/LL8V5vDN3cXKBVsLBjf4rGlpVCikF84stV2a9021DbutoHt5turtn8sYhddLTYprih+X8IqufqN6TeV33zaEb9joNSydP9OzE7ezuFdDrsOl0mX5ZaN7/bb3VFOLy8qf7UnZs+VimUVdXsJe4V7Ryt9K7uqNKp2Vr2vTqy+XeNc01arWLu9dn4fe9/Qfsf9rXVKdcV17w5wD9yp96jvaNBqqDiIOZh58EljWGPft4xvm5sUmoqbPhziHRo9HHS4t9mqufmI4pHSFrhF2DJ9NProje9cv+tqNWytb6O1FR8Dx4THnn4f+/3wcZ/jPScYJ1p/0Pyhtp3SXtQBdeR0zHYmdo52RXYNnvQ+2dNt293+o9GPh06pnqo5LXu69AzhTMGZT2dzz86dSz83cz7h/HhPTM/9CxEXbvUG9g5c9Ll4+ZL7pQt9Tn1nL9tdPnXF5srJq4yrndcsr3X0W/S3/2TxU/uA5UDHdavrXTesb3QPLh88M+QwdP6m681Lt7xuXbu94vbgcOjwnZHokdE77DtTd1PuvriXeW/h/sYH6AdFD6UeVjxSfNTws+7PbaOWo6fHXMf6Hwc/vj/OGn/2S8Yv7ycKnpCfVEyqTDZPmU2dmnafvvF05dOJZ+nPFmYKf5X+tfa5zvMffnP8rX82YnbiBf/Fp99LXsq/PPRq2aueuYC5R69TXy/MF72Rf3P4LeNt37vwd5MLWe+x7ys/6H7o/ujz8cGn1E+f/gUDmPP8usTo0wAAAAlwSFlzAAALEgAACxIB0t1+/AAAAFlJREFUGFdj+PTzDy4EkrOJK0qatbNg5bGi1aead92ceuzBrOMPoHJAFNS9unjTjZkX3m2+/fHI4y8QQYSZ6WsuL7vxGS4BRCj2zTr9DJlLyC24EHlyP/8AAM4S3fSBQSuwAAAAAElFTkSuQmCC";
    private const string imgArrowUp = "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";

    private static Image Base64ToImage(string base64String)
    {
        return Image.FromStream(new System.IO.MemoryStream(Convert.FromBase64String(base64String)));
    }
    private static string ImageToBase64(Image image)
    {
        using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
        {
            image.Save(stream, image.RawFormat);
            return Convert.ToBase64String(stream.ToArray());
        }
    }
}