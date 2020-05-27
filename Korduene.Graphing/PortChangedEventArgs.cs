using System;

namespace Korduene.Graphing
{
    public class PortChangedEventArgs : EventArgs
    {
        public IPort Port { get; set; }

        public string PropertyName { get; set; }

        public PortChangedEventArgs(IPort port, string propertyName)
        {
            Port = port;
            PropertyName = propertyName;
        }
    }
}
