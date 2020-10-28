// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddressBook.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Dheer Singh Meena"/>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AddressBookSystemCollection
{
    public class AddressBook
    {
        //creating a new list to store contact details entered by the user
        private List<Contact> list = new List<Contact>();
        //create a dictionary(generic collection) to store keyvalue pair
        private Dictionary<string, Contact> d = new Dictionary<string, Contact>();
        //create a city dictionary to store city details
        public static Dictionary<string, Contact> cityDictionary = new Dictionary<string, Contact>();
        //create a state dictionary to store state details
        public static Dictionary<string, Contact> stateDictionary = new Dictionary<string, Contact>();
        public List<Contact> GetList()
        {
            return list;
        }
        public Dictionary<string, Contact> GetDictionary()
        {
            return d;
        }
        //method to add address to the list
        public void AddAddress(string kname, Contact c)
        {
            list.Add(c);
            d.Add(kname, c);

        }
        //method to view contact details using key name
        public Contact ViewByKeyName(string kname)
        {
            //iterating the keyvalue pair using for each loop
            foreach (KeyValuePair<string, Contact> kvp in d)
            {
                if (kvp.Key == kname)
                    return kvp.Value;
            }
            return null;
        }
        public List<Contact> ViewAddressBook(int input)
        {
            if(input == 1)
            {
                list = list.OrderBy(c => c.GetName()).ToList();
            }
            else if (input == 2)
            {
                list = list.OrderBy(c => c.GetCity()).ToList();
            }
            else if (input == 3)
            {
                list = list.OrderBy(c => c.GetState()).ToList();
            }
            if(input ==4) 
            {
                list = list.OrderBy(c => c.GetZip()).ToList();
            }
            return list;
        }
        //method to edit the contact details
        public void EditNumber(string ename, string newnumber)
        {
            Boolean flag = false;
            foreach (Contact cc in list)
            {
                if (cc.GetName().Equals(ename))
                {
                    flag = true;
                    cc.SetPhoneNo(newnumber);
                    Console.WriteLine("Number edited successfully");
                    break;
                }
            }
            if (!flag)
            {
                Console.WriteLine("No such name found!!!");
            }


        }
        //method to delete the contact from the phonebook
        public void RemoveContact(string rname)
        {
            Boolean flag = false;
            foreach (Contact cc in list)
            {
                if (cc.GetName().Equals(rname))
                {
                    flag = true;
                    list.Remove(cc);
                    Console.WriteLine("Number removed successfully");
                    break;
                }
            }
            if (!flag)
            {
                Console.WriteLine("No such name found!!!");
            }
        }
        //method to check for any duplicate entry 
        public bool CheckForDuplicateEntry(string name)
        {
            foreach (Contact c in list)
            {
                if (c.GetName().Equals(name))
                {
                    return true;
                }
            }
            return false;
        }
        //UC8-Ability to search Person in a City or State across the multiple address book
        public List<Contact> SearchPeopleByCityOrState(string location)
        {
            //using foreach loop to add city name entered by the user into the city dictionary
            foreach (Contact c in list)
            {
                cityDictionary.Add(c.GetCity(), c);
            }
            //using foreach loop to add state name entered by the user into the state dictionary
            foreach (Contact c in list)
            {
                stateDictionary.Add(c.GetState(), c);
            }
            //creating a new list as list of people to store the people of similar city together
            List<Contact> listofpeople = new List<Contact>();
            //iterating the key value pair in the city dictionary using foreach loop
            foreach (KeyValuePair<string, Contact> kvp in cityDictionary)
            {
                // adding the key value pair into the list
                if (kvp.Key.Equals(location))
                {
                    listofpeople.Add(kvp.Value);
                }
            }
            //iterating the key value pair in the state dictionary using foreach loop
            foreach (KeyValuePair<string, Contact> kvp in stateDictionary)
            {
                // adding the key value pair into the list
                if (kvp.Key.Equals(location))
                {
                    listofpeople.Add(kvp.Value);
                }
            }
            return listofpeople;
        }
        //method to view address by city name
        public void AddressByCity()
        {
            //creating hashset with name cityset
            HashSet<string> citySet = new HashSet<string>();
            //using for each loop to iterate the list and add city into hashset
            foreach (Contact c in list)
            {

                citySet.Add(c.GetCity());
            }
            //iterating the hashset to display contact details with city name
            foreach (string s in citySet)
            {
                Console.WriteLine("Contacts with address " + s + " are : ");
                Console.WriteLine();
                foreach (Contact cc in list)
                {

                    if (cc.GetCity().Equals(s))
                        Console.WriteLine("Name : " + cc.GetName() + "  Address : " + cc.GetAddress() + "  ZIP : " + cc.GetZip() + "  Contact No : " + cc.GetPhoneNo() + "  EmailID : " + cc.GetEmail());

                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }
        //method to view address by state name
        public void AddressByState()
        {
            //creating hashset with name stateset
            HashSet<string> stateSet = new HashSet<string>();
            //using for each loop to iterate the list and add state into hashset
            foreach (Contact c in list)
            {

                stateSet.Add(c.GetState());
            }
            //iterating the hashset to display contact details with state name
            foreach (string s in stateSet)
            {
                Console.WriteLine("Contacts with address " + s + " are : ");
                Console.WriteLine();
                foreach (Contact cc in list)
                {

                    if (cc.GetState().Equals(s))
                        Console.WriteLine("Name : " + cc.GetName() + "  Address : " + cc.GetAddress() + "  ZIP : " + cc.GetZip() + "  Contact No : " + cc.GetPhoneNo() + "  EmailID : " + cc.GetEmail());

                }
                Console.WriteLine();
                Console.WriteLine();
            }
           
        }
        /// <summary>
        ///UC13 Ability to read and write  the addressbook with person cotacts using File IO method.
        /// </summary>
        public void WriteAllText()
        {
            //Creating a variable to assign file path.
            string path = @"C:\Users\dheer1998meena\source\repos\AddressBookSystemFileIO_Day-20\AddressBookSystemFileIO_Day-20\Contact.txt";
            // Checking file if it is exist or not.
            if (File.Exists(path))
            {
                // Creating a obj that store the all written text at a once and a streamwritter that appends enodeed text to specified file or to be new file.
                using (StreamWriter sw = File.AppendText(path))
                {
                    List<Contact> li = ViewAddressBook(1);
                    //Iterating foreach loop to geting all details of person contact.
                    foreach (Contact cc in li)
                    {
                        sw.Write("Name :" + cc.GetName() + "Address :" + cc.GetAddress() + "City :" + cc.GetCity() + "State :" + cc.GetState() + "Zip :" + cc.GetZip() + "Phone Number :" + cc.GetPhoneNo() + "Email :" + cc.GetEmail());

                        Console.WriteLine();
                    }

                }
                ReadAllText();
            }

        }
        // Creating ReadAllText method.
        public void ReadAllText()
        {
            //Creating a variable to assign file path.
            string path = @"C:\Users\dheer1998meena\source\repos\SystemIo_rum\SystemIo_rum\Contact.txt";
            // Checking file if it is exist or not.
            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    Console.WriteLine(sr.ReadToEnd());
                }

            }

        }
    }
}
