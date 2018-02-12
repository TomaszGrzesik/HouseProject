using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HouseProject
{
    public partial class Form1 : Form
    {
        Location currentLocation;

        RoomWithDoor livingRoom;
        Room diningRoom;
        RoomWithDoor kitchen;

        OutsideWithDoor frontYard;
        OutsideWithDoor backYard;
        Outside garden;


        public Form1()
        {
            InitializeComponent();
            CreateObjects();
            MoveToNewLocation(livingRoom);
        }

        private void CreateObjects()
        {
            livingRoom = new RoomWithDoor("Living room", "Antic carpet", "Wooden door");
            diningRoom = new Room("Dinning room", "Crystal chandelier");
            kitchen = new RoomWithDoor("Kitchen", "steel cutlery", "steel door");

            garden = new Outside("Garden", false);
            frontYard = new OutsideWithDoor("Front yard", false, "Wooden door");
            backYard = new OutsideWithDoor("Back yard", true, "steel door");

            diningRoom.Exits = new Location[] {livingRoom, kitchen };
            livingRoom.Exits = new Location[] { diningRoom};
            kitchen.Exits = new Location[] { diningRoom};
            frontYard.Exits = new Location[] { backYard, garden};
            backYard.Exits = new Location[] { frontYard, garden};
            garden.Exits = new Location[] { backYard, frontYard};

            livingRoom.DoorLocation = frontYard;
            frontYard.DoorLocation = livingRoom;

            kitchen.DoorLocation = backYard;
            backYard.DoorLocation = kitchen;
        }


       
        private void MoveToNewLocation(Location newLocation)
        {
            currentLocation = newLocation;
            exits.Items.Clear();
            for (int i = 0; i < currentLocation.Exits.Length; i++)
                exits.Items.Add(currentLocation.Exits[i].Name);
            exits.SelectedIndex = 0;

            description.Text = currentLocation.Description;

            if (currentLocation is IHasExteriorDoor)
                goThroughTheDoor.Visible = true;
            else
                goThroughTheDoor.Visible = false;

        }

        private void goThroughTheDoor_Click(object sender, EventArgs e)
        {
            IHasExteriorDoor hasDoor = currentLocation as IHasExteriorDoor;
            MoveToNewLocation(hasDoor.DoorLocation);
        }

        private void goHere_Click(object sender, EventArgs e)
        {
            MoveToNewLocation(currentLocation.Exits[exits.SelectedIndex]);
        }
    }
}
