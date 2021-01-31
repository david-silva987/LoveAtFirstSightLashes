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
        /// Get all clients in database
        /// </summary>
        /// <returns></returns>
        public Task<List<Client>> GetAllClientsName()
        {
            return _database.Table<Client>().OrderBy(x => x.Prenom).ToListAsync();
        }

        /// <summary>
        /// Save new client
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public Task<int> SaveNewClient(Client client)
        {
            return _database.InsertAsync(client);
        }

        /// <summary>
        /// Get Id for chosen client
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<int> GetIdClient(string name)
        {
            var idClient = await _database.QueryAsync<Client>("SELECT Id FROM 'Client' WHERE Prenom = '" + name + "'");
            var id = idClient[0];
            return id.Id;

        }

        /// <summary>
        /// Get first client
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<Client> GetClient(string name)
        {
            return  _database.Table<Client>().Where(x => x.Prenom == name).FirstOrDefaultAsync();


        }

        /// <summary>
        /// Get number of meetings for a chosen client
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<int> GetNbRDV(string name)
        {
            var idClient = await _database.QueryAsync<Client>("SELECT NbRDV FROM 'Client' WHERE Prenom = '" + name + "'");
            var id = idClient[0];
            return id.NbRDV;

        }

        /// <summary>
        /// Get name of client with a chosen id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> GetNameClient(int id)
        {
            var nameClient = await _database.QueryAsync<Client>("SELECT Prenom FROM 'Client' WHERE Id = '" + id + "'");
            var name = nameClient[0];
            return name.Prenom;

        }

        /// <summary>
        /// Create a new meeting
        /// </summary>
        /// <param name="meeting"></param>
        /// <returns></returns>
        public Task<int> SaveNewMeeting(Meeting meeting)
        {
            return _database.InsertAsync(meeting);
        }

        /// <summary>
        /// Get all meetings for a chosen day
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<List<Meeting>> GetMeetingsForDay(string date)
        {
            return await _database.QueryAsync<Meeting>("Select Name_Client,DateRDV,HourRDV,TypePose from 'Meeting'  where SheCame = false and DateRDV = '"+date+"'");

        }

        /// <summary>
        /// Get All meetings for today, displayed onAppearing
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<List<Meeting>> GetAllMeetingsForToday(string date)
        {
            return await _database.QueryAsync<Meeting>("Select Name_Client,DateRDV,HourRDV,TypePose from 'Meeting'  where SheCame = false and DateRDV = '" + date + "'");

        }

        /// <summary>
        /// Increment number of meetings for a chosen client
        /// </summary>
        /// <param name="client_name"></param>
        /// <returns></returns>
        public Task<int> UpdateNBRDV(string client_name)
        {
            return _database.ExecuteAsync("UPDATE 'Client' SET NbRDV = NbRDV+1 where Prenom = ?", client_name);
        }

        /// <summary>
        /// Remove specific meeting (if client didn't come)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<string> RemoveMeeting(int id)
        {
            return _database.ExecuteScalarAsync<string>("DELETE FROM 'Meeting' WHERE Id =" + id);

        }

        /// <summary>
        /// Confirm specific meeting (if client came)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<string> ConfirmMeeting(int id)
        {
            return _database.ExecuteScalarAsync<string>("UPDATE 'Meeting' SET SheCame=true WHERE Id =" + id);

        }


    }
}

