using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Conversive.Verbot5;
using System.Speech;
using System.Speech.Synthesis;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Linq;

namespace VerbotWindowsApplicationSample
{
    class SuggestedPhone
    {
        SpeechSynthesizer reader = new SpeechSynthesizer();
        Form formMain;
        public SuggestedPhone(Form form, int id, string name, bool isItRandom)
        {
            formMain = form;

            int intWidth = (formMain.Size.Width - 800) / 2;
            int intHeight = (formMain.Size.Height - 230) / 2;

            // panelLogin

            string strSpeech = "Horey! I found what you need! Your phone is. " + name.ToString();
            reader.Dispose();
            reader = new SpeechSynthesizer();
            reader.SpeakAsync(strSpeech);

            Panel panelContent = new Panel();
            panelContent.Dock = DockStyle.Fill;
            panelContent.Name = "panelContent";
            panelContent.TabIndex = 2;
            panelContent.Controls.Clear();


            // labelHello

            Label labelName = new Label();
            labelName.Size = new Size(1366, 60);
            labelName.TextAlign = ContentAlignment.MiddleCenter;
            labelName.BackColor = Color.Transparent;
            labelName.ForeColor = Color.Black;
            labelName.Font = new System.Drawing.Font("Century Gothic", 30, System.Drawing.FontStyle.Bold);
            labelName.Location = new Point(0, 5);
            labelName.Name = "labelName";
            labelName.TabIndex = 0;
            if (isItRandom == true)
            {
                labelName.Text = "Maybe you like this phone: " + name.ToString();
            }
            else
            {
                labelName.Text = "Sugessted phone: " + name.ToString();
            }


            // pictureBoxLoginBorder

            PictureBox pictureBoxPhone= new PictureBox();
            //          pictureBoxPhone.BackgroundImage = Properties.Resources.img_txt_230_black;
            pictureBoxPhone.BackColor = Color.White;
            pictureBoxPhone.Location = new Point(283, 50);
            pictureBoxPhone.Name = "pictureBoxLoginBorder";
            pictureBoxPhone.Size = new Size(800, 600);
            pictureBoxPhone.TabIndex = 4;
            pictureBoxPhone.TabStop = false;
            pictureBoxPhone.Cursor = Cursors.IBeam;

    //        MessageBox.Show(id.ToString());
            if (id == 1)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img1;
            }
            else if (id == 2)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img2;
            }
            else if (id == 3)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img3;
            }
            else if (id == 4)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img4;
            }
            else if (id == 5)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img5;
            }
            else if (id == 6)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img6;
            }
            else if (id == 7)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img7;
            }
            else if (id == 8)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img8;
            }
            else if (id == 9)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img9;
            }
            else if (id == 10)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img10;
            }
            else if (id == 11)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img11;
            }
            else if (id == 12)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img12;
            }
            else if (id == 13)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img13;
            }
            else if (id == 14)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img14;
            }
            else if (id == 15)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img15;
            }
            else if (id == 16)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img16;
            }
            else if (id == 17)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img17;
            }
            else if (id == 18)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img18;
            }
            else if (id == 19)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img19;
            }
            else if (id == 20)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img20;
            }
            else if (id == 21)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img21;
            }
            else if (id == 22)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img22;
            }
            else if (id == 23)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img23;
            }
            else if (id == 24)
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img24;
            }
            else
            {
                pictureBoxPhone.BackgroundImage = Properties.Resources.img25;
            }
            //     pictureBoxPhone.Click += new EventHandler(pictureBoxPhone_Click);




            panelContent.Controls.Add(labelName);
            panelContent.Controls.Add(pictureBoxPhone);



            var panelWebbrowser = formMain.Controls.Find("panelWebbrowser", true).FirstOrDefault();
            formMain.Controls.Add(panelContent);
            panelWebbrowser.Controls.Add(panelContent);
         //   panelWebbrowser.Controls.Clear();
            Point pointPanel = panelContent.PointToScreen(new Point(0, 0));
            panelContent.Parent = panelWebbrowser;
            panelContent.Location = panelWebbrowser.PointToClient(pointPanel);
            panelContent.BringToFront();



            DialogResult result = MessageBox.Show("Thank you for using Mobile Adviser Machine!\nWould you like search it from Lazada? ", "Would you like search it from Lazada?",

            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string strURL = "https://www.lazada.com.my/catalog/?q=" + name.ToString();
                System.Diagnostics.Process.Start(strURL.ToString());
                Application.Exit();
            }
            else if (result == DialogResult.No)
            {
                Application.Restart();
            }
        }
    }
}
