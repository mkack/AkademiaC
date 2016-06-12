using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Statki.Model
{
    class Gracz
    {
        protected const int RozmiarPola = 10;
        
        static protected Random rnd = new Random();

        public List<List<Morze>> MojePole { get; set; }
        public List<List<Morze>> PolePrzeciwnika { get; set; }

        private List<Statek> _mojeStatki = new List<Statek>();
        private List<Statek> _statkiPrzeciwnika = new List<Statek>();

        public Gracz()
        {
            MojePole = new List<List<Morze>>();
            PolePrzeciwnika = new List<List<Morze>>();

            for (int i = 0; i != RozmiarPola; ++i)
            {
                MojePole.Add(new List<Morze>());
                PolePrzeciwnika.Add(new List<Morze>());

                for (int j = 0; j != RozmiarPola; ++j)
                {
                    MojePole[i].Add(new Morze(i, j));
                    PolePrzeciwnika[i].Add(new Morze(i, j));
                }
            }

            foreach (RodzajStatku type in Enum.GetValues(typeof(RodzajStatku)))
            {
                _mojeStatki.Add(new Statek(type));
                _statkiPrzeciwnika.Add(new Statek(type));
            }

            Reset();
        }

        public void Reset()
        {
            for (int i = 0; i != RozmiarPola; ++i)
            {
                for (int j = 0; j != RozmiarPola; ++j)
                {
                    MojePole[i][j].Reset(RodzajPola.Woda);
                    PolePrzeciwnika[i][j].Reset(RodzajPola.Nieznane);
                }
            }

            _mojeStatki.ForEach(s => s.Reincarnate());
            _statkiPrzeciwnika.ForEach(s => s.Reincarnate());
            RozmieszczenieStatkow();
        }

        private bool WolnePole(int row, int col)
        {
            return (MojePole[row][col].ShipIndex == -1) ? true : false;
        }

        private bool PlaceVertical(int shipIndex, int remainingLength)
        {
            int startPosRow = rnd.Next(RozmiarPola - remainingLength);
            int startPosCol = rnd.Next(RozmiarPola);

            Func<bool> PlacementPossible = () =>
            {
                int tmp = remainingLength;
                for (int row = startPosRow; tmp != 0; ++row)
                {
                    if (!WolnePole(row, startPosCol))
                        return false;
                    --tmp;
                }
                return true;
            };

            if (PlacementPossible())
            {
                for (int row = startPosRow; remainingLength != 0; ++row)
                {
                    MojePole[row][startPosCol].Type = RodzajPola.Nietrafiony;
                    MojePole[row][startPosCol].ShipIndex = shipIndex;
                    --remainingLength;
                }
                return true;
            }

            return false;
        }

        private bool PlaceHorizontal(int shipIndex, int remainingLength)
        {
            int startPosRow = rnd.Next(RozmiarPola);
            int startPosCol = rnd.Next(RozmiarPola - remainingLength);

            Func<bool> PlacementPossible = () =>
            {
                int tmp = remainingLength;
                for (int col = startPosCol; tmp != 0; ++col)
                {
                    if (!WolnePole(startPosRow, col))
                        return false;
                    --tmp;
                }
                return true;
            };

            if (PlacementPossible())
            {
                for (int col = startPosCol; remainingLength != 0; ++col)
                {
                    MojePole[startPosRow][col].Type = RodzajPola.Nietrafiony;
                    MojePole[startPosRow][col].ShipIndex = shipIndex;
                    --remainingLength;
                }
                return true;
            }

            return false;
        }

        private void RozmieszczenieStatkow()
        {
            bool startAgain = false;

            for (int i = 0; i != _mojeStatki.Count && !startAgain; ++i)
            {
                bool vertical = Convert.ToBoolean(rnd.Next(2));
                bool placed = false;

                int loopCounter = 0;
                for (; !placed && loopCounter != 10000; ++loopCounter)
                {
                    int remainingLength = _mojeStatki[i].Dlugosc;

                    if (vertical)
                        placed = PlaceVertical(i, remainingLength);
                    else
                        placed = PlaceHorizontal(i, remainingLength);
                }

                if (loopCounter == 10000)
                    startAgain = true;
            }

            if (startAgain)
                RozmieszczenieStatkow();
        }

        private void ZatopionyStatek(int i, List<List<Morze>> grid)
        {
            foreach (var row in grid)
            {
                foreach (var square in row)
                {
                    if (square.ShipIndex == i)
                        square.Type = RodzajPola.Zatopiony;
                }
            }
        }

        private void MojZatopiony(int i)
        {
            ZatopionyStatek(i, MojePole);
        }

        public void ZatopionyKompa(int i)
        {
            ZatopionyStatek(i, PolePrzeciwnika);
        }


        protected void Strzal(int row, int col, Gracz otherPlayer)
        {
            int damagedIndex;
            bool zatopiony;
            RodzajPola newType = otherPlayer.Strzal(row, col, out damagedIndex, out zatopiony);
            PolePrzeciwnika[row][col].ShipIndex = damagedIndex;

            if (zatopiony)
                ZatopionyKompa(damagedIndex);
            else
                PolePrzeciwnika[row][col].Type = newType;
        }

        public RodzajPola Strzal(int row, int col, out int damagedIndex, out bool zatopiony)
        {
            zatopiony = false;
            damagedIndex = -1;

            switch (MojePole[row][col].Type)
            {
                case RodzajPola.Woda:
                    return RodzajPola.Woda;
                case RodzajPola.Nietrafiony:
                    var square = MojePole[row][col];
                    damagedIndex = square.ShipIndex;
                    if (_mojeStatki[damagedIndex].Trafiony())
                    {
                        MojZatopiony(square.ShipIndex);
                        zatopiony = true;
                    } else {
                        square.Type = RodzajPola.Trafiony;
                    }
                    return square.Type;
                case RodzajPola.Trafiony:
                    goto default;
                case RodzajPola.Nieznane:
                    goto default;
                case RodzajPola.Zatopiony:
                    goto default;
                default:
                    throw new Exception("blad");
            }
        }

        public bool BrakStatkow()
        {
            return _mojeStatki.All(ship => ship.Zatopiony);
        }

        public void RuchAutomatyczny(Gracz otherPlayer)
        {
            bool takenShot = false;
            while (!takenShot)
            {
                int row = rnd.Next(RozmiarPola);
                int col = rnd.Next(RozmiarPola);

                if (PolePrzeciwnika[row][col].Type == RodzajPola.Nieznane)
                {
                    Strzal(row, col, otherPlayer);
                    takenShot = true;
                }
            }
        }
    }
}
