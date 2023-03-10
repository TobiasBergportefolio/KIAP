using KiAP_projekt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiAP_projekt.ViewModel.Login
{
   
    public class LoginDialogViewModel
    {
        //We make an instance of memberRepository in a private field
        private MemberRepository memberRepository;
        public LoginDialogViewModel()
        {
            memberRepository = new MemberRepository();
        }

        //This method takes a username and password as input and calls the VerifyUser method from the memebrRepository
        //This is used to verify the user when they attempt to log in
        public bool VerifyUser(string username, string password)
        {
            bool verifyUser = memberRepository.VerifyUser(username, password);
            return verifyUser;
        }

        //This method gets the ID of the user matching the username and password
        //by calling the GetUserID method from the memberRepository
        public void GetUserID(string username, string password)
        {
            memberRepository.GetUserID(username, password);
        }

    }
}
