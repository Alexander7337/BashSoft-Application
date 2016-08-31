using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Launcher.Models;
using System;
using System.Linq;

namespace BashSoft
{
    //Data class reads data from a file in current directory if it exists.

    public class DataReworked
    {
        private bool isDataInitialized;

        //private Dictionary<string, Dictionary<string, List<int>>> studentsByCourse;
        private Dictionary<string, Course> courses;
        private Dictionary<string, Student> students;


        private DataFilters filter;
        private DataSortersReworked sorter;

        public DataReworked(DataSortersReworked sorter, DataFilters filter)
        {
            this.filter = filter;
            this.sorter = sorter;
            //this.studentsByCourse = new Dictionary<string, Dictionary<string, List<int>>>();
        }

        public void LoadData(string fileName)
        {
            if (this.isDataInitialized)
            {
                //OutputWriter.WriteMessageOnNewLine("Reading data...");
                //studentsByCourse = new Dictionary<string, Dictionary<string, List<int>>>();
                //this.ReadData(fileName);

                OutputWriter.DisplayException(ExceptionMessages.DataAlreadyInitialisedException);
                return;
            }
            //else
            //{
            //    OutputWriter.WriteMessageOnNewLine(ExceptionMessages.DataAlreadyInitialisedException);
            //}

            this.students = new Dictionary<string, Student>();
            this.courses = new Dictionary<string, Course>();
            this.ReadData(fileName);
        }

        public void UnloadData()
        {
            if (!this.isDataInitialized)
            {
                OutputWriter.DisplayException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }

            //this.studentsByCourse = new Dictionary<string, Dictionary<string, List<int>>>();
            this.students = null;
            this.courses = null;
            this.isDataInitialized = false;
        }

        public void ReadData(string fileName)
        {
            //LAB 5. REQUIRES REPLACEMENT OF THE INPUT VARIABLE AND WHILE LOOP
            //string input = Console.ReadLine();

            //while (!string.IsNullOrEmpty(input))
            //{


            string path = SessionData.currentPath + "\\" + fileName;
            if (File.Exists(path))
            {
                OutputWriter.WriteMessageOnNewLine("Reading data...");

                //string pattern = @"([A-Z][a-zA-Z#+]*_[A-Z][a-z]{2}_\d{4})\s+([A-Z][a-z]{0,3}\d{2}_\d{2,4})\s+(\d+)";
                string pattern = @"([A-Z][a-zA-Z#\++]*_[A-Z][a-z]{2}_\d{4})\s+([A-Za-z]+\d{2}_\d{2,4})\s([\s0-9]+)";
                Regex rgx = new Regex(pattern);
                string[] allInputLines = File.ReadAllLines(path);

                for (int line = 0; line < allInputLines.Length; line++)
                {

                    if (!string.IsNullOrEmpty(allInputLines[line]) && rgx.IsMatch(allInputLines[line]))
                    {
                        Match currentMatch = rgx.Match(allInputLines[line]);
                        string courseName = currentMatch.Groups[1].Value;
                        string username = currentMatch.Groups[2].Value;

                        //int mark;
                        //bool hasParsedScore = int.TryParse(currentMatch.Groups[3].Value, out mark);

                        //string[] data = allInputLines[line].Split(' ');
                        //string course = data[0].Trim();
                        //string student = data[1].Trim();
                        //int mark = int.Parse(data[2]);

                        //if (hasParsedScore && mark >= 0 && mark <= 100)
                        //{
                        //    if (!studentsByCourse.ContainsKey(course))
                        //    {
                        //        studentsByCourse.Add(course, new Dictionary<string, List<int>>());
                        //        studentsByCourse[course].Add(student, new List<int>());
                        //        studentsByCourse[course][student].Add(mark);
                        //    }
                        //    else if (!studentsByCourse[course].ContainsKey(student))
                        //    {
                        //        studentsByCourse[course].Add(student, new List<int>());
                        //        studentsByCourse[course][student].Add(mark);
                        //    }
                        //    else
                        //    {
                        //        studentsByCourse[course][student].Add(mark);
                        //    }
                        //}

                        string scoresStr = currentMatch.Groups[3].Value;

                        try
                        {
                            int[] scores = scoresStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                                    .Select(int.Parse)
                                                    .ToArray();
                            if (scores.Any(x => x > 100 || x < 0))
                            {
                                OutputWriter.DisplayException(ExceptionMessages.InvalidScore);
                            }

                            if (scores.Length > Course.NumberOfTasksOnExam)
                            {
                                OutputWriter.DisplayException(ExceptionMessages.InvalidNumberOfScores);
                                continue;
                            }

                            if (!this.students.ContainsKey(username))
                            {
                                this.students.Add(username, new Student(username));
                            }

                            if (!this.courses.ContainsKey(courseName))
                            {
                                this.courses.Add(courseName, new Course(courseName));
                            }

                            Course course = this.courses[courseName];
                            Student student = this.students[username];

                            student.EnrollInCourse(course);
                            student.SetMarkOnCourse(courseName, scores);

                            course.EnrollStudent(student);
                        }
                        catch (FormatException fex)
                        {
                            OutputWriter.DisplayException(fex.Message + $"at line : {line}");
                        }
                        //catch (ArgumentException ae)
                        //{
                        //    OutputWriter.DisplayException(ae.Message);
                        //}
                    }
                }

                isDataInitialized = true;
                OutputWriter.WriteMessageOnNewLine("Data read!");

            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
            }
            //input = Console.ReadLine();
            //}

            //throw new NotImplementedException();
        }

