using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace KiAP_projekt.Model
{
    public class MemberRepository
    {
        //Creating a list of members as a private field
        private List<Member> members;

        //string for establishing connection to database
        private string connectionString = "Server=10.56.8.36; database=P2DB05; user id=P2-05; password=OPENDB_05;";

        //The constructor populates the local list with data from the database
        public MemberRepository()
        {
            //We instantiate the List of Members
            members = new List<Member>();

            //Establishing a connection to our Database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Opening the Database connection
                connection.Open();

                //We make an SQL Statements that will select coloumns from our Database.
                SqlCommand cmd = new SqlCommand("SELECT MemberID, Name, Password, PhoneNumber, Email, Role FROM MEMBER", connection);

                //We make a datareader which will read the Values in our chosen Coloumns
                using (SqlDataReader dr = cmd.ExecuteReader())

                {
                    // We make a While-Loop to ensure the datareader keeps on reading while there are more rows to read
                    while (dr.Read())
                    {

                        //We assign values from our Database to variables
                        //and convert the values when necessary to the correct C# datatypes.
                        int memberID = int.Parse(dr["MemberID"].ToString());
                        string name = dr["Name"].ToString();
                        string password = dr["Password"].ToString();
                        string phoneNumber = dr["PhoneNumber"].ToString();
                        string email = dr["Email"].ToString();
                        string role = dr["Role"].ToString();

                        //We instantiate a new object of Member using the variables retrieved from the database.
                        Member member = new Member(memberID, name, password, phoneNumber, email, role);

                        //We add the new object our local list members.
                        members.Add(member);
                    }
                }
            }
        }

        //This method adds a new member to the database and the local list
        //The method doesn't return a value so the return type is void.
        public void Add(Member member)
        {
            //We establish a connection to our Database.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Open the connection.
                connection.Open();

                //We make a SQL Statement which will insert values into the chosen Coloumns. The @ will provide us with additional protection against SQL-injections.
                SqlCommand cmd = new SqlCommand("INSERT INTO MEMBER(Name, Password, PhoneNumber, Email, Role)" +
                    "VALUES(@Name, @Password, @PhoneNumber, @Email, @Role)", connection);

                //We add values to the parameters of the SQL statement and declare the SQL datatype
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = member.Name;
                cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = member.Password;
                cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar).Value = member.PhoneNumber;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = member.Email;
                cmd.Parameters.Add("@Role", SqlDbType.NVarChar).Value = member.Role;

                //We execute a nonquery as we are not retrieving any data from the database
                cmd.ExecuteNonQuery();

                //We add the new member to the local list
                members.Add(member);
            }
        }

        //This method removes the member from our database and local list
        public void Delete(Member member)
        {
            //We establish the connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //We open the connection to the database
                connection.Open();

                //We create an SQL command which deletes the member with the specified ID
                SqlCommand cmd = new SqlCommand("DELETE FROM MEMBER WHERE MemberID = @MemberID", connection);

                //We add the ID of the member as a parameter to the SQL statement
                cmd.Parameters.Add("@MemberID", SqlDbType.Int).Value = member.MemberID;

                //We are not retrieving data from the database so we execute a nonquery.
                cmd.ExecuteNonQuery();
            }
            members.Remove(member);
        }

        //This method updates values for a member
        public void Update(int id, string name, string phoneNumber, string email)
        {
            //We loop through the list to find the member with the specified ID
            foreach (Member m in members)
            {
                //We update the properties of the matching Member
                if (m.MemberID == id)
                {
                    m.Name = name;
                    m.PhoneNumber = phoneNumber;
                    m.Email = email;
                }
                //We break out of the loop once the member has been found and updated
                //so we don't keep cycling through the loop
                break;
            }

            //We establish a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //We open the connection
                connection.Open();

                //We make an SQL statement which updates the values of the member in the database
                SqlCommand cmd = new SqlCommand("UPDATE MEMBER " +
                                                "SET Name = @Name, PhoneNumber = @PhoneNumber, Email = @Email " +
                                                "WHERE MemberID = @MemberID", connection);

                //We assign values to the parameters of the SQL statement
                cmd.Parameters.Add("@MemberID", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
                cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar).Value = phoneNumber;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;

                //We are not retrieving data from the database so we execute a nonquery
                cmd.ExecuteNonQuery();
            }
        }

        //This method returns a member that has the specified ID
        public Member GetById(int id)
        {
            //We cycle through the list to find the member that has the specified ID
            foreach (Member m in members)
            {
                if (m.MemberID == id)
                {
                    return m;
                }
            }
            //if no members have the matching ID we return null.
            return null;
        }

        //this returns a list of all members
        public List<Member> GetAll()
        {
            //We create a new list which we will return at the end
            List<Member> result = new List<Member>();

            //We establish a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //We open the connection
                connection.Open();

                //We make an SQL statement which will retrieve the specified columns from all the rows of the database
                SqlCommand cmd = new SqlCommand("SELECT MemberID, Name, Password, PhoneNumber, Email, Role FROM MEMBER", connection);
                
                //We use a datareader to retrieve the rows from the database
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    //We create a loop which will read the next row from the stream
                    //as long as there are more nonempty rows
                    while (dr.Read())
                    {
                        //We assign the valus retrieved from the database to variables
                        int memberID = int.Parse(dr["MemberID"].ToString());
                        string name = dr["Name"].ToString();
                        string password = dr["Password"].ToString();
                        string phoneNumber = dr["PhoneNumber"].ToString();
                        string email = dr["Email"].ToString();
                        string role = dr["Role"].ToString();

                        //We instantiate a new member using the retrievd data from the database
                        Member member = new Member(memberID, name, password, phoneNumber, email, role);

                        //We add the member to the list "result"
                        result.Add(member);

                    }
                }
            }
            //we return the list "result" which now includes all the members from the database
            return result;
        }

        //This comment sends a mail to the newly created member
        public void SendMail(string name, string password, string email)
        {
            string mailSubject = $"Velkommen til KiAPklyngemøde platformen {name}.";

            string mailMessage = $"Kære {name}\n\nDin klyngekoordinator har hermed oprettet dig som medlem til KiAPklyngemøde platformen.\n\n" +
                $"Din brugernavn er: {name}\nDit password er: {password}\n\nLog ind på KiAPklyngemøde platformen og ændr dit password under 'min side'.";

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("kiapklynge@gmail.com", "[,:SyZ2Am&*!WPGh");
            System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage("kiapklynge@gmail.com", email, mailSubject, mailMessage);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnFailure;
            client.Send(mm);
        }


      
        //This method determines whether or not the the specified username and password
        //correspond to a row in the database
        public bool VerifyUser(string username, string password)
        {
            //We establish a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //We open the connection
                connection.Open();

                //We make an SQL command that returns returns a value if the condition is met.
                SqlCommand cmd = new SqlCommand("SELECT MemberID " +
                    "FROM MEMBER " +
                    "WHERE Name='" + username + "' and Password='" + password + "'", connection);

                //We use a datareader to read the output from the Command
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    //This simply returns the value or dr.Read(),
                    //which will be true if the DataReader returns a row.
                    //If the reader returns a row that means that
                    //the username and password do correspond to a row in the database and the user should be verified
                    return dr.Read();
                }

            }
        }


        //This method retrieves and saves the ID of the user logging into the system
        public void GetUserID(string username, string password)
        {
            //We establish a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //We open the database connection
                connection.Open();

                //We make an SQL command which retrieves the ID of the specified username and password
                SqlCommand cmd = new SqlCommand("SELECT MemberID " +
                    "FROM MEMBER " +
                    "WHERE Name='" + username + "' and Password='" + password + "'", connection);
                
                //Using a datareader we retrieve the MemberID
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    //We use a streamwriter to log the memberID
                    using (StreamWriter streamWriter = new StreamWriter("tempID.txt", false))
                    {
                        //We write the memberID to the text file
                        dr.Read();
                        streamWriter.WriteLine(dr["MemberID"].ToString());
                    }

                }

            }
        }

        // This Methods gets the role of the current user
        public string GetRole()
        {
            //We use a streamreader to get the id of the user saved in a text file
            //and convert it to an int
            using (StreamReader streamReader = new StreamReader("tempID.txt"))
            {
                int memberID = int.Parse(streamReader.ReadLine());

                //We establish a connection to the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //We open the database connection
                    connection.Open();

                    //We make an SQL statement to retrieve the Role from the Member with the given ID
                    SqlCommand cmd = new SqlCommand("SELECT Role FROM MEMBER WHERE MemberID ='" + memberID + "'", connection);

                    //Using a datareader we retrieve the data
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dr.Read();
                        return dr["Role"].ToString();
                    }

                }
            }
        }



    }
}
