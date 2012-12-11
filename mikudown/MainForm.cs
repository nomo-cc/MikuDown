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
using System.Configuration;
using System.IO.Compression;


namespace mikudown
{
	
	
	
	
	
	
	
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		List<string> _items = new List<string>();
		
		
		
		/*	public bool zip_fileexists(string zipname, string fname)
		{
			//	statout("Testing zipfile "+zipname);
			try
			{
				ZipFile zop = ZipFile.Read(zipname);
				
				foreach (ZipEntry e in zop)
				{
					//	if (header)
					//	{
					if(e.FileName.ToString().Contains(fname))
					{
						return true;
					}
					//	}
				}
			}
			catch
			{
				statout("Exception at testing zipfile "+zipname);
			}
			
			return false;
		}
		 */
		
		/*	public void zip_addtozip(string zipname, string fname)
		{

			bool didit = false;
			while(didit==false)
			{
				try
				{
					if(File.Exists(zipname))
					{
						statout("Adding to zipfile "+zipname);
						using (ZipFile zop = ZipFile.Read(zipname))
						{
							zop.AddFile(fname);
							zop.Save(zipname);
							didit=true;
						}
						
					}
					else
					{
						statout("Creating zip file: "+zipname);
						using (ZipFile zop = new ZipFile())
						{
							zop.AddFile(fname);
							zop.Save(zipname);
							didit=true;
						}
						
					}
				}
				catch
				{
					Thread.Sleep(1000);
				}
				
			}
			
		}
		
		 */
		
		
		public bool zip_fileexists(string zipname, string fname)
		{
			try
			{
				using (ZipArchive archive = ZipFile.OpenRead(zipname))
				{
					foreach (ZipArchiveEntry entry in archive.Entries)
					{
						if (entry.FullName.Contains(fname))
						{
							return true;
						}
					}
				}
			}
			catch
			{
				
			}
			return false;
		}
		
		public bool zip_downloadtozip(string zipname, string durl)
		{
			try
			{
				string fname =	durl.Substring(durl.LastIndexOf('/')+1);
				
				WebClient client = new WebClient();
				byte[] arr = client.DownloadData(durl);
				


				if(File.Exists(zipname))
				{
					statout("Adding to zipfile "+zipname+": "+fname);
					using (FileStream zipToOpen = new FileStream(zipname, FileMode.Open))
					{
						using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
						{
							ZipArchiveEntry entry = archive.CreateEntry(fname);
							using (Stream entryStream = entry.Open())
							{
								entryStream.Write(arr, 0, arr.Length);
							}
						}
					}

				}
				else
				{
					statout("Creating zip file: "+zipname+", adding "+fname);
					using (FileStream zipToOpen = new FileStream(zipname, FileMode.Create))
					{
						using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
						{
							ZipArchiveEntry entry = archive.CreateEntry(fname);
							using (Stream entryStream = entry.Open())
							{
								entryStream.Write(arr, 0, arr.Length);
							}
						}
					}

				}
			}
			catch(Exception e)
			{
				statout(e.ToString());
				return false;
			}
			
			return true;
		}
		
		
		
