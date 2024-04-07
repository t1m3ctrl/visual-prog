using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Reactive.Linq;

namespace CollectionChangeEventFactory
{
    public static class CollectionChangeEventFactory
    {
        public static IObservable<NotifyCollectionChangedEventArgs> CreateObservable(ObservableCollection<object> collection)
        {
            return Observable.FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                    handler => collection.CollectionChanged += handler,
                    handler => collection.CollectionChanged -= handler)
                .Select(pattern => pattern.EventArgs);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var collection = new ObservableCollection<object>();

            // Подписываемся на событие изменения коллекции
            var observable = CollectionChangeEventFactory.CreateObservable(collection);

            // Подписываемся на событие и логируем данные об изменениях в файл
            observable.Subscribe(args =>
            {
                LogToFile(args);
            });

            // Добавляем элементы для демонстрации
            collection.Add("Item 1");
            collection.Add("Item 2");
            collection.Remove("Item 1");
            collection.Add("Item 3");

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static void LogToFile(NotifyCollectionChangedEventArgs args)
        {
            string logMessage = $"Action: {args.Action}, NewItems: {args.NewItems}, OldItems: {args.OldItems}";
            File.AppendAllText("log.txt", logMessage + Environment.NewLine);
        }
    }
}
