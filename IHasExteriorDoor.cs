namespace HouseProject
{
    interface IHasExteriorDoor
    {
        string DoorDescription{ get; }
        Location DoorLocation { get; set; }
    }
}
