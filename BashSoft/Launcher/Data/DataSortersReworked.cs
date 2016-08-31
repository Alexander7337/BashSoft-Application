using System.Collections.Generic;
using System.Linq;

namespace BashSoft
{
    public class DataSortersReworked
    {
        //public void OrderAndTake(Dictionary<string, List<int>> wantedData, string comparison, int studentsToTake)
            public void OrderAndTake(Dictionary<string, double> studentsWithMarks, string comparison, int studentsToTake)
        {
            comparison = comparison.ToLower();
            if (comparison == "ascending")
            {
                //PrintStudents(studentsWithMarks.OrderBy(x => x.Value.Sum())
                //                        .Take(studentsToTake)
                //                        .ToDictionary(pair => pair.Key, pair => pair.Value));
                this.PrintStudents(studentsWithMarks.OrderBy(x => x.Value)
                                        .Take(studentsToTake)
                                        .ToDictionary(pair => pair.Key, pair => pair.Value));
            }
            else if (comparison == "descending")
            {
                //PrintStudents(studentsWithMarks.OrderByDescending(x => x.Value.Sum())
                //                        .Take(studentsToTake)
                //                        .ToDictionary(pair => pair.Key, pair => pair.Value));
                PrintStudents(studentsWithMarks.OrderByDescending(x => x.Value)
                        .Take(studentsToTake)
                        .ToDictionary(pair => pair.Key, pair => pair.Value));
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidComparisonQuery);
            }
        }

        //private void PrintStudents(Dictionary<string, List<int>> studentsSorted)
        //{
        //    foreach (KeyValuePair<string, List<int>> kvp in studentsSorted)
        //    {
        //        OutputWriter.PrintStudent(kvp);
        //    }
        //}

        private void PrintStudents(Dictionary<string, double> studentsSorted)
        {
            foreach (KeyValuePair<string, double> student in studentsSorted)
            {
                OutputWriter.PrintStudent(new KeyValuePair<string, double>(student.Key, student.Value));
            }
        }
    }
}
