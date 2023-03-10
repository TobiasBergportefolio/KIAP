using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiAP_projekt.Model;

namespace KiAP_projekt.ViewModel
{
 public class MemberViewModel
    {
        //This class is used to present the information of our Member class to the View layer
        //to ensure that the view layer has no knowlegde of the model layer

        //This is the specific instance of the Member class that this class will represent
        private Member member;

        public int MemberID
        {
            get { return member.MemberID; }
        }
        public string Name
        {
            get { return member.Name; }
        }
        public string Password
        {
            get { return member.Password; }
        }
        public string PhoneNumber
        {
            get { return member.PhoneNumber; }
        }
        public string Email
        {
            get { return member.Email; }
        }
        public string Role
        {
            get { return member.Role; }
        }

        //The constructor takes a member as input and assigns it to the private member field
        public MemberViewModel(Member member)
        {
            this.member = member;
        }

        //This method specifies what to output when the ToString method is called
        public override string ToString()
        {
            return $"{Name}, {Email}";
        }
    }
}
