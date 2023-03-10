using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace KiAP_projekt.Model
{
    public class ClusterMeetingRepository
    {
        //Creating a list of clusterMeetings in a private Field. 
        private List<ClusterMeeting> clusterMeetings;

        //private string to connect to the Database server.
        private string connectionString = "Server=10.56.8.36; database=P2DB05; user id=P2-05; password=OPENDB_05;";

        //The constructor populates the list.
        public ClusterMeetingRepository()
        {
            //We instantiate the list of ClusterMeeting
            clusterMeetings = new List<ClusterMeeting>();

            //We establish a connection to the Database.
            //The Using Statement closes the connection automatically.
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                //We open the connection.
                connection.Open();

                //SQL Statement that selects columns from the database.
                SqlCommand cmd = new SqlCommand("SELECT ClusterMeetingID, ClusterPackageName, Date, Time, Duration, Online, Address, PostalCode, City, RegistrationDeadline FROM CLUSTERMEETING", connection);
                
                //Creating a Datareader to read our values from the selected columns 
                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    //We create a While-Loop for the datareader to read the values as long as there are rows to read
                    while (dr.Read())
                    {
                        //We assign values from the database to our variables. We parse the variables that are not strings to their corresponding datatypes.
                        int clusterMeetingID = int.Parse(dr["ClusterMeetingID"].ToString());
                        string clusterPackageName = dr["ClusterPackageName"].ToString();
                        DateTime date = DateTime.Parse(dr["Date"].ToString());
                        DateTime time = DateTime.Parse(dr["Time"].ToString());
                        double duration = double.Parse(dr["Duration"].ToString());
                        bool online = bool.Parse(dr["Online"].ToString());
                        string address = dr["Address"].ToString();
                        string postalCode = dr["PostalCode"].ToString();
                        string city = dr["City"].ToString();
                        DateTime registrationDeadline = DateTime.Parse(dr["RegistrationDeadline"].ToString());

                        //We instantiate a new object of ClusterMeeting using the values we have retrieved from the Database.
                        ClusterMeeting clusterMeeting = new ClusterMeeting(clusterMeetingID, clusterPackageName, date, time, duration, registrationDeadline, online, address, postalCode, city);
                        
                        //Finished is being determined here as the value is not automatically updated in the database.
                        clusterMeeting.Finished = DateTime.Now > date;
                        
                        //We add the object to our local list clusterMeetings.
                        clusterMeetings.Add(clusterMeeting);
                    }
                }
            }

        }

        
        //Return type is Void as it does not return anything.
        //This method Adds ClusterMeeting to our Database and the List clusterMeetings.
        public void Add(ClusterMeeting clusterMeeting)
        {
            //We establish the connection to the database through the connectionstring operation.
            //The Using Statement closes the connection automatically.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Opening the connection.
                connection.Open();

                //SQL Statement that insert the List to the CLUSTERMEETING Table Database.
                //We Assign the values with a @ to protect from SQL-Injections.
                SqlCommand cmd = new SqlCommand("INSERT INTO CLUSTERMEETING(ClusterPackageName, Date, Time, Duration, Online, Address, PostalCode, City, Finished, RegistrationDeadline)" +
                    "VALUES(@ClusterPackageName, @Date, @Time, @Duration, @Online, @Address, @PostalCode, @City, @Finished, @RegistrationDeadline)", connection);

                //We aasign ClusterMeeting properties as values to the parameters of the statement.
                //These values will be converted and added to the database row.
                cmd.Parameters.Add("@ClusterPackageName", SqlDbType.NVarChar).Value = clusterMeeting.ClusterPackageName;
                cmd.Parameters.Add("@Date", SqlDbType.DateTime2).Value = clusterMeeting.Date;
                cmd.Parameters.Add("@Time", SqlDbType.DateTime2).Value = clusterMeeting.Time;
                cmd.Parameters.Add("@Duration", SqlDbType.Float).Value = clusterMeeting.Duration;
                cmd.Parameters.Add("@Online", SqlDbType.Bit).Value = clusterMeeting.Online;

                //The if statement checks if the address is Null. If this is the case it assign a Database Null value to address, postalcode and city.
                //This is because the database doesn't recognise the C# Null value. 
                if (clusterMeeting.Address == null)
                {
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@PostalCode", SqlDbType.NVarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                //The Else If statement checks if the online is checked. If this is the case we assign Null to PostalCode
                //and city and the link will be assigned to the address column.
                else if (clusterMeeting.Online)
                {
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = clusterMeeting.Address;
                    cmd.Parameters.Add("@PostalCode", SqlDbType.NVarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                //If the Else statement is executed that means values are assigned to address, postalcode and city.
                //These values are then assigned to the corresponding database columns.
                else
                {
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = clusterMeeting.Address;
                    cmd.Parameters.Add("@PostalCode", SqlDbType.NVarChar).Value = clusterMeeting.PostalCode;
                    cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = clusterMeeting.City;
                }
                //We aasign ClusterMeeting properties as values to the parameters of the statement. These values will be converted and added to the database row.
                cmd.Parameters.Add("@Finished", SqlDbType.Bit).Value = clusterMeeting.Finished;
                cmd.Parameters.Add("@RegistrationDeadline", SqlDbType.DateTime2).Value = clusterMeeting.RegistrationDeadline;

                //As long as we do not retrieve any data from the database it's not a query and thus a nonquery.
                cmd.ExecuteNonQuery();

                //Add the clusterMeeting to the local list clusterMeetings. 
                clusterMeetings.Add(clusterMeeting);
            }
        }

        //Return type is Void as it does not return anything.
        //This method Deletes ClusterMeeting to our Database and the List clusterMeetings.
        public void Delete(ClusterMeeting clusterMeeting)
        {
            //We establish the connection to the database through the connectionstring operation.
            //The Using Statement closes the connection automatically.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Opening the connection.
                connection.Open();
               
                //Choosing which ClusterMeeting we want to delete from the database based on the ID.
                SqlCommand cmd = new SqlCommand("DELETE FROM CLUSTERMEETING WHERE ClusterMeetingID = @ClusterMeetingID", connection);
                
                //We convert the datatype and assign it a varible.
                cmd.Parameters.Add("@ClusterMeetingID", SqlDbType.Int).Value = clusterMeeting.ClusterMeetingID;

                //As we are not retrieving any data from the database we execute a nonQuery
                cmd.ExecuteNonQuery();
            }

            //We remove ClusterMeeting from our local list
            clusterMeetings.Remove(clusterMeeting);
        }

        //Method that updates values in clustermeeting 
        public void Update(int id, DateTime date, DateTime time, double duration, string clusterPackageName, DateTime registrationDeadline, bool online, string address = null, string postalCode = null, string city = null)
        {
            //We loop through clustermeeting list to find clusterMeeting that matches id       
            foreach (ClusterMeeting cm in clusterMeetings)
            {
                //If a clusterMeeting matches the id, we assign the new values to the properties of the matching clusterMeeting
                if (cm.ClusterMeetingID == id)
                {
                    cm.Date = date;
                    cm.Time = time;
                    cm.Duration = duration;
                    cm.ClusterPackageName = clusterPackageName;
                    cm.RegistrationDeadline = registrationDeadline;
                    cm.Online = online;
                    cm.Address = address;
                    cm.PostalCode = postalCode;
                    cm.City = city;
                }
                //We break out of the loop once we have found the matching clusterMeeting 
                break;
            }

            //Replacing in the database

            //We establish the connection to the database through the connectionstring operation.
            //The Using Statement closes the connection automatically.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Opening the connection.
                connection.Open();

                //SQL Statement that Update the List to the CLUSTERMEETING Table Database. We Assign the values with a @ to protect from SQL-Injections.
                SqlCommand cmd = new SqlCommand("UPDATE CLUSTERMEETING " +
                                                "SET ClusterPackageName = @ClusterPackageName, Date = @Date, Time = @Time, Duration = @Duration, Online = @Online, Address = @Address, PostalCode = @PostalCode, City = @City, RegistrationDeadline = @RegistrationDeadline " +
                                                "WHERE ClusterMeetingID = @ClusterMeetingID", connection);
                
                //We assign ClusterMeeting properties as values to the parameters of the statement.
                //These values will be converted and added to the database row.
                cmd.Parameters.Add("@ClusterPackageName", SqlDbType.NVarChar).Value = clusterPackageName;
                cmd.Parameters.Add("@Date", SqlDbType.DateTime2).Value = date;
                cmd.Parameters.Add("@Time", SqlDbType.DateTime2).Value = time;
                cmd.Parameters.Add("@Duration", SqlDbType.Float).Value = duration;
                cmd.Parameters.Add("@Online", SqlDbType.Bit).Value = online;

                //The if statement checks if the address is Null. If this is the case it assign Null to address, postalcode and city.
                //This is because the database doesn't recognise the C# Null value.
                if (address == null)
                {
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@PostalCode", SqlDbType.NVarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                //The Else If statement checks if the online is checked. If this is the case we assign Null to PostalCode and city and the link will be assigned to the address column.
                else if (online)
                {
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = address;
                    cmd.Parameters.Add("@PostalCode", SqlDbType.NVarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                //If the Else statement is executed that means values are assigned to address, postalcode and city.
                //These values are then assigned to the corresponding database columns.
                else
                {
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = address;
                    cmd.Parameters.Add("@PostalCode", SqlDbType.NVarChar).Value = postalCode;
                    cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = city;
                }
                cmd.Parameters.Add("@RegistrationDeadline", SqlDbType.DateTime2).Value = registrationDeadline;
                cmd.Parameters.Add("@ClusterMeetingID", SqlDbType.Int).Value = id;

                //As long as we do not retrieve any data from the database
                //it's not a query and thus we execute a nonquery.
                cmd.ExecuteNonQuery();
            }
        }

        //Return type is ClusterMeeting as we are getting a ClusterMeeting from the method
        //This method gets the ClusterMeeting from our Database and the List clusterMeetings.
        public ClusterMeeting GetById(int id)
        {
            //We create a new instance of ClusterMeeting which we will return when we get the ClusterMeeting
            ClusterMeeting clusterMeeting;

            //We establish the connection to the database through the connectionstring operation.
            //The Using Statement closes the connection automatically.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Opening the connection.
                connection.Open();

                //SQL Statement that selects the row from the CLUSTERMEETING Table Database which matches the ID.
                //We Assign the values with a @ to protect from SQL-Injections.
                SqlCommand cmd = new SqlCommand("SELECT * FROM CLUSTERMEETING WHERE ClusterMeetingID = @ClusterMeetingID", connection);
                
                //We assign the id as a parameter to the Statement
                cmd.Parameters.Add("@ClusterMeetingID", SqlDbType.Int).Value = id;

                //Creating a Datareader to read our values from the selected columns 
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    //We create a While-Loop for the datareader to read the values as long as there are rows to read
                    while (dr.Read())
                    {
                        //We create variables to hold the values we retrieve from the database
                        //and they are converting to the correct datatypes
                        int clusterMeetingID = int.Parse(dr["ClusterMeetingID"].ToString());
                        string clusterPackageName = dr["ClusterPackageName"].ToString();
                        DateTime date = DateTime.Parse(dr["Date"].ToString());
                        DateTime time = DateTime.Parse(dr["Time"].ToString());
                        double duration = double.Parse(dr["Duration"].ToString());
                        bool online = bool.Parse(dr["Online"].ToString());
                        string address = dr["Address"].ToString();
                        string postalCode = dr["PostalCode"].ToString();
                        string city = dr["City"].ToString();
                        bool finished = bool.Parse(dr["Finished"].ToString());
                        DateTime registrationDeadline = DateTime.Parse(dr["RegistrationDeadline"].ToString());

                        //We instantiate a new object of ClusterMeeting using the values we have retrieved from the Database.
                        clusterMeeting = new ClusterMeeting(clusterMeetingID, clusterPackageName, date, time, duration, registrationDeadline, online, address, postalCode, city);

                        //Finished is being determined here as the value is not automatically updated in the database.
                        clusterMeeting.Finished = DateTime.Now > date;

                        //Returns the object.
                        return clusterMeeting;
                    }
                }
            }

            return null;
        }

        public List<ClusterMeeting> GetAll()
        {
            //We instantiate a list of ClusterMeetings to return
            List<ClusterMeeting> result = new List<ClusterMeeting>();

            //We establish the connection to the database through the connectionstring operation.
            //The Using Statement closes the connection automatically.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Opening the connection.
                connection.Open();

                //SQL Statement that selects the all rows from the CLUSTERMEETING Table Database
                SqlCommand cmd = new SqlCommand("SELECT ClusterMeetingID, ClusterPackageName, Date, Time, Duration, Online, Address, PostalCode, City, RegistrationDeadline FROM CLUSTERMEETING", connection);

                //Creating a Datareader to read our values from the selected columns 
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    //We create a While-Loop for the datareader to read the values as long as there are rows to read
                    while (dr.Read())
                    {
                        //We We create variables to hold the values we retrieve from the database.
                        //and convert them to the correct datatypes.
                        int clusterMeetingID = int.Parse(dr["ClusterMeetingID"].ToString());
                        string clusterPackageName = dr["ClusterPackageName"].ToString();
                        DateTime date = DateTime.Parse(dr["Date"].ToString());
                        DateTime time = DateTime.Parse(dr["Time"].ToString());
                        double duration = double.Parse(dr["Duration"].ToString());
                        bool online = bool.Parse(dr["Online"].ToString());
                        string address = dr["Address"].ToString();
                        string postalCode = dr["PostalCode"].ToString();
                        string city = dr["City"].ToString();
                        DateTime registrationDeadline = DateTime.Parse(dr["RegistrationDeadline"].ToString());

                        //We instantiate a new object of ClusterMeeting using the values we have retrieved from the Database.
                        ClusterMeeting clusterMeeting = new ClusterMeeting(clusterMeetingID, clusterPackageName, date, time, duration, registrationDeadline, online, address, postalCode, city);
                        
                        //Finished is being determined here as the value is not automatically updated in the database.
                        clusterMeeting.Finished = DateTime.Now > date;

                        //We add the object to our the list result that we created.
                        result.Add(clusterMeeting);
                    }
                }
            }
            //Returns the list result.
            return result;
        }

        //This method only retrieves any upcoming meetings
        public List<ClusterMeeting> GetUpcomingMeetings()
        {
            //Creating a new list of the ClusterMeeting object with the name result.
            List<ClusterMeeting> result = new List<ClusterMeeting>();

            //We establish a connection to the Database.
            //The Using Statement closes the connection automatically.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Opening the connection.
                connection.Open();

                //SQL Statement that selects the List to the CLUSTERMEETING Table Database
                SqlCommand cmd = new SqlCommand("SELECT * FROM CLUSTERMEETING", connection);

                //Creating a Datareader to read our values from the selected columns 
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    //We create a While-Loop for the datareader to read the values as long as there are rows to read
                    while (dr.Read())
                    {
                        //We We create variables to hold the values we retrieve from the database.
                        //and convert them to the correct datatypes.
                        int clusterMeetingID = int.Parse(dr["ClusterMeetingID"].ToString());
                        string clusterPackageName = dr["ClusterPackageName"].ToString();
                        DateTime date = DateTime.Parse(dr["Date"].ToString());
                        DateTime time = DateTime.Parse(dr["Time"].ToString());
                        double duration = double.Parse(dr["Duration"].ToString());
                        bool online = bool.Parse(dr["Online"].ToString());
                        string address = dr["Address"].ToString();
                        string postalCode = dr["PostalCode"].ToString();
                        string city = dr["City"].ToString();
                        DateTime registrationDeadline = DateTime.Parse(dr["RegistrationDeadline"].ToString());

                        //We instantiate a new object of ClusterMeeting using the values we have retrieved from the Database.
                        ClusterMeeting clusterMeeting = new ClusterMeeting(clusterMeetingID, clusterPackageName, date, time, duration, registrationDeadline, online, address, postalCode, city);
                        
                        //Finished is being determined here as the value is not updated in the database.
                        clusterMeeting.Finished = DateTime.Now > date;

                        //If the Finished status of the ClusterMeeting is false we add it to the list result.
                        if (clusterMeeting.Finished == false)
                        {
                            result.Add(clusterMeeting);
                        }
                    }
                }
            }
            //Returns the list Result.
            return result;
        }

        public void SendMail(string clusterPackageName, DateTime date, DateTime time, double duration, DateTime registrationDeadline)
        {
            string mailSubject = $"Invitation til klyngemødet: {clusterPackageName} d. {date.ToShortDateString()}";

            string mailMessage = $"Kære xxx\n\nDin klyngekoordinator har hermed inviteret dig til klygemødet med emnet: {clusterPackageName}" +
                $"\nMødet vil foregå: d. {date.ToShortDateString()} kl. {time.ToString("HH:mm")} og vil vare i ca. {duration} timer.\nTilmeldingsfrist d. {registrationDeadline.ToShortDateString()}." +
                $"\nLog ind på KiAPklyngemøde platformen og se mere information om mødet.\n\nMed venlig hilsen xxxx";

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("kiapklynge@gmail.com", "[,:SyZ2Am&*!WPGh");
            System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage("kiapklynge@gmail.com", "Christianleube@hotmail.com", mailSubject, mailMessage);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnFailure;
            client.Send(mm);
        }
    }
}
