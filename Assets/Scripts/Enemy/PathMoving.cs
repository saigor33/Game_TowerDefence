using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TaskGame_TowerDefence
{
    public class PathMoving : MonoBehaviour
    {
        [SerializeField] private EdgeCollider2D _pathMovingCollider;

        public LinkedList<Vector2> ListPositionPathMoving { get; set; }

        private void Awake()
        {
            if (_pathMovingCollider == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлен путь (EdgeCollider2D), по которуму будет двигаться противник PathMovingCollider");
           
            InitPathMoving();
        }

        /// <summary>
        /// Создание пути движения противника в зависимости от нарисованнного EdgeCollider2D
        /// </summary>
        private void InitPathMoving()
        {
            if(_pathMovingCollider.points.Length ==0)
                Debug.LogError($"TaskGame_TowerDefence ({this}): В EdgeCollider2D нет точек по которым должны следовать противники");


            ListPositionPathMoving = new LinkedList<Vector2>();

            for (int i=0; i< _pathMovingCollider.points.Length; i++)
                ListPositionPathMoving.Add(_pathMovingCollider.points[i]);
        }


    }

    public class Node<T>
    {
        public Node(T data)
        {
            Data = data;
        }
        public T Data { get; set; }
        public Node<T> Next { get; set; }
    }

    public class LinkedList<T>
    {
        private int _count;

        public Node<T> Head { get; set; }
        public Node<T> Tail { get; set; }


        public void Add(T data)
        {
            Node<T> node = new Node<T>(data);

            if (Head == null)
                Head = node;
            else
                Tail.Next = node;
            Tail = node;

            _count++;
        }

        public bool Remove(T data)
        {
            Node<T> current = Head;
            Node<T> previous = null;

            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;
                        if (current.Next == null)
                            Tail = previous;
                    }
                    else
                    {
                        Head = Head.Next;

                        if (Head == null)
                            Tail = null;
                    }
                    _count--;
                    return true;
                }

                previous = current;
                current = current.Next;
            }
            return false;
        }

        public int Count { get { return _count; } }
        public bool IsEmpty { get { return _count == 0; } }

        public void Clear()
        {
            Head = null;
            Tail = null;
            _count = 0;
        }

        // в случае необходимости добавить другие методы
    }


}

