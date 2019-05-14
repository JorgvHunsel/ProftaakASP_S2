using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Data.Interfaces;
using Models;

namespace Data.Contexts
{
    public class UserContextSQL : IUserContext
    {
        private const string ConnectionString =
            @"Data Source=mssql.fhict.local;Initial Catalog=dbi423244;User ID=dbi423244;Password=wsx234;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private static readonly SqlConnection _conn = new SqlConnection(ConnectionString);

        public void AddNewUser(User newUser)
        {
            try
            {
                string query = "INSERT INTO [User] (FirstName, LastName, Birthdate, Sex, Email, Address, PostalCode, City, Password, AccountType, Status) VALUES (@FirstName, @LastName, @Birthdate, @Sex, @Email, @Address, @PostalCode, @City, @Password, @AccountType, @Status)";
                _conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, _conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", SqlDbType.NVarChar).Value = newUser.FirstName;
                    cmd.Parameters.AddWithValue("@LastName", SqlDbType.NVarChar).Value = newUser.LastName;
                    cmd.Parameters.AddWithValue("@Birthdate", SqlDbType.Date).Value = newUser.BirthDate;
                    cmd.Parameters.AddWithValue("@Sex", SqlDbType.NVarChar).Value = newUser.UserGender.ToString();
                    cmd.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = newUser.EmailAddress;
                    cmd.Parameters.AddWithValue("@Address", SqlDbType.NVarChar).Value = newUser.Address;
                    cmd.Parameters.AddWithValue("@PostalCode", SqlDbType.NChar).Value = newUser.PostalCode;
                    cmd.Parameters.AddWithValue("@City", SqlDbType.NVarChar).Value = newUser.City;
                    cmd.Parameters.AddWithValue("@Password", SqlDbType.VarChar).Value = newUser.Password;
                    cmd.Parameters.AddWithValue("@AccountType", SqlDbType.NVarChar).Value = newUser.UserAccountType.ToString();
                    cmd.Parameters.AddWithValue("@Status", SqlDbType.Bit).Value = true;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public void EditUser(User currentUser, string password)
        {
            try
            {
                string query = "UPDATE [User] " +
                               "SET FirstName = @FirstName, LastName = @LastName, Sex = @Sex, Email = @Email, Address = @Address, PostalCode = @PostalCode, City = @City, Status = @Status " +
                               "WHERE UserID = @UserID";
                if (password != "")
                {
                    query = "UPDATE [User] " +
                            "SET FirstName = @FirstName," +
                            " LastName = @LastName, " +
                            "Birthdate = @Birthdate, " +
                            "Sex = @Sex, " +
                            "Email = @Email, " +
                            "Address = @Address, " +
                            "PostalCode = @PostalCode, " +
                            "City = @City, " +
                            "Password = @Password, " +
                            "Status = @Status " +
                            "WHERE UserID = @UserID";
                }
                _conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, _conn))
                {
                    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = currentUser.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = currentUser.LastName;
                    cmd.Parameters.Add("@Birthdate", SqlDbType.DateTime).Value = currentUser.BirthDate;
                    cmd.Parameters.Add("@Sex", SqlDbType.Char).Value = currentUser.UserGender;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = currentUser.EmailAddress;
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = currentUser.Address;
                    cmd.Parameters.Add("@PostalCode", SqlDbType.NChar).Value = currentUser.PostalCode;
                    cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = currentUser.City;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = currentUser.UserId;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;
                    cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = currentUser.Status;

                    cmd.ExecuteNonQuery();
                }

                
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }


        public List<User> GetAllUsers()
        {
            string query =
                "SELECT UserID, AccountType, FirstName, LastName, Birthdate, Sex, Email, Address, PostalCode, City, Status, Password " +
                "FROM [User]";

            _conn.Open();
            SqlCommand cmd = new SqlCommand(query, _conn);

            List<User> Users = new List<User>();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int userID = reader.GetInt32(0);
                    string accountType = reader.GetString(1);
                    string firstName = reader.GetString(2);
                    string lastName = reader.GetString(3);
                    DateTime birthDate = reader.GetDateTime(4);
                    User.Gender gender = (User.Gender)Enum.Parse(typeof(User.Gender), reader.GetString(5));
                    string email = reader.GetString(6);
                    string address = reader.GetString(7);
                    string postalCode = reader.GetString(8);
                    string city = reader.GetString(9);
                    bool status = reader.GetBoolean(10);
                    string password = reader.GetString(11);



                    if (accountType == "CareRecipient")
                    {

                        User user = new CareRecipient(userID, firstName, lastName, address, city, postalCode, email,
                            birthDate, gender, status, User.AccountType.CareRecipient, password);
                        Users.Add(user);
                    }

                    else if (accountType == "Volunteer")
                    {
                        User user = new CareRecipient(userID, firstName, lastName, address, city, postalCode, email,
                            birthDate, gender, status, User.AccountType.Volunteer, password);
                        Users.Add(user);
                    }
                    else
                    {
                        User user = new CareRecipient(userID, firstName, lastName, address, city, postalCode, email,
                            birthDate, gender, status, User.AccountType.Admin, password);
                        Users.Add(user);
                    }
                }
            }

