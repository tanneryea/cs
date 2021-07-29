using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;

namespace Project1
{
    class Program
    {
        static void Main(string[] args)
        {
            String stringPathToElectionFile = args[0];
            Election electionCurrent = new Election();
            Boolean success = electionCurrent.PopulateElectionFromFile(stringPathToElectionFile);

            Console.WriteLine(success);
            Console.WriteLine(electionCurrent.electionDate);


            PoliticalRace testRace = electionCurrent.GetRaceByCode("STA");

            Console.WriteLine(testRace.raceOffice);

            String winner = testRace.GetWinnerName();

            Console.WriteLine(winner);
        }
    }

    class Election /*Completed*/
    {
        public DateTime electionDate; /* ElectionDate field */
        private String electionName = "Past Election"; /* A name for this election. Does not have to correspond to data in the files. */
        private List<PoliticalRace> PoliticalRaceList = new List<PoliticalRace>(); /* All the political races in this election. Array of objects */

        public Boolean PopulateElectionFromFile(String fileLocation) /* Using an election file, fully populate this election object and all of its internal objects (below) */
        {
            if (File.Exists(fileLocation)) //Stole Odom's code here for ease of reading file into array
            {
                String stringEntireText = File.ReadAllText(fileLocation); /* Read the whole file into memory as a string (could go line by line. */
                String stringNewLine = "\r\n"; /* Force using Windows newline instead of Environment.NewLine */
                Char charDelimiter = '\t'; /* This file uses tabs, but we could change this variable if other files use other delimiters */
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
                            for (int i = 0; i < listStringHeader.Count; i++)
                            {
                                ordicRow.Add(listStringHeader[i], arrayStringTempRow[i]);
                                /* (Above) Add each column in the row to an ordered dictionary keyed to its header name. 
								 We can look up this column data in the future either by its order by its header name. */
                            }
                            listOrdicRows.Add(ordicRow);
                        }
                    }
                }

                foreach (OrderedDictionary row in listOrdicRows) //Goes through file
                {
                    bool duplicate = false;
                    foreach (PoliticalRace race in PoliticalRaceList) //Looks through each race in the race list
                    {
                        if (race.raceCode == row["RaceCode"].ToString()) //If it's a duplicate, throws out of the loop
                        {
                            duplicate = true;
                            break;
                        }
                    }
                    if (!duplicate) //If not a duplicate, adds it to race list
                    {
                        String raceCode = row["RaceCode"].ToString();
                        String raceOffice = row["OfficeDesc"].ToString();
                        PoliticalRaceList.Add(new PoliticalRace(raceCode, raceOffice, listOrdicRows));
                    }
                }

                foreach (OrderedDictionary row in listOrdicRows) //Converts election date into variable
                {
                    electionDate = Convert.ToDateTime(row["ElectionDate"]);
                }

