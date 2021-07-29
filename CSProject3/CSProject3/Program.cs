using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;

namespace CSProject3
{
    class Program
    {
        static void Main(string[] args)
        {
            String stringPathToElectionFile = "starwars_win.csv";
            StarWars survey = new StarWars();
            Boolean success = survey.PopulateFromFile(stringPathToElectionFile);

            survey.hatedMovie();
            survey.favoriteMovie();

        }
    }

    public class StarWars /*Completed*/
    {
        int episodeI = 0;
        int episodeIbad = 0;
        int episodeII = 0;
        int episodeIIbad = 0;
        int episodeIII = 0;
        int episodeIIIbad = 0;
        int episodeIV = 0;
        int episodeIVbad = 0;
        int episodeV = 0;
        int episodeVbad = 0;
        int episodeVI = 0;
        int episodeVIbad = 0;
        string leastPopular = "";
        int worstReviews = 0;

        public Boolean PopulateFromFile(String fileLocation) /* Using an election file, fully populate this election object and all of its internal objects (below) */
        {
            if (File.Exists(fileLocation)) //Stole Odom's code here for ease of reading file into array
            {
                String stringEntireText = File.ReadAllText(fileLocation); /* Read the whole file into memory as a string (could go line by line. */
                String stringNewLine = "\r\n"; /* Force using Windows newline instead of Environment.NewLine */
                Char charDelimiter = ','; /* This file uses tabs, but we could change this variable if other files use other delimiters */
                String[] arrayStringLines = stringEntireText.Split(stringNewLine); /* Convert the big string into an array of rows */

                /* Create some more variables we will need. */
                int lineNumber = 0;
                List<String> listStringHeader = new List<String>();
                List<OrderedDictionary> listOrdicRows = new List<OrderedDictionary>();

                /* Go through every line/row and process appropriately. */
                foreach (String stringLine in arrayStringLines)
                {
                    lineNumber++;
                    /* If a line is blank, do not do anything with it. */
                    if (String.IsNullOrWhiteSpace(stringLine))
                    {
                        Console.WriteLine("");
                    }
                    else
                    {
                        /* Create a list to holding header information if this is the first line (the header). */
                        if (lineNumber == 1)
                        {
                            String[] arrayStringTempHeader = stringLine.Split(charDelimiter);
                            foreach (String stringHeaderColumn in arrayStringTempHeader)
                            {
                                listStringHeader.Add(stringHeaderColumn.Trim());
                                /* We can dice the header string a lot in just one line (above). */
                            }
                        }
                        /* If this is not the first line, treat it like a data row. */
                        else
                        {
                            String[] arrayStringTempRow = stringLine.Split(charDelimiter);
                            OrderedDictionary ordicRow = new OrderedDictionary();
                            for (int i = 9; i < 15; i++)
                            {
                                ordicRow.Add(listStringHeader[i], arrayStringTempRow[i]);
                                /* (Above) Add each column in the row to an ordered dictionary keyed to its header name. 
								 We can look up this column data in the future either by its order by its header name. */
                            }
                            listOrdicRows.Add(ordicRow);
                        }
                    }
                }

                foreach (OrderedDictionary row in listOrdicRows)
                {

                    int tempI = string.IsNullOrEmpty(row["Star Wars: Episode I  The Phantom Menace"].ToString()) ? 7 : int.Parse(row["Star Wars: Episode I  The Phantom Menace"].ToString());
                    episodeI += tempI;
                    if (tempI == 6)
                    {
                        episodeIbad++;
                        if (episodeIbad > worstReviews)
                        {
                            worstReviews = episodeIbad;
                            leastPopular = "Star Wars: Episode I  The Phantom Menace";
                        }
                    }

                    int tempII = string.IsNullOrEmpty(row["Star Wars: Episode II  Attack of the Clones"].ToString()) ? 7 : int.Parse(row["Star Wars: Episode II  Attack of the Clones"].ToString());
                    episodeII += tempII;
                    if (tempII == 6)
                    {
                        episodeIIbad++;
                        if (episodeIIbad > worstReviews)
                        {
                            worstReviews = episodeIIbad;
                            leastPopular = "Star Wars: Episode II  Attack of the Clones";
                        }
                    }


                    int tempIII = string.IsNullOrEmpty(row["Star Wars: Episode III  Revenge of the Sith"].ToString()) ? 7 : int.Parse(row["Star Wars: Episode III  Revenge of the Sith"].ToString());
                    episodeIII += tempIII;
                    if (tempIII == 6)
                    {
                        episodeIIIbad++;
                        if (episodeIIIbad > worstReviews)
                        {
                            worstReviews = episodeIIIbad;
                            leastPopular = "Star Wars: Episode III  Revenge of the Sith";
                        }
                    }


                    int tempIV = string.IsNullOrEmpty(row["Star Wars: Episode IV  A New Hope"].ToString()) ? 7 : int.Parse(row["Star Wars: Episode IV  A New Hope"].ToString());
                    episodeIV += tempIV;
                    if (tempIV == 6)
                    {
                        episodeIVbad++;
                        if (episodeIVbad > worstReviews)
                        {
                            worstReviews = episodeIVbad;
                            leastPopular = "Star Wars: Episode IV  A New Hope";
                        }
                    }


                    int tempV = string.IsNullOrEmpty(row["Star Wars: Episode V The Empire Strikes Back"].ToString()) ? 7 : int.Parse(row["Star Wars: Episode V The Empire Strikes Back"].ToString());
                    episodeV += tempV;
                    if (tempV == 6)
                    {
                        episodeVbad++;
                        if (episodeVbad > worstReviews)
                        {
                            worstReviews = episodeVbad;
                            leastPopular = "Star Wars: Episode V The Empire Strikes Back";
                        }
                    }

                    int tempVI = string.IsNullOrEmpty(row["Star Wars: Episode VI Return of the Jedi"].ToString()) ? 7 : int.Parse(row["Star Wars: Episode VI Return of the Jedi"].ToString());
                    episodeVI += tempVI;
                    if (tempVI == 6)
                    {
                        episodeVIbad++;
                        if (episodeVIbad > worstReviews)
                        {
                            worstReviews = episodeVIbad;
                            leastPopular = "Star Wars: Episode VI Return of the Jedi";
                        }
                    }

                }

                return true;
            }

            else
            {
                return false;
            }

        }

        public void hatedMovie()
        {
            Console.WriteLine("Least Favorite Movie: " + leastPopular);
            Console.WriteLine("Number of 6 scores: " + worstReviews);
        }

        public void favoriteMovie()
        {
            var movieDict = new Dictionary<string, int>();
            movieDict.Add("Star Wars: Episode I  The Phantom Menace", episodeI);
            movieDict.Add("Star Wars: Episode II  Attack of the Clones", episodeII);
            movieDict.Add("Star Wars: Episode III  Revenge of the Sith", episodeIII);
            movieDict.Add("Star Wars: Episode IV  A New Hope", episodeIV);
            movieDict.Add("Star Wars: Episode V The Empire Strikes Back", episodeV);
            movieDict.Add("Star Wars: Episode VI Return of the Jedi", episodeVI);

            var s = String.Empty;
            var min = Int32.MaxValue;
            foreach (var item in movieDict)
            {
                if (item.Value < min)
                {
                    s = item.Key;
                    min = item.Value;
                }
            }

            Console.WriteLine("Favorite Movie: " + s);
            Console.WriteLine("Score: " + min);

        }
    }
}
























