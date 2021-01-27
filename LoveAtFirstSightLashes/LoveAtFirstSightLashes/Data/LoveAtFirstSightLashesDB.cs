using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoveAtFirstSightLashes.Models;
using SQLite;

//Database: https://docs.microsoft.com/en-us/xamarin/get-started/tutorials/local-database/?tutorial-step=1&tabs=vswin

namespace LoveAtFirstSightLashes.Data
{
    /// <summary>
    /// Database interaction with application
    /// </summary>
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Client>().Wait();
            _database.CreateTableAsync<Meeting>().Wait();
        }

        /// <summary>
        /// Get all transactions in database
        /// </summary>
        /// <returns></returns>
        public Task<List<Client>> GetAllClientsName()
        {
            return _database.Table<Client>().OrderBy(x => x.Prenom).ToListAsync();
        }

        public Task<int> SaveNewClient(Client client)
        {
            return _database.InsertAsync(client);
        }

        public async Task<int> GetIdClient(string name)
        {
            var idClient = await _database.QueryAsync<Client>("SELECT Id FROM 'Client' WHERE Prenom = '" + name + "'");
            var id = idClient[0];
            return id.Id;

        }

        public async Task<int> GetNbRDV(string name)
        {
            var idClient = await _database.QueryAsync<Client>("SELECT NbRDV FROM 'Client' WHERE Prenom = '" + name + "'");
            var id = idClient[0];
            return id.NbRDV;

        }

        public async Task<string> GetNameClient(int id)
        {
            var nameClient = await _database.QueryAsync<Client>("SELECT Prenom FROM 'Client' WHERE Id = '" + id + "'");
            var name = nameClient[0];
            return name.Prenom;

        }

        public Task<int> SaveNewMeeting(Meeting meeting)
        {
            return _database.InsertAsync(meeting);
        }

        public async Task<List<Meeting>> GetAllMeetings()
        {
            return await _database.QueryAsync<Meeting>("Select * FROM 'Meeting' where SheCame = false");
        }

        public async Task<List<Meeting>> GetMeetingsForDay(string date)
        {
            return await _database.QueryAsync<Meeting>("Select Name_Client,DateRDV,HourRDV,TypePose from 'Meeting'  where SheCame = false and DateRDV = '"+date+"'");

        }

        public Task<int> UpdateNBRDV(string client_name)
        {
            return _database.ExecuteAsync("UPDATE 'Client' SET NbRDV = NbRDV+1 where Prenom = ?", client_name);
        }


        public Task<string> RemoveMeeting(int id)
        {
            return _database.ExecuteScalarAsync<string>("DELETE FROM 'Meeting' WHERE Id =" + id);

        }


        public Task<string> ConfirmMeeting(int id)
        {
            return _database.ExecuteScalarAsync<string>("UPDATE 'Meeting' SET SheCame=true WHERE Id =" + id);

        }


    }
}