                return true;
            }
            else //Displays if no file could be found
            {
                Console.WriteLine("Could not find file: " + fileLocation);
                return false;
            }
        }

        public List<PoliticalRace> GetRaces() /* !COMPLETED BLOCK! Return all the races run in this election. */
        {
            return PoliticalRaceList;
        }

        public PoliticalRace GetRaceByCode(String stringRaceCode) /* !COMPLETED BLOCK! Returns a single race by its code. */
        {
            foreach (PoliticalRace requestedRace in PoliticalRaceList)
            {
                if (requestedRace.raceCode == stringRaceCode)
                {
                    return requestedRace;
                }
            }
            return null;
        }

        public Dictionary<String, String> GetRaceCodes() /* !COMPLETED BLOCK! A list of all the race codes. */
        {
            Dictionary<String, String> raceCodeDict = new Dictionary<String, String>(); //Creates new dictionary

            foreach (PoliticalRace individualRace in PoliticalRaceList)
            {
                if (!raceCodeDict.ContainsKey(individualRace.raceCode)) //If Race doesn't exist, adds race to dictionary
                {
                    raceCodeDict.Add(individualRace.raceCode, individualRace.raceOffice);
                }
            }

            return raceCodeDict; //Returns dictionary
        }
    }

    class PoliticalRace /*Completed*/
    {
        /*Need to populate fields. Use a constructor */
        public String raceCode;
        public String raceOffice;
        private List<Candidate> CandidateList = new List<Candidate>(); /*Array of candidate objects*/
        private CountyResults raceResults;
        private List<OrderedDictionary> listOrdicRows;

        public PoliticalRace(String Code, String Office, List<OrderedDictionary> listOrdicRows)
        {
            raceCode = Code;
            raceOffice = Office;
            raceResults = new CountyResults(listOrdicRows, raceCode);
            this.listOrdicRows = listOrdicRows;

            foreach (OrderedDictionary row in listOrdicRows) //Populates candidate list
            {
                bool duplicate = false;
                foreach (Candidate indivCandidate in CandidateList) //Avoids adding if duplicate
                {
                    if (indivCandidate.nameLast == row["CanNameLast"].ToString() && indivCandidate.nameFirst == row["CanNameFirst"].ToString() && raceCode == row["RaceCode"].ToString())
                    {
                        duplicate = true;
                        break;
                    }
                }
                if (!duplicate && raceCode == row["RaceCode"].ToString()) //Adds if not duplicate
                {
                    String firstName = row["CanNameFirst"].ToString();
                    String middleName = row["CanNameMiddle"].ToString();
                    String lastName = row["CanNameLast"].ToString();
                    String partyName = row["PartyName"].ToString();
                    CandidateList.Add(new Candidate(firstName, middleName, lastName, partyName));
                }
            }
        }

        public String GetWinnerName() /*Completed*/
        {
            String winnerName = raceResults.overallWinner;
            return winnerName;
        }

        public Candidate GetWinnerCandidate() /*Completed*/
        {
            String winnerName = raceResults.overallWinner;

            foreach (Candidate potentialWinner in CandidateList)
            {
                if (potentialWinner.nameLast == winnerName)
                {
                    return potentialWinner;
                }
            }

            return null;
        }

        public List<County> GetCountiesWonByCandidate(String CandidateName) /*Completed*/
        {
            List<County> countyList = new List<County>();

            foreach (KeyValuePair<String, String> x in raceResults.countyDict) //Iterates through county dictionary
            {
                if (x.Value == CandidateName) //If value is equal to parameter, checks the whole file. If not, returns to dictionary until it finds candidate
                {
                    foreach(OrderedDictionary row in listOrdicRows)
                    {
                        String fullName = row["CanNameFirst"].ToString() + " " + row["CanNameLast"].ToString();
                        if (x.Value == fullName && x.Key == row["CountyName"].ToString()) //Populates county list for candidate if dictionary has winner and county
                        {
                            String countyCode = row["CountyCode"].ToString();
                            String countyName = row["CountyName"].ToString();
                            int precincts = int.Parse(row["Precincts"].ToString());
                            int precinctsReporting = int.Parse(row["PrecinctsReporting"].ToString());
                            countyList.Add(new County(countyCode, countyName, precincts, precinctsReporting));
                        }
                    }
                } 
            }

            return countyList; //Returns list of conties

        }

    }

    class Candidate /*COMPLETED BLOCK*/
    {
        public String nameFirst; /* CanNameFirst field */
        public String nameMiddle; /* CanNameMiddle field */
        public String nameLast; /* CanNameLast field */
        public String fullName;
        private String partyName;

        public Candidate(String firstName, String middleName, String lastName, String party)
        {
            nameFirst = firstName;
            nameMiddle = middleName;
            nameLast = lastName;
            partyName = party;
            fullName = nameFirst + " " + nameMiddle + " " + nameLast;
        }

        public String GetPartyName() /*!COMPLETED BLOCK!*/
        {
            return partyName;
        }
    }

    class CountyResults /*Completed*/
    {
        public List<String> CountyList = new List<String>();
        public Dictionary<String, String> countyDict = new Dictionary<String, String>(); /*Key is county name, value is list of ordered dictionaries*/
        public String overallWinner = "";
        private int greatestVotes = 0;
        private List<String> CandidateName = new List<String>();
        private String raceCode;
        private List<OrderedDictionary> listOrdicRows;

        public CountyResults(List<OrderedDictionary> listOrdicRows, String raceCode) /*Completed*/
        {
            this.raceCode = raceCode;
            this.listOrdicRows = listOrdicRows;


            foreach (OrderedDictionary row in listOrdicRows) //Populates county list of strings
            {
                bool duplicate = false;
                foreach (String indivCounty in CountyList)
                {
                    if (indivCounty == row["CountyName"].ToString() && raceCode == row["RaceCode"].ToString())
                    {
                        duplicate = true;
                        break;
                    }
                }
                if (!duplicate && raceCode == row["RaceCode"].ToString())
                {                    
                    String countyName = row["CountyName"].ToString();
                    CountyList.Add(countyName);
                }
            }
            candidateNameList();
            determineWinner();
            countyWinner();
        }

        public void candidateNameList() /*Completed. Adds candidates for race into list regardless of county*/
        {
            foreach (OrderedDictionary row in listOrdicRows) //Functions as similar dictionary populating functions
            {
                bool duplicate = false;
                foreach (String indivCandidate in CandidateName)
                {
                    String fullName = row["CanNameFirst"].ToString() + " " + row["CanNameLast"].ToString();
                    if (indivCandidate == fullName && raceCode == row["RaceCode"].ToString())
                    {
                        duplicate = true;
                        break;
                    }
                }
                if (!duplicate && raceCode == row["RaceCode"].ToString())
                {
                    String fullName = row["CanNameFirst"].ToString() + " " + row["CanNameLast"].ToString();
                    CandidateName.Add(fullName);
                }
            }
        }

        public void determineWinner() /*Completed. Determines overall winner of race based on votes*/
        {
            foreach (String indivCandidate in CandidateName)
            {
                String tempCandidateName = indivCandidate;
                int tempVoteTotals = 0;
                foreach (OrderedDictionary row in listOrdicRows)
                {
                    String fullName = row["CanNameFirst"].ToString() + " " + row["CanNameLast"].ToString();
                    if (tempCandidateName == fullName)
                    {
                        tempVoteTotals += int.Parse(row["CanVotes"].ToString()); //Adds vote numbers to running total if candidate found
                    }
                }
                if (tempVoteTotals > greatestVotes) //Switches winner if their votes exceed current greatest votes
                {
                    overallWinner = tempCandidateName;
                }
            }
        }

        public void countyWinner() /*Completed. Populates dictionary of counties and who won them*/
        {

            foreach (String testedCounty in CountyList) //Grabs county from list
            {
                int countyVotes = 0;
                String countyWinner = "";
                foreach (String potentialWinner in CandidateName) //Grabs candidate from list
                {
                    foreach (OrderedDictionary row in listOrdicRows) //Looks through file
                    {
                        String fullName = row["CanNameFirst"].ToString() + " " + row["CanNameLast"].ToString();
                        if (potentialWinner == fullName && testedCounty == row["CountyName"].ToString()) //If county and candidate match row
                        {
                            if (int.Parse(row["CanVotes"].ToString()) > countyVotes) //If their votes exceed the current votes for other county candidates
                            {
                                countyVotes = int.Parse(row["CanVotes"].ToString()); //Change county votes and winner
                                countyWinner = potentialWinner;
                            }
                        }

                    }
                }
                countyDict.Add(testedCounty, countyWinner); //Adds to dictionary once all candidates for that county check. Proceeds to next county
            }
        }

    }

    class County /*!COMPLETED BLOCK!*/
    {
        private String countyCode;
        public String countyName;
        private int countyPrecincts;
        private int countyPrecinctsReporting;

        public County(String Code, String Name, int Precincts, int Reporting)
        {
            countyCode = Code;
            countyName = Name;
            countyPrecincts = Precincts;
            countyPrecinctsReporting = Reporting;
        }

        public float PercentageOfPrecinctsReporting()
        {
            if (countyPrecincts == 0 && countyPrecinctsReporting == 0)
            {
                return 100; //Avoids divide by 0 error
            }
            else
            {
                return (countyPrecinctsReporting / countyPrecincts) * 100;
            }
        }

        public String GetCountyName()
        {
            return countyName;
        }
    }
    
}














    


