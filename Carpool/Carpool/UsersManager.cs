﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Carpool
{
    class UsersManager
    {
        IMobileServiceTable<Users> usersTable;
        MobileServiceClient client;

        public UsersManager()
        {
            client = new MobileServiceClient(
                Constants.ApplicationURL,
                Constants.ApplicationKey);

            usersTable = client.GetTable<Users>();
        }

        //public async Task<ObservableCollection<TodoItem>> GetTodoItemsAsync()
        //{
        //    try
        //    {
        //        return new ObservableCollection<TodoItem>(
        //            await todoTable.Where(todoItem => todoItem.Done == false).ToListAsync());
        //    }
        //    catch (MobileServiceInvalidOperationException msioe)
        //    {
        //        Debug.WriteLine(@"INVALID {0}", msioe.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(@"ERROR {0}", e.Message);
        //    }
        //    return null;
        //}

        public async Task<ObservableCollection<Users>> GetUsersAsync(string email)
        {
            try
            {
                return new ObservableCollection<Users>
                (
                    await usersTable.Where (user => user.Email==email).ToListAsync ()
                );
            } catch (MobileServiceInvalidOperationException msioe) {
                Debug.WriteLine (@"INVALID {0}", msioe.Message);
			} catch (Exception e) {
				Debug.WriteLine (@"ERROR {0}", e.Message);
			}
            return null;
        }

        public async Task SaveUserAsync(Users user)
        {
            if (user.ID == null)
            {
                await usersTable.InsertAsync(user);
            }
            else
            {
                await usersTable.UpdateAsync(user);
            }
        }
    }
}
