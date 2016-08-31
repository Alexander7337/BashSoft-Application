using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Executor.Attributes;
using Executor.Contracts;
using Executor.Exceptions;

namespace Executor.IO.Commands
{
    [Alias("display")]
    public class DisplayCommand : Command, IExecutable
    {
        [Inject]
        private IDatabase repository;

        public DisplayCommand(string input, string[] data/*, IContentComparer tester, */
            /*IDatabase repository, IDownloadManager downloadManager, IDirectoryManager ioManager*/) 
            : base(input, data/*, tester, repository, downloadManager, ioManager*/)
        {
        }

        public override void Execute()
        {
            string[] data = this.Data;

            if (data.Length != 3)
            {
                throw new InvalidCommandException(this.Input);
            }

            string entityToDisplay = data[1];
            string sortType = data[2];
            if (entityToDisplay.Equals("students", StringComparison.OrdinalIgnoreCase))
            {
                IComparer<IStudent> studentComparator = this.CreateStudentComparator(sortType);
                ISimpleOrderedBag<IStudent> list = this.repository.GetAllStudentsSorted(studentComparator);
                OutputWriter.WriteMessageOnNewLine(list.JoinWith(Environment.NewLine));
            }
            else if (entityToDisplay.Equals("courses", StringComparison.OrdinalIgnoreCase))
            {
                IComparer<ICourse> courseComparator = this.CreateCourseComparator(sortType);
                ISimpleOrderedBag<ICourse> list = this.repository.GetAllCoursesSorted(courseComparator);
                OutputWriter.WriteMessageOnNewLine(list.JoinWith(Environment.NewLine));
            }
        }

        private IComparer<ICourse> CreateCourseComparator(string sortType)
        {
            if (sortType.Equals("ascending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<ICourse>.Create((course1, course2) => course1.CompareTo(course2));
            }
            else if (sortType.Equals("descending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<ICourse>.Create((course1, course2) => course2.CompareTo(course1));
            }
            else
            {
                throw new InvalidOperationException(this.Input);
            }
        }

        private IComparer<IStudent> CreateStudentComparator(string sortType)
        {
            if (sortType.Equals("ascending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<IStudent>.Create((student1, student2) => student1.CompareTo(student2));
            }
            else if (sortType.Equals("descending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<IStudent>.Create((student1, student2) => student2.CompareTo(student1));
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
            
        }
    }
}
