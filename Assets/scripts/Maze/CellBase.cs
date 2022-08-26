

/*
  This class is just so i don't see code i don't need to see
  and it makes my code look some bit cleaner 
  and in a way and more readable fpr myself
*/
public class CellBase
{

    //bool for if its the end or the start
    public bool start;
    public bool end;
    
    //here are the variable used in the cell
    public bool visited;
    public int x;
    public int y;
    public bool[] walls = new bool[] { true, true, true, true, true };
                                   // floor, north, south, east, west; 
}