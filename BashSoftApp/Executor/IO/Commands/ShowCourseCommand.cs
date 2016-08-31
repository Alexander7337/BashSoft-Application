using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Executor.Attributes;
using Executor.Contracts;
using Executor.Exceptions;
using Executor.Network;
using Executor.Repository;
using Executor.Judge;

namespace Executor.IO.Commands
{
    [Alias("show")]
    class ShowCourseCommand : Command, IExecutable
    {
        [Inject]
        private IDatabase repository;

        public ShowCourseCommand(string input, string[] data/*, IContentComparer tester,*/
            /*IDatabase repository, IDownloadManager downloadManager, IDirectoryManager ioManager*/)
            : base(input, data/*, tester, repository, downloadManager, ioManager*/)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length == 2)
            {
                string courseName = this.Data[1];
                this.repository.GetAllStudentsFromCourse(courseName);
            }
            else if (this.Data.Length == 3)
            {
                string courseName = this.Data[1];
                string userName = this.Data[2];
                this.repository.GetStudentScoresFromCourse(courseName, userName);
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }
    }
}
