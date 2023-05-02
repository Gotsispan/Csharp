using AMQMatching;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.Load += new EventHandler(this.Form1_Load);

            InitializeComponent();
        }

        private clsCustomAutoCompleteTextbox ClsCustomAutoCompleteTextbox1 = null;
        private clsCustomAutoCompleteTextbox ClsCustomAutoCompleteTextbox2 = null;
        private clsCustomAutoCompleteTextbox ClsCustomAutoCompleteTextbox3 = null;

        private List<string> MasterList1 = new List<string>();
        private List<string> MasterList2 = new List<string>();
        private List<string> MasterList3 = new List<string>();

        public void Form1_Load(object sender, System.EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - 426, 10);

            this.ClsCustomAutoCompleteTextbox1 = new clsCustomAutoCompleteTextbox();
            this.ClsCustomAutoCompleteTextbox1.AutoCompleteFormBorder = System.Windows.Forms.FormBorderStyle.None;
            this.ClsCustomAutoCompleteTextbox1.AutoCompleteList = null;
            this.ClsCustomAutoCompleteTextbox1.Location = new System.Drawing.Point(17, 57);
            this.ClsCustomAutoCompleteTextbox1.Name = "clsCustomAutoCompleteTextbox1";
            this.ClsCustomAutoCompleteTextbox1.OnEnterSelect = true;
            this.ClsCustomAutoCompleteTextbox1.SelectionMethods = clsCustomAutoCompleteTextbox.SelectOptions.OnEnterSingleClick;
            this.ClsCustomAutoCompleteTextbox1.SelectTextAfterItemSelect = true;
            this.ClsCustomAutoCompleteTextbox1.ShowAutoCompleteOnFocus = false;
            this.ClsCustomAutoCompleteTextbox1.Size = new System.Drawing.Size(352, 20);
            this.ClsCustomAutoCompleteTextbox1.TabIndex = 0;

            this.ClsCustomAutoCompleteTextbox2 = new clsCustomAutoCompleteTextbox();
            this.ClsCustomAutoCompleteTextbox2.AutoCompleteFormBorder = System.Windows.Forms.FormBorderStyle.None;
            this.ClsCustomAutoCompleteTextbox2.AutoCompleteList = null;
            this.ClsCustomAutoCompleteTextbox2.Location = new System.Drawing.Point(17, 127);
            this.ClsCustomAutoCompleteTextbox2.Name = "clsCustomAutoCompleteTextbox2";
            this.ClsCustomAutoCompleteTextbox2.OnEnterSelect = true;
            this.ClsCustomAutoCompleteTextbox2.SelectionMethods = clsCustomAutoCompleteTextbox.SelectOptions.OnEnterSingleClick;
            this.ClsCustomAutoCompleteTextbox2.SelectTextAfterItemSelect = true;
            this.ClsCustomAutoCompleteTextbox2.ShowAutoCompleteOnFocus = false;
            this.ClsCustomAutoCompleteTextbox2.Size = new System.Drawing.Size(352, 20);
            this.ClsCustomAutoCompleteTextbox2.TabIndex = 0;

            this.ClsCustomAutoCompleteTextbox3 = new clsCustomAutoCompleteTextbox();
            this.ClsCustomAutoCompleteTextbox3.AutoCompleteFormBorder = System.Windows.Forms.FormBorderStyle.None;
            this.ClsCustomAutoCompleteTextbox3.AutoCompleteList = null;
            this.ClsCustomAutoCompleteTextbox3.Location = new System.Drawing.Point(17, 197);
            this.ClsCustomAutoCompleteTextbox3.Name = "clsCustomAutoCompleteTextbox3";
            this.ClsCustomAutoCompleteTextbox3.OnEnterSelect = true;
            this.ClsCustomAutoCompleteTextbox3.SelectionMethods = clsCustomAutoCompleteTextbox.SelectOptions.OnEnterSingleClick;
            this.ClsCustomAutoCompleteTextbox3.SelectTextAfterItemSelect = true;
            this.ClsCustomAutoCompleteTextbox3.ShowAutoCompleteOnFocus = false;
            this.ClsCustomAutoCompleteTextbox3.Size = new System.Drawing.Size(352, 20);
            this.ClsCustomAutoCompleteTextbox3.TabIndex = 0;

            this.Controls.Add(this.ClsCustomAutoCompleteTextbox1);
            this.ClsCustomAutoCompleteTextbox1.BeforeDisplayingAutoComplete +=
            new EventHandler<clsCustomAutoCompleteTextbox.clsAutoCompleteEventArgs>(BeforeDisplayingAutoComplete1);
            List<string> L = animearray.ToList();
            MasterList1 = L;
            this.ClsCustomAutoCompleteTextbox1.AutoCompleteList = L;


            this.Controls.Add(this.ClsCustomAutoCompleteTextbox2);
            this.ClsCustomAutoCompleteTextbox2.BeforeDisplayingAutoComplete +=
            new EventHandler<clsCustomAutoCompleteTextbox.clsAutoCompleteEventArgs>(BeforeDisplayingAutoComplete2);
            List<string> M = songsarray.ToList();
            MasterList2 = M;
            this.ClsCustomAutoCompleteTextbox2.AutoCompleteList = M;

            this.Controls.Add(this.ClsCustomAutoCompleteTextbox3);
            this.ClsCustomAutoCompleteTextbox3.BeforeDisplayingAutoComplete +=
            new EventHandler<clsCustomAutoCompleteTextbox.clsAutoCompleteEventArgs>(BeforeDisplayingAutoComplete3);
            List<string> N = artistsarray.ToList();
            MasterList3 = N;
            this.ClsCustomAutoCompleteTextbox3.AutoCompleteList = N;

            for (int i = 5; i < 45; i++)
            {
                string buttonName = "button" + i.ToString();
                if (this.Controls.ContainsKey(buttonName))
                {
                    System.Windows.Forms.Button button = this.Controls[buttonName] as System.Windows.Forms.Button;
                    if (button != null)
                    {
                        button.Click += new EventHandler(buttonchoices_Click);
                    }
                }
            }

            for (int i = 5; i < 45; i++)
            {
                string buttonName = "button" + i.ToString();
                if (this.Controls.ContainsKey(buttonName))
                {
                    System.Windows.Forms.Button button = this.Controls[buttonName] as System.Windows.Forms.Button;
                    if (button != null && i != 23)
                    {
                        button.Text = "";
                        button.Size = new System.Drawing.Size(120, 40);
                    }
                }
            }



        }

        private void BeforeDisplayingAutoComplete1(object sender, clsCustomAutoCompleteTextbox.clsAutoCompleteEventArgs e)
        {

            string Name = this.ClsCustomAutoCompleteTextbox1.Text.ToLower();
            List<string> Display = new List<string>();
            foreach (string Str in MasterList1)
            {
                if ((Str.ToLower().IndexOf(Name) > -1))
                {
                    Display.Add(Str);
                }
            }
            e.AutoCompleteList = Display;
            e.SelectedIndex = 0;
        }

        private void BeforeDisplayingAutoComplete2(object sender, clsCustomAutoCompleteTextbox.clsAutoCompleteEventArgs e)
        {

            string Name = this.ClsCustomAutoCompleteTextbox2.Text.ToLower();
            List<string> Display = new List<string>();
            foreach (string Str in MasterList2)
            {
                if ((Str.ToLower().IndexOf(Name) > -1))
                {
                    Display.Add(Str);
                }
            }
            e.AutoCompleteList = Display;
            e.SelectedIndex = 0;
        }

        private void BeforeDisplayingAutoComplete3(object sender, clsCustomAutoCompleteTextbox.clsAutoCompleteEventArgs e)
        {
            string Name = this.ClsCustomAutoCompleteTextbox3.Text.ToLower();
            List<string> Display = new List<string>();
            foreach (string Str in MasterList3)
            {
                if ((Str.ToLower().IndexOf(Name) > -1))
                {
                    Display.Add(Str);
                }
            }
            e.AutoCompleteList = Display;
            e.SelectedIndex = 0;
        }


        private void buttontop_Click(object sender, EventArgs e)
        {
            string textToCopy;
            if (sender == button1)
            {
                if (!String.IsNullOrEmpty(ClsCustomAutoCompleteTextbox1.Text))
                {
                    ClsCustomAutoCompleteTextbox1.Text = "";
                }
            }
            if (sender == button2)
            {
                if (!String.IsNullOrEmpty(ClsCustomAutoCompleteTextbox2.Text))
                {
                    ClsCustomAutoCompleteTextbox2.Text = "";
                }
            }
            if (sender == button3)
            {
                if (!String.IsNullOrEmpty(ClsCustomAutoCompleteTextbox3.Text))
                {
                    ClsCustomAutoCompleteTextbox3.Text = "";
                }
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            ClsCustomAutoCompleteTextbox1.Clear();
            ClsCustomAutoCompleteTextbox2.Clear();
            ClsCustomAutoCompleteTextbox3.Clear();
            for (int i = 5; i < 45; i++)
            {
                string buttonName = "button" + i.ToString();
                if (this.Controls.ContainsKey(buttonName))
                {
                    System.Windows.Forms.Button button = this.Controls[buttonName] as System.Windows.Forms.Button;
                    if (button != null && i != 23)
                    {
                        button.Text = "";
                    }
                }
            }
        }

        private void buttonchoices_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button button = sender as System.Windows.Forms.Button;
            if (!String.IsNullOrEmpty(button.Text))
            {
                string textToCopy = button.Text;
                Clipboard.SetText(textToCopy);
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            for (int i = 5; i < 45; i++)
            {
                string buttonName = "button" + i.ToString();
                if (this.Controls.ContainsKey(buttonName))
                {
                    System.Windows.Forms.Button button = this.Controls[buttonName] as System.Windows.Forms.Button;
                    if (button != null && i != 23)
                    {
                        button.Text = "";
                    }
                }
            }
            List<int> animeindexes = new List<int>();
            List<int> songindexes = new List<int>();
            List<int> artistindexes = new List<int>();
            if (!String.IsNullOrEmpty(ClsCustomAutoCompleteTextbox1.Text))
            {
                for (int i = 0; i < animearraydupes.Length; i++)
                {
                    if (animearraydupes[i] == ClsCustomAutoCompleteTextbox1.Text)
                    {
                        animeindexes.Add(i);
                    }
                }
            }
            if (!String.IsNullOrEmpty(ClsCustomAutoCompleteTextbox2.Text))
            {
                for (int i = 0; i < songsarraydupes.Length; i++)
                {
                    if (songsarraydupes[i] == ClsCustomAutoCompleteTextbox2.Text)
                    {
                        songindexes.Add(i);
                    }
                }
            }
            if (!String.IsNullOrEmpty(ClsCustomAutoCompleteTextbox3.Text))
            {
                for (int i = 0; i < artistsarraydupes.Length; i++)
                {
                    if (artistsarraydupes[i] == ClsCustomAutoCompleteTextbox3.Text)
                    {
                        artistindexes.Add(i);
                    }
                }
            }

            List<int> commons = new List<int>();
            for (int i = 0; i < songsarraydupes.Length; i++)
            {
                var cont1 = animeindexes.Contains(i);
                var cont2 = songindexes.Contains(i);
                var cont3 = artistindexes.Contains(i);
                if (String.IsNullOrEmpty(ClsCustomAutoCompleteTextbox1.Text))
                {
                    cont1 = true;
                }
                if (String.IsNullOrEmpty(ClsCustomAutoCompleteTextbox2.Text))
                {
                    cont2 = true;
                }
                if (String.IsNullOrEmpty(ClsCustomAutoCompleteTextbox3.Text))
                {
                    cont3 = true;
                }

                if (cont1 && cont2 && cont3)
                {
                    commons.Add(i);
                }
            }

            for (int i = 0; i < commons.Count; i++)
            {
                Debug.WriteLine("{0} , {1}, {2}", animearraydupes[commons[i]], songsarraydupes[commons[i]], artistsarraydupes[commons[i]]);
                if (i < 12)
                {
                    string buttonName1;
                    string buttonName2;
                    string buttonName3;
                    if (i > 5)
                    {
                        buttonName1 = "button" + (5 + 3 * i + 1).ToString();
                        buttonName2 = "button" + (5 + 3 * i + 1 + 1).ToString();
                        buttonName3 = "button" + (5 + 3 * i + 2 + 1).ToString();
                    }
                    else
                    {
                        buttonName1 = "button" + (5 + 3 * i).ToString();
                        buttonName2 = "button" + (5 + 3 * i + 1).ToString();
                        buttonName3 = "button" + (5 + 3 * i + 2).ToString();
                    }
                    if (this.Controls.ContainsKey(buttonName1) && this.Controls.ContainsKey(buttonName2) && this.Controls.ContainsKey(buttonName3))
                    {
                        System.Windows.Forms.Button button1 = this.Controls[buttonName1] as System.Windows.Forms.Button;
                        System.Windows.Forms.Button button2 = this.Controls[buttonName2] as System.Windows.Forms.Button;
                        System.Windows.Forms.Button button3 = this.Controls[buttonName3] as System.Windows.Forms.Button;
                        if (button1 != null && button2 != null && button3 != null)
                        {
                            button1.Text = animearraydupes[commons[i]];
                            button2.Text = songsarraydupes[commons[i]];
                            button3.Text = artistsarraydupes[commons[i]];

                        }
                    }
                }
            }
        }


    }
    public class clsCustomAutoCompleteTextbox : System.Windows.Forms.TextBox
    {
        private bool First = true;

        private object sender;

        private clsAutoCompleteEventArgs e;

        public List<string> test = new List<string>();

        public int Tabs = 0;

        private int mSelStart;

        private int mSelLength;

        private List<string> myAutoCompleteList = new List<string>();

        private ListBox myLbox = new ListBox();

        private Form myForm = new Form();

        private Form myParentForm;

        private bool DontHide = false;

        private bool SuspendFocus = false;

        private clsAutoCompleteEventArgs Args;

        private Timer HideTimer = new Timer();

        private Timer FocusTimer = new Timer();

        private bool myShowAutoCompleteOnFocus;

        private System.Windows.Forms.FormBorderStyle myAutoCompleteFormBorder = FormBorderStyle.None;

        private bool myOnEnterSelect;

        private int LastItem;

        private SelectOptions mySelectionMethods = (SelectOptions.OnDoubleClick | SelectOptions.OnEnterPress);

        private bool mySelectTextAfterItemSelect = true;

        private List<string> value;

        private int Cnt = 0;

        public bool SelectTextAfterItemSelect
        {
            get
            {
                return mySelectTextAfterItemSelect;
            }
            set
            {
                mySelectTextAfterItemSelect = value;
            }
        }

        [System.ComponentModel.Browsable(false)]
        public SelectOptions SelectionMethods
        {
            get
            {
                return mySelectionMethods;
            }
            set
            {
                mySelectionMethods = value;
            }
        }

        public bool OnEnterSelect
        {
            get
            {
                return myOnEnterSelect;
            }
            set
            {
                myOnEnterSelect = value;
            }
        }

        public System.Windows.Forms.FormBorderStyle AutoCompleteFormBorder
        {
            get
            {
                return myAutoCompleteFormBorder;
            }
            set
            {
                myAutoCompleteFormBorder = value;
            }
        }

        public bool ShowAutoCompleteOnFocus
        {
            get
            {
                return myShowAutoCompleteOnFocus;
            }
            set
            {
                myShowAutoCompleteOnFocus = value;
            }
        }

        public ListBox Lbox
        {
            get
            {
                return myLbox;
            }
        }

        public List<string> AutoCompleteList { get; set; }

        public event EventHandler<clsAutoCompleteEventArgs> BeforeDisplayingAutoComplete;

        public event EventHandler<clsItemSelectedEventArgs> ItemSelected;

        public enum SelectOptions
        {
            None = 0,

            OnEnterPress = 1,

            OnSingleClick = 2,

            OnDoubleClick = 4,

            OnTabPress = 8,

            OnRightArrow = 16,

            OnEnterSingleClick = 3,

            OnEnterSingleDoubleClicks = 7,

            OnEnterDoubleClick = 5,

            OnEnterTab = 9,
        }

        public class clsAutoCompleteEventArgs : EventArgs
        {

            private List<string> myAutoCompleteList;

            private bool myCancel;

            private int mySelectedIndex;

            private List<string> value;

            public int SelectedIndex
            {
                get
                {
                    return mySelectedIndex;
                }
                set
                {
                    mySelectedIndex = value;
                }
            }

            public bool Cancel
            {
                get
                {
                    return myCancel;
                }
                set
                {
                    myCancel = value;
                }
            }
            public List<string> AutoCompleteList { get; set; }
        }

        public override string SelectedText
        {
            get
            {
                return base.SelectedText;
            }
            set
            {
                base.SelectedText = value;
            }
        }

        public override int SelectionLength
        {
            get
            {
                return base.SelectionLength;
            }
            set
            {
                base.SelectionLength = value;
            }
        }

        public clsCustomAutoCompleteTextbox()
        {
            HideTimer.Tick += new EventHandler(HideTimer_Tick);
            FocusTimer.Tick += new EventHandler(FocusTimer_Tick);

            myLbox.Click += new EventHandler(myLbox_Click);
            myLbox.DoubleClick += new EventHandler(myLbox_DoubleClick);
            myLbox.GotFocus += new EventHandler(myLbox_GotFocus);
            myLbox.KeyDown += new KeyEventHandler(myLbox_KeyDown);

            myLbox.KeyUp += new KeyEventHandler(myLbox_KeyUp);
            myLbox.LostFocus += new EventHandler(myLbox_LostFocus);
            myLbox.MouseClick += new MouseEventHandler(myLbox_MouseClick);
            myLbox.MouseDoubleClick += new MouseEventHandler(myLbox_MouseDoubleClick);
            myLbox.MouseDown += new MouseEventHandler(myLbox_MouseDown);


            this.GotFocus += new EventHandler(clsCustomAutoCompleteTextbox_GotFocus);
            this.KeyDown += new KeyEventHandler(clsCustomAutoCompleteTextbox_KeyDown);
            this.Leave += new EventHandler(clsCustomAutoCompleteTextbox_Leave);
            this.LostFocus += new EventHandler(clsCustomAutoCompleteTextbox_LostFocus);
            this.Move += new EventHandler(clsCustomAutoCompleteTextbox_Move);
            this.ParentChanged += new EventHandler(clsCustomAutoCompleteTextbox_ParentChanged);


        }

        override protected void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
        {


            base.OnKeyUp(e);
            ShowOnChar(new string(((char)(e.KeyValue)), 1));
        }

        private void ShowOnChar(string C)
        {


            if (IsPrintChar(C))
            {
                this.ShowAutoComplete();
            }
        }

        private bool IsPrintChar(int C)
        {


            return IsPrintChar(((char)(C)));
        }

        private bool IsPrintChar(byte C)
        {


            return IsPrintChar(((char)(C)));
        }

        private bool IsPrintChar(char C)
        {


            return IsPrintChar(C.ToString());
        }

        private bool IsPrintChar(string C)
        {

            if (System.Text.RegularExpressions.Regex.IsMatch(C, "[^\\t\\n\\r\\f\\v]"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void clsCustomAutoCompleteTextbox_GotFocus(object sender, System.EventArgs e)
        {

            if ((!this.SuspendFocus
                        && (this.myShowAutoCompleteOnFocus
                        && (this.myForm.Visible == false))))
            {
                this.ShowAutoComplete();
            }

        }

        private void clsCustomAutoCompleteTextbox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

            if (!SelectItem(e.KeyCode, false, false))
            {
                if ((e.KeyCode == Keys.Up))
                {
                    if ((myLbox.SelectedIndex > 0))
                    {
                        MoveLBox((myLbox.SelectedIndex - 1));
                    }
                }
                else if ((e.KeyCode == Keys.Down))
                {
                    MoveLBox((myLbox.SelectedIndex + 1));
                }
            }

        }

        new void SelectAll()
        {
        }

        private void MoveLBox(int Index)
        {

            try
            {
                if ((Index
                            > (myLbox.Items.Count - 1)))
                {
                    Index = (myLbox.Items.Count - 1);
                }
                myLbox.SelectedIndex = Index;
            }
            catch
            {
            }

        }

        private void clsCustomAutoCompleteTextbox_Leave(object sender, System.EventArgs e)
        {

            DoHide(sender, e);

        }

        private void clsCustomAutoCompleteTextbox_LostFocus(object sender, System.EventArgs e)
        {

            DoHide(sender, e);

        }

        private void clsCustomAutoCompleteTextbox_Move(object sender, System.EventArgs e)
        {

            MoveDrop();

        }

        private void clsCustomAutoCompleteTextbox_ParentChanged(object sender, System.EventArgs e)
        {

            if (myParentForm != null) myParentForm.Deactivate -= new EventHandler(myParentForm_Deactivate);
            myParentForm = GetParentForm(this);
            if (myParentForm != null) myParentForm.Deactivate += new EventHandler(myParentForm_Deactivate);
        }

        private void HideTimer_Tick(object sender, System.EventArgs e)
        {

            MoveDrop();
            DoHide(sender, e);
            Cnt++;
            if ((Cnt > 300))
            {
                if (!AppHasFocus(""))
                {
                    DoHideAuto();
                }
                Cnt = 0;
            }

        }

        private void myLbox_Click(object sender, System.EventArgs e)
        {
        }

        private void myLbox_DoubleClick(object sender, System.EventArgs e)
        {
        }

        private bool SelectItem(Keys Key, bool SingleClick)
        {
            return SelectItem(Key, SingleClick, false);
        }

        private bool SelectItem(Keys Key)
        {
            return SelectItem(Key, false, false);
        }

        private bool SelectItem(Keys Key, bool SingleClick, bool DoubleClick)
        {

            // Warning!!! Optional parameters not supported
            // Warning!!! Optional parameters not supported
            // Warning!!! Optional parameters not supported
            bool DoSelect = true;
            SelectOptions Meth = SelectOptions.None;
            LastItem = -1;

            if (((this.mySelectionMethods & SelectOptions.OnEnterPress) > 0) && (Key == Keys.Enter))
            {
                Meth = SelectOptions.OnEnterPress;
            }
            else if (((this.mySelectionMethods & SelectOptions.OnRightArrow) > 0) && Key == Keys.Right)
            {
                Meth = SelectOptions.OnRightArrow;
            }
            else if (((this.mySelectionMethods & SelectOptions.OnTabPress) > 0) && Key == Keys.Tab)
            {
                Meth = SelectOptions.OnTabPress;
            }
            else if (((this.mySelectionMethods & SelectOptions.OnSingleClick) > 0) && SingleClick)
            {
                Meth = SelectOptions.OnEnterPress;
            }
            else if (((this.mySelectionMethods & SelectOptions.OnDoubleClick) > 0) && DoubleClick)
            {
                Meth = SelectOptions.OnEnterPress;
            }
            else
            {
                DoSelect = false;
            }

            LastItem = myLbox.SelectedIndex;
            if (DoSelect)
            {
                DoSelectItem(Meth);
            }

            return DoSelect;
        }
        public class clsItemSelectedEventArgs : EventArgs
        {

            private int myIndex;

            private SelectOptions myMethod;

            private string myItemText;

            public clsItemSelectedEventArgs()
            {
            }

            public clsItemSelectedEventArgs(int Index, SelectOptions Method, string ItemText)
            {
                myIndex = Index;
                myMethod = Method;
                myItemText = ItemText;
            }

            public string ItemText
            {
                get
                {
                    return myItemText;
                }
                set
                {
                    myItemText = value;
                }
            }

            public SelectOptions Method
            {
                get
                {
                    return myMethod;
                }
                set
                {
                    myMethod = value;
                }
            }

            public int Index
            {
                get
                {
                    return myIndex;
                }
                set
                {
                    myIndex = value;
                }
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, ref int ProcessID);

        private bool AppHasFocus(string ExeNameWithoutExtension)
        {
            bool Out = false;
            // Warning!!! Optional parameters not supported
            int PID = 0;

            if ((ExeNameWithoutExtension == ""))
            {
                ExeNameWithoutExtension = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            }
            IntPtr activeHandle = GetForegroundWindow();
            GetWindowThreadProcessId(activeHandle, ref PID);
            if ((PID > 0))
            {
                // For Each p As Process In Process.GetProcessesByName(ExeNameWithoutExtension)
                if ((PID == System.Diagnostics.Process.GetCurrentProcess().Id))
                {
                    Out = true;
                }
                //  Next
            }

            return Out;
        }

        private void SaveSelects()
        {
            this.mSelStart = this.SelectionStart;
            this.mSelLength = this.SelectionLength;
        }

        private void LoadSelects()
        {
            this.SelectionStart = this.mSelStart;
            this.SelectionLength = this.mSelLength;
        }

        private void ShowAutoComplete()
        {

            Args = new clsAutoCompleteEventArgs();
            // With...
            Args.Cancel = false;
            Args.AutoCompleteList = this.myAutoCompleteList;
            if ((myLbox.SelectedIndex == -1))
            {
                Args.SelectedIndex = 0;
            }
            else
            {
                Args.SelectedIndex = myLbox.SelectedIndex;
            }

            if (BeforeDisplayingAutoComplete != null) BeforeDisplayingAutoComplete(this, Args);
            this.myAutoCompleteList = Args.AutoCompleteList;
            // If Me.myAutoCompleteList IsNot Nothing AndAlso Me.myAutoCompleteList.Count - 1 < Args.SelectedIndex Then
            //   Args.SelectedIndex = Me.myAutoCompleteList.Count - 1
            // End If
            if ((!Args.Cancel && (Args.AutoCompleteList != null) && Args.AutoCompleteList.Count > 0))
            {
                DoShowAuto();
            }
            else
            {
                DoHideAuto();
            }

        }

        private void DoShowAuto()
        {
            SaveSelects();

            myLbox.BeginUpdate();
            try
            {
                myLbox.Items.Clear();
                myLbox.Items.AddRange(this.myAutoCompleteList.ToArray());
                this.MoveLBox(Args.SelectedIndex);
            }
            catch (Exception ex)
            {
            }
            myLbox.EndUpdate();
            myParentForm = GetParentForm(this);
            if (myParentForm != null)
            {
                myLbox.Name = ("mmmlbox" + DateTime.Now.Millisecond);
                if ((myForm.Visible == false))
                {
                    myForm.Font = this.Font;
                    myLbox.Font = this.Font;
                    myLbox.Visible = true;
                    myForm.Visible = false;
                    myForm.ControlBox = false;
                    myForm.Text = "";
                    if (First)
                    {
                        myForm.Width = this.Width;
                        myForm.Height = 200;
                    }
                    First = false;
                    if (!myForm.Controls.Contains(myLbox))
                    {
                        myForm.Controls.Add(myLbox);
                    }
                    myForm.FormBorderStyle = FormBorderStyle.None;
                    myForm.ShowInTaskbar = false;
                    // With...
                    myLbox.Dock = DockStyle.Fill;
                    myLbox.SelectionMode = SelectionMode.One;
                    // Frm.Controls.Add(myLbox)
                    DontHide = true;
                    SuspendFocus = true;
                    myForm.TopMost = true;
                    myForm.FormBorderStyle = this.myAutoCompleteFormBorder;
                    myForm.BringToFront();
                    MoveDrop();
                    myForm.Visible = true;
                    myForm.Show();
                    MoveDrop();
                    HideTimer.Interval = 10;
                    this.Focus();
                    SuspendFocus = false;
                    HideTimer.Enabled = true;
                    DontHide = false;
                    LoadSelects();
                }
            }

        }

        void MoveDrop()
        {

            Point Pnt = new Point(this.Left, (this.Top
                            + (this.Height + 2)));
            Point ScreenPnt = this.PointToScreen(new Point(-2, this.Height));
            // Dim FrmPnt As Point = Frm.PointToClient(ScreenPnt)
            if (myForm != null)
            {
                myForm.Location = ScreenPnt;
                // myForm.BringToFront()
                // myForm.Focus()
                // myLbox.Focus()
                // Me.Focus()
            }

        }

        void DoHide(object sender, EventArgs e)
        {

            HideAuto();

        }

        private void DFocus(int Delay)
        {

            // Warning!!! Optional parameters not supported
            FocusTimer.Interval = Delay;
            FocusTimer.Start();

        }

        private void DFocus()
        {
            DFocus(10);
        }

        private void DoHideAuto()
        {

            myForm.Hide();
            HideTimer.Enabled = false;
            FocusTimer.Enabled = false;

        }

        private void HideAuto()
        {

            if ((myForm.Visible && HasLostFocus()))
            {
                DoHideAuto();
            }

        }

        private bool HasLostFocus()
        {

            bool Out = false;
            if (this.myForm == null || myForm.ActiveControl != this.myLbox)
            {
                Out = true;
            }
            if (this.myParentForm == null || this.myParentForm.ActiveControl != this)
            {
                Out = true;
            }

            return Out;
        }

        private Form GetParentForm(Control InCon)
        {

            Control TopCon = FindTopParent(InCon);
            Form Out = null;
            if ((TopCon is Form))
            {
                Out = ((Form)(TopCon));
            }

            return Out;
        }

        private Control FindTopParent(Control InCon)
        {

            Control Out;
            if ((InCon.Parent == null))
            {
                Out = InCon;
            }
            else
            {
                Out = FindTopParent(InCon.Parent);
            }

            return Out;
        }

        private void DoSelectItem(SelectOptions Method)
        {

            if (((this.myLbox.Items.Count > 0)
                        && (this.myLbox.SelectedIndex > -1)))
            {
                string Value = this.myLbox.SelectedItem.ToString();
                string Orig = this.Text;
                this.Text = Value;
                if (mySelectTextAfterItemSelect)
                {
                    try
                    {
                        this.SelectionStart = Orig.Length;
                        this.SelectionLength = (Value.Length - Orig.Length);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    // Me.SelectionStart = Me.Text.Length
                    // Me.SelectionLength = 0
                }

                clsItemSelectedEventArgs a;
                a = new clsItemSelectedEventArgs();
                a.Index = this.myLbox.SelectedIndex;
                a.Method = Method;
                a.ItemText = Value;

                if (ItemSelected != null) ItemSelected(this, a);

                //ItemSelected(this, new clsItemSelectedEventArgs(this.myLbox.SelectedIndex, Method, Value));
                this.DoHideAuto();
            }

        }

        private void myLbox_GotFocus(object sender, System.EventArgs e)
        {

            DFocus();

        }

        private void myLbox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

            SelectItem(e.KeyCode);

        }

        private void ProcessKeyEvents(KeyEventArgs e)
        {


            if ((e.KeyCode >= Keys.A) && (e.KeyCode <= Keys.Z))
                base.OnKeyUp(e);


            //Keys.Back;
            //Keys.Enter;
            //Keys.Left;
            //Keys.Right;
            //Keys.Up;
            //Keys.Down;
            //(Keys.NumPad0 & (e.KeyCode <= Keys.NumPad9));
            //(Keys.D0 & (e.KeyCode <= Keys.D9));


        }

        private void myLbox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (IsPrintChar(e.KeyChar))
            {
                // Me.OnKeyPress(e)
                // Call MoveDrop()
            }

        }

        private void myLbox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (IsPrintChar(e.KeyValue))
            {
                // Me.OnKeyUp(e)
                // Call MoveDrop()
            }

        }

        private void myLbox_LostFocus(object sender, System.EventArgs e)
        {

            DoHide(sender, e);

        }

        private void myLbox_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            // If e.Button <> Windows.Forms.MouseButtons.None Then
            SelectItem(Keys.None, true);
            // End If

        }

        private void myLbox_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            // If e.Button <> Windows.Forms.MouseButtons.None Then
            SelectItem(Keys.None, false, true);
            // End If

        }

        private void myForm_Deactivate(object sender, System.EventArgs e)
        {


        }

        private void myParentForm_Deactivate(object sender, System.EventArgs e)
        {


        }

        private void FocusTimer_Tick(object sender, System.EventArgs e)
        {

            this.Focus();

        }

        private void myLbox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            myLbox_MouseClick(sender, e);
        }
    }
}