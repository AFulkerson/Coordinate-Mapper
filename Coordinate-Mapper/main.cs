using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;

namespace Coordinate_Mapper
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void displayError(Exception e)
        {
            MessageBox.Show("Something went wrong!\n\n" + e.Message + "\n\nI just can't go on...", "Error", MessageBoxButtons.OK);
        }

        private void appResults(string txt)
        {
            tb_results.AppendText(txt + Environment.NewLine);
        }

        private string getFilePath()
        {
            string fp = null;       //SVG FILE PATH

            OpenFileDialog fd = new OpenFileDialog();

            fd.Filter = "SVG (.svg)|*.svg";
            fd.FilterIndex = 1;

            fd.Multiselect = false;

            // GETS FILE PATH. IF CANCELED, RETURNS THE LAST FILE SELECTED
            DialogResult dr = fd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                fp = fd.FileName;
            }
            else if (dr == DialogResult.Cancel)
            {
                fp = globalVars.filePath;
            }

            fd.Dispose();
            return fp;

        }

        private void get_coord(string filePath)
        {
            //CALCULATE COORDINATES FROM SVG FILE AND WRITE THEM TO THE CSV OUTPUT FILE

            //LOAD SVG FILE
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(filePath);
            }
            catch (Exception er)
            {
                displayError(er);
                return;
            }

            XmlElement el = doc.DocumentElement;

            // CREATE LIST FOR THE OUTPUT - FIRST LINE IS HEADERS
            // EACH ITEM IN THE LIST IS A COMPLETE LINE FOR THE OUTPUT FILE THAT WILL BE 
            // WRITTEN TO THE COMMA DELIMMITED CSV FILE. 
            List<string> outputRecords = new List<string>();

            // LIST OF COORDINATES FOR EACH PATH. IF PATH IS POLY THEN THIS LIST WILL BE JOINED WITH
            // OUTPUTRECORDS LIST
            List<string> outputCoord = new List<string>();

            // LIST OF RESULTS FOR EACH POLY
            List<string> polyResults = new List<string>();

            // GET HEIGHT OF SVG CANVAS. USED TO CALC INVERTED Y COORD
            int height = 0;
            int width = 0;
            //if (el.HasAttribute("viewBox"))
            try
            {
                string[] docSize = el.GetAttribute("viewBox").Split();
                height = Convert.ToInt32(Math.Round(Convert.ToDecimal(docSize[3])));
                width = Convert.ToInt32(Math.Round(Convert.ToDecimal(docSize[2])));
            }
            catch (Exception er)
            {
                displayError(er);
                return;
            }

            //VARS FOR GETTING COORDINATES
            long order = 0;                 //POINT ORDER
            Int32 pathOrder = 0;            //PATH ORDER (RESETS FOR EACH PATH)
            decimal x = 0, y = 0;
            string id, d, resultCP = null;  //PATH ID FROM SVG, PATH COORDINATE STRING FROM SVG, CONFIRMATION MESSAGE POLY MAPPED
            //string result1 = null;             //CONFIRM POLY MAPPED
            string[] coordPair;             //ARRAY OF COORDINATE PAIRS WHEN d IS SPLIT
            string[] pairTemp;              //USED TO STORE THE SPLIT COORDPAIR ([0] = X [1] = Y)
            bool absPath = false;           //CHECK IF PATH USES ABSOLUTE POSITIONING FOR COORDINATES

            outputRecords.Add("Path Name, Point Order, X, Y");      // FIRST LINE OF OUTPUT FILE
            //appResults("Results:");
            //appResults("");

            //GET COORDINATES FOR EACH POLY
            foreach (XmlElement item in el.GetElementsByTagName("path"))
            {
                pathOrder = 0;
                x = 0;
                y = 0;
                id = item.GetAttribute("id");
                d = item.GetAttribute("d");
                coordPair = d.Split();

                foreach (string pair in coordPair)
                {
                    order++;
                    pairTemp = pair.Split(',');

                    double Num;
                    bool isNum = double.TryParse(pairTemp[0], out Num);

                    if (isNum)
                    {
                        if (absPath)
                        {
                            x = decimal.Parse(pairTemp[0], System.Globalization.NumberStyles.Float);
                            y = height - decimal.Parse(pairTemp[1], System.Globalization.NumberStyles.Float);
                        }
                        else
                        {
                            x = x + decimal.Parse(pairTemp[0], System.Globalization.NumberStyles.Float);
                            if (pathOrder == 0)
                            {
                                y = height - decimal.Parse(pairTemp[1], System.Globalization.NumberStyles.Float);
                            }
                            else
                            {
                                y = y - decimal.Parse(pairTemp[1], System.Globalization.NumberStyles.Float);
                            }
                        }

                        outputCoord.Add(id + "," + order + "," + x + "," + y);
                        resultCP = "Polygon: " + id + " created successfully";
                        pathOrder++;
                    }
                    else
                    {
                        if (pairTemp[0].Equals("M"))
                        {
                            absPath = true;
                            resultCP = "";
                        }
                        else if (pairTemp[0].Equals("m"))
                        {
                            absPath = false;
                            resultCP = "";
                        }
                        // (ANY DRAW COMMAND THAT ISN'T 'M/m' [DRAW TO] OR 'Z/z' [ENDS PATH] IDENTIFIES THE
                        // PATH AS A NON-POLYGON SHAPE AND WILL BE SKIPPED
                        else if (!(pairTemp[0].Equals("Z")) && !(pairTemp[0].Equals("z")))
                        {
                            outputCoord.Clear();
                            resultCP = "Skipped shape: " + id + ". Not a polygon";
                            break;
                        }
                    }
                }
                //UPDATE OUTPUT
                //appResults(resultCP);
                polyResults.Add(resultCP);
                outputRecords.AddRange(outputCoord);
                outputCoord.Clear();
            }

            string outputFilePath = filePath.Substring(0, filePath.Length - 4) + "_coordinates.csv";

            try
            {
                System.IO.File.WriteAllLines(outputFilePath, outputRecords);
                //FINISH SCRIPT 
                appResults("Results:");
                appResults("");
                foreach (string item in polyResults)
                {
                    appResults(item);
                }
                appResults("");
                appResults("Output file: " + outputFilePath + " saved");
                appResults("");
                appResults("Image height: " + height);
                appResults("Image width: " + width);
                appResults("");
                appResults("Script complete");
            }
            catch (Exception er)
            {
                displayError(er);
            }

        }   //GET COORDINATES

        private void main_Load(object sender, EventArgs e)
        {
            lbl_selectedFile.Text = null;
            btn_runScript.Enabled = false;

            // TOOLTIPS
            ToolTip tt = new ToolTip();

            tt.SetToolTip(this.btn_selectFile, "Select SVG file");
            tt.SetToolTip(this.btn_runScript, "generate coordinate file");
        }

        private void btn_runScript_Click(object sender, EventArgs e)
        {
            tb_results.Clear();
            get_coord(globalVars.filePath);
        }

        private void btn_selectFile_Click(object sender, EventArgs e)
        {
            globalVars.filePath = getFilePath();

            int strLength = 45;

            if (globalVars.filePath != null)
            {
                if (globalVars.filePath.Length <= strLength)
                {
                    lbl_selectedFile.Text = globalVars.filePath;
                }
                else
                {
                    lbl_selectedFile.Text = "..." + globalVars.filePath.Substring(globalVars.filePath.Length - (strLength - 3));
                }
            }
            else
            {
                lbl_selectedFile.Text = null;
            }
        }

        private void lbl_selectedFile_TextChanged(object sender, EventArgs e)
        {
            // ONLY ENABLE RUN SCRIPT BUTTON IF THERE IS A FILE SELECTED
            if (lbl_selectedFile.Text.Length > 0 && globalVars.filePath.Length > 0)
            {
                btn_runScript.Enabled = true;
            }
            else
            {
                btn_runScript.Enabled = false;
            }

            btn_runScript.Focus();

        }

        public static class globalVars
        {
            public static string filePath = null;
        }
    }
}
