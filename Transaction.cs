using System;
using System.Collections.Generic;

namespace FirstBankOfSuncoast
{
    class Transaction
    {
        // amount 
        public int Amount { get; set; }

        // date
        public DateTime Date = DateTime.Now;

        // memo (chickfila transaction #792)
        public string Memo { get; set; }

        // transaction type (withdraw & deposit)
        public string Type { get; set; }

        // account (checking / savings)
        public AccountType Account { get; set; }

    }

}