            _conn.Close();
            return Users;
        }

        public int GetUserId(string email)
        {
            try
            {
                string query = "SELECT [UserID] FROM [User] WHERE [Email] = @email";
                _conn.Open();

                SqlParameter emailParam = new SqlParameter();
                emailParam.ParameterName = "@email";

                SqlCommand cmd = new SqlCommand(query, _conn);
                emailParam.Value = email;
                cmd.Parameters.Add(emailParam);

                int UserId = (int)cmd.ExecuteScalar();

                return UserId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public bool CheckIfAccountIsActive(string email)
        {
            try
            {
                string query = "SELECT [Status] FROM [User] WHERE [Email] = @email";
                _conn.Open();

                SqlParameter emailParam = new SqlParameter();
                emailParam.ParameterName = "@email";

                SqlCommand cmd = new SqlCommand(query, _conn);
                emailParam.Value = email;
                cmd.Parameters.Add(emailParam);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetBoolean(0))
                        {
                            _conn.Close();
                            return true;
                        }
                    }
                }
                else
                {
                    return false;
                }

                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _conn.Close();
            }

            return false;
        }

        public User CheckValidityUser(string emailAdress, string password)
        {
            try
            {
                string query =
                    "SELECT UserID, AccountType, FirstName, LastName, Birthdate, Sex, Email, Address, PostalCode, City, Status, Password " +
                    "FROM [User] " +
                    "WHERE [Email] = @Email";
                _conn.Open();

                SqlDataAdapter cmd = new SqlDataAdapter();
                cmd.SelectCommand = new SqlCommand(query, _conn);

                cmd.SelectCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = emailAdress;
                cmd.SelectCommand.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;

                DataTable dt = new DataTable();
                cmd.Fill(dt);

                int userID = Convert.ToInt32((dt.Rows[0].ItemArray[0]));
                string accountType = dt.Rows[0].ItemArray[1].ToString();
                string firstName = dt.Rows[0].ItemArray[2].ToString();
                string lastName = dt.Rows[0].ItemArray[3].ToString();
                DateTime birthDate = Convert.ToDateTime(dt.Rows[0].ItemArray[4].ToString());
                User.Gender gender = (User.Gender)Enum.Parse(typeof(User.Gender), dt.Rows[0].ItemArray[5].ToString());
                string email = dt.Rows[0].ItemArray[6].ToString();
                string address = dt.Rows[0].ItemArray[7].ToString();
                string postalCode = dt.Rows[0].ItemArray[8].ToString();
                string city = dt.Rows[0].ItemArray[9].ToString();
                bool status = Convert.ToBoolean(dt.Rows[0].ItemArray[10].ToString());
                string hashedPassword = dt.Rows[0].ItemArray[11].ToString();


                if(!Hasher.SecurePasswordHasher.Verify(password, hashedPassword))
                    throw new ArgumentException("Password invalid");

                if (accountType == "CareRecipient")
                {
                    return new CareRecipient(userID, firstName, lastName, address, city, postalCode, email,
                        birthDate, gender, status, User.AccountType.CareRecipient, hashedPassword);
                }

                else if (accountType == "Volunteer")
                {
                    return new CareRecipient(userID, firstName, lastName, address, city, postalCode, email,
                        birthDate, gender, status, User.AccountType.Volunteer, hashedPassword);
                }
                else if ((accountType == "Admin"))
                {
                    return new CareRecipient(userID, firstName, lastName, address, city, postalCode, email,
                         birthDate, gender, status, User.AccountType.Admin, hashedPassword);
                }
                else if (accountType == "Professional")
                {
                    return new Professional(userID, firstName, lastName, address, city, postalCode, email,
                        birthDate, gender, status, User.AccountType.Professional, hashedPassword);
                }


                else
                {
                    throw new AggregateException("User not found");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public bool CheckIfUserAlreadyExists(string email)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM [User] WHERE [Email] = @email";

                _conn.Open();

                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@email",
                    Value = email
                });

                int numberofAccounts = (int)cmd.ExecuteScalar();

                if (numberofAccounts == 1)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _conn.Close();
            }
            return true;
        }

        public User GetCurrentUserInfo(string email)
        {
            try
            {
                string query =
                    "SELECT UserID, AccountType, FirstName, LastName, Birthdate, Sex, Email, Address, PostalCode, City, Status, Password " +
                    "FROM [User] " +
                    "WHERE Email = @email";

                _conn.Open();
                SqlParameter emailParam = new SqlParameter();
                emailParam.ParameterName = "@email";
                SqlCommand cmd = new SqlCommand(query, _conn);
                emailParam.Value = email;
                cmd.Parameters.Add(emailParam);
                User currentUser = new Admin("a", "b", "c,", "d", "e", "f", Convert.ToDateTime("1988/12/20"), User.Gender.Man, true, User.AccountType.CareRecipient, "");
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string accountType = reader.GetString(1);
                        User.Gender gender = (User.Gender)Enum.Parse(typeof(User.Gender), reader.GetString(5));


                        if (accountType == "Admin")
                        {
                            currentUser = new Admin(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetString(7), reader.GetString(9), reader.GetString(8), email,
                                reader.GetDateTime(4), gender, reader.GetBoolean(10), User.AccountType.Admin, reader.GetString(11));
                        }
                        else if (accountType == "Professional")
                        {
                            currentUser = new Professional(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetString(7), reader.GetString(9), reader.GetString(8), email,
                                reader.GetDateTime(4), gender, reader.GetBoolean(10), User.AccountType.Professional, reader.GetString(11));
                        }
                        else if (accountType == "Volunteer")
                        {
                            currentUser = new Volunteer(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetString(7), reader.GetString(9), reader.GetString(8), email,
                                reader.GetDateTime(4), gender, reader.GetBoolean(10), User.AccountType.Volunteer, reader.GetString(11));
                        }
                        else
                        {
                            currentUser = new CareRecipient(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetString(7), reader.GetString(9), reader.GetString(8), email,
                                reader.GetDateTime(4), gender, reader.GetBoolean(10), User.AccountType.CareRecipient, reader.GetString(11));
                        }
                        return currentUser;
                    }
                    return currentUser;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public User GetUserById(int userId)
        {
            try
            {
                string query =
                    "SELECT UserID, AccountType, FirstName, LastName, Birthdate, Sex, Email, Address, PostalCode, City, Status, Password " +
                    "FROM [User] " +
                    "WHERE [UserID] = @UserId";
                _conn.Open();

                SqlDataAdapter cmd = new SqlDataAdapter();
                cmd.SelectCommand = new SqlCommand(query, _conn);

                cmd.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                DataTable dt = new DataTable();
                cmd.Fill(dt);

                int userID = Convert.ToInt32((dt.Rows[0].ItemArray[0]));
                string accountType = dt.Rows[0].ItemArray[1].ToString();
                string firstName = dt.Rows[0].ItemArray[2].ToString();
                string lastName = dt.Rows[0].ItemArray[3].ToString();
                DateTime birthDate = Convert.ToDateTime(dt.Rows[0].ItemArray[4].ToString());
                User.Gender gender = (User.Gender)Enum.Parse(typeof(User.Gender), dt.Rows[0].ItemArray[5].ToString());
                string email = dt.Rows[0].ItemArray[6].ToString();
                string address = dt.Rows[0].ItemArray[7].ToString();
                string postalCode = dt.Rows[0].ItemArray[8].ToString();
                string city = dt.Rows[0].ItemArray[9].ToString();
                bool status = Convert.ToBoolean(dt.Rows[0].ItemArray[10].ToString());
                string password = dt.Rows[0].ItemArray[11].ToString();


                if (accountType == "CareRecipient")
                {
                    return new CareRecipient(userID, firstName, lastName, address, city, postalCode, email,
                        birthDate, gender, status, User.AccountType.CareRecipient, password);
                }

                else if (accountType == "Volunteer")
                {
                    return new CareRecipient(userID, firstName, lastName, address, city, postalCode, email,
                        birthDate, gender, status, User.AccountType.Volunteer, password);
                }
                else if (accountType == "Admin")
                {
                    return new CareRecipient(userID, firstName, lastName, address, city, postalCode, email,
                         birthDate, gender, status, User.AccountType.Admin, password);
                }
                else if (accountType == "Professional")
                {
                    return new Professional(userID, firstName, lastName, address, city, postalCode, email,
                        birthDate, gender, status, User.AccountType.Professional, password);
                }

                return null;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public bool IsEmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
