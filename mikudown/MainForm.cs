/*
 * Created by SharpDevelop.
 * User: nmo
 * Date: 11.02.2012
 * Time: 14:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using System.Threading;
using System.IO;
using System.Globalization;

namespace mikudown
{
	
	
	
	
	
	
	
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		public int downloadfile(string d_url, string d_file)
		{
			try
			{
				using (WebClient Client = new WebClient ())
				{
					Client.DownloadFile(d_url, d_file);
				}
				return 0;
			}
			catch(Exception e)
			{
				statout("Exception in downloadfile "+d_url+" "+e);
				return -1;
			}
		}
		
		
		public  void statout(string towrite)
		{
			try
			{
				textBox3.AppendText(towrite+"\r\n");
			}
			catch(Exception e)
			{
			}
		}
		
		
		
		
		public void threadeddownload(Object parameter)
		{
			//Tu was...
			downloadfile(((downloadobject)parameter).dfrom, ((downloadobject)parameter).dto );
		}
		
		
		
		
		public class downloadobject
		{
			public string dfrom;
			public string dto;
			
			public downloadobject(string pfrom, string pto)
			{
				this.dfrom = pfrom;
				this.dto = pto;
			}
			
		}
		
		
		public static int counter=0;
		public static int overall=0;
		
		public void ParseXML(string fileName)
		{
			
			
			
			XmlDocument xmlDocument = new XmlDocument();
			
			// Load the XML file
			xmlDocument.Load(fileName);
			
			// Get all book nodes
			XmlNodeList postNodes = xmlDocument.GetElementsByTagName("post");
			
			// Iterate through all books
			foreach (XmlNode postNode in postNodes)
			{
				if(isrunning==false)
				{
					button1.Text = "Go";
					button1.Enabled = true;
					break;
				}
				// Use indexers to get the isbn, author and title
				//   XmlAttribute postmd5Attribute = postNode.Attributes["md5"];
				XmlAttribute posturlAttribute = postNode.Attributes["file_url"];
				//  XmlNode authorNode = bookNode["author"];
				XmlAttribute postdateAttribute = postNode.Attributes["created_at"];
				
				//created_at="Sun Feb 26 10:15:39 +0100 2012"
				string dformat = "ddd MMM dd HH:mm:ss zzz yyyy";
				
				DateTime fdate = new DateTime();
				CultureInfo provider = CultureInfo.InvariantCulture;
				
				string ddate = postdateAttribute.Value;
				string durl = posturlAttribute.Value;
				
				string fname =	durl.Substring(durl.LastIndexOf('/')+1);
				
				try {
					fdate = DateTime.ParseExact(ddate, dformat, provider);
					
				}
				catch (FormatException) {
					statout("Couldn't parse date: " + ddate );
				}
				
				
				string ffolder="";
				if(checkBox1.Checked==false)
				{
					int thousands = get10code(fname);
					//int thousands = (int)((float)counter / 1000.0);
					ffolder = textBox2.Text+"\\"+thousands.ToString("00");
				}
				else
				{
					ffolder = textBox2.Text+"\\"+fdate.Year.ToString("0000")+"-"+fdate.Month.ToString("00");
				}
				
				System.IO.Directory.CreateDirectory(ffolder);
				
				string fpath = ffolder+"\\"+ fname;
				
				
				// Print the book information to the console
				//  statout("Downloading " + durl + " to "+fname);
				
				if(File.Exists(fpath) == false)
				{
					
					ParameterizedThreadStart pts = new ParameterizedThreadStart(this.threadeddownload);
					Thread thread = new Thread(pts);
					thread.Start((object)new downloadobject(durl, fpath));
					while (!thread.IsAlive);
					
					while (thread.IsAlive)
					{
						Application.DoEvents();
						Thread.Sleep(2);
						
					}
					thread.Join ();
					
					//	int dresult = downloadfile(durl, fpath);
					int dresult=0;
					if(File.Exists(fpath) == false)dresult=1;
					
					if(dresult==0)
					{
						statout("Download ok: " + durl );
						counter++;
						
					}
					else
					{
						statout("Failed: " + durl + " to "+fpath);
					}
				}
				else
				{
					statout("File already exists: "+fpath.Substring(textBox2.Text.Length));
				}
				overall++;
				showprogress();
				Application.DoEvents();
			}
			
		}
		
		
		public void showprogress()
		{
			try{
				statusbox.Clear();
				statusbox.AppendText("Downloaded "+counter+" files. Skipped "+(overall-counter)+" files.");
				
				progressBar1.Value = overall;
				
			}
			catch(Exception e)
			{}
		}
		
		
		public int get10code(string input)
		{
			int result=0;
			
			for(int i=0; i< input.Length-4; i++)
			{
				result += (int)input[i];
			}

			result = result % 10;
			
			return result+1;
		}
		
		
		public void startit()
		{
			int rfrom=System.Convert.ToInt32(textBox4.Text);
			int rto=System.Convert.ToInt32(textBox5.Text);
			
			progressBar1.Maximum = ((rto-(rfrom-1)) * 100);
			
			for(int i=rfrom; i<=rto; i++)
			{
				if(isrunning==false)
				{
					break;
				}
				//counter = (i-1) * 100;
				statout("Parsing page "+i+": "+textBox1.Text+i.ToString());
				System.IO.Directory.CreateDirectory(textBox2.Text);
				int dresult=downloadfile(textBox1.Text+i.ToString(), textBox2.Text+"\\list.xml");
				if(dresult==0)
				{
					statout("OK.");
					ParseXML(textBox2.Text+"\\list.xml");
				}
			}
			statout("All finished.");
			
			isrunning=false;
			textBox1.ReadOnly = false;
			textBox2.ReadOnly = false;
			textBox4.ReadOnly = false;
			textBox5.ReadOnly = false;
			button1.Text = "Go";
			//button1.Enabled = false;
		}
		
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public static bool isrunning=false;
		
		void Button1Click(object sender, EventArgs e)
		{
			if(!isrunning)
			{
				counter=0;
				overall=0;
				textBox1.ReadOnly = true;
				textBox2.ReadOnly = true;
				textBox4.ReadOnly = true;
				textBox5.ReadOnly = true;
				button1.Text = "Stop";
				isrunning=true;
				Application.DoEvents();
				startit();
				
			}
			else
			{
				isrunning=false;
				textBox1.ReadOnly = false;
				textBox2.ReadOnly = false;
				textBox4.ReadOnly = false;
				textBox5.ReadOnly = false;
				button1.Text = "...";
				button1.Enabled = false;
				
			}
			
			
			
			//      thread.Join();
			
			
			
			
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			System.Windows.Forms.FolderBrowserDialog objDialog = new FolderBrowserDialog();
			objDialog.Description = "Save folders with downloaded images to:";
			objDialog.SelectedPath=@"C:\";       // Vorgabe Pfad (und danach der gewählte Pfad)
			DialogResult objResult = objDialog.ShowDialog(this);
			if (objResult == DialogResult.OK)
				//MessageBox.Show("Neuer Pfad : " + objDialog.SelectedPath);
				textBox2.Text = objDialog.SelectedPath;
		}
		
		void Label2Click(object sender, EventArgs e)
		{
			
		}
		
		void setChecks()
		{
		
		}
		
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			if(checkBox1.Checked == true) checkBox2.Checked=false;
			else checkBox2.Checked=true;
		}
		
		void CheckBox2CheckedChanged(object sender, EventArgs e)
		{
			if(checkBox2.Checked == true) checkBox1.Checked=false;
			else checkBox1.Checked=true;
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			textBox3.AppendText("How to use this tool:\r\n\r\n");
			textBox3.AppendText("MikuDown will parse the XML list pages from [Source] and download the images to [Output location].\r\n");
			textBox3.AppendText("(There are 100 images per page, with roughly 26000 images on safebooru in total.)\r\n\r\n");
			textBox3.AppendText("To avoid having too many image files in one single folder, MikuDown will create subfolders.\r\n");
			textBox3.AppendText("The subfolder structure can either be \"DownloadDirectory\\Year-Month\"\r\n");
			textBox3.AppendText("or \"DownloadDirectory\\01 to 10\" (the files will be sorted by a hash of their filename, nevermind the details.)\r\n\r\n");
			textBox3.AppendText("MikuDown will skip files that already exist.\r\n");
			textBox3.AppendText("\r\n");
			textBox3.AppendText("Beware! This is a quick-n-dirty, one-purpose tool. No guarantees or liabilities.\r\n");
			textBox3.AppendText("Cheers, nomo - miku@nomo.cc\r\n");
			textBox3.AppendText("\r\n");
		}
	}
}
