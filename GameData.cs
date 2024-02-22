using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeroMaker
{
    public class GameData
    {
        private static GameData? _instance;

        public Mixture Mixture = new Mixture();

        // Obiekt blokujący używany do synchronizacji w środowisku wielowątkowym.
        private static readonly object _lock = new object();

        // Prywatny konstruktor, aby zapobiec tworzeniu instancji z zewnątrz.
        private GameData() { }

        // Publiczna właściwość statyczna zapewniająca dostęp do instancji.
        public static GameData Instance
        {
            get
            {
                // Podwójne sprawdzenie blokady, aby zapewnić bezpieczeństwo w środowisku wielowątkowym.
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new GameData();
                        }
                    }
                }
                return _instance;
            }
        }

        // Tutaj dodaj pola i metody swojej klasy.
    }
}
