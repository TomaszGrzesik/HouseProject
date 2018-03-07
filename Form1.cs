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
            livingRoom = new RoomWithDoor("Living room", "Antic carpet","wardrobe", "Wooden door");
            diningRoom = new Room("Dinning room", "Crystal chandelier");
            kitchen = new RoomWithDoor("Kitchen", "steel cutlery", "cupboard", "steel door");
            stairs = new Room("Stairs", "wooden handrail");
            hallway = new RoomWithHidingPlace("Hallway 1st floor", "long carpet", "wardrobe");
            bathroom = new RoomWithHidingPlace("Bathroom", "amazing bath", "curtain");
            masterBedroom = new RoomWithHidingPlace("Bedroom", "big bed", "bed");
            secondBedroom = new RoomWithHidingPlace("Second Bedroom", "second big bed", "second bed");

            driveway = new OutsideWithHidingPlace("Road",true,"garage");
            garden = new OutsideWithHidingPlace("Garden", false, "shed");
            frontYard = new OutsideWithDoor("Front yard", false, "Wooden door");
            backYard = new OutsideWithDoor("Back yard", true, "steel door");
 

            diningRoom.Exits = new Location[] {livingRoom, kitchen };
            livingRoom.Exits = new Location[] { diningRoom};
            kitchen.Exits = new Location[] { diningRoom};
            stairs.Exits = new Location[] { livingRoom, hallway };
            hallway.Exits = new Location[] { stairs, bathroom, masterBedroom, secondBedroom };
            bathroom.Exits = new Location[] { hallway };
            masterBedroom.Exits = new Location[] { hallway };
            secondBedroom.Exits = new Location[] { hallway };

            frontYard.Exits = new Location[] { backYard, garden};
            backYard.Exits = new Location[] { frontYard, garden};
            garden.Exits = new Location[] { backYard, frontYard};
            driveway.Exits = new Location[] { backYard, frontYard };

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

        private void ResetGame(bool displayMessage)
        {
            if (displayMessage)
            {
                MessageBox.Show("You found me after" + Moves + "moves");
                IHidingPlace foundLocation = currentLocation as IHidingPlace;
                description.Text = "You found enemy after " + Moves + "moves, he was " + foundLocation.HidingPlace + ".";
            }
            Moves = 0;
            hide.Visible = true;
            goHere.Visible = false;
            check.Visible = false;
            goThroughTheDoor.Visible = false;
            exits.Visible = false;
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

        private void check_Click(object sender, EventArgs e)
        {
            Moves++;
            if (opponent.Check(currentLocation))
                ResetGame(true);
            else
                RedrawForm();

        }

        private void hide_Click(object sender, EventArgs e)
        {
            hide.Visible = false;

            for(int i = 1; i <= 10; i++)
            {
                opponent.Move();
                description.Text = i + "...";
                Application.DoEvents();
                System.Threading.Thread.Sleep(200);
            }

            description.Text = "Ready or not - I'M COMING!";
            Application.DoEvents();
            System.Threading.Thread.Sleep(500);

            goHere.Visible = true;
            exits.Visible = true;
            MoveToNewLocation(livingRoom);
        }
    }
}
