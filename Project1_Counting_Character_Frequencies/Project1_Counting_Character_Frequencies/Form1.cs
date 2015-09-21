﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms.DataVisualization.Charting;

namespace Project1_Counting_Character_Frequencies
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void btn_open_Click(object sender, EventArgs e)
        {
            StreamReader inStream = null;
            string line;
            Dictionary<string, int> wordList = new Dictionary<string,int> {};
            int longestsize=0;
            int[] letter_freq = new int[26];
            for (int i = 0; i < 26; i++)
            {
               letter_freq[i] = 0;
            }
           // int intvale = 'a';
            
 
         

            OpenFileDialog openFileDiag = new OpenFileDialog();
            openFileDiag.InitialDirectory = @"C:\";
            openFileDiag.Title = "Open Text File";
            openFileDiag.CheckFileExists = true;
            openFileDiag.CheckPathExists = true;
            openFileDiag.Filter="text file (*.txt)|*.txt";
            

            if (openFileDiag.ShowDialog() == DialogResult.OK)
            {
                txt_filepath.Text = openFileDiag.FileName;              
                try
                {
                    if ((inStream = File.OpenText(openFileDiag.FileName)) != null)
                    {
                        using (inStream)
                        {
                            while ((line = inStream.ReadLine()) != null) // while not end of file
                            {
                                if (line != string.Empty) // if line not empty
                                {
                                    line = line.ToLower();
                                    // replace all non-alphabetic and space with empty
                                    line = Regex.Replace(line, @"[^a-z ]", ""); 

                                    // read each word from each sentence and put it into array
                                    string[] words = line.Split(' ');

                                    // check if the word is already in the dictionary or not
                                    foreach (string word in words)
                                    {
                                        if (wordList.ContainsKey(word))
                                        {
                                            wordList[word]++; // if yes, then increase it value by 1
                                        }
                                        else
                                        {
                                            wordList.Add(word, 1);// if not, add that word into the dictionary
                                        }

                                        // update longest word length
                                        if (word.Length > longestsize)
                                        {
                                            longestsize = word.Length; 
                                        }


                                        foreach (char c in word)
                                        {
                                            letter_freq[c - 97]++;
                                        }
                                    }
                                    

                                }// end if

                            }//end while

                        }//end using
                    }//end if

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: unable to open file");
                }
            }//end if

            string str = "";
            // prinf longest words
            foreach (var temp in wordList)
            {
                if (temp.Key.ToString().Length == longestsize)
                {
                    str += " " + temp.Key.ToString();
                }
                
            }// end foreach
            

            label1.Text = wordList["the"].ToString() + "   " + wordList["principles"].ToString();
            label2.Text = str;

            string str1 = "";

            int unicode = 97;
            

            for (int i = 0; i < 26; i++)
            {
                //str1 += " " + letter_freq[i].ToString();
                char c = (char)(unicode + i);
                Series series = chart_letter.Series.Add(c.ToString());
                series.Points.Add(letter_freq[i]);
            }
            //label2.Text = str1;

        }// end btn_open click
    }
}
