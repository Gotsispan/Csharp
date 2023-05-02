using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;

using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;



namespace MyCalendar
{
    public static class DateTimeDayOfMonthExtensions
    {
        public static DateTime FirstDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        public static int DaysInMonth(this DateTime value)
        {
            return DateTime.DaysInMonth(value.Year, value.Month);
        }

        public static DateTime LastDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.DaysInMonth());
        }
    }


    class CustomComparer : IComparer<List<string>>
    {
        private readonly Dictionary<string, int> _sortDictionary;

        public CustomComparer(Dictionary<string, int> sortDictionary)
        {
            _sortDictionary = sortDictionary;
        }

        public int Compare(List<string> x, List<string> y)
        {
            string xValue = x[0];
            string yValue = y[0];

            int xSortValue = _sortDictionary[xValue];
            int ySortValue = _sortDictionary[yValue];

            return xSortValue.CompareTo(ySortValue);
        }
    }

    public partial class Form1 : Form
    {
        public int monthshown = DateTime.Today.Month;
        public int identifier = 0;
        public int yearshown = DateTime.Today.Year;
        public int dayshown = DateTime.Today.Day;
        public int buttonCount = 42; // number of buttons to create
        public int buttonperrow = 7;
        public int buttonWidth = 50; // width of each button
        public int buttonHeight = 50; // height of each button
        public int lineNumber = 0;
        public List<string> monthNames = new List<string> {
            "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
            };
        public string[] daysofweek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        public Panel panel1;
        public Panel paneldown;
        public ComboBox comboBox2 = new ComboBox();
        public ComboBox comboBox1 = new ComboBox();
        public ComboBox comboBox3down = new ComboBox();
        public ComboBox comboBox2down = new ComboBox();
        public ComboBox comboBox1down = new ComboBox();
        public TextBox textBoxdown = new TextBox();
        public Panel timetablePanel  = new Panel();
        public VScrollBar scrollbar = new VScrollBar();


        public int PanelWidth { get; set; }
        public int PanelHeight { get; set; }
        public int numIntervals = 24 * 4;
        public int labelHeight { get; set; }

        public Dictionary<string, System.Drawing.Color> noteCategories = new Dictionary<string, System.Drawing.Color>()
        {
            { "Personal", Color.Blue },
            { "Work", Color.Green },
            { "Urgent", Color.Red },
            { "Ideas", Color.Orange },
            { "To-Do", Color.Purple },
            { "Reminders", Color.Pink },
            { "Shopping", Color.Brown },
            { "Important", Color.Yellow }
        };


        public Dictionary<string, int> priofnotes = new Dictionary<string, int>()
        {
            { "Personal",6 },
            { "Work", 3 },
            { "Urgent", 1 },
            { "Ideas", 7 },
            { "To-Do", 5 },
            { "Reminders", 4 },
            { "Shopping", 8 },
            { "Important", 2 }
        };

        public string[] typesofnotes = { "Personal", "Work", "Urgent", "Ideas", "To-Do", "Reminders", "Shopping", "Important" };

        private void comboBox1down_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> stringintervals1 = new List<string>();

            TimeSpan interval = TimeSpan.Zero;
            for (int i = 0; i < 24*4; i++)
            {
                if (string.Compare(comboBox1down.Text, interval.ToString(@"hh\:mm")) <= 0)
                {
                    stringintervals1.Add(interval.ToString(@"hh\:mm")); 
                }
                interval = interval.Add(TimeSpan.FromMinutes(15));
            }
            stringintervals1.Add("24:00");
            comboBox2down.DataSource = stringintervals1;
        }

        private void comboBox2down_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        public Form1()
        {
            this.Load += new EventHandler(this.Form1_Load);
            InitializeComponent();
            this.MaximizeBox = false;
        }

        private void submitdown(object sender, EventArgs e)
        {
            string stringToAdd = "," + comboBox1down.Text + "," + comboBox2down.Text + "," + comboBox3down.Text + "," + textBoxdown.Text  ;

            string[] lines = System.IO.File.ReadAllLines("C:\\Users\\pango\\Documents\\GitHub\\Csharp\\MyCalendar\\datestodata.txt");

            // check if the line number is within the range of the lines array
            if (lineNumber >= 0 && lineNumber < lines.Length)
            {
                // add the string to the end of the selected line
                lines[lineNumber] += stringToAdd;
            }

            // write the updated lines array back to the file
            File.WriteAllLines("C:\\Users\\pango\\Documents\\GitHub\\Csharp\\MyCalendar\\datestodata.txt", lines);
            Button button = new Button();
            button.Text = dayshown.ToString();
            button.Name = "submit" + comboBox1down.Text;
            Createnotes(button, EventArgs.Empty);
            //comboBox1down.SelectedIndex = 24;

        }

        private void deletepanel(object sender, EventArgs e)
        {
            string filePath = "C:\\Users\\pango\\Documents\\GitHub\\Csharp\\MyCalendar\\datestodata.txt";
            Button control = sender as Button;
            int butti = 1;
            if (control != null)
            {
                string senderName = control.Name;
                butti = int.Parse(senderName.Substring(6));
                // do something with senderName
            }

            int lineNumber2 = lineNumber + 1;

            List<string> lines = File.ReadAllLines(filePath).ToList();
            // Modify the desired line
            if (lines.Count >= lineNumber2)
            {
                string[] lineArray = lines[lineNumber2 - 1].Split(',');
                string[] resultArray1 = lineArray.Take(3 + (butti)*4).ToArray();
                string[] resultArray2 = lineArray.Skip(3 + (butti+1)*4).ToArray();
                string[] resultArray = resultArray1.Concat(resultArray2).ToArray();
                lines[lineNumber2 - 1] = string.Join(",", resultArray);
            }

            // Write the modified lines back to the file
            File.WriteAllLines(filePath, lines);

            Control panelToRemove = timetablePanel.Controls.Find("Panel" + butti, true).FirstOrDefault();
            if (panelToRemove != null && panelToRemove is Panel)
            {
                timetablePanel.Controls.Remove(panelToRemove);
            }

            Button button = new Button();
            button.Text = dayshown.ToString();
            Createnotes(button, EventArgs.Empty);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                int dayofmo = int.Parse(clickedButton.Text);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comb = sender as ComboBox;
            try
            {
                yearshown = int.Parse(comb.Text);
                Createbuttonsandnotes(new DateTime(yearshown,monthshown,1));
                Button button = new Button();
                button.Text = 1.ToString();
                Createnotes(button, EventArgs.Empty);
            }
            catch {  }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comb = sender as ComboBox;
            
            if (monthNames.IndexOf(comb.Text) != -1)
            {
                monthshown = monthNames.IndexOf(comb.Text) + 1;
                Createbuttonsandnotes(new DateTime(yearshown, monthshown, 1));
                Button button = new Button();
                button.Text = 1.ToString();
                Createnotes(button, EventArgs.Empty);
            }
        }

        private void pullmonth(object sender, EventArgs e)
        {
            try
            {
                if (monthshown > 1) { 
                    monthshown -= 1;
                    comboBox2.SelectedItem = monthNames[monthshown - 1];
                    Createbuttonsandnotes(new DateTime(yearshown, monthshown, 1));
                    Button button = new Button();
                    button.Text = 1.ToString();
                    Createnotes(button, EventArgs.Empty);
                }
            }
            catch { }
        }

        private void pushmonth(object sender, EventArgs e)
        {
            try
            {
                if (monthshown < 12)
                {
                    monthshown += 1;
                    comboBox2.SelectedItem = monthNames[monthshown - 1];
                    Createbuttonsandnotes(new DateTime(yearshown, monthshown, 1));
                    Button button = new Button();
                    button.Text = 1.ToString();
                    Createnotes(button, EventArgs.Empty);

                }
            }
            catch { }
        }

        private void panel_MouseEnter(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;
            object tagValue = panel.Tag;
            ((Panel)sender).BackColor = ControlPaint.Dark(noteCategories[tagValue.ToString()], 0.1f);

            string panelName = ((Control)sender).Name;
            int number = int.Parse(Regex.Replace(panelName, "[^0-9]+", string.Empty));

            Point mousePos = timetablePanel.PointToClient(Control.MousePosition);
            int heightwanted = 0;
            for (int i=0; i<24*4; i++)
            {
                if (mousePos.Y >= 7 + i * labelHeight && mousePos.Y < 7 + (i+1) * labelHeight)
                {
                    heightwanted = 7 + i * labelHeight;
                    break;
                }
            }    

            Label myLabel = new Label();
            myLabel.Size = new Size(timetablePanel.Width - scrollbar.Width - 40, labelHeight - 1);
            myLabel.Location = new Point(40, heightwanted);
            myLabel.Text = panel.Text;
            myLabel.TextAlign = ContentAlignment.MiddleLeft;
            myLabel.BackColor = ControlPaint.Dark(noteCategories[tagValue.ToString()], 0.1f);
            myLabel.Name = "Label" + number.ToString();
            myLabel.Visible = true;
            timetablePanel.Controls.Add(myLabel);
            myLabel.BringToFront();
        }

        // Define the MouseLeave event handler
        private void panel_MouseLeave(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;
            object tagValue = panel.Tag;
            ((Panel)sender).BackColor = noteCategories[tagValue.ToString()];

            string panelName = ((Control)sender).Name;
            int number = int.Parse(Regex.Replace(panelName, "[^0-9]+", string.Empty));

            // Get the label with the matching name
            Label label = panel.Parent.Controls.Find("label" + number.ToString(), true).FirstOrDefault() as Label;

            // Hide the label
            if (label != null)
            {
                label.Dispose();
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            List<int> years = Enumerable.Range(1990, DateTime.Now.Year + 20 - 1989).ToList();
            Panel panelup = new Panel();
            panelup.Location = new Point(30, 10);
            panelup.Size = new Size(buttonWidth * buttonperrow, 120);

            comboBox1.Items.AddRange(years.Cast<object>().ToArray());
            comboBox1.Size = new Size(50, 23);
            comboBox1.SelectedItem = yearshown;
            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            comboBox1.TextChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            comboBox1.Location = new Point(70, 50);

            Label label1 = new Label();
            label1.Text = "Year:";
            label1.AutoSize = false;
            label1.Size = new Size(50, 20);
            label1.Font = new Font(label1.Font.FontFamily, 11);
            label1.ForeColor = Color.WhiteSmoke;
            label1.Location = new Point(70, 30);

            Label label2 = new Label();
            label2.Text = "Month:";
            label2.Font = new Font(label2.Font.FontFamily, 11);
            label2.AutoSize = false;
            label2.ForeColor = Color.WhiteSmoke;
            label2.Size = new Size(60, 20);
            label2.Location = new Point(195, 30);

            Button buttonless = new Button();
            buttonless.Text = "<";
            buttonless.ForeColor = Color.WhiteSmoke;
            buttonless.Location = new Point(130, 38);
            buttonless.Size = new Size(25,25);
            buttonless.Click += new EventHandler(pullmonth);

            Button buttonmore = new Button();
            buttonmore.Text = ">";
            buttonmore.ForeColor = Color.WhiteSmoke;
            buttonmore.Location = new Point(265, 38);
            buttonmore.Size = new Size(25, 25);
            buttonmore.Click += new EventHandler(pushmonth);


            comboBox2.Items.AddRange(monthNames.ToArray());
            comboBox2.AutoSize = false;
            comboBox2.Size = new Size(100, 23);
            comboBox2.SelectedItem = monthNames[monthshown-1];
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.SelectedIndexChanged += new EventHandler(comboBox2_SelectedIndexChanged);
            comboBox2.TextChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            comboBox2.Location = new Point(190, 50);

            // Define the dimensions of the table
            panel1 = new Panel();
            paneldown = new Panel();
            double downperc = 0.75;
            double leftperc = 0.49;
            PanelWidth = (int)(0.5 * ClientSize.Width);
            PanelHeight = (int)(5 * ClientSize.Height);
            labelHeight = (int)(PanelHeight / numIntervals);

            int[] starpos = { 30, 80 }; ;
            panel1.Location = new Point(starpos[0], starpos[1]);
            panel1.Size = new Size(buttonWidth * buttonperrow, buttonHeight * (1 + (int)Math.Floor((decimal)buttonCount / buttonperrow)));
            panel1.BackColor = Color.LightGray;
            panel1.Paint += new PaintEventHandler(panel1_Paint);

            paneldown.Location = new Point(0, (int)(ClientSize.Height * downperc));
            paneldown.Size = new Size((int)(ClientSize.Width * leftperc), (int)(ClientSize.Height * 0.25));
            paneldown.BackColor = Color.Red;

            double[] formdownsizes = { 0.9, 0.4 };
            double[] buttondownizes = { 0.4, 0.2 };
            // Create a new textbox control
            textBoxdown.Location = new Point((int)((paneldown.Width * (1 - formdownsizes[0]) / 2)), 40);
            textBoxdown.AutoSize = false;
            textBoxdown.Size = new Size((int)(paneldown.Width * formdownsizes[0]), (int)(paneldown.Height * formdownsizes[1]));
            textBoxdown.TabStop = false;

            // Create a new button control
            Button buttondown = new Button();
            buttondown.Text = "Submit";
            buttondown.AutoSize = false;
            buttondown.BackColor = Color.Blue;
            buttondown.ForeColor = Color.WhiteSmoke;
            buttondown.Location = new Point((int)((paneldown.Width * (1 - buttondownizes[0]) / 2)), 50 + (int)(paneldown.Height * formdownsizes[1]));
            buttondown.Size = new Size((int)(paneldown.Width * buttondownizes[0]), (int)(paneldown.Height * buttondownizes[1]));
            buttondown.Click += new EventHandler(submitdown);

            // Add the textbox and button controls to the panel

            double[] sizesinarow = { 0.11, 0.16, 0.06, 0.16, 0.12, 0.35 };
            double[] sizesadding = { 0, 0, 0, 0, 0, 0 };
            TimeSpan[] intervals = new TimeSpan[24 * 4];
            string[] stringintervals1 = new string[24 * 4];

            TimeSpan interval = TimeSpan.Zero;
            for (int i = 0; i < intervals.Length; i++)
            {
                intervals[i] = interval;
                stringintervals1[i] = interval.ToString(@"hh\:mm"); ;
                interval = interval.Add(TimeSpan.FromMinutes(15));
            }

            string[] stringintervals2 = (string[])stringintervals1.Clone();


            double sum = 0;
            for (int i = 0; i < sizesinarow.Length; i++)
            {
                sizesadding[i] = sum;
                sum += sizesinarow[i];
            }

            Label label1down = new Label();
            label1down.Text = "From";
            label1down.AutoSize = false;
            label1down.Size = new Size((int)(sizesinarow[0] * paneldown.Width), 20);
            label1down.Font = new Font(label1down.Font.FontFamily, 11);
            label1down.ForeColor = Color.WhiteSmoke;
            label1down.Location = new Point(5 + (int)(sizesadding[0] * paneldown.Width), 5);

            Label label2down = new Label();
            label2down.Text = "to";
            label2down.Font = new Font(label2down.Font.FontFamily, 11);
            label2down.AutoSize = false;
            label2down.ForeColor = Color.WhiteSmoke;
            label2down.Size = new Size((int)(sizesinarow[2] * paneldown.Width), 20);
            label2down.Location = new Point(5 + (int)(sizesadding[2] * paneldown.Width), 5);

            Label label3down = new Label();
            label3down.Text = "Type:";
            label3down.AutoSize = false;
            label3down.Size = new Size((int)(sizesinarow[4] * paneldown.Width), 20);
            label3down.Font = new Font(label3down.Font.FontFamily, 11);
            label3down.ForeColor = Color.WhiteSmoke;
            label3down.Location = new Point(5 + (int)(sizesadding[4] * paneldown.Width), 5);


            comboBox1down.Size = new Size((int)(sizesinarow[1] * paneldown.Width), 20);
            //comboBox1down.DataSource = stringintervals1;
            foreach (var item in stringintervals1)
            {
                comboBox1down.Items.Add(item);
            }
            //comboBox1down.SelectedIndex = 24;
            comboBox1down.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1down.SelectedIndexChanged += comboBox1down_SelectedIndexChanged;
            //comboBox1down.TextChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            comboBox1down.Location = new Point(5 + (int)(sizesadding[1] * paneldown.Width), 5);

            comboBox2down.AutoSize = false;
            comboBox2down.Size = new Size((int)(sizesinarow[3] * paneldown.Width), 20);
            //comboBox2down.DataSource = stringintervals2;
            List<string> box2down = new List<string>();
            TimeSpan intervall = TimeSpan.Zero;
            for (int i = 0; i < 24 * 4; i++)
            {
                if (string.Compare(stringintervals1[24], intervall.ToString(@"hh\:mm")) <= 0)
                {
                    box2down.Add(intervall.ToString(@"hh\:mm"));
                }
                intervall = intervall.Add(TimeSpan.FromMinutes(15));
            }
            box2down.Add("24:00");
            comboBox2down.DataSource = box2down;
            comboBox2down.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2down.SelectedIndexChanged += comboBox2down_SelectedIndexChanged;
            //comboBox2down.TextChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            comboBox2down.Location = new Point(5 + (int)(sizesadding[3] * paneldown.Width), 5);

            comboBox3down.AutoSize = false;
            comboBox3down.Size = new Size((int)(sizesinarow[5] * paneldown.Width), 20);
            comboBox3down.SelectedItem = monthNames[monthshown - 1];
            comboBox3down.DataSource = typesofnotes;
            //comboBox3down.SelectedIndexChanged += new EventHandler(comboBox2_SelectedIndexChanged);
            //comboBox3down.TextChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            comboBox3down.Location = new Point(5 + (int)(sizesadding[5] * paneldown.Width), 5);


            paneldown.Controls.Add(comboBox1down);
            paneldown.Controls.Add(comboBox2down);
            paneldown.Controls.Add(comboBox3down);
            paneldown.Controls.Add(label1down);
            paneldown.Controls.Add(label2down);
            paneldown.Controls.Add(label3down);
            paneldown.Controls.Add(textBoxdown);
            paneldown.Controls.Add(buttondown);


            timetablePanel.Location = new Point(this.ClientSize.Width / 2, 0);
            timetablePanel.Size = new Size(PanelWidth, PanelHeight);
            timetablePanel.BackColor = Color.Transparent;
            timetablePanel.ForeColor = Color.WhiteSmoke;


            Panel lineleft = new Panel();
            lineleft.BackColor = Color.WhiteSmoke;
            lineleft.Size = new Size(1, ((24*4)) * labelHeight+2);
            lineleft.Location = new Point(0, 4);
            timetablePanel.Controls.Add(lineleft);

            Panel linetop = new Panel();
            linetop.BackColor = Color.WhiteSmoke;
            linetop.Size = new Size(timetablePanel.Width, 1);
            linetop.Location = new Point(0, 4);
            timetablePanel.Controls.Add(linetop);

            // Create labels to populate the timetable panel
            for (int i = 0; i < numIntervals; i++)
            {
                Label label = new Label();
                label.AutoSize = false;
                label.TextAlign = ContentAlignment.MiddleLeft;
                label.Size = new Size(timetablePanel.Width - scrollbar.Width, labelHeight);
                label.Location = new Point(0, 5 + i * labelHeight);
                label.Margin = new Padding(1, 1, 1, 1);
                label.Text = string.Format("{0:00}:{1:00}", i / 4, (i % 4) * 15);
                timetablePanel.Controls.Add(label);

                // Add lines between labels
                if (i < numIntervals) // Don't draw line after last label
                {
                    Panel line = new Panel();
                    line.BackColor = Color.WhiteSmoke;
                    line.Size = new Size(timetablePanel.Width - scrollbar.Width, 1);
                    line.Location = new Point(0, label.Bottom + 1);
                    timetablePanel.Controls.Add(line);
                }
            }

            // Set the scrollbar properties

            scrollbar.Dock = DockStyle.Right;
            scrollbar.Minimum = 0;
            scrollbar.Maximum = timetablePanel.Height - ClientSize.Height;
            int startingPosition = 24 * labelHeight;
            scrollbar.Value = startingPosition;
            scrollbar.SmallChange = labelHeight;
            scrollbar.LargeChange = 2 * labelHeight;
            timetablePanel.Location = new Point(this.ClientSize.Width / 2, -scrollbar.Value);

            // Add the panel and scrollbar to the form
            Controls.Add(timetablePanel);
            timetablePanel.Controls.Add(scrollbar);
            timetablePanel.VerticalScroll.Value = timetablePanel.VerticalScroll.Maximum / 2;


            // Handle the Scroll event
            scrollbar.Scroll += (sender2, e2) =>
            {
                timetablePanel.Location = new Point(this.ClientSize.Width / 2, -scrollbar.Value);
            };
           

            // Add myPanel to the form
            this.BackColor = Color.MidnightBlue;


            panelup.Controls.Add(buttonless);
            panelup.Controls.Add(buttonmore);

            this.Controls.Add(timetablePanel);
            this.Controls.Add(panel1);
            this.Controls.Add(paneldown);
            this.Controls.Add(comboBox1);
            this.Controls.Add(comboBox2);
            this.Controls.Add(label1);
            this.Controls.Add(label2);
            this.Controls.Add(panelup);

            DateTime currentDate = DateTime.Today;
            Createbuttonsandnotes(currentDate);
            Button button = new Button();
            button.Text = currentDate.Day.ToString();
            Createnotes(button, EventArgs.Empty);

            // create buttons dynamically in a for loop

            // add the button to the panel control
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Get the Graphics object from the PaintEventArgs parameter
            Graphics g = e.Graphics;

            // Get the size of the panel
            Size size = panel1.Size;

            // Calculate the number and size of cells in the table
            int x = 7;
            int y = 7;
            int cellWidth = size.Width / x;
            int cellHeight = size.Height / y;

            // Draw the vertical lines of the table
            for (int i = 1; i < x; i++)
            {
                int xPos = i * cellWidth;
                g.DrawLine(Pens.Black, xPos, 0, xPos, size.Height);
            }

            // Draw the horizontal lines of the table
            for (int i = 1; i < y; i++)
            {
                int yPos = i * cellHeight;
                g.DrawLine(Pens.Black, 0, yPos, size.Width, yPos);
            }

            for (int i = 0; i < 7; i++)
            {
                Label label = new Label();
                label.Text = daysofweek[i].Substring(0, 3).ToUpper();
                label.Font = new Font(label.Font.FontFamily, 11);
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Name = "Label " + (i).ToString();
                label.BackColor = Color.Transparent;
                label.Size = new Size(buttonWidth, buttonHeight);
                label.Location = new Point(i * buttonWidth, 0);
                panel1.Controls.Add(label);
            }
        }

        public void Createnotes(object sender, EventArgs e)
        {

            for (int i = 0; i < 2; i++)
            {
                foreach (Control control in timetablePanel.Controls)
                {
                    if (control is Panel && control.Height != 1 && control.Width != 1)
                    {
                        timetablePanel.Controls.Remove(control);
                    }
                }
            }

            int dayy = 1;
            Button clickedButton = (Button)sender;
            string buttonText = clickedButton.Text;
            comboBox1down.SelectedIndex = 24;

            int startingPosition = 24 * labelHeight;

            scrollbar.Value = startingPosition;
            timetablePanel.Location = new Point(this.ClientSize.Width / 2, -scrollbar.Value);


            for (int i = 0; i < 31; i++)
            { 
                if (buttonText == i.ToString())
                {
                    dayy = i;
                    // do something with the day index...
                    break; // exit the loop once the correct button is found
                }
            }
            dayshown = dayy;

            foreach (Button button in panel1.Controls.OfType<Button>())
            {
                if (button.Name == "Button " + dayy.ToString()) { button.BackColor = Color.LightCoral; }
                else { button.BackColor = Color.MidnightBlue; }
            }

            DateTime curr = new DateTime(yearshown,monthshown,dayy);

            if (curr.Year >= 2023)
            {
                int year = curr.Year; // example year
                int month = curr.Month; // example month
                int day = curr.Day; // example day

                DateTime startDate = new DateTime(2023, 1, 1); // start date of your data
                DateTime currentDate = new DateTime(year, month, day); // current date

                TimeSpan diff = currentDate - startDate;
                int daysSinceStart = diff.Days; // number of days since start date

                lineNumber = daysSinceStart; // add 1 to a
            }
            else
            {
                int year = curr.Year; // example year
                int month = curr.Month; // example month
                int day = curr.Day; // example day
                DateTime startDate = new DateTime(2022, 12, 31); // start date of your data
                DateTime currentDate = new DateTime(year, month, day); // current date
                DateTime baseDate1 = new DateTime(2023, 1, 1);
                DateTime baseDate2 = new DateTime(2045, 12, 31);
                TimeSpan diff = currentDate - startDate;
                TimeSpan diffbase = baseDate2 - baseDate1;
                int daysbase = diffbase.Days;
                int daysSinceStart = Math.Abs(diff.Days);
                lineNumber = daysSinceStart + daysbase + 1; // add 1 to a
            }

            string[] lines = System.IO.File.ReadAllLines("C:\\Users\\pango\\Documents\\GitHub\\Csharp\\MyCalendar\\datestodata.txt");
            string myLine = lines[lineNumber];

            List<List<string>> notelist = new List<List<string>>();
            string[] parts = lines[lineNumber].Split(',');
            List<string> currentList = null;


            // Ignore the first 3 elements
            int startIndex = 3;

            // Divide the remaining elements into groups of 4
            int elementsPerGroup = 4;
            for (int i = startIndex; i < parts.Length; i += elementsPerGroup)
            {
                // Create a new List<string> object for each group of 4 elements
                List<string> groupList = new List<string>();

                // Add the 4 elements to the current list
                for (int j = i; j < i + elementsPerGroup && j < parts.Length; j++)
                {
                    groupList.Add(parts[j]);
                }

                // Add the current list to the list of lists
                notelist.Add(groupList);
            }


            if (notelist.Count != 0 && notelist[0].Count >= 3)
            {
                notelist = notelist.OrderBy(lst => priofnotes[lst[2]]).ToList();
                int columnIndex = 0; // The column index of the datetimes

                DateTime minDateTime = notelist
                    .Where(row => row.Count > columnIndex) // Only consider rows with a value in the specified column
                    .Select(row => DateTime.Parse(row[columnIndex])) // Convert the string to a DateTime
                    .Min(); // Find the minimum value

                string minTimeString = minDateTime.ToString("HH:mm");
                DateTime dt00 = DateTime.ParseExact("00:00", "HH:mm", CultureInfo.InvariantCulture);
                DateTime dt11 = DateTime.ParseExact(minTimeString, "HH:mm", CultureInfo.InvariantCulture);
                TimeSpan dt1100 = dt11 - dt00;
                if ((int)(dt1100.TotalMinutes / 15) * labelHeight < scrollbar.Maximum) { scrollbar.Value = (int)(dt1100.TotalMinutes / 15) * labelHeight; }
                else { scrollbar.Value = scrollbar.Maximum; }

                if (clickedButton.Name.Contains("submit"))
                {
                    string startime = clickedButton.Name.Substring(6);
                    Debug.WriteLine(startime);
                    DateTime dt0 = DateTime.ParseExact("00:00", "HH:mm", CultureInfo.InvariantCulture);
                    DateTime dt1 = DateTime.ParseExact(startime, "HH:mm", CultureInfo.InvariantCulture);
                    TimeSpan dt2 = dt1 - dt0;
                    if ((int)(dt2.TotalMinutes / 15) * labelHeight < scrollbar.Maximum) { scrollbar.Value = (int)(dt2.TotalMinutes / 15) * labelHeight; }
                    else { scrollbar.Value = scrollbar.Maximum; }
                }
                DateTime date = new DateTime(yearshown, monthshown, dayy);
                if (date.Date == DateTime.Today)
                {
                    TimeSpan time = DateTime.Now.TimeOfDay;  // get the current time
                    int closest15 = (int)Math.Round(time.TotalMinutes / 15.0) * 15;  // calculate the closest 15 minute interval
                    int intervalsPassed = closest15 / 15;
                    //scrollbar.Value = intervalsPassed * labelHeight;
                }

                timetablePanel.Location = new Point(this.ClientSize.Width / 2, -scrollbar.Value);
            
                int notepanelwidth = 40;
                int maxnotesperday = 8;
                for (int i = 0; i < Math.Min(notelist.Count, maxnotesperday); i++)
                {
                    DateTime dt0 = DateTime.ParseExact("00:00", "HH:mm", CultureInfo.InvariantCulture);
                    DateTime dt1 = DateTime.ParseExact(notelist[i][0], "HH:mm", CultureInfo.InvariantCulture);
                    DateTime dt2 = DateTime.ParseExact(notelist[i][1], "HH:mm", CultureInfo.InvariantCulture);
                    TimeSpan duration1 = dt1 - dt0;
                    TimeSpan duration2 = dt2 - dt0;
                    int numIntervals1 = (int)(duration1.TotalMinutes / 15);
                    int numIntervals2 = (int)(duration2.TotalMinutes / 15);
                    Panel myPanel = new Panel();
                    // Set the panel size and location
                    myPanel.Size = new Size(notepanelwidth, (numIntervals2 - numIntervals1 + 1) * labelHeight);
                    myPanel.Location = new Point(40 + i * (5 + notepanelwidth), 7 + numIntervals1 * labelHeight);

                    myPanel.BackColor = noteCategories[notelist[i][2]];

                    // Set the panel's text and font
                    //myPanel.Text = notelist[i][3];
                    myPanel.Tag = notelist[i][2];
                    myPanel.Font = new Font("Arial", 12);
                    myPanel.Text = notelist[i][3];
                    myPanel.Name = "Panel" + i.ToString();

                    // Attach the MouseEnter and MouseLeave event handlers
                    myPanel.MouseEnter += new EventHandler(panel_MouseEnter);
                    myPanel.MouseLeave += new EventHandler(panel_MouseLeave);

                    Button deleteButton = new Button();
                    deleteButton.Size = new Size(18, 20);
                    deleteButton.Name = "Detele" + i.ToString();
                    deleteButton.Location = new Point(myPanel.Width - deleteButton.Width, 0); // 5 pixels from the top right corner
                    deleteButton.Font = new Font("Arial", 8);
                    deleteButton.Font = new Font(deleteButton.Font, FontStyle.Bold);
                    deleteButton.Text = "X";
                    deleteButton.TextAlign = ContentAlignment.MiddleCenter;
                    deleteButton.Padding = new Padding(1, 0, 0, 0);
                    deleteButton.Click += new EventHandler(deletepanel); // Dispose the panel when the delete button is clicked

                    myPanel.Controls.Add(deleteButton);
                    identifier++;

                    // Add the panel to a form or another container
                    timetablePanel.Controls.Add(myPanel);
                    myPanel.BringToFront();
                }
            }

            int count = 0;
            foreach (Control control in timetablePanel.Controls)
            {
                if (control is Panel && control.Height != 1 && control.Width != 1)
                {
                    count++;
                    Panel childPanel = (Panel)control;
                    // Access the properties of childPanel here
                    Console.WriteLine(childPanel.Name);
                    Console.WriteLine(childPanel.Size);
                    // etc.
                }
            }

        }
        public void Createbuttonsandnotes(DateTime curr)
        {

            for (int i=0; i<2;  i++)
            {
                foreach (Control control in panel1.Controls)
                {
                    if (control is Button)
                    {
                        panel1.Controls.Remove(control);
                    }
                }
            }

            int startingPosition = 24 * labelHeight;
            comboBox1down.SelectedIndex = 24;
            scrollbar.Value = startingPosition;
            timetablePanel.Location = new Point(this.ClientSize.Width / 2, -scrollbar.Value);

            var firstday = DateTimeDayOfMonthExtensions.FirstDayOfMonth(curr).DayOfWeek.ToString();
            var monthlength = DateTimeDayOfMonthExtensions.DaysInMonth(curr);
            int firstdayindex = Array.IndexOf(daysofweek, firstday);
            dayshown = 1;
            for (int i = firstdayindex; i < firstdayindex + monthlength; i++)
            {

                Button button = new Button();
                button.Text = "";
                button.Text = (i + 1 - firstdayindex).ToString();
                button.BackColor = Color.Navy;
                button.ForeColor = Color.GhostWhite;
                if (i + 1 - firstdayindex == DateTime.Today.Day && curr.Month == DateTime.Today.Month && curr.Year == DateTime.Today.Year)
                {
                    button.BackColor = Color.CornflowerBlue;
                }
                button.Name = "Button " + (i + 1 - firstdayindex).ToString();
                button.Size = new Size(buttonWidth, buttonHeight);
                button.Click += (sender, e) =>
                {
                    Createnotes(sender, e);
                };
                button.Location = new Point((i % buttonperrow) * buttonWidth, (1+(int)Math.Floor((decimal)i / buttonperrow)) * buttonHeight);

                if (panel1 != null)
                {
                    panel1.Controls.Add(button);
                }
            }


        }
    }
}