        private bool IsQueryForCoursePossible(string courseName)
        {
            if (isDataInitialized)
            {
                if (this.courses.ContainsKey(courseName))
                {
                    return true;
                }
                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.InexistingCourseInDataBase);
                }

                return true;
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }

            return false;
        }

        private bool IsQueryForStudentPossible(string courseName, string studentUserName)
        {
            if (IsQueryForCoursePossible(courseName) && this.courses[courseName].StudentsByName.ContainsKey(studentUserName))
            {
                return true;
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InexistingStudentInDataBase);
            }

            return false;
        }

        public void GetStudentScoresFromCourse(string courseName, string username)
        {
            if (IsQueryForStudentPossible(courseName, username))
            {
                OutputWriter.PrintStudent(new KeyValuePair<string, double>(username, this.courses[courseName].StudentsByName[username].MarksByCourseName[courseName]));
            }
        }

        public void GetAllStudentsFromCourse(string courseName)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine($"{courseName}:");
                foreach (var studentMarksEntry in this.courses[courseName].StudentsByName)
                {
                    //OutputWriter.PrintStudent(studentMarksEntry);
                    this.GetStudentScoresFromCourse(courseName, studentMarksEntry.Key);
                }
            }
        }

        //LAB 7. FUNCTIONAL PROGRAMMING
        public void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    //studentsToTake = studentsByCourse[courseName].Count;
                    studentsToTake = this.courses[courseName].StudentsByName.Count;
                }
                //DataFilters.FilterAndTake(studentsByCourse[courseName], givenFilter, studentsToTake.Value);
                //filter.FilterAndTake(studentsByCourse[courseName], givenFilter, studentsToTake.Value);

                Dictionary<string, double> marks = this.courses[courseName].StudentsByName
                    .ToDictionary(x => x.Key, x => x.Value.MarksByCourseName[courseName]);
                this.filter.FilterAndTake(marks, givenFilter, studentsToTake.Value);
            }
        }

        public void OrderAndTake(string courseName, string comparison, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].StudentsByName.Count;
                }
                //DataSortersReworked.OrderAndTake(studentsByCourse[courseName], comparison, studentsToTake.Value);

                //sorter.OrderAndTake(studentsByCourse[courseName], comparison, studentsToTake.Value);
                Dictionary<string, double> marks =
                    this.courses[courseName].StudentsByName.ToDictionary(x => x.Key, x => x.Value.MarksByCourseName[courseName]);
                this.sorter.OrderAndTake(marks, comparison, studentsToTake.Value);
            }
        }
    }
}
