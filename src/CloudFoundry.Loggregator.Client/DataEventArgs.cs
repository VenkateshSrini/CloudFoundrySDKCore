namespace CloudFoundry.Loggregator.Client
{
    using System;

    public class DataEventArgs : EventArgs
    {
        public ApplicationLog Data
        {
            get;
            set;
        }
    }
}