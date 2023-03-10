using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiAP_projekt.Model
{
    public class Member
    {
        public int MemberID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        //This property has a default value as a member has this role per default when created
        public string Role { get; set; } = "Klyngemedlem";

        //This constructor assigns values to the properties
        public Member(string name, string password, string phoneNumber, string email)
        {
            Name = name;
            Password = password;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        //Constructor overloading using chaining to be able to assign MemberID ad Role when istantiating an object of this class.
        //This is used when retrieving data from the database
        public Member(int memberID, string name, string password, string phoneNumber, string email, string role) : this
            (name, password, phoneNumber, email)
        {
            MemberID = memberID;
            Role = role;
        }

        // Constructor for which only assigns MemberID for use in Verify user logic
        public Member(int memberID)
        {
            MemberID = memberID;
        }

    }
}
