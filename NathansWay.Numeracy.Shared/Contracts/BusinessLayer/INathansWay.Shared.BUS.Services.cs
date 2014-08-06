// Core
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// SQLite
using SQLite.Net.Interop;

// NathansWay
using NathansWay.Shared.Global;
using NathansWay.Shared.DB;
using NathansWay.Shared.BUS.Entity;


namespace NathansWay.Shared.BUS.Services.Data
{
    /// <summary>
    /// Service for returning and saving assignment information
    /// </summary>
    public interface IBusinessDataServices
	{
		Task<int>  SomeBusOperation (IBusEntity t, CancellationToken cancellationToken = default(CancellationToken));

		//        /// <summary>
//        /// Gets a list of assignments
//        /// </summary>
//        Task<List>Assignment>> GetAssignmentsAsync (CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Gets a list of items
//        /// </summary>
//        Task<List<Item>> GetItemsAsync (CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Gets a list of items for an assignment
//        /// </summary>
//        Task<List<AssignmentItem>> GetItemsForAssignmentAsync (Assignment assignment, CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Gets a list of labor hours for an assignment
//        /// </summary>
//        Task<List<Labor>> GetLaborForAssignmentAsync (Assignment assignment, CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Gets a list of expenses for an assignment
//        /// </summary>
//        Task<List<Expense>> GetExpensesForAssignmentAsync (Assignment assignment, CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Saves an assignment
//        /// </summary>
//        Task<int> SaveAssignmentAsync (Assignment assignment, CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Saves a signature for an assignment
//        /// </summary>
//        Task<int> SaveSignatureAsync (Data.Signature signature, CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Gets a signature for an assignment or null if none
//        /// </summary>
//        Task<Data.Signature> GetSignatureAsync (Assignment assignment, CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Saves an assignment item
//        /// </summary>
//        Task<int> SaveAssignmentItemAsync (AssignmentItem assignmentItem, CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Saves a labor entry
//        /// </summary>
//        Task<int> SaveLaborAsync (Labor labor, CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Saves an expense entry
//        /// </summary>
//        Task<int> SaveExpenseAsync (Expense expense, CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Saves a photo for an expense
//        /// </summary>
//        Task<int> SaveExpensePhotoAsync (ExpensePhoto photo, CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Gets an expense photo for an expense or null if there is none
//        /// </summary>
//        Task<ExpensePhoto> GetExpensePhotoAsync (Expense expense, CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Deletes an assignment
//        /// </summary>
//        Task<int> DeleteAssignmentAsync (Assignment assignment, CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Deletes an assignment item
//        /// </summary>
//        Task<int> DeleteAssignmentItemAsync (AssignmentItem AssignmentItem, CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Deletes a labor entry
//        /// </summary>
//        Task<int> DeleteLaborAsync (Labor labor, CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Deletes an expense entry
//        /// </summary>
//        Task<int> DeleteExpenseAsync (Expense expense, CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Saves a timer entry to record an assignment
//        /// </summary>
//        Task<int> SaveTimerEntryAsync (TimerEntry entry, CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Deletes a timer entry to stop recording an assignment
//        /// </summary>
//        Task<int> DeleteTimerEntryAsync (TimerEntry entry, CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Retrieves the stored timer entry if there was one
//        /// </summary>
//        Task<TimerEntry> GetTimerEntryAsync (CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Retrieves all the photos for an assignment
//        /// </summary>
//        Task<List<Photo>> GetPhotosForAssignmentAsync (Assignment assignment, CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Saves a photo for an assignment
//        /// </summary>
//        Task<int> SavePhotoAsync (Photo photo, CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Deletes a photo
//        /// </summary>
//        Task<int> DeletePhotoAsync (Photo photo, CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Gets the list of available documents
//        /// </summary>
//        Task<List<Document>> GetDocumentsAsync (CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Gets the history for an assignment
//        /// </summary>
//        Task<List<AssignmentHistory>> GetAssignmentHistoryAsync (Assignment assignment, CancellationToken cancellationToken = default(CancellationToken));
//
//        /// <summary>
//        /// Gets the past assignment
//        /// </summary>
//        Task<List<Assignment>> GetAssignmentFromHistory (AssignmentHistory assignmentHistory, CancellationToken cancellationToken = default(CancellationToken));
    }
}