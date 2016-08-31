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
    [Alias("readdb")]
    class ReadDatabaseCommand : Command, IExecutable
    {
        [Inject]
        private IDatabase repository;

        public ReadDatabaseCommand(string input, string[] data/*, IContentComparer tester,*/
           /* IDatabase repository, IDownloadManager downloadManager, IDirectoryManager ioManager*/)
            : base(input, data/*, tester, repository, downloadManager, ioManager*/)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }

            string fileName = this.Data[1];
            this.repository.LoadData(fileName);
        }
    }
}
