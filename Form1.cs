using System;
using System.Windows.Forms;

namespace HouseProject
{
    public partial class Form1 : Form
    {
        int Moves;

        Location currentLocation;

        RoomWithDoor livingRoom;
        Room diningRoom;
        Room stairs;
        RoomWithDoor kitchen;
        RoomWithHidingPlace hallway;
        RoomWithHidingPlace bathroom;
        RoomWithHidingPlace masterBedroom;
        RoomWithHidingPlace secondBedroom;

        OutsideWithDoor frontYard;
        OutsideWithDoor backYard;
        OutsideWithHidingPlace garden;
        OutsideWithHidingPlace driveway;

        Opponent opponent;

        public Form1()
        {
            InitializeComponent();
            CreateObjects();
            MoveToNewLocation(livingRoom);
            opponent = new Opponent(frontYard);
            ResetGame(false);
        }

        private void CreateObjects()
        {
            livingRoom = new RoomWithDoor("Living room", "Antic carpet", "Wooden door");
            diningRoom = new Room("Dinning room", "Crystal chandelier");
            kitchen = new RoomWithDoor("Kitchen", "steel cutlery", "steel door");


            garden = new OutsideWithHidingPlace("Garden", false);
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
            Moves++;
            currentLocation = newLocation;
            RedrawForm();
        }

        private void RedrawForm()
        {
            exits.Items.Clear();
            for (int i = 0; i < currentLocation.Exits.Length; i++)
                exits.Items.Add(currentLocation.Exits[i].Name);
            exits.SelectedIndex = 0;
            description.Text = currentLocation.Description + "\r\n(step number " + Moves + ")";

            if (currentLocation is IHidingPlace)
            {
                IHidingPlace hidingPlace = currentLocation as IHidingPlace;
                check.Text = "Check" + hidingPlace.HidingPlace;
                check.Visible = true;
            }
            else
                check.Visible = false;
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
