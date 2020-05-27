using System;

namespace Korduene.Graphing
{
    public class NodeChangedEventArgs : EventArgs
    {
        public NodeChangeEventType ChangeType { get; private set; }

        public IPort Port { get; private set; }

        public NodeChangedEventArgs(NodeChangeEventType changeType)
        {
            this.ChangeType = changeType;
        }

        public NodeChangedEventArgs(NodeChangeEventType changeType, IPort port) : this(changeType)
        {
            this.Port = port;
        }
    }
}
