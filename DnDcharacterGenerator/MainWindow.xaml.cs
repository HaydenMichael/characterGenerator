using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DnDcharacterGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        public int getRace()
        {
            Stats a = new Stats();
            var rnd = new Random();
            string[] allRaces = a.raceList;
            if (raceSelect.SelectedIndex == 0)
            {
                int randomRace = rnd.Next(1, allRaces.Length - 1);
                //raceSelect.SelectedIndex = randomRace;
                return randomRace;
            }
            else
            {
                int race = raceSelect.SelectedIndex;
                return race;
            }
            
        }

        public int getClass()
        {
            Stats a = new Stats();
            var rnd = new Random();
            string[] allClasses = a.classList;
            if (classSelect.SelectedIndex == 0)
            {
                int randomClass = rnd.Next(1, allClasses.Length - 1);
                classSelect.SelectedIndex = randomClass;
                return randomClass;
            }
            else return classSelect.SelectedIndex;            
        }



        //get subrace menu
        //grabs a list of strings (in an array) and adds a new item to the combo box for each item in that array
        public void subRaceItems(params string[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                subRaceSelect.Items.Add(String.Format(list[i]));
            }
        }

        public void getSubRaceList()
        {
            if (subRaceSelect != null)
            {
                //clear the combobox before updating it
                int race = getRace();
                subRaceSelect.Items.Clear();
                //dwarf
                string[] subList = { };
                if (race == 1)
                {
                    subList = new string[] { "Hill Dwarf", "Mountain Dwarf" };  
                }
                //elf
                if (race == 2)
                {
                    subList = new string[] { "High Elf", "Wood Elf", "Dark Elf" };
                }
                //halfling
                if (race == 3)
                {
                    subList = new string[] { "Lightfoot Halfling", "Stout Halfling" };
                }
                //gnome
                if (race == 6)
                {
                    subList = new string[] { "Forest Gnome", "Rock Gnome" };
                }
                subRaceSelect.SelectedIndex = 0;
                subRaceItems(subList);
            }
            
            else { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //declare the utility class for use here
            Utility u = new Utility();
            Stats a = new Stats();

            //generate the name after clicking the button
            
            int race = getRace();
            raceSelect.SelectedIndex = race;
            int subRace = subRaceSelect.SelectedIndex;
            int classDnD = getClass();
            string randomCharacterName = u.nameGen(race);
            characterName.Text = randomCharacterName;

            //print class and race even if its random
            string[] raceList = a.raceList;
            targetRace.Text = raceList[race];
            string[] classList = a.classList;
            targetClass.Text = classList[classDnD];

            //test attribute generator
            //int[] testInt = u.getAllAttributes();
            //tester.Text = (testInt[0].ToString() + " " + testInt[1].ToString() + " " + testInt[2].ToString() + " " + testInt[3].ToString() + " " + testInt[4].ToString() + " " + testInt[5].ToString());
            //u.prioritizeAttributes(classDnD);

            //assign all attributes
            int[] allAttributes = u.getAllAttributes();
            int[] attributePriority = u.prioritizeAttributes(classDnD);

            //fetch race modifier list from utilities
            int[] raceModifierList = u.raceAttributeModifier(race, subRace);
            //int strengthPriority = attributePriority[0]; int dexterityPriority = attributePriority[1]; int constitutionPriority = attributePriority[2]; int intelligencePriority = attributePriority[3];
            //int wisdomPriority = attributePriority[4]; int charismaPriority = attributePriority[5];
            strengthTarget.Text = (allAttributes[attributePriority[0]] + raceModifierList[0]).ToString(); 
            dexterityTarget.Text = (allAttributes[attributePriority[1]] + raceModifierList[1]).ToString();
            constitutionTarget.Text = (allAttributes[attributePriority[2] + raceModifierList[2]]).ToString();
            intelligenceTarget.Text = (allAttributes[attributePriority[3]] + raceModifierList[3]).ToString();
            wisdomTarget.Text = (allAttributes[attributePriority[4]] + raceModifierList[4]).ToString();
            charismaTarget.Text = (allAttributes[attributePriority[5]] + raceModifierList[5]).ToString();

        }

        private void raceSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getSubRaceList();
        }
    }

    public class Stats
    {
        public string[] raceList = { "Random", "Dwarf", "Elf", "Halfling", "Human", "Dragonborn", "Gnome", "Half-Elf", "Half-Orc", "Tiefling" };
        public string[] classList = { "Random", "Barbarian", "Bard", "Cleric", "Druid", "Fighter", "Monk", "Paladin", "Ranger", "Rogue", "Sorcerer", "Warlock", "Wizard" };
    }

    public class Utility
    {


        public string nameGen(int nameGenPass)
        {
            var rnd = new Random();
            //first names arrays
            string[] randomFirstName = { };
            string[] dwarfFirstNames = {"Adrik", "Alberich", "Baern", "Barendd", "Brottor", "Bruenor",
                                        "Dain", "Darrak", "Delg", "Eberk", "Harbek", "Kildrak", "Morgran",
                                        "Orsik", "Oskar", "Rangrim", "Rurik", "Taklin", "Thoradin", "Thorin",
                                        "Tordek", "Traubon", "Travok", "Ulfgar", "Veit", "Vondal", "Amber", "Artin",
                                        "Audhild", "Bardryn", "Dagnal", "Diesa", "Eldeth", "Falkrunn", "Finellen",
                                        "Gunnloda", "Gurdis", "Helja", "Hlin", "Kathra", "Kristryd", "Ilde", "Liftrasa",
                                        "Madred", "Riswynn", "Sannl", "Torbera", "Torgga", "Vistra"};;
            string[] elfFirstNames = {"Adran", "Aelar", "Aramil", "Arannis", "Aust", "Beiro", "Berrian", "Carric", "Enialis",
                                      "Erdan", "Erevan", "Galinndan", "Hadarai", "Heian", "Himo", "Immeral", "Ivellios", "Laucian",
                                      "Mindartis", "Paelis", "Peren", "Quarion", "Riardon", "Rolen", "Soveliss", "Thamior", "Tharivol",
                                      "Theren", "Varis", "Adrie", "Althaea", "Anastrianna", "Andraste", "Antinua", "Bethrynna", "Birel",
                                      "Caelynn", "Drusilia", "Enna", "Felosial", "Ielenia", "Jelenneth", "Keyleth", "Leshanna", "Lia",
                                      "Meriele", "Mialee", "Naivara", "Quelenna", "Quillathe", "Sariel", "Shanairra", "Shava", "Silaqui",
                                      "Theirasta", "Thia", "Vadania", "Valanthe", "Xanaphia"};
            string[] halflingFirstnames = {"Alton", "Ander", "Cade", "Corrin", "Eldon", "Errich", "Finnan", "Garret", "Lindal", "Lyle",
                                           "Merric", "Milo", "Osborn", "Perrin", "Reed", "Roscoe", "Wellby", "Andry", "Bree", "Callie",
                                           "Cora", "Euphemia", "Jillian", "Kithri", "Lavinia", "Lidda", "Merla", "Nedda", "Paela", "Portia",
                                           "Seraphina", "Shaena", "Trym", "Vani", "Verna"};
            string[] humanFirstNames = { "Borovik", "Faurgar", "Jandar", "Kanithar", "Madislak", "Ramevik", "Shaumar", "Vladislak" };
            string[] dragonbornFirstnames = {"Arjhan", "Balasar", "Bharash", "Donaar", "Ghesh", "Heskan", "Kriv", "Medrash", "Mehen",
                                             "Nadarr", "Pandjed", "Patrin", " Rhogar", "Shamash", "Shedinn", "Tarhun", "Torinn", "Akra",
                                             "Biri", "Daar", "Farideh", "Harann", "Havilar", "Jheri", "Kava", "Korinn", "Mishann", "Nala",
                                             "Perra", "Raiann", "Sora", "Surina", "Thava", "Uadjit"};
            string[] gnomeFirstNames = {"Alston", "Alvynn", "Boddynock", "Brocc", "Burgell", "Dimble", "Eldon", "Erky", "Fonkin", "Frug",
                                        "Gerbo", "Gimble", "Glim", "Jebeddo", "Kellen", "Namfoodle", "Orryn", "Roondar", "Seebo", "Sindri",
                                        "Warryn", "Wrenn", "Zook", "Bimpnottin", "Breena", "Caramip", "Carlin", "Donella", "Duvamil",
                                        "Ella", "Ellyjobell", "Ellywick", "Lilli", "Loopmottin", "Lorilla", "Mardnab", "Nissa", "Nyx",
                                        "Oda", "Orla", "Roywyn", "Shamil", "Tana", "Waywocker", "Zanna"};
            string[] halfElfFirstNames = {"Adran", "Aelar", "Aramil", "Arannis", "Aust", "Beiro", "Berrian", "Carric", "Enialis",
                                      "Erdan", "Erevan", "Galinndan", "Hadarai", "Heian", "Himo", "Immeral", "Ivellios", "Laucian",
                                      "Mindartis", "Paelis", "Peren", "Quarion", "Riardon", "Rolen", "Soveliss", "Thamior", "Tharivol",
                                      "Theren", "Varis", "Adrie", "Althaea", "Anastrianna", "Andraste", "Antinua", "Bethrynna", "Birel",
                                      "Caelynn", "Drusilia", "Enna", "Felosial", "Ielenia", "Jelenneth", "Keyleth", "Leshanna", "Lia",
                                      "Meriele", "Mialee", "Naivara", "Quelenna", "Quillathe", "Sariel", "Shanairra", "Shava", "Silaqui",
                                      "Theirasta", "Thia", "Vadania", "Valanthe", "Xanaphia", "Borovik", "Faurgar", "Jandar", "Kanithar",
                                      "Madislak", "Ramevik", "Shaumar", "Vladislak" };
            string[] halfOrcFirstNames = {"Dench", "Feng", "Gell", "Henk", "Holg", "Imsh", "Keth", "Krusk", "Mhurren", "Ront", "Shump",
                                          "Thokk", "Baggi", "Emen", "Engong", "Kansif", "Myev", "Ovak", "Ownka", "Shautha", "Sutha", "Vola",
                                          "Volen", "Yevelda"};
            string[] tieflingFirstNames = {"Akmenos", "Ammon", "Barakas", "Damakos", "Ekemon", "Iados", "Kairon", "Leucis", "Melech", "Mordai",
                                           "Morthos", "Pelaios", "Skamos", "Therai", "Akta", "Anakis", "Bryseis", "Criella", "Damaia", "Ea",
                                           "Kallista", "Lerissa", "Makaria", "Nemeia", "Orianna", "Phelaia", "Rieta"};




            //last names arrays
            string[] randomLastName = { };
            string[] dwarfLastNames = {"Balderk", "Battlehammer", "Brawnanvil", "Dankil", "Fireforge", "Frostbeard", "Gorunn", "Holderhek", "Ironfist", "Loderr",
                                       "Lutgehr", "Rumnaheim", "Strakeln", "Torunn", "Ungart"};
            string[] elfLastNames = {"Amakiir", "Amastacia", "Galanodel", "Holimion", "Ilphelkiir", "Liadon", "Miliamne", "Naïlo", "Siannodel", "Xiloscient"};
            string[] halflingLastNames = {"Brushgather", "Goodbarrel", "Greenbottle", "High-hill", "Hilltopple", "Leagallow", "Tealeaf", "Thorngage",
                                          "Tosscobble", "Underbough"};
            string[] humanLastNames = { "Chergoba", "Dyernina", "Iltazyara", "Murnyethara", "Stayanoga", "Ulmokina" };
            string[] dragonbornLastNames = {"Clethtinthiallor", "Daardendrian", "Delmirev", "Drachedandion", "Fenkenkabradon", "Kepeshkmolik", "Kerrhylon",
                                            "Kimbatuul", "Linxakasendalor", "Myastan", "Nemmonis", "Norixius", "Ophinshtalajiir", "Prexijandilin", "Shestendeliath",
                                            "Turnuroth", "Verthisathurgiesh", "Yarjerit"};
            string[] gnomeLastNames = {"Beren", "Daergel", "Folkor", "Garrick", "Nackle", "Murning", "Ningel", "Raulnor", "Scheppen", "Timbers", "Turen"};
            string[] halfElfLastNames = {"Amakiir", "Amastacia", "Galanodel", "Holimion", "Ilphelkiir", "Liadon", "Miliamne", "Naïlo", "Siannodel", "Xiloscient",
                                         "Chergoba", "Dyernina", "Iltazyara", "Murnyethara", "Stayanoga", "Ulmokina"};
            string[] halfOrcLastNames = {"", "", "", "", "", "the Bandit", "Skullpummeler", "the Slasher", "the Broken Fist" };
            string[] tieflingLastNames = {"" };
            

            //array of all the possible first name arrays
            string[][] allFirstNames = { randomFirstName, dwarfFirstNames,elfFirstNames, halflingFirstnames, humanFirstNames, dragonbornFirstnames,
                                         gnomeFirstNames, halfElfFirstNames, halfOrcFirstNames, tieflingFirstNames };
            //array of all the possible last name arrays
            string[][] allLastNames = { randomLastName, dwarfLastNames, elfLastNames, halflingLastNames, humanLastNames, dragonbornLastNames, gnomeLastNames,
                                        halfElfLastNames, halfOrcLastNames, tieflingLastNames};

            
            //select random number based on the length of the array
            string[] nameArraySelect = allFirstNames[nameGenPass];
            string[] nameArraySelectLast = allLastNames[nameGenPass];
            //select an item on one the arrays
            int allIndex = rnd.Next(nameArraySelect.Length);
            int AllIndexLast = rnd.Next(nameArraySelectLast.Length);


            //select a random array if the user picks random
            int randomRaceIndex = rnd.Next(2, allFirstNames.Length - 1);
            string[] randomRaceSelect = allFirstNames[randomRaceIndex];
            int allIndexRandom = rnd.Next(randomRaceSelect.Length);
            string firstNameOutputRandom = randomRaceSelect[allIndexRandom];
            //random for last names
            //int randomRaceIndexLast = rnd.Next(2, allLastNames.Length - 1);
            // ^ don't want it to select a different race for the first name over the last name
            string[] randomRaceSelectLast = allLastNames[randomRaceIndex];
            int allIndexRandomLast = rnd.Next(randomRaceSelectLast.Length);
            string lastNameOutputRandom = randomRaceSelectLast[allIndexRandomLast];


            //if selection is random
            if (nameGenPass == 0)
            {
                return (firstNameOutputRandom + " " + lastNameOutputRandom);
            }
            //if a race is manually selected
            else
            {
                string firstNameOutput = nameArraySelect[allIndex];
                string lastNameOutput = nameArraySelectLast[AllIndexLast];
                return (firstNameOutput + " " + lastNameOutput);
            }
            
        }

        //roll a list of attribute scores to use before assignment to particular attributes
        public int getAttributeScore()
        {
            var rnd = new Random();
            int[] attribute = new int[4];
            for (int i = 0; i < 4; i++)
            {
                attribute[i] = rnd.Next(1,7);
            }
            //put array in order from highest numbers to lowest numbers
            Array.Sort(attribute);
            Array.Reverse(attribute);

            //add together the sum of items left on the array
            int sum = 0;
            //Array.ForEach(attribute, i => sum += i);
            for (int i = 0; i < 3; i++)
            {
                sum += attribute[i];
            }

            return sum;
        }

        //get complete list of attributes
        public int[] getAllAttributes()
        {
            //create a six number array using numbers generated by the getAttributeScore() method
            int[] attributesList = new int[6];
            for (int i = 0; i < 6; i++)
            {
                attributesList[i] = getAttributeScore();
            }
            //sort list of attributes from highest to lowest
            Array.Sort(attributesList);
            Array.Reverse(attributesList);
            //int[] testing = { 1, 2, 3 };
            return attributesList;
        }

        public int[] prioritizeAttributes(int classDnD)
        {
            var rnd = new Random();
            int strength = 0; int dexterity = 0; int constitution = 0; int intelligence = 0; int wisdom = 0; int charisma = 0;
            //method that assigns attributes based on the users selected class (0 is highest priority, 5 is lowest)
            // if random placeholder
            if (classDnD == 0) { strength = 0; dexterity = 1; constitution = 2; intelligence = 3; wisdom = 4; charisma = 5; }
            //barbarian
            else if (classDnD == 1) {strength = 0; dexterity = 2; constitution = 1; intelligence = 5; wisdom = 4; charisma = 3;}
            //bard
            else if (classDnD == 2) {strength = 5; dexterity = 1; constitution = 2; intelligence = 4; wisdom = 3; charisma = 0;}
            //cleric
            else if (classDnD == 3) {strength = 1; dexterity = 3; constitution = 2; intelligence = 4; wisdom = 0; charisma = 5;}
            //druid
            else if (classDnD == 4) {strength = 4; dexterity = 2; constitution = 1; intelligence = 3; wisdom = 0; charisma = 5;}
            //fighter
            else if (classDnD == 5)
            {
                //fighter can be an archer or just a standard melee fighter; determine which here
                int flipCoin = rnd.Next(1, 3);
                //melee
                if (flipCoin == 1) {strength = 0; dexterity = 2; constitution = 1; intelligence = 5; wisdom = 4; charisma = 3;}
                //ranged
                else {strength = 3; dexterity = 0; constitution = 1; intelligence = 5; wisdom = 2; charisma = 4;}
            }
            //monk
            else if (classDnD == 6){strength = 5; dexterity = 0; constitution = 2; intelligence = 3; wisdom = 1; charisma = 4;}
            //paladin
            else if (classDnD == 7) {strength = 0; dexterity = 3; constitution = 2; intelligence = 5; wisdom = 4; charisma = 1;}
            //ranger
            else if (classDnD == 8) {strength = 5; dexterity = 0; constitution = 2; intelligence = 4; wisdom = 1; charisma = 3;}
            //rogue
            else if (classDnD == 9)
            {
                //rogue also has two styles of play to switch between
                int flipCoin = rnd.Next(1, 3);
                if (flipCoin == 1) {strength = 5; dexterity = 0; constitution = 2; intelligence = 1; wisdom = 4; charisma = 3;}
                else {strength = 5; dexterity = 0; constitution = 2; intelligence = 3; wisdom = 4;  charisma = 1;}
            }
            //sorcerer
            else if (classDnD == 10) {strength = 5; dexterity = 4; constitution = 1; intelligence = 2; wisdom = 3; charisma = 0;}
            //warlock
            else if (classDnD == 11) {strength = 5; dexterity = 4; constitution = 1; intelligence = 2; wisdom = 3; charisma = 0;}
            //wizard
            else if (classDnD == 12) {strength = 5; dexterity = 4; constitution = 1; intelligence = 2; wisdom = 3; charisma = 0;}

            //take modified variables and put them into an array
            int[] array = { strength, dexterity, constitution, intelligence, wisdom, charisma };
            return array;
        }

        public int[] raceAttributeModifier(int raceSelect, int subRaceSelect)
        {
            //attribute modifiers go in order on the array: strength, dexterity, constitution, intelligence, wisdom, charisma
            //dwarf
            int[] modifier = { 0, 0, 0, 0, 0, 0 };
            if (raceSelect == 1)
            {
                //hill
                if (subRaceSelect == 0)
                {
                    modifier = new int[] { 0, 0, 2, 0, 0, 1 };
                }
                //mountain
                else
                {
                    modifier = new int[] { 2, 0, 2, 0, 0, 0 };
                }            
            }
            //elf
            if (raceSelect == 2)
            {
                //high elf
                if (subRaceSelect == 0)
                {
                    modifier = new int[] { 0, 2, 0, 1, 0, 0 };
                }
                //wood elf
                else if (subRaceSelect == 1)
                {
                    modifier = new int[] { 0, 2, 0, 0, 1, 0 };
                }
                //dark elf
                else
                {
                    modifier = new int[] { 0, 2, 0, 0, 0, 1 };
                }
            }
            //halfling
            if (raceSelect == 3)
            {
                //lightfoot
                if (subRaceSelect == 0)
                {
                    modifier = new int[] { 0, 2, 0, 0, 0, 1 };
                }
                //stout
                else
                {
                    modifier = new int[] { 0, 2, 1, 0, 0, 0 };
                }
            }
            
            //human
            if (raceSelect == 4)
            {
                modifier = new int[] { 1, 1, 1, 1, 1, 1 };
            }

            //dragonborn
            if (raceSelect == 5)
            {
                modifier = new int[] { 2, 0, 0, 0, 0, 1 };
            }

            //gnome
            if (raceSelect == 6)
            {
                //forest
                if (subRaceSelect == 0)
                {
                    modifier = new int[] { 0, 1, 0, 2, 0, 0 };
                }
                //rock
                else
                {
                    modifier = new int[] { 0, 0, 1, 2, 0, 0 };
                }
            }

            //half elf
            if (raceSelect == 7)
            {
                modifier = new int[] { 0, 1, 0, 1, 0, 2 };
            }

            //half orc
            if (raceSelect == 8)
            {
                modifier = new int[]{ 2, 0, 1, 0, 0, 0 };
            }

            //tiefling
            if (raceSelect == 9)
            {
                modifier = new int[] { 0, 0, 0, 1, 0, 2 };
            }
            return modifier;
        }
    }
}