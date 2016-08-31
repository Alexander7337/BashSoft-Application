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
    [Alias("mkdir")]
    class MakeDirectoryCommand : Command, IExecutable
    {
        [Inject]
        private IDirectoryManager inputOutputManager;

        public MakeDirectoryCommand(string input, string[] data/*, IContentComparer tester,*/
            /*IDatabase repository, IDownloadManager downloadManager, IDirectoryManager ioManager*/)
            : base(input, data/*, tester, repository, downloadManager, ioManager*/)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }

            string folderName = this.Data[1];
            this.inputOutputManager.CreateDirectoryInCurrentFolder(folderName);
        }
    }
}
