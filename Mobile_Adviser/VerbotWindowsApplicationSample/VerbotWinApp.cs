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

namespace VerbotWindowsApplicationSample
{
    public class VerbotWinApp : Form
	{
        bool isItRandom = false;
        string strOS = "";
        int intPhoneID = 0;
        string name = "";
        string strQuery = "";
        bool isFirstPage = true;
        bool customerDetected = false;
        Verbot5Engine verbot;
		State state;
		string stCKBFileFilter = "Compiled Verbot Knowledge Bases (*.ckb)|*.ckb";
		string stFormName = "Verbot SDK Windows App Sample";
		private Panel panelTop;
		private Splitter mainSplitter;
        private IContainer components;

        string[] arrayPhoneID = new string[0];
        string[] arrayPhoneName = new string[0];
        string[] arrayPhoneCompanyID = new string[0];
        string[] arrayPhoneDescription = new string[0];

        string[] arrayCompanyID = new string[0];
        string[] arrayCompanyName = new string[0];
        string[] arrayCompanyImage = new string[0];
        string[] arrayCompanyDescription = new string[0];

        string[] arrayQuestionID = new string[0];
        string[] arrayQuestionTitle = new string[0];
        string[] arrayQuestionAnswer = new string[0];
        string[] arrayQuestionQuery = new string[0];

        string[] arrayQuestions = new string[30];
        string[] arrayAnswers = new string[30];

        int intQuestionCounter = 0;
        string stringBotName = "John";
        string stringUserName = "User";

        MySqlConnection sqlConnection = new MySqlConnection();

        private Panel panelBottom;
        private Panel panelCenter;
        private Panel panelWebbrowser;
        private Panel panelOutputText;
        public TextBox outputTextBox;
        private Panel panelInputText;
        private Panel panelText;
        private TextBox inputTextBox;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Panel panelSend;
        private PictureBox btnSend;
        public System.IO.Ports.SerialPort serialPort3;
        public Timer timer1;
        private WebBrowser webBrowser1;


        public void MySqlLoader()
        {
            System.Diagnostics.Process.Start("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 8");
            //string strDatabaseName = "mobileadvisordb";
            sqlConnection.ConnectionString = "datasource=localhost;port=3306;username=root;password=;";

            try
            {
                int intCountPhones = 0;
                sqlConnection.Open();
                string strQuery = "SELECT * FROM mobileadvisordb.smartphones";
                MySqlCommand sqlCommand = new MySqlCommand(strQuery, sqlConnection);
                MySqlDataReader sqlReader = sqlCommand.ExecuteReader();
                while (sqlReader.Read())
                {
                    Array.Resize(ref arrayPhoneID, intCountPhones + 1);
                    Array.Resize(ref arrayPhoneName, intCountPhones + 1);
                    Array.Resize(ref arrayPhoneCompanyID, intCountPhones + 1);
                    Array.Resize(ref arrayPhoneDescription, intCountPhones + 1);

                    arrayPhoneID[intCountPhones] = sqlReader["id"].ToString();
                    arrayPhoneName[intCountPhones] = sqlReader["name"].ToString();
                    arrayPhoneCompanyID[intCountPhones] = sqlReader["company_id"].ToString();
                    arrayPhoneDescription[intCountPhones] = sqlReader["desc"].ToString();

                    intCountPhones++;
                }
            }
            catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message);
            }
            sqlConnection.Close();


            try
            {
                int intCountCompany = 0;
                sqlConnection.Open();
                string strQuery = "SELECT * FROM mobileadvisordb.companies";
                MySqlCommand sqlCommand = new MySqlCommand(strQuery, sqlConnection);
                MySqlDataReader sqlReader = sqlCommand.ExecuteReader();
                while (sqlReader.Read())
                {
                    Array.Resize(ref arrayCompanyID, intCountCompany + 1);
                    Array.Resize(ref arrayCompanyName, intCountCompany + 1);
                    Array.Resize(ref arrayCompanyImage, intCountCompany + 1);
                    Array.Resize(ref arrayCompanyDescription, intCountCompany + 1);

                    arrayCompanyID[intCountCompany] = sqlReader["id"].ToString();
                    arrayCompanyName[intCountCompany] = sqlReader["name"].ToString();
                    arrayCompanyImage[intCountCompany] = sqlReader["image"].ToString();
                    arrayCompanyDescription[intCountCompany] = sqlReader["description"].ToString();

                    intCountCompany++;
                }
            }
            catch (Exception ex)
            {
             //   MessageBox.Show(ex.Message);
            }
            sqlConnection.Close();


