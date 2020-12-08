namespace Things
{
    public class message
    {
        public bool isActive;
        public string type;
        public int value;

        public message(string typeofItem, int pos, bool state)
        {
            isActive = state;
            type     = typeofItem;
            value = pos;
        }
    }
}