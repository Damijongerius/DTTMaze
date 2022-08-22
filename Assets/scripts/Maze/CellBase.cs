

/*
  This class is just so i don't see code i don't need to see
  and it makes my code look some bit cleaner 
  and in a way and more readable fpr myself
*/
public class CellBase
{
    //here i put the variables i use in Cell
    public Walls walls = new Walls();
    public bool visited;
    public int x;
    public int y;
    public class Walls
    {
        public bool wallN;
        public bool wallE;
        public bool wallS;
        public bool wallW;
    }
}