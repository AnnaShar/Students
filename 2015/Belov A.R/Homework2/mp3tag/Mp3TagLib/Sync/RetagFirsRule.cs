﻿using System;
using System.Collections.Generic;

namespace Mp3TagLib.Sync
{
    public class RetagFirsRule:ISyncRule
    {
        public List<SyncOperation> OperationsList { get; private set; }

        public RetagFirsRule()
        {
            OperationsList = new List<SyncOperation> { new Retag(), new Rename() };
        }
    }
}
