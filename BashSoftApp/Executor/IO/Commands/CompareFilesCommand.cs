﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Executor.Attributes;
using Executor.Contracts;
using Executor.Exceptions;
using Executor.Network;
using Executor.Judge;
using Executor.Repository;

namespace Executor.IO.Commands
{
    [Alias("cmp")]
    class CompareFilesCommand : Command, IExecutable
    {
        [Inject]
        private IContentComparer tester;

        public CompareFilesCommand(string input, string[] data/*, IContentComparer tester,*/
            /*IDatabase repository, IDownloadManager downloadManager, IDirectoryManager ioManager*/)
            : base(input, data/*, tester, repository, downloadManager, ioManager*/)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 3)
            {
                throw new InvalidCommandException(this.Input);
            }

            string firstPath = this.Data[1];
            string secondPath = this.Data[2];

            this.tester.CompareContent(firstPath, secondPath);
        }
    }
}
