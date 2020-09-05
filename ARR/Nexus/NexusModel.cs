using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker.ARR.Nexus
{
    public class NexusModel
    {
        internal string CurrentNexus;
        internal int CurrentLight;

        public NexusModel()
        {

        }

        public string NexusActivity { get; internal set; }
    }
}
