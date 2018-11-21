using System;
using System.Collections.Generic;
using SportFixtures.Data.Entities;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface IEncounterBusinessLogic
    {
        /// <summary>
        /// Adds a encounter.
        /// </summary>
        /// <param name="encounter"></param>
        void Add(Encounter encounter);

        /// <summary>
        /// Updates a encounter.
        /// </summary>
        /// <param name="encounter"></param>
        void Update(Encounter encounter);

        /// <summary>
        /// Deletes a encounter.
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// Returns a exception if encounter does not exist.
        /// </summary>
        /// <param name="encounterId"></param>
        void CheckIfExists(int encounterId);

        /// <summary>
        /// Adds a comment to the encounter of the comment.
        /// </summary>
        /// <param name="comment"></param>
        void AddCommentToEncounter(Comment comment);

        /// <summary>
        /// Returns the encounter with the given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Encounter GetById(int id);

        /// <summary>
        /// Checks if there's already an encounter for the teams on the same day, on the repository.
        /// </summary>
        /// <param name="encounter"></param>
        /// <returns></returns>
        bool TeamsHaveEncountersOnTheSameDay(Encounter encounter);

        /// <summary>
        /// Checks if there's already an encounter for the teams on the same day, on the given list.
        /// </summary>
        /// <param name="encounters"></param>
        /// <param name="encounter"></param>
        /// <returns></returns>
        bool TeamsHaveEncountersOnTheSameDay(ICollection<Encounter> encounters, Encounter encounter);

        /// <summary>
        /// Returns all the encounters in the repository.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Encounter> GetAll();

        /// <summary>
        /// Returns all the encounter of given sport.
        /// </summary>
        /// <param name="sportId"></param>
        /// <returns></returns>
        IEnumerable<Encounter> GetAllEncountersOfSport(int sportId);

        /// <summary>
        /// Returns all the encounter of given team.
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        IEnumerable<Encounter> GetAllEncountersOfTeam(int teamId);

        /// <summary>
        /// Returns all the encounter of given date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        IEnumerable<Encounter> GetAllEncountersOfTheDay(DateTime date);

        /// <summary>
        /// Adds results for a given encounter.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        void AddResults(ICollection<PositionInEncounter> results, int encounterId);

        /// <summary>
        /// Adds many encounters.
        /// </summary>
        /// <param name="encounters"></param>
        /// <returns></returns>
        void AddMany(ICollection<Encounter> encounters);
    }
}