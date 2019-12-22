using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    public class MemberItem : OwnerItem
    {
        public MemberItem(Member participant) : base(participant)
        {
        }

        public Member Member => base.Owner as Member;
    }
}
