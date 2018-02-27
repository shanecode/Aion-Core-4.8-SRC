using ASM31.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM31
{
    public class ASMDB
    {
        private Globals glb = Globals.instance;
        private MySqlConnection loginDBConn, gameDBConn;

        private List<Account> players = new List<Account>();
        private List<Toon> toons = new List<Toon>();


        static ASMDB instance = new ASMDB();

        public static ASMDB getInstance()
        {
            if (instance == null)
                instance = new ASMDB();
            return instance;
        }

        private ASMDB()
        {

        }

        public MySqlConnection getLoginDBConn()
        {
            if (glb.getLoginDBUrl() == null)
                return null;
            loginDBConn = new MySqlConnection(glb.getLoginDBUrl());
            loginDBConn.Open();
            return loginDBConn;
        }

        public MySqlConnection getGameDBConn()
        {
            if (glb.getGameDBUrl() == null)
                return null;
            gameDBConn = new MySqlConnection(glb.getGameDBUrl());
            gameDBConn.Open();
            return gameDBConn;
        }

        public void closeLoginConn()
        {
            if (loginDBConn != null)
                loginDBConn.Close();
            loginDBConn = null;
        }

        public void closeGameConn()
        {
            if (gameDBConn != null)
                gameDBConn.Close();
            gameDBConn = null;
        }

        object tryGet(MySqlDataReader rdr, string col, char ctype)
        {
            int ord = rdr.GetOrdinal(col);
            if (rdr.IsDBNull(ord))
            {
                // handle null column value since Mysql c# connector does nto like those at all
                if (ctype == 'd')
                    return DateTimePicker.MinimumDateTime; // return a datatime that should not expire
                        
                else if (ctype == 'f')
                    return 0.0;
                return null;
            }

            switch (ctype)
            {
                case 'i': return rdr.GetInt32(ord);
                case 's': return rdr.GetString(ord);
                case 'b': return rdr.GetByte(ord);
                case 'f': return rdr.GetDouble(ord);
                case 'd': return rdr.GetDateTime(ord);
            }
            return null;
        }

        public List<Account> getAccounts()
        {
            if (players == null)
                players = new List<Account>();
            else
                players.Clear();
            string SQL = "Select * from account_data";
            MySqlDataReader rdr;

            try
            {
                MySqlCommand cmd = new MySqlCommand(SQL, getLoginDBConn());
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    int id = (int)tryGet(rdr, "id", 'i');
                    string name = (string)tryGet(rdr, "name", 's');
                    string password = (string)tryGet(rdr, "password", 's');
                    byte activated = (byte)tryGet(rdr, "activated", 'b');
                    byte bal = (byte)tryGet(rdr, "access_level", 'b');
                    AccountType access_level = (AccountType)bal;
                    bal = (byte)tryGet(rdr, "membership", 'b');
                    Membership membership = (Membership)bal;
                    bal = (byte)tryGet(rdr, "old_membership", 'b');
                    Membership old_membership = (Membership)bal;
                    byte last_server = (byte)tryGet(rdr, "last_server", 'b');
                    string last_ip = (string)tryGet(rdr, "last_ip", 's');
                    string last_mac = (string)tryGet(rdr, "last_mac", 's');
                    string ip_force = (string)tryGet(rdr, "ip_force", 's');
                    DateTime expire = (DateTime)tryGet(rdr, "expire", 'd');
                    int toll = (int)tryGet(rdr, "toll", 'i');
                    double balance = (double)tryGet(rdr, "balance", 'f');
                    string email = (string)tryGet(rdr, "email", 's');
                    string question = (string)tryGet(rdr, "question", 's');
                    string answer = (string)tryGet(rdr, "answer", 's');

                    Account acct = new Account();
                    acct.id = id;
                    acct.name = name;
                    acct.password = password;
                    acct.activated = activated;
                    acct.access_level = access_level;
                    acct.membership = membership;
                    acct.old_membership = old_membership;
                    acct.last_server = last_server;
                    acct.last_ip = last_ip;
                    acct.last_mac = last_mac;
                    acct.ip_force = ip_force;
                    acct.expire = expire;
                    acct.toll = toll;
                    acct.balance = balance;
                    acct.email = email;
                    acct.question = question;
                    acct.answer = answer;
                    players.Add(acct);
                }
            }
            catch (MySqlException e)
            {
                clearMySQLErrors();
                //
                Console.Out.WriteLine("MySqlException caught " + e.Message);
            }
            finally
            {
                closeLoginConn();
            }
            return players;
        }



        public List<Toon> getToons()
        {
            toons.Clear();
            string SQL = "Select *  from players";
            MySqlDataReader rdr;
            try
            {

                MySqlCommand cmd = new MySqlCommand(SQL, getGameDBConn());
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string name = rdr.GetString("name");
                    string race = rdr.GetString("race");
                    short online = rdr.GetInt16("online");
                    toons.Add(new Toon(name, race, (online == 1)));
                }
                rdr.Close();
                closeGameConn();
            }
            catch (MySqlException ex)
            {
                clearMySQLErrors();
                //
                Console.Out.WriteLine("MySqlException caught " + ex.Message);
                closeGameConn();
            }
            return toons;
        }
        internal void refreshAll()
        {
            getAccounts();
            getToons();
        }
        public int getPlayerCount(Membership membership)
        {
            if (players.Count == 0)
                players = getAccounts();

            int count = 0;
            foreach (Account p in players)
                if (p.membership == membership)
                    ++count;
            return count;
        }


        public int getToonCount(Races race)
        {
            if (toons.Count == 0)
                getToons();
            if (race == Races.All)
                return toons.Count;
            string sRace;
            if (race == Races.Asmo)
                sRace = "ASMODIANS";
            else
                sRace = "ELYOS";
            int count = 0;
            foreach (Toon aToon in toons)
            {
                if (aToon.race.Equals(sRace))
                    ++count;
            }
            return count;
        }

        public int getOnlineToonCount(Races race)
        {
            if (toons.Count == 0)
                getToons();

            string sRace = "";
            if (race == Races.Asmo)
                sRace = "ASMODIANS";
            else if (race == Races.Elyo)
                sRace = "ELYOS";
            int count = 0;
            foreach (Toon aToon in toons)
            {
                if (aToon.isOnline)
                {
                    if (race == Races.All)
                        count++;
                    else if (aToon.race.Equals(sRace))
                        count++;
                }
            }
            return count;
        }

        public List<Toon> getOnlineToons()
        {
            List<Toon> rToons = new List<Toon>();
            if (toons.Count == 0)
                getToons();

            foreach (Toon aToon in toons)
            {
                if (aToon.isOnline)
                    rToons.Add(aToon);
            }
            return rToons;
        }


        public void clearMySQLErrors()
        {
            string msp = getMySQLPath();
            if (msp == null)
                return;

            // execute this
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = msp + "\\mysqladmin.exe";
            // Enter the executable to run, including the complete path
            start.Arguments = "flush-hosts";        // Enter in the command line arguments
            Process proc = Process.Start(start);    // fire off the process.
            proc.WaitForExit(250);                  // wait max 1/4 second

        }

        private string getMySQLPath()
        {
            string mysqlPath = null;
            Process[] processlist = Process.GetProcesses();

            foreach (Process proc in processlist)
            {
                string s = proc.ProcessName;
                if (s.StartsWith("mysql"))
                {
                    ProcessModule module = proc.MainModule;
                    s = module.FileName;
                    mysqlPath = s.Substring(0, s.LastIndexOf("\\"));
                    // yay!
                    break;
                }
            }
            return mysqlPath;
        }

        // Maintenance note:
        // update statement will only update the modifiable fields. 
        // to support other updates youj will need tujp change the sql string and add the appropriate parameters
        public bool updateAccount(Account acct)
        {
            bool result = true;    //  succeeded

            string sql = "update account_data set" +
                " name = @name," +
                " password = @password," +
                " activated = @activated," +
                " access_level = @access_level," +
                " membership = @membership," +
                " old_membership = @old_membership," +
                " expire = @expire," +
                " toll = @toll," +
                " balance = @balance," +
                " email = @email," +
                " question = @question," +
                " answer = @answer" +
                " where id = @id";
            MySqlConnection dbcon = getLoginDBConn();
            MySqlCommand dbcmd = new MySqlCommand(sql, dbcon);

            try
            {
                dbcmd.Parameters.AddWithValue("@name", acct.name);
                dbcmd.Parameters.AddWithValue("@password", acct.password);
                dbcmd.Parameters.AddWithValue("@activated", acct.activated);
                dbcmd.Parameters.AddWithValue("@access_level", (byte)acct.access_level);
                dbcmd.Parameters.AddWithValue("@membership", (byte)acct.membership);
                dbcmd.Parameters.AddWithValue("@old_membership", (byte)acct.old_membership);
                dbcmd.Parameters.AddWithValue("@expire", acct.expire);
                dbcmd.Parameters.AddWithValue("@toll", acct.toll);
                dbcmd.Parameters.AddWithValue("@balance", acct.balance);
                dbcmd.Parameters.AddWithValue("@email", acct.email);
                dbcmd.Parameters.AddWithValue("@question", acct.question);
                dbcmd.Parameters.AddWithValue("@answer", acct.answer);
                dbcmd.Parameters.AddWithValue("@id", acct.id);
                MySqlDataReader rdr = dbcmd.ExecuteReader();

                while (rdr.Read()) ;
                result = true;
            }
            catch (MySqlException e)
            {
                string msg = e.Message;
                result = false;
            }
            dbcon.Close();
            return result;
        }





        /*
            private string getUserCount(string type)
            {
                string SQL = "Select count(*) as COUNT from account_data where membership = "
                        + type;
                MySqlDataReader rdr;
                try
                {

                    MySqlCommand cmd = new MySqlCommand(SQL,loginDBConn);
                    rdr = cmd.ExecuteReader();
 
                    while (rdr.Read())
                    {
                        return rdr.GetString("COUNT");
                    }

                } catch (MySqlException)
                {
                }
                return "NA";

            }

            public string getFreeUserCount()
            {
                return getUserCount("0");
            }

            public string getPremiumUserCount()
            {
                return getUserCount("1");
            }

            public string getVIPUserCount()
            {
                return getUserCount("2");
            }

            public string getCharCount(string type, boolean onlineOnly)
            {
                string SQL = "Select count(*) as COUNT from players";

                // build where cause

                if (type.equalsIgnoreCase("elyos"))
                {
                    if (onlineOnly)
                        SQL = SQL + " where (online = 1 and race = 'ELYOS')";
                    else
                        SQL = SQL + " where race = 'ELYOS'";
                } else if (type.equalsIgnoreCase("asmos"))
                {
                    if (onlineOnly)
                        SQL = SQL + " where (online = 1 and race = 'ASMODIANS')";
                    else
                        SQL = SQL + " where race = 'ASMODIANS'";
                }
                if (type.equalsIgnoreCase("all"))
                {
                    if (onlineOnly)
                        SQL = SQL + " where online = 1";
                }

                try
                {
                    Statement stmt = gameDBConn.createStatement();
                    ResultSet rs = stmt.executeQuery(SQL);
                    while (rs.next())
                    {
                        return rs.getstring("COUNT");
                    }

                } catch (SQLException e)
                {
                    // TODO Auto-generated catch block
                    e.printStackTrace();
                }
                return "NA";
            }

            public List<string> getOnlineChars()
            {
                ArrayList<string> chars = new ArrayList<string>();
                string SQL = "Select name from players where online = 1";

                try
                {
                    Statement stmt = gameDBConn.createStatement();
                    ResultSet rs = stmt.executeQuery(SQL);
                    while (rs.next())
                    {
                        string aName = rs.getstring("name");
                        if (aName != null && aName.length() > 0)
                            chars.add(aName);

                    }

                } catch (SQLException e)
                {
                    // TODO Auto-generated catch block
                    e.printStackTrace();
                }
                return chars;
            }

            public void disconnect()
            {

                try
                {
                    loginDBConn.close();
                } catch (SQLException e)  {		}
                try
                {
                    gameDBConn.close();
                } catch (SQLException e)  {		}
                connected = false;
            }
	
            public void connect()
            {
                string suffix = "&user=" + glb.getDbUser() + "&password=" + glb.getDbPasswd();
                try
                {
                    loginDBConn = DriverManager.getConnection(glb.getLoginDBName() + suffix);
                } catch (SQLException e)  
                {
                    return;	
                }
                try
                {
                    gameDBConn = DriverManager.getConnection(glb.getGameDBName() + suffix);
                } catch (SQLException e)  
                {	
                    return;
                }
                connected = true;
            }
	
            public List<Account> getAccounts()
            {
                List<Account> accounts = new ArrayList<Account>();
		
                string SQL = "Select * from account_data";
                if (!isConnected())
                    connect();
                try
                {
                    Statement stmt = loginDBConn.createStatement();
                    ResultSet rs = stmt.executeQuery(SQL);
                    while (rs.next())
                    {
                        int 	id              = rs.getInt("id");
                        string 	name            = rs.getstring("name");
                        string 	password        = rs.getstring("password");
                        byte 	activated       = rs.getByte("activated");
                        byte 	access_level    = rs.getByte("access_level");
                        byte 	membership      = rs.getByte("membership");
                        byte 	old_membership  = rs.getByte("old_membership");
                        byte 	last_server     = rs.getByte("last_server");
                        string 	last_ip         = rs.getstring("last_ip");
                        string 	last_mac        = rs.getstring("last_mac");
                        string 	ip_force        = rs.getstring("ip_force");
                        Date 	expire          = rs.getDate("expire");
                        long 	toll            = rs.getLong("toll");
                        double 	balance         = rs.getDouble("balance");
                        string 	email           = rs.getstring("email");
                        string 	question        = rs.getstring("question");
                        string 	answer          = rs.getstring("answer");
				
                        Account account = new Account(id,name,password,activated,access_level,membership,
                                old_membership,last_server,last_ip,last_mac,ip_force,expire,toll,balance,email,question,answer);
                        accounts.add(account);
                    }

                } catch (SQLException e)
                {
                    // TODO Auto-generated catch block
                    e.printStackTrace();
                }		
                finally
                {
                    disconnect();
                }
                return accounts;
            }

            public boolean updateAccount(Account acct)
            {
                string SQL = "update account_data set " + 
                        "name = ?, " + 
                        "password = ?, " + 
                        "activated = ?, " + 
                        "access_level = ?, " + 
                        "membership = ?, " + 
                        "old_membership = ?, " + 
                        "last_server = ?, " + 
                        "last_ip = ?, " + 
                        "last_mac = ?, " + 
                        "ip_force = ?, " + 
                        "expire = ?, " + 
                        "toll = ?, " + 
                        "balance = ?, " + 
                        "email = ?, " + 
                        "question = ?, " + 
                        "answer = ? "+
                        "where id = ?";
		
		
                try
                {
                    if (loginDBConn.isClosed())
                        connect();
			
                    PreparedStatement update = loginDBConn.prepareStatement(SQL);
			
                    update.setstring(1,acct.name);
                    update.setstring(2,acct.password);
                    update.setByte  (3,acct.activated);
                    update.setByte  (4,acct.access_level);
                    update.setByte  (5,acct.membership);
                    update.setByte  (6,acct.old_membership);
                    update.setByte  (7,acct.last_server);
                    update.setstring(8,acct.last_ip);
                    update.setstring(9,acct.last_mac);
                    update.setstring(10,acct.ip_force);
                    update.setDate  (11,acct.expire);
                    update.setLong  (12,acct.toll);
                    update.setDouble(13,acct.balance);
                    update.setstring(14,acct.email);
                    update.setstring(15,acct.question);
                    update.setstring(16,acct.answer);			
                    update.setInt   (17,acct.id);
			
                    update.execute();
			
                } catch (SQLException e)
                {
                    return false;
                }
		
                return true;		// update successful
            }

            public void addBanIP(string last_ip)
            {
                string SQL = "insert into banned_ip (mask) value (?)";
		
                try
                {
                    if (loginDBConn.isClosed())
                        connect();
			
                    PreparedStatement update = loginDBConn.prepareStatement(SQL);
			
                    update.setstring(1,last_ip);
                    update.execute();
			
                } catch (SQLException e)
                {
                }
		
		
            }

            public void addMAC(string last_mac, string acctName)
            {
                string SQL = "insert into banned_mac (address, time, details) value (?,?,?)";
		
                try
                {
                    if (loginDBConn.isClosed())
                        connect();
			
                    PreparedStatement update = loginDBConn.prepareStatement(SQL);
			
                    update.setstring(1,last_mac);
                    update.setTimestamp(2, getCurrentTimeStamp());
                    update.setstring(3, acctName);
                    update.execute();
			
                } catch (SQLException e)
                {
                }
		
            }
            private java.sql.Timestamp getCurrentTimeStamp() {
		 
                java.util.Date today = new java.util.Date();
                return new java.sql.Timestamp(today.getTime());
            }*/


    }
}
