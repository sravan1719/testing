using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Populate_HubPage
{
    public class ReadData
    {
        public List<string> Type = new List<string>();
        public List<string> Area = new List<string>();
        public List<string> SuiteName = new List<string>();
        public List<string> WlnDemoResult = new List<string>();
        public List<string> WlnQedResult = new List<string>();
        public List<string> EdgeDemoResult = new List<string>();
        public List<string> EdgeQedResult = new List<string>();
        public List<string> Date = new List<string>();
        public List<string> Notes = new List<string>();

        public int GetTotalCountOfSuites()
        {
            using (var rd = new StreamReader("D:\\Automation_Task\\testing\\Results.csv"))
            {
                var splits = rd.ReadLine().Split(',');
                while (!rd.EndOfStream)
                {
                    splits = rd.ReadLine().Split(',');
                    Type.Add(splits[0]);
                    Area.Add(splits[1]);
                    SuiteName.Add(splits[2]);
                    Date.Add(splits[3]);
                    WlnDemoResult.Add(splits[4]);
                    WlnQedResult.Add(splits[5]);
                    EdgeDemoResult.Add(splits[6]);
                    EdgeQedResult.Add(splits[7]);
                    Notes.Add(splits[8]);
                }
            }
            return SuiteName.Count;
        }

        public string GetType(int index)
        {
            return Type[index];
        }

        public string GetArea(int index)
        {
            return Area[index];
        }

        public string GetSuiteName(int index)
        {
            return SuiteName[index];
        }

        public string GetDate(int index)
        {
            return Date[index];
        }

        public string GetNotes(int index)
        {
            if (Notes[index] == "")
                Notes[index] = " ";
            return Notes[index];
        }

        public List<string> GetSuiteResults(int index)
        {
            List<string> Results = new List<string>();
            this.ReplaceResultsIfEmpty(WlnDemoResult[index], Results);
            this.ReplaceResultsIfEmpty(WlnQedResult[index], Results);
            this.ReplaceResultsIfEmpty(EdgeDemoResult[index], Results);
            this.ReplaceResultsIfEmpty(EdgeQedResult[index], Results);

            return Results;
        }

        public void ReplaceResultsIfEmpty(string value,List<string> SuiteResults)
        {
            if (value == "")
                return ;            
            SuiteResults.Add(value);
        }
    }
}
