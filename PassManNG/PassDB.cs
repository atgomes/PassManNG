using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace PassManNG
{
    public class PassDB
    {
        private string connectionString = Properties.Settings.Default.PassDatabase;

        public Items GetItem(int ID)
        {
            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand(con);
            cmd.CommandText = "SELECT * FROM MyTable WHERE ID = '" + Convert.ToString(ID) + "'";


            try
            {
                con.Open();
                SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    Items item = new Items((string)reader["dKey"], (string)reader["profile"], (string)reader["ePassword"], (string)reader["username"], (string)reader["notes"]);
                    return (item);
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                con.Close();
            }
        }

        public Items GetItem(String Username)
        {
            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand(con);
            cmd.CommandText = "SELECT * FROM MyTable WHERE username = '" + Convert.ToString(Username) + "'";

            try
            {
                con.Open();
                SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    Items item = new Items((string)reader["dKey"], (string)reader["profile"], (string)reader["ePassword"], (string)reader["username"], (string)reader["notes"]);
                    return (item);
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                con.Close();
            }
        }

        public List<Items> GetAllItems()
        {
            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand(con);
            cmd.CommandText = "SELECT * FROM MyTable";

            List<Items> novosItems = new List<Items>();
            try
            {
                con.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Items item = new Items((string)reader["dKey"], (string)reader["profile"], (string)reader["ePassword"], (string)reader["username"], (string)reader["notes"]);

                    novosItems.Add(item);
                }
            }
            finally
            {
                con.Close();
            }

            return (novosItems);
        }

        public void UpItem(Items item)
        {
            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand(con);

            cmd.CommandText = "INSERT INTO MyTable (dKey,profile,ePassword,username,notes) VALUES ('" + item.DirectKey + "','" + item.Owner + "','" + item.EncryptedPassword + "','" + item.Username + "','" + item.Notes + "')";
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void RemoveItem(String label)
        {
            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand(con);
            cmd.CommandText = "DELETE FROM MyTable WHERE dKey = '" + label + "'";

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void RemoveAllItens()
        {
            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand(con);
            cmd.CommandText = "DELETE FROM MyTable";

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void RemoveAllItens(String owner)
        {
            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand(con);
            cmd.CommandText = "DELETE FROM MyTable WHERE profile = '" + owner + "'";

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void UpProfile(Items profile)
        {
            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand(con);

            cmd.CommandText = "INSERT INTO Profiles (dKey,ePassword,username,notes) VALUES ('" + profile.DirectKey + "','" + profile.EncryptedPassword + "','" + profile.Username + "','" + profile.Notes + "')";
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void UpdatePasswordsByField(String field, String newValue, String oldValue)
        {
            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand(con);

            cmd.CommandText = "UPDATE MyTable SET " + field + " = REPLACE(" + field + ", '" + oldValue + "', '" + newValue + "') WHERE " + field + " = '" + oldValue +"'";

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public Items GetProfile(String username)
        {
            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand(con);
            cmd.CommandText = "SELECT * FROM Profiles WHERE username = '" + username + "'";

            try
            {
                con.Open();
                SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    Items item = new Items((string)reader["dKey"], (string)reader["username"], (string)reader["ePassword"], (string)reader["username"], (string)reader["notes"]);
                    return (item);
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                con.Close();
            }
        }

        public void RemoveProfile(String username)
        {
            SQLiteConnection con = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand(con);
            cmd.CommandText = "DELETE FROM Profiles WHERE username = '" + username + "'";

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