		public void zip_displayfile(string str)
		{
			string[] substrs = str.Split('|');
			
			string zname = substrs[1];
			string fname = substrs[0];
			
			using (ZipArchive archive = ZipFile.OpenRead(zname))
			{
				
				foreach (ZipArchiveEntry entry in archive.Entries)
				{
					if (entry.FullName.Contains(fname))
					{
						using (var stream = entry.Open())
						{
							byte[] buffer = new byte[entry.Length];
							
							stream.Read(buffer, 0, (int)entry.Length);
							
							using (MemoryStream ms = new MemoryStream(buffer))
							{
								pictureBox2.Image = Image.FromStream(ms);
							}
						}
						break;
					}
				}
				
			}
			
		}
		
		
		
		
		
		
		
		
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
			catch
			{
			}
		}
		
		
		public void addImage(string poth)
		{
			_items.Add(poth);
			
			listBox1.DataSource = null;
			listBox1.DataSource = _items;
			
			listBox1.SelectedIndex = listBox1.FindString(poth);
		}
		
		
		
		public void threadeddownload(Object parameter)
		{
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
		
		public static string ffolder;
		
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
				
				
				ffolder="";
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
						//	statout("Download ok: " + durl );
						addImage(fpath);
						displayImage(fpath);
						counter++;
						
					}
					else
					{
						statout("Failed: " + durl + " to "+fpath);
					}
					takeiteasy();
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
		
		
		
		
		
		
		
		
		public void ParseXMLZip(string fileName)
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
				
				
				ffolder=textBox2.Text;
				string fpartfolder="";
				if(checkBox1.Checked==false)
				{
					int thousands = get10code(fname);
					//int thousands = (int)((float)counter / 1000.0);
					fpartfolder = thousands.ToString("00");
				}
				else
				{
					fpartfolder = fdate.Year.ToString("0000")+"-"+fdate.Month.ToString("00");
				}
				
				string zippath = ffolder+"\\"+fpartfolder+".zip";
				
				System.IO.Directory.CreateDirectory(ffolder);
				
				string fpath = ffolder+"\\"+ fname;
				
				
				//zip_downloadtozip(zippath, durl);
				
				
				// Print the book information to the console
				//  statout("Downloading " + durl + " to "+fname);
				
				if(zip_fileexists(zippath, fname) == false)
				{
					
					/*	ParameterizedThreadStart pts = new ParameterizedThreadStart(this.threadeddownload);
					Thread thread = new Thread(pts);
					thread.Start((object)new downloadobject(durl, fname));
					while (!thread.IsAlive);
					
					while (thread.IsAlive)
					{
						Application.DoEvents();
						Thread.Sleep(2);
						
					}
					thread.Join ();
					statout("Downloaded to raw file: "+fname);
					
					//	int dresult = downloadfile(durl, fpath);
					int dresult=0;
					if(File.Exists(fname) == false)dresult=1;
					
					if(dresult==0)
					{
					//	statout("Trying to zip file.");
						zip_addtozip(zippath, fname);
						File.Delete(fname);
						
						//	statout("Download ok: " + durl );
						//	addImage(fpath);
						//	displayImage(fpath);
						counter++;
						
					}
					else
					{
						statout("Failed: " + durl + " to "+fpath);
					}
					 */
					
					if(zip_downloadtozip(zippath, durl)==true)
					{
						
						addImage(fname+"|"+zippath);
						
						counter++;
						takeiteasy();

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
		
		
		
		
		
		
		public void takeiteasy()
		{
			//take it easy mode
			if(checkBox4.Checked == true)
			{
				for(int s=0;s<500;s++)
				{
					Application.DoEvents();
					Thread.Sleep(10);
				}
				
			}
		}
		
		
		
		
		
		
		
		
		
		public void showprogress()
		{
			try{
				statusbox.Clear();
				statusbox.AppendText("Downloaded "+counter+" files. Skipped "+(overall-counter)+" files.");
				
				progressBar1.Value = overall;
				
			}
			catch
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
				textBox4.Text = ""+i;
				System.IO.Directory.CreateDirectory(textBox2.Text);
				//int dresult=downloadfile(textBox1.Text+i.ToString(), textBox2.Text+"\\list.xml");
				//if(dresult==0)
				//{
				//	statout("OK.");
				string xmlurl = textBox1.Text+i.ToString();
				if(checkBox3.Checked==false)
				{
					//ParseXML(textBox2.Text+"\\list.xml");
					ParseXML(xmlurl);
				}
				else
				{
					//	ParseXMLZip(textBox2.Text+"\\list.xml");
					ParseXMLZip(xmlurl);
				}
				//}
				
			}
			statout("All finished.");
			
			isrunning=false;
			textBox1.ReadOnly = false;
			textBox2.ReadOnly = false;
			textBox4.ReadOnly = false;
			textBox5.ReadOnly = false;
			checkBox3.Enabled = true;
			checkBox2.Enabled = true;
			checkBox1.Enabled = true;
			button1.Text = "Go";
			//button1.Enabled = false;
		}
		
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			loadSettings();
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
				checkBox3.Enabled = false;
				checkBox2.Enabled = false;
				checkBox1.Enabled = false;
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
				checkBox3.Enabled = true;
				checkBox2.Enabled = true;
				checkBox1.Enabled = true;
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
			textBox3.AppendText("Help/Info:\r\n");
			textBox3.AppendText("MikuDown will parse the XML list pages from [Source] and download the images to [Output location].\r\n");
			textBox3.AppendText("(There are 100 images per page, with roughly 30000 images on safebooru in total.)\r\n\r\n");
			textBox3.AppendText("To avoid having too many image files in one single folder, MikuDown will create subfolders (per month or 1-10).\r\n");
			//textBox3.AppendText("The subfolder structure can either be \"DownloadDirectory\\Year-Month\"\r\n");
			//textBox3.AppendText("or \"DownloadDirectory\\01 to 10\" (the files will be sorted by a hash of their filename, nevermind the details.)\r\n\r\n");
			textBox3.AppendText("MikuDown will skip files that already exist.\r\n");
			textBox3.AppendText("\r\n");
			textBox3.AppendText("\r\n");			
			textBox3.AppendText("\"Take it easy\" aka Background mode: Wait a few seconds after each download to reduce net and system load.\r\n");
			textBox3.AppendText("\r\n");
			textBox3.AppendText("Beware! This is a quick-n-dirty, one-purpose tool. No guarantees or liabilities.\r\n");
			textBox3.AppendText("Cheers, nomo - miku@nomo.cc\r\n");
			textBox3.AppendText("\r\n");
			
		}
		
		
		
		public void displayImage(string path)
		{
			try
			{
				/*	// Open a Stream and decode a JPEG image
				Stream imageStreamSource = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
				JpegBitmapDecoder decoder = new JpegBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
				BitmapSource bitmapSource = decoder.Frames[0];

				// Draw the Image
				Image myImage = new Image();
				myImage.Source = bitmapSource;
				myImage.Stretch = Stretch.None;
				myImage.Margin = new Thickness(20);
				 */

				pictureBox2.Image = new Bitmap(path);

			}
			catch{}
		}
		
		
		
		
		
		
		public string getAppSetting(string key)
		{
			string result="";
			//Laden der AppSettings
			Configuration config = ConfigurationManager.OpenExeConfiguration(
				System.Reflection.Assembly.GetExecutingAssembly().Location);
			//Zurückgeben der dem Key zugehörigen Value
			try{
				result = config.AppSettings.Settings[key].Value;
			}
			catch
			{
			}
			return result;
		}
		
		public void setAppSetting(string key, string value)
		{
			//Laden der AppSettings
			Configuration config = ConfigurationManager.OpenExeConfiguration(
				System.Reflection.Assembly.GetExecutingAssembly().Location);
			//Überprüfen ob Key existiert
			if (config.AppSettings.Settings[key] != null)
			{
				//Key existiert. Löschen des Keys zum "überschreiben"
				config.AppSettings.Settings.Remove(key);
			}
			//Anlegen eines neuen KeyValue-Paars
			config.AppSettings.Settings.Add(key, value);
			//Speichern der aktualisierten AppSettings
			config.Save(ConfigurationSaveMode.Modified);
		}
		
		
		public void loadSettings()
		{
			string lastfolder = getAppSetting("lastfolder");
			if(lastfolder!="")
				textBox2.Text = lastfolder;
			
			string bitrate = getAppSetting("topage");
			if(bitrate!="")
			{
				textBox5.Text = bitrate;
			}
			
			string dozip = getAppSetting("usezip");
			if(dozip!="")
			{
				checkBox3.Checked = Convert.ToBoolean(dozip);
			}
			
		}
		
		
		public void saveSettings()
		{
			setAppSetting("lastfolder", textBox2.Text);
			setAppSetting("topage", textBox5.Text);
			
			setAppSetting("usezip", checkBox3.Checked.ToString());
			
		}
		
		
		
		
		
		
		
		
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			isrunning=false;
			saveSettings();
		}
		
		void TextBox3TextChanged(object sender, EventArgs e)
		{
			
		}
		
		void ListBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			if(listBox1.Items.Count != 0 && listBox1.SelectedItem != null)
			{
				try
				{
					string curItem = listBox1.SelectedItem.ToString();
					//	string fpath = ffolder+"\\"+ curItem;
					
					statout("Displaying image "+curItem);
					
					if(curItem.Contains("|"))
					{
						zip_displayfile(curItem);
					}
					else
					{
						displayImage(curItem);
					}
				}
				catch(Exception ex)
				{
					//	MessageBox.Show(ex.ToString());
					statout(ex.ToString());
				}

			}
		}
		

	}
}
