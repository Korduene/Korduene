using Korduene.Graphing;
using Korduene.Graphing.UndoData;
using System.Collections.Generic;

namespace Korduene.Common.UndoUnits
{
    /// <summary>
    /// Node move undo unit
    /// </summary>
    /// <seealso cref="Korduene.Common.UndoUnit" />
    public class MemberMoveUndoUnit : UndoUnit
    {
        private List<MemberMoveUndoData> _data;

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public List<MemberMoveUndoData> Data
        {
            get { return _data; }
            set
            {
                if (_data != value)
                {
                    _data = value;
                    OnPropertyChanged();
                }
            }
        }

        public MemberMoveUndoUnit(string name) : base(name)
        {
            _data = new List<MemberMoveUndoData>();
        }

        public MemberMoveUndoUnit(string name, IEnumerable<IGraphMember> members) : base(name)
        {
            _data = new List<MemberMoveUndoData>();

            foreach (var member in members)
            {
                _data.Add(new MemberMoveUndoData(member, member.LastLocation.X, member.LastLocation.Y));
            }
        }

        public override void Undo()
        {
            if (_data == null)
            {
                return;
            }

            foreach (var item in _data)
            {
                var curX = item.Member.Location.X;
                var curY = item.Member.Location.Y;
                item.Member.Location = new GraphPoint(item.X, item.Y);
                item.X = curX;
                item.Y = curY;
            }

            base.Undo();
        }
    }
}
