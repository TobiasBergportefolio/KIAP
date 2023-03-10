using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiAP_projekt.Model;

namespace KiAP_projekt.ViewModel
{
    public class CreateMemberViewModel
    {
        private MemberRepository memberRepository = new MemberRepository();

        //This method instantiates a new member and then calls the Add method from the MemberRepository
        //to add the new member to our database
        public void CreateMember(string name, string password, string phoneNumber, string email)
        {
            //A new member is instantiated, and then used as the argument for the Add method
            Member member = new Member(name, password, phoneNumber, email);
            memberRepository.Add(member);
        }

        //This method calls the SendMail method from the MemberRepository to send a mail to the new member
        public void SendMemberMail(string name, string password, string email)
        {
            memberRepository.SendMail(name, password, email);
        }
    }
}
