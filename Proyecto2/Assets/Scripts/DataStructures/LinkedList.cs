using System;

namespace DataStructures
{
    public class LinkedList<T> 
    {


        public Node<T> Head { get; set; }
        public int Len      { get; set; }

        public LinkedList(){
            this.Head = null;
            this.Len = 0;
        }

      

    
        public void Add( T value){
        
            var current = this.Head;

            if (this.Len==0 )
            {
                this.Head = new Node<T>(value);
            } 
            else
            {
                for (int i = 0; i < Len - 1; i ++)
                {
                    current = current.Next;
                }

                current.Next = new Node<T>(value);
            }
            Len++;

        }

        public void Remove(int index){

            var current = this.Head;
            if (current == null) return;
            if (Len==0)
                return ;

            if (index >= this.Len)
                index = Len - 1;

            for (int i = 0; i < index - i; i++)
                current = current.Next;

            current.Next = current.Next.Next;

            Len--;

        }

        public T Get(int index)
        {

            if (index >= this.Len)
                index = this.Len - 1;

            var current = this.Head;

            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            return current.Data;
        }

        public Node<T> Get(T value) {
            var current = this.Head;

            for (int i = 0; i < Len; i++) {

               // if (current.Compare(value) > 0) {break;}
                current = current.Next;
            }
            return current;
        }


        public class Node<TR> {
        
    
            public Node<TR> Next{get; set; }
            public TR Data   {get; set; }
            public Node(TR t){
                Next = null;
                Data = t;
            }

         

          
        }
    }
}
