﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface ItemRepositoryService
    {
        IEnumerable<Item> ReadItems(string selector);
        Item ReadItem(string id);
        Item ReadItem(int id);
        Item CreateItem(Item item);
        Item UpdateItem(Item item);
        IEnumerable<Debit> BulkAddDebitItems(List<Debit> debitList);
        IEnumerable<Deposit> BulkAddDepositItems(List<Deposit> depositList);
        IEnumerable<Bobbin> ReadBobbins();
        IEnumerable<Bobbin> ReadBobbinsPerInfra(int id);
        void CreateBobbin(Bobbin bobbin);
        IEnumerable<CableType> ReadCableTypes();
        void CreateBobbinDebit(BobbinDebit bobbinDebit);
        Bobbin ReadBobbin(int id);
        void UpdateBobbin(Bobbin bobbin);
        BobbinDebit ReadBobbinDebit(int id);
        void DeleteBobbinDebit(int id);
        void DeleteBobbin(int id);
        Debit ReadDebit(int id);
        void DeleteDebit(int id);
        void DeleteItem(int id);
        IEnumerable<Debit> ReadDebits();
        IEnumerable<Deposit> ReadDeposits();
        Bobbin ReadLatestBobbin();
        IEnumerable<Bobbin> ReadBobbinsReturned(bool isReturned);
        void BulkAddItems(IEnumerable<Item> items);
    }
}
