namespace Things
{
    public class message
    {
        public int action;
        public string type;
        public int value;

        public static int Write = 1 ;
        public static int Overwrite=2;
        public static int Erase=  3;

        public message(string typeofItem, int pos, int state)
        {
            action   = state;
            type     = typeofItem;
            value    = pos;
        }
    }
}