            try
            {
                int intCountQuestion = 0;
                sqlConnection.Open();
                string strQuery = "SELECT * FROM mobileadvisordb.questions";
                MySqlCommand sqlCommand = new MySqlCommand(strQuery, sqlConnection);
                MySqlDataReader sqlReader = sqlCommand.ExecuteReader();
                while (sqlReader.Read())
                {
                    Array.Resize(ref arrayQuestionID, intCountQuestion + 1);
                    Array.Resize(ref arrayQuestionTitle, intCountQuestion + 1);
                    Array.Resize(ref arrayQuestionAnswer, intCountQuestion + 1);
                    Array.Resize(ref arrayQuestionQuery, intCountQuestion + 1);

                    arrayQuestionID[intCountQuestion] = sqlReader["id"].ToString();
                    arrayQuestionTitle[intCountQuestion] = sqlReader["question_title"].ToString();
                    arrayQuestionAnswer[intCountQuestion] = sqlReader["question_answer"].ToString();
                    arrayQuestionQuery[intCountQuestion] = sqlReader["query"].ToString();

                    intCountQuestion++;
                }
            }
            catch (Exception ex)
            {
             //   MessageBox.Show(ex.Message);
            }
            sqlConnection.Close();

        }

        void ItemGetter()
        {
            try
            {
                sqlConnection.Open();
                string Query = "SELECT * FROM mobileadvisordb.smartphones_features WHERE " + strQuery + "";
                MySqlCommand sqlCommand = new MySqlCommand(Query, sqlConnection);
                MySqlDataReader sqlReader = sqlCommand.ExecuteReader();
                while (sqlReader.Read())
                {

                    intPhoneID = (int)sqlReader["smartphone_id"];
                    
                }

       //         MessageBox.Show(Query.ToString());
            }
            catch (Exception ex)
            {
            //    MessageBox.Show(ex.Message);
            }
            sqlConnection.Close();

            ShowPhone(intPhoneID);
        }

        public void CallSuggestedPhone()
        {
            SuggestedPhone SuggestedPhone = new SuggestedPhone(this, intPhoneID, name, isItRandom);
        }

        void ShowPhone(int intID)
        {
            if (intID == 0)
            {
                isItRandom = true;

                Random rnd = new Random();
                intID = rnd.Next(1, 21);

                int[] aArray = { 2, 3, 4, 5, 6, 7, 8, 10, 12, 13, 14, 15, 16, 17, 18, 20, 21};
                if (strOS == "a")
                {intID = aArray[rnd.Next(0, 16)];}

                int[] iArray = { 1, 9, 11, 19, };
                if (strOS == "i")
                { intID = iArray[rnd.Next(0, 3)]; }

                if (strOS == "w")
                {intID = 12;}

                if (strOS == "b")
                { intID = 13; }
            }

            try
            {
                sqlConnection.Open();
                string Query4 = "SELECT * FROM mobileadvisordb.smartphones WHERE id=" + intID + "";
                MySqlCommand sqlCommand = new MySqlCommand(Query4, sqlConnection);
                MySqlDataReader sqlReader = sqlCommand.ExecuteReader();
                while (sqlReader.Read())
                {
                    name = sqlReader["name"].ToString();
                    intPhoneID = (int)sqlReader["id"];
                    // MessageBox.Show(name.ToString());
                }
            }
            catch (Exception ex)
            {
            //    MessageBox.Show(ex.Message);
            }
            sqlConnection.Close();

            CallSuggestedPhone();
        }

        public VerbotWinApp()
        {
            InitializeComponent();
            serialPort3.Open();
            this.verbot = new Verbot5Engine();
            this.state = new State();
            this.verbot.AddCompiledKnowledgeBase(@"C:\Users\Admin\Desktop\Verbot\Mobile_Adviser\KnowledgeBase\old\mobileAdviser.ckb");
            this.state.CurrentKBs.Clear();
            this.state.CurrentKBs.Add(@"C:\Users\Admin\Desktop\Verbot\Mobile_Adviser\KnowledgeBase\old\mobileAdviser.ckb");
            this.Text = this.stFormName;

            MySqlLoader();
            void_start();
        }

        public void void_start()
        {
            string stringText = "Let me know your name please?";
            outputTextBox.Text = stringBotName + ": " + stringText;
        }

        protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.panelTop = new System.Windows.Forms.Panel();
            this.mainSplitter = new System.Windows.Forms.Splitter();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panelCenter = new System.Windows.Forms.Panel();
            this.panelWebbrowser = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.panelOutputText = new System.Windows.Forms.Panel();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.panelInputText = new System.Windows.Forms.Panel();
            this.panelText = new System.Windows.Forms.Panel();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelSend = new System.Windows.Forms.Panel();
            this.btnSend = new System.Windows.Forms.PictureBox();
            this.serialPort3 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelCenter.SuspendLayout();
            this.panelWebbrowser.SuspendLayout();
            this.panelOutputText.SuspendLayout();
            this.panelInputText.SuspendLayout();
            this.panelText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelSend.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSend)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.White;
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(800, 50);
            this.panelTop.TabIndex = 1;
            // 
            // mainSplitter
            // 
            this.mainSplitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.mainSplitter.Location = new System.Drawing.Point(0, 50);
            this.mainSplitter.Name = "mainSplitter";
            this.mainSplitter.Size = new System.Drawing.Size(800, 3);
            this.mainSplitter.TabIndex = 2;
            this.mainSplitter.TabStop = false;
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.White;
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 350);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(800, 50);
            this.panelBottom.TabIndex = 11;
            // 
            // panelCenter
            // 
            this.panelCenter.BackColor = System.Drawing.Color.White;
            this.panelCenter.Controls.Add(this.panelWebbrowser);
            this.panelCenter.Controls.Add(this.panelOutputText);
            this.panelCenter.Controls.Add(this.panelInputText);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Location = new System.Drawing.Point(0, 53);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(800, 297);
            this.panelCenter.TabIndex = 12;
            // 
            // panelWebbrowser
            // 
            this.panelWebbrowser.Controls.Add(this.webBrowser1);
            this.panelWebbrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWebbrowser.Location = new System.Drawing.Point(0, 0);
            this.panelWebbrowser.Name = "panelWebbrowser";
            this.panelWebbrowser.Size = new System.Drawing.Size(800, 62);
            this.panelWebbrowser.TabIndex = 10;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(800, 62);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new System.Uri("http://127.0.0.1/index.html", System.UriKind.Absolute);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // panelOutputText
            // 
            this.panelOutputText.Controls.Add(this.outputTextBox);
            this.panelOutputText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelOutputText.Location = new System.Drawing.Point(0, 62);
            this.panelOutputText.Name = "panelOutputText";
            this.panelOutputText.Size = new System.Drawing.Size(800, 200);
            this.panelOutputText.TabIndex = 9;
            // 
            // outputTextBox
            // 
            this.outputTextBox.BackColor = System.Drawing.Color.White;
            this.outputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.outputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputTextBox.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputTextBox.ForeColor = System.Drawing.Color.Navy;
            this.outputTextBox.Location = new System.Drawing.Point(0, 0);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputTextBox.Size = new System.Drawing.Size(800, 200);
            this.outputTextBox.TabIndex = 4;
            // 
            // panelInputText
            // 
            this.panelInputText.Controls.Add(this.panelText);
            this.panelInputText.Controls.Add(this.panelSend);
            this.panelInputText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelInputText.Location = new System.Drawing.Point(0, 262);
            this.panelInputText.Name = "panelInputText";
            this.panelInputText.Size = new System.Drawing.Size(800, 35);
            this.panelInputText.TabIndex = 8;
            // 
            // panelText
            // 
            this.panelText.Controls.Add(this.inputTextBox);
            this.panelText.Controls.Add(this.pictureBox3);
            this.panelText.Controls.Add(this.pictureBox2);
            this.panelText.Controls.Add(this.pictureBox1);
            this.panelText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelText.Location = new System.Drawing.Point(0, 0);
            this.panelText.Name = "panelText";
            this.panelText.Size = new System.Drawing.Size(710, 35);
            this.panelText.TabIndex = 9;
            // 
            // inputTextBox
            // 
            this.inputTextBox.BackColor = System.Drawing.Color.White;
            this.inputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.inputTextBox.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputTextBox.Location = new System.Drawing.Point(15, 7);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(600, 20);
            this.inputTextBox.TabIndex = 3;
            this.inputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.getReplyButton_KeyDown);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.White;
            this.pictureBox3.BackgroundImage = global::VerbotWindowsApplicationSample.Properties.Resources.txt_center;
            this.pictureBox3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox3.Location = new System.Drawing.Point(35, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(640, 35);
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click_1);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.BackgroundImage = global::VerbotWindowsApplicationSample.Properties.Resources.txt_right;
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox2.Location = new System.Drawing.Point(675, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(35, 35);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BackgroundImage = global::VerbotWindowsApplicationSample.Properties.Resources.txt_left;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 35);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panelSend
            // 
            this.panelSend.Controls.Add(this.btnSend);
            this.panelSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSend.Location = new System.Drawing.Point(710, 0);
            this.panelSend.Name = "panelSend";
            this.panelSend.Size = new System.Drawing.Size(90, 35);
            this.panelSend.TabIndex = 3;
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.White;
            this.btnSend.BackgroundImage = global::VerbotWindowsApplicationSample.Properties.Resources.btn_send;
            this.btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSend.Location = new System.Drawing.Point(0, 0);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(90, 35);
            this.btnSend.TabIndex = 0;
            this.btnSend.TabStop = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // serialPort3
            // 
            this.serialPort3.PortName = "COM6";
            this.serialPort3.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort3_DataReceived);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // VerbotWinApp
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.ControlBox = false;
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.mainSplitter);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.Name = "VerbotWinApp";
            this.TransparencyKey = System.Drawing.Color.White;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelCenter.ResumeLayout(false);
            this.panelWebbrowser.ResumeLayout(false);
            this.panelOutputText.ResumeLayout(false);
            this.panelOutputText.PerformLayout();
            this.panelInputText.ResumeLayout(false);
            this.panelText.ResumeLayout(false);
            this.panelText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelSend.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnSend)).EndInit();
            this.ResumeLayout(false);

        }

		[STAThread]
		static void Main() 
		{

            Application.Run(new VerbotWinApp());

        }




        private void inputTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar == '\r')
			{
				e.Handled = true;
				this.getReply();
			}
		}

        SpeechSynthesizer reader = new SpeechSynthesizer();
        public void getReply()
        {
            int intGetInteger = int.Parse(Regex.Replace((inputTextBox.Text + "0"), "[^0-9]+", string.Empty)) / 10;
            string stInput = this.inputTextBox.Text.Trim();
            Reply reply = this.verbot.GetReply(stInput, this.state);
            if (reply != null)
            {
                isFirstPage = false;
                if ((inputTextBox.Text.Length == 0) && (intQuestionCounter != 11))
                {
                    reader.Dispose();
                    reader = new SpeechSynthesizer();

                    reader.SpeakAsync("You did not answered, " + stringUserName + ". " + arrayQuestionTitle[intQuestionCounter] + " ");
                    outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + "You did not answered, " + stringUserName + ". ";
                    outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                    parseEmbeddedOutputCommands(reply.AgentText);
                    runProgram(reply.Cmd);
                    arrayAnswers[intQuestionCounter] = "id > 0";
                    intQuestionCounter++;
                    inputTextBox.Text = "";
                }


                if ((intQuestionCounter == 0) && (inputTextBox.TextLength > 0))
                {
                    stringUserName = inputTextBox.Text;
                    reader.Dispose();
                    reader = new SpeechSynthesizer();
                    reader.SpeakAsync("Nice name, " + stringUserName + ". And " + arrayQuestionTitle[intQuestionCounter]);
                    outputTextBox.Text = outputTextBox.Text + Environment.NewLine + "User: " + stringUserName;
                    outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": Nice name, " + stringUserName + "!";
                    outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                    arrayAnswers[intQuestionCounter] = "id > 0";
                    intQuestionCounter++;
                    inputTextBox.Text = "";
                }

                if ((intQuestionCounter == 1) && (inputTextBox.TextLength > 0))
                {
                    switch (inputTextBox.Text.ToUpper())
                    {
                        case "1":
                        case "ANDROID":
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync("Android? Nice operating system! " + arrayQuestionTitle[intQuestionCounter]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringUserName + ": " + inputTextBox.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + "Android. Nice operating system.";
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "os=1";
                            intQuestionCounter++;
                            inputTextBox.Text = "";
                            strOS = "a";
                            break;
                        case "2":
                        case "IOS":
                        case "APPLE":
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync("Wow. IO S! Your choice is best! " + arrayQuestionTitle[intQuestionCounter]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringUserName + ": " + inputTextBox.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + "Wow. IOS. Your choice is best!";
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "os=2";
                            intQuestionCounter++;
                            inputTextBox.Text = "";
                            strOS = "i";
                            break;
                        case "3":
                        case "WINDOWSPHONE":
                        case "WIN":
                        case "WINDOWS":
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync("Windows Phone. Better than Symbian OS! " + arrayQuestionTitle[intQuestionCounter]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringUserName + ": " + inputTextBox.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + "Windows Phone. Better than Symbian OS.";
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "os=3";
                            intQuestionCounter++;
                            inputTextBox.Text = "";
                            strOS = "w";
                            break;
                        case "4":
                        case "BLACKBERRY":
                        case "BLACK":
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync("Blackberry. I am sure you like it! " + arrayQuestionTitle[intQuestionCounter]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringUserName + ": " + inputTextBox.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + "Blackberry. I am sure you like it.";
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "os=4";
                            intQuestionCounter++;
                            inputTextBox.Text = "";
                            strOS = "b";
                            break;
                        default:
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync(reply.Text + ". " + arrayQuestionTitle[intQuestionCounter-1]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + reply.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter-1];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "id > 0";
                            inputTextBox.Text = "";
                            //getReply();
                            break;
                    }
                }

                if ((intQuestionCounter == 2) && (inputTextBox.TextLength > 0))
                {
                    switch (inputTextBox.Text.ToUpper())
                    {
                        case "1":
                        case "METAL":
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync("Metal? Metal is always better than plastic! " + arrayQuestionTitle[intQuestionCounter]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringUserName + ": " + inputTextBox.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + "Metal? Metal is always better than plastic! ";
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "chassis=1";
                            intQuestionCounter++;
                            inputTextBox.Text = "";
                            break;
                        case "2":
                        case "PLASTIC":
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync("Plastic? Nowadays most phones are plastic. " + arrayQuestionTitle[intQuestionCounter]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringUserName + ": " + inputTextBox.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + "Plastic? Nowadays most phones are plastic.";
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "chassis=2";
                            intQuestionCounter++;
                            inputTextBox.Text = "";
                            break;
                        case "3":
                        case "I DON'T MIND":
                        case "I DONT MIND":
                        case "I DO NOT MIND":
                        case "I DONOT MIND":
                        case "I DON T MIND":
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync("Okey! You know it is just material. " + arrayQuestionTitle[intQuestionCounter]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringUserName + ": " + inputTextBox.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + "Okey! You know it is just material.";
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "id>1";
                            intQuestionCounter++;
                            inputTextBox.Text = "";
                            break;
                        default:
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync(reply.Text + ". " + arrayQuestionTitle[intQuestionCounter-1]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + reply.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter-1];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "id > 0";
                            inputTextBox.Text = "";
                            //getReply();
                            break;
                    }
                }

                if ((intQuestionCounter == 3) && (inputTextBox.TextLength > 0))
                {
                    switch (intGetInteger)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync(intGetInteger.ToString() + " inch! This one is small screen. " + arrayQuestionTitle[intQuestionCounter]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringUserName + ": " + inputTextBox.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + intGetInteger.ToString() + " inch! This one is small screen.";
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "screen_size<5";
                            intQuestionCounter++;
                            inputTextBox.Text = "";
                            break;
                        case 5:
                        case 6:
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync(intGetInteger.ToString() + " inch! This one is medium screen. " + arrayQuestionTitle[intQuestionCounter]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringUserName + ": " + inputTextBox.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + intGetInteger.ToString() + " inch! This one is medium screen.";
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "screen_size>4 AND screen_size<7";
                            intQuestionCounter++;
                            inputTextBox.Text = "";
                            break;
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync(intGetInteger.ToString() + " inch! Wow! You like large one. " + arrayQuestionTitle[intQuestionCounter]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringUserName + ": " + inputTextBox.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + intGetInteger.ToString() + " inch! Wow! You like large one. ";
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "screen_size>6";
                            intQuestionCounter++;
                            inputTextBox.Text = "";
                            break;
                        default:
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync(reply.Text + ". " + arrayQuestionTitle[intQuestionCounter-1]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + reply.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter-1];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "id > 0";
                            inputTextBox.Text = "";
                            //getReply();
                            break;
                    }
                }

                if ((intQuestionCounter == 4) && (inputTextBox.TextLength > 0))
                {
                    switch (inputTextBox.Text.ToUpper())
                    {
                        case "1":
                        case "TOUCH":
                        case "TOUCH SCREEN":
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync("Touch Screen! Great! Less buttons. " + arrayQuestionTitle[intQuestionCounter]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringUserName + ": " + inputTextBox.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + "Touch Screen! Great! Less buttons.";
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "touchscreen=1";
                            intQuestionCounter++;
                            inputTextBox.Text = "";
                            break;
                        case "2":
                        case "PHYSICAL KEYBOARD":
                        case "PHYSICAL":
                        case "KEYBOARD":
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync("Wow! Keyboard! Old fashion! You are kind of charismatic person. " + arrayQuestionTitle[intQuestionCounter]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringUserName + ": " + inputTextBox.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + "Wow! Keyboard! Old fashion! You are kind of charismatic person.";
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "physical_keyboard=1";
                            intQuestionCounter++;
                            inputTextBox.Text = "";
                            break;
                        default:
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync(reply.Text + ". " + arrayQuestionTitle[intQuestionCounter-1]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + reply.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter-1];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "id > 0";
                            inputTextBox.Text = "";
                            //getReply();
                            break;
                    }
                }

                if ((intQuestionCounter == 5) && (inputTextBox.TextLength > 0))
                {

                    if (intGetInteger > 0)
                    {
                        reader.Dispose();
                        reader = new SpeechSynthesizer();
                        reader.SpeakAsync(intGetInteger.ToString() + " USD! Wow! It is more than " + intGetInteger*4.3 + " Malaysian Ringgit! " + arrayQuestionTitle[intQuestionCounter]);
                        outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringUserName + ": " + inputTextBox.Text;
                        outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + intGetInteger.ToString() + " USD! Wow! It is more than " + intGetInteger * 4.3 + " Malaysian Ringgit! ";
                        outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                        parseEmbeddedOutputCommands(reply.AgentText);
                        runProgram(reply.Cmd);
                        arrayAnswers[intQuestionCounter] = "price<=" + intGetInteger.ToString();
                        intQuestionCounter++;
                        inputTextBox.Text = "";
                    }
                    else {
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync(reply.Text + ". " + arrayQuestionTitle[intQuestionCounter - 1]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + reply.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter - 1];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "id > 0";
                            inputTextBox.Text = "";
                            //getReply();
                    }
                }

                if ((intQuestionCounter == 6) && (inputTextBox.TextLength > 0))
                {
                    switch (intGetInteger)
                    {
                        case 4:
                        case 8:
                        case 16:
                        case 32:
                        case 64:
                        case 128:
                        case 256:
                            intGetInteger *= 1000;
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync(intGetInteger.ToString() + " MB! Great! It is enough. " + arrayQuestionTitle[intQuestionCounter]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringUserName + ": " + inputTextBox.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + intGetInteger.ToString() + " MB! Great! It is enough.";
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "internal_storage_size>" + intGetInteger.ToString();
                            intQuestionCounter++;
                            inputTextBox.Text = "";
                            break;
                        default:
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync(reply.Text + ". " + arrayQuestionTitle[intQuestionCounter - 1]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + reply.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter - 1];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "id > 0";
                            inputTextBox.Text = "";
                            //getReply();
                            break;
                    }
                }

                if ((intQuestionCounter == 7) && (inputTextBox.TextLength > 0))
                {
                    switch (inputTextBox.Text.ToUpper())
                    {
                        case "1":
                        case "INTERNAL":
                        case "INTERNAL ONLY":
                        case "ONLY INTERNAL":
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync("Internal storage also enough to store many files! " + arrayQuestionTitle[intQuestionCounter]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringUserName + ": " + inputTextBox.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + "Internal storage also enough to store many files!";
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "internal_storage=1";
                            intQuestionCounter++;
                            inputTextBox.Text = "";
                            break;
                        case "2":
                        case "EXTERNAL":
                        case "INTERNAL AND EXTERNAL":
                        case "EXTERNAL AND INTERNAL":
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync("It become too big! Great! " + arrayQuestionTitle[intQuestionCounter]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringUserName + ": " + inputTextBox.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + "It become too big! Great!";
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "external_storage=1";
                            intQuestionCounter++;
                            inputTextBox.Text = "";
                            break;
                        default:
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync(reply.Text + ". " + arrayQuestionTitle[intQuestionCounter - 1]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + reply.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter - 1];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "id > 0";
                            inputTextBox.Text = "";
                            //getReply();
                            break;
                    }
                }

                if ((intQuestionCounter == 8) && (inputTextBox.TextLength > 0))
                {
                    switch (inputTextBox.Text.ToUpper())
                    {
                        case "1":
                        case "YES":
                        case "REMOVABLE":
                        case "BATTERY REMOVABLE":
                        case "YES I DO":
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync("Nice! You can change your battery if you want! " + arrayQuestionTitle[intQuestionCounter]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringUserName + ": " + inputTextBox.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + "Nice! You can change your battery if you want!";
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "battery_removable=1";
                            intQuestionCounter++;
                            inputTextBox.Text = "";
                            break;
                        case "2":
                        case "NOT":
                        case "NO":
                        case "NOT REMOVABLE":
                        case "NOT BATTERY REMOVABLE":
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync("The quality of this battery is better than normal one. " + arrayQuestionTitle[intQuestionCounter]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringUserName + ": " + inputTextBox.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + "The quality of this battery is better than normal one.";
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "battery_removable=0";
                            intQuestionCounter++;
                            inputTextBox.Text = "";
                            break;
                        default:
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync(reply.Text + ". " + arrayQuestionTitle[intQuestionCounter - 1]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + reply.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter - 1];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "id > 0";
                            inputTextBox.Text = "";
                            //getReply();
                            break;
                    }
                }

                if ((intQuestionCounter == 9) && (inputTextBox.TextLength > 0))
                {
                    switch (inputTextBox.Text.ToUpper())
                    {
                        case "1":
                        case "YES":
                        case "GAMING":
                        case "GAMER":
                        case "YES I DO":
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync("Nice! GPU is important for gamers. " + arrayQuestionTitle[intQuestionCounter]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringUserName + ": " + inputTextBox.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + "Nice! GPU is important for gamers. ";
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "gpu=1 AND cores>=2";
                            intQuestionCounter++;
                            inputTextBox.Text = "";
                            break;
                        case "2":
                        case "NOT":
                        case "NO":
                        case "NO GAMER":
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync("Okey! So you are not a gamer! " + arrayQuestionTitle[intQuestionCounter]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringUserName + ": " + inputTextBox.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + "Okey! So you are not gamer!";
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "";
                            intQuestionCounter++;
                            inputTextBox.Text = "";
                            break;
                        default:
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync(reply.Text + ". " + arrayQuestionTitle[intQuestionCounter - 1]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + reply.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter - 1];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "id > 0";
                            inputTextBox.Text = "";
                            //getReply();
                            break;
                    }
                }

                if ((intQuestionCounter == 10) && (inputTextBox.TextLength > 0))
                {
                    switch (inputTextBox.Text.ToUpper())
                    {
                        case "1":
                        case "YES":
                        case "PHOTOGRAPHY":
                        case "PHOTO":
                        case "YES I DO":
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync("Nice! The camera should be in high quality.");
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringUserName + ": " + inputTextBox.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + "Nice! GPU is important for gamers. ";
                            //outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "back_camera=1 AND back_camera_res>=8 AND autofocus=1 AND flash=1 OR hdr=1 or image_stablization=1";
                            intQuestionCounter++;
                            inputTextBox.Text = "";
                            break;
                        case "2":
                        case "NOT":
                        case "NO":
                        case "NO PHOTOGRAPHY":
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync("Great! Then you can buy new Canon camera. This one is just phone. ");
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringUserName + ": " + inputTextBox.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + "Great! Then you can buy new Canon camera. This one is just phone.";
                            //outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "";
                            intQuestionCounter++;
                            inputTextBox.Text = "";
                            break;
                        default:
                            reader.Dispose();
                            reader = new SpeechSynthesizer();
                            reader.SpeakAsync(reply.Text + ". " + arrayQuestionTitle[intQuestionCounter - 1]);
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + reply.Text;
                            outputTextBox.Text = outputTextBox.Text + Environment.NewLine + stringBotName + ": " + arrayQuestionTitle[intQuestionCounter - 1];
                            parseEmbeddedOutputCommands(reply.AgentText);
                            runProgram(reply.Cmd);
                            arrayAnswers[intQuestionCounter] = "id > 0";
                            inputTextBox.Text = "";
                            //getReply();
                            break;
                    }
                }

                if (intQuestionCounter == 11){
                    panelText.Visible = false;
                    panelOutputText.Visible = false;
                    panelSend.Visible = false;

                    reader.Dispose();

                    int i = 1;

                    while (i < 10)
                    {
                        strQuery = strQuery + arrayAnswers[i].ToString() + " AND ";
                        i++;
                    }
                    strQuery = strQuery + "id > 0";

                    ItemGetter();
                }
            }
            this.inputTextBox.Text = "";
            outputTextBox.SelectionStart = outputTextBox.TextLength;
            outputTextBox.ScrollToCaret();
            void_img_loader();
        }

		private void parseEmbeddedOutputCommands(string text)
		{
			string startCommand = "<";
			string endCommand = ">";

			int start = text.IndexOf(startCommand);
			int end = -1;

			while(start != -1)
			{
				end = text.IndexOf(endCommand, start);
				if(end != -1)
				{
					string command = text.Substring(start + 1, end - start - 1).Trim();
					if(command != "")
					{
						this.runEmbeddedOutputCommand(command);
					}
				}
				start = text.IndexOf(startCommand, start+1);
			}
        }//parseEmbeddedOutputCommands(string text)

		private void runEmbeddedOutputCommand(string command)
		{
			int spaceIndex = command.IndexOf(" ");
			
			string function;
			string args;
			if(spaceIndex == -1)
			{
				function = command.ToLower();
				args = "";
			}
			else
			{
				function = command.Substring(0, spaceIndex).ToLower();
				args = command.Substring(spaceIndex + 1);
			}

			try
			{
				switch(function)
				{
					case "quit":
					case "exit":

						this.Close();
						break;
					case "run":
						this.runProgram(args);
						break;
                    case "search":
                        MessageBox.Show(args);
                        break;
					default:
						break;
				}//switch
			}
			catch {}
		}//runOutputEmbeddedCommand(string command)

		private void runProgram(string command)
		{
			try
			{
				string[] pieces = this.splitOnFirstUnquotedSpace(command);

				System.Diagnostics.Process proc = new System.Diagnostics.Process();
				proc.StartInfo.FileName = pieces[0].Trim(); 
				proc.StartInfo.Arguments = pieces[1];
				proc.StartInfo.CreateNoWindow = true;
				proc.StartInfo.UseShellExecute = true;
				proc.Start();
			}
			catch{}
        }//runProgram(string filename, string args)
		
		public string[] splitOnFirstUnquotedSpace(string text)
		{
			string[] pieces = new string[2];
			int index = -1;
			bool isQuoted = false;
			//find the first unquoted space
			for(int i = 0; i < text.Length; i++)
			{
				if(text[i] == '"')
					isQuoted = !isQuoted;
				else if(text[i] == ' ' && !isQuoted)
				{
					index = i;
					break;
				}
			}

			//break up the string
			if(index != -1)
			{
				pieces[0] = text.Substring(0, index);
				pieces[1] = text.Substring(index + 1);
			}
			else
			{
				pieces[0] = text;
				pieces[1] = "";
			}

			return pieces;
		}//splitOnFirstUnquotedSpace(string text)

        private void inputTextBox_TextChanged(object sender, EventArgs e)
        {
            inputTextBox.Size = new Size(panelText.Width-30, 35);
        }

        private void getReplyButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                getReply();
            }
        }
        
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser1.Document.Window.ScrollTo(0, 70);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            getReply();
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            inputTextBox.Focus();
        }

        public void void_img_loader()
        {
            panelWebbrowser.Controls.Clear();

            PictureBox pictureBoxAnswer = new PictureBox();
            pictureBoxAnswer.Location = new Point(15, 15);
            pictureBoxAnswer.Size = new Size(400, 400);
            pictureBoxAnswer.BackColor = Color.Transparent;
            panelWebbrowser.Controls.Add(pictureBoxAnswer);

            PictureBox pictureBoxQuestion = new PictureBox();
            pictureBoxQuestion.Location = new Point(0, 0);
            pictureBoxQuestion.Size = new Size(1366, 430);
            panelWebbrowser.Controls.Add(pictureBoxQuestion);



            if (intQuestionCounter == 1)
            {
                pictureBoxQuestion.BackgroundImage = Properties.Resources.question_1;
                pictureBoxAnswer.BackgroundImage = Properties.Resources.answer_1;
            }
            if (intQuestionCounter == 2)
            {
                pictureBoxQuestion.BackgroundImage = Properties.Resources.question_2;
                pictureBoxAnswer.BackgroundImage = Properties.Resources.answer_2;
            }
            if (intQuestionCounter == 3)
            {
                pictureBoxQuestion.BackgroundImage = Properties.Resources.question_3;
                pictureBoxAnswer.BackgroundImage = Properties.Resources.answer_3;
            }
            if (intQuestionCounter == 4)
            {
                pictureBoxQuestion.BackgroundImage = Properties.Resources.question_4;
                pictureBoxAnswer.BackgroundImage = Properties.Resources.answer_4;
            }
            if (intQuestionCounter == 5)
            {
                pictureBoxQuestion.BackgroundImage = Properties.Resources.question_5;
                pictureBoxAnswer.BackgroundImage = Properties.Resources.answer_5;
            }
            if (intQuestionCounter == 6)
            {
                pictureBoxQuestion.BackgroundImage = Properties.Resources.question_6;
                pictureBoxAnswer.BackgroundImage = Properties.Resources.answer_6;
            }
            if (intQuestionCounter == 7)
            {
                pictureBoxQuestion.BackgroundImage = Properties.Resources.question_7;
                pictureBoxAnswer.BackgroundImage = Properties.Resources.answer_7;
            }
            if (intQuestionCounter == 8)
            {
                pictureBoxQuestion.BackgroundImage = Properties.Resources.question_8;
                pictureBoxAnswer.BackgroundImage = Properties.Resources.answer_8;
            }
            if (intQuestionCounter == 9)
            {
                pictureBoxQuestion.BackgroundImage = Properties.Resources.question_9;
                pictureBoxAnswer.BackgroundImage = Properties.Resources.answer_9;
            }
            if (intQuestionCounter == 10)
            {
                pictureBoxQuestion.BackgroundImage = Properties.Resources.question_10;
                pictureBoxAnswer.BackgroundImage = Properties.Resources.answer_10;
            }
        }


        private void serialPort3_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (customerDetected==false && isFirstPage==true)
            {
                void_start();
                string stringText = "Let me know your name please?";
                reader.Dispose();
                reader = new SpeechSynthesizer();
                reader.SpeakAsync(stringText);
                customerDetected = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            customerDetected = false;
    }
    }
}
