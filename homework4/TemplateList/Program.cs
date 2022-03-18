using System;
using System.Collections.Generic;

namespace TemplateList
{
    //public class Node<T>
    //{
    //    public Node<T> Next { get; set; }
    //    public T Data { get; set; }
    //    public Node(T t)
    //    {
    //        Next = null;
    //        Data = t;
    //    }
    //}

    //public class TemplateList<T>
    //{
    //    private Node<T> head, tail;
    //    public TemplateList()
    //    {
    //        head = tail = null;
    //    }
    //    public Node<T> Head
    //    {
    //        get => head;
    //    }

    //    public void Insert(T t)
    //    {
    //        Node<T> node = new Node<T>(t);
    //        if (tail == null)
    //        {
    //            head = tail = node;
    //        }
    //        else
    //        {
    //            tail.Next = node;
    //            tail = node;
    //        }
    //    }

    //    public delegate int ForEach(Action<T> action);//foreach 和 action之间的关系？ 谁调用foreach？（main）
        
    //    public void print(TemplateList<T> L)
    //    {
    //        for(Node<T> tmp=L.head; tmp!=null;tmp = tmp.Next)
    //        {
    //            Console.WriteLine(tmp.Data);
    //        }   
    //    }
    //}

    class Program
    {
        
        static void Main(string[] args)
        {
            List<int> l = new List<int>();
            for(int i = 0; i != 10; ++i)
            {
                l.Add(i);
            }
            int min=10, max=0, sum=0;
            l.ForEach(Data=> { 
                sum += Data;
                min = Data > min ? min : Data;
                max = Data < max ? max : Data;
            });
        }
    }
}
