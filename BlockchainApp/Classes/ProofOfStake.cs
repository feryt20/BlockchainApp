//namespace BlockchainApp.Classes
//{
//    public class ProofOfStake
//    {
//        private Dictionary<string, int> Stakes { get; set; }

//        public ProofOfStake()
//        {
//            Stakes = new Dictionary<string, int>();
//        }

//        public void AddStake(string node, int amount)
//        {
//            if (Stakes.ContainsKey(node))
//            {
//                Stakes[node] += amount;
//            }
//            else
//            {
//                Stakes[node] = amount;
//            }
//        }

//        public string SelectValidator()
//        {
//            int totalStakes = Stakes.Values.Sum();
//            Random random = new Random();
//            int selected = random.Next(totalStakes);
//            int current = 0;

//            foreach (var stake in Stakes)
//            {
//                current += stake.Value;
//                if (current >= selected)
//                {
//                    return stake.Key;
//                }
//            }

//            return null;
//        }
//    }
//}
