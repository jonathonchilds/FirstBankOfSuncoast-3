﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace FirstBankOfSuncoast
{
    enum AccountType
    {
        Checking, Savings
    }

    enum TransactionType
    {
        Debit, Credit
    }

    class Program
    {
        static string PromptForString(string prompt)
        {
            Console.Write(prompt);
            Console.Write("\n> ");
            return Console.ReadLine();
        }

        static string PromptForMenuString(string prompt)
        {
            Console.Write(prompt);
            Console.Write("\n> ");
            var userInput = Console.ReadLine();
            return userInput.ToUpper();
        }

        static int PromptForInteger(string prompt)
        {
            Console.Write(prompt);
            Console.Write("\n> ");
            int userInput;
            var isThisGoodInput = Int32.TryParse(Console.ReadLine(), out userInput);
            if (isThisGoodInput)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, that isn't a valid input, I'm using 0 as your answer.");
                return 0;
            }
        }

        public static List<Transaction> Transactions = new List<Transaction>();

        static void Main(string[] args)
        {
            var fileReader = new StreamReader("Transactions.csv");
            var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);
            Transactions = csvReader.GetRecords<Transaction>().ToList();
            fileReader.Close();

            var keepGoing = true;
            while (keepGoing)
            {

                var userAnswer = PromptForMenuString("Which account do you want? \n(C)hecking \n(S)avings \n(V)iew balance \n(Q)uit");

                // to (V)iew
                switch (userAnswer)
                {
                    case "Q":
                        keepGoing = false;
                        var fileWriter = new StreamWriter("Transactions.csv");
                        var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
                        csvWriter.WriteRecords(Transactions);
                        fileWriter.Close();
                        break;

                    case "C":
                        CheckingMenu();
                        break;

                    case "S":
                        SavingsMenu();
                        break;

                    default:
                        Console.WriteLine("☠️ ☠️ ☠️ ☠️ ☠️ NOPE! ☠️ ☠️ ☠️ ☠️ ☠️");
                        break;
                }
            }
        }

        public static void CheckingMenu()
        {
            var checkingAnswer = PromptForMenuString("What would you like to do? (V)iew balance (D)eposit (W)ithdraw \n");
            switch (checkingAnswer)
            {
                case "V":
                    ShowBalance(AccountType.Checking);
                    break;
                case "D":
                    DepositFunds(AccountType.Checking);
                    break;
                case "W":
                    WithdrawFunds(AccountType.Checking);
                    break;
                default:
                    Console.WriteLine($"Wrong answer! Back to the main menu with you. 🥾💥 ");
                    break;
            }
        }

        public static void WithdrawFunds(AccountType accountType)
        {
            switch (accountType)
            {
                case AccountType.Checking:
                    break;
                case AccountType.Savings:
                    break;
            }
        }

        public static void DepositFunds(AccountType accountType)
        {
            switch (accountType)
            {
                case AccountType.Checking:
                    var newTransaction = new Transaction();
                    var depositAmount = PromptForInteger("How much would you like to deposit?");
                    newTransaction.Memo = PromptForString("Please enter a memo for this transaction, punk.");
                    newTransaction.Date = DateTime.Now;
                    newTransaction.Amount = depositAmount;
                    newTransaction.Type = TransactionType.Credit;
                    Transactions.Add(newTransaction);
                    break;
                case AccountType.Savings:
                    break;
            }
        }

        public static void ShowBalance(AccountType accountType)
        {
            switch (accountType)
            {
                case AccountType.Checking:
                    foreach (var transaction in Transactions.Where(transaction => transaction.Account == AccountType.Checking))
                    {
                        Console.WriteLine($"{transaction.Date} | {transaction.Memo} \t\t ${transaction.Amount} ");
                    }
                    break;
                case AccountType.Savings:
                    break;
            }
        }

        public static void SavingsMenu()
        {
            var savingsAnswer = PromptForMenuString("What would you like to do? \n (V)iew balance (D)eposit (W)ithdraw \n :");
            switch (savingsAnswer)
            {
                case "V":
                    ShowBalance(AccountType.Savings);
                    break;
                case "D":
                    DepositFunds(AccountType.Savings);
                    break;
                case "W":
                    WithdrawFunds(AccountType.Savings);
                    break;
                default:
                    Console.WriteLine($"Wrong answer! Back to the main menu with you. 🥾💥 ");
                    break;
            }
        }
    }
}


//Problem: 
//  Make a console to manage two accounts. 
//  Create a menu to field interactions from the user.
//  Retrieve balances and retrieve transactions. 
//  Create a CSV file to store the transactions and produce the sum of the transactions that take place. 
//  Store the history of transaction List<Transaction>
//  Design a transaction class to support: checking, savings, deposits, and withdraws.
//  Load past transactions 
//  Cannot withdraw an amount more than the available funds. 
//  The input from the user always has to be a positive int. 
//  Create a menu. 
//     deposit transaction for savings.
//     deposit transaction for checking.
//     withdraw transaction for savings.
//     withdraw transaction for checking.
//     see the balance of my savings and checking.

//Examples: 
//  Un starting balance: 100
//  Deposit: 20; 30; 10; -5; 1000; -10000
//  Withdraw: 10, 50, 100, -30, -10, 60, 500, 0,
//  Savings: 200, 10, 50, 100, -30, -10, 60, 500, 0,

//Data Structures:
// Positive Int's
// Lists 
// using system collections generic
// system.io
// linq
// csv helper
// lots of sass
// lots of mute

//CCCOOODDDEEEEEE 









// The application should store a history of transactions in a SINGLE List<Transaction>. Your task is to design the Transaction class to support both checking and savings as well as deposits and withdraws.
// The application should load past transactions from a file when it first starts.
// As a user I should be able to see the list of transactions designated savings.
// As a user I should be able to see the list of transactions designated checking.
// Never allow withdrawing more money than is available. That is, we cannot allow our checking or savings balances to go negative.
// When prompting for an amount to deposit or withdraw always ensure the amount is positive. The value we store in the Transaction shall be positive as well. (e.g. a Transaction that is a withdraw of 25 both inputs and records a positive 25)
// As a user I should have a menu option to make a deposit transaction for savings.
// As a user I should have a menu option to make a deposit transaction for checking.
// As a user I should have a menu option to make a withdraw transaction for savings.
// As a user I should have a menu option to make a withdraw transaction for checking.
// As a user I should have a menu option to see the balance of my savings and checking.
// The application should, after each transaction, write all the transactions to a file. This is the same file the application loads.


