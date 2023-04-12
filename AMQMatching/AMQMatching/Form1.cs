using AMQMatching;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AMQMatcher
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.Form1_Load);
        }
        public string[] artistsarray { get; set; }
        public string[] songsarray { get; set; }
        public string[] animearray { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection autoCompleteCollection1 = new AutoCompleteStringCollection();
            autoCompleteCollection1.AddRange(artistsarray);
            textBox1.AutoCompleteCustomSource = autoCompleteCollection1;

            textBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection autoCompleteCollection2 = new AutoCompleteStringCollection();
            autoCompleteCollection2.AddRange(songsarray);
            textBox2.AutoCompleteCustomSource = autoCompleteCollection2;

            textBox3.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection autoCompleteCollection3 = new AutoCompleteStringCollection();
            autoCompleteCollection3.AddRange(animearray);
            textBox3.AutoCompleteCustomSource = autoCompleteCollection3;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string textToCopy1 = textBox3.Text;
            Clipboard.SetText(textToCopy1);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string textToCopy2 = textBox2.Text;
            Clipboard.SetText(textToCopy2);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string textToCopy3 = textBox1.Text;
            Clipboard.SetText(textToCopy3);
        }
    }

}
