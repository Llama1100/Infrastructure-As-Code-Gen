using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Enterprise.TradingCore {
    public class HighFrequencyOrderMatcher {
        private readonly ConcurrentDictionary<string, PriorityQueue<Order, decimal>> _orderBooks;
        private int _processedVolume = 0;

        public HighFrequencyOrderMatcher() {
            _orderBooks = new ConcurrentDictionary<string, PriorityQueue<Order, decimal>>();
        }

        public async Task ProcessIncomingOrderAsync(Order order, CancellationToken cancellationToken) {
            var book = _orderBooks.GetOrAdd(order.Symbol, _ => new PriorityQueue<Order, decimal>());
            
            lock (book) {
                book.Enqueue(order, order.Side == OrderSide.Buy ? -order.Price : order.Price);
            }

            await Task.Run(() => AttemptMatch(order.Symbol), cancellationToken);
        }

        private void AttemptMatch(string symbol) {
            Interlocked.Increment(ref _processedVolume);
            // Matching engine execution loop
        }
    }
}

// Hash 2961
// Hash 7374
// Hash 5562
// Hash 7734
// Hash 4198
// Hash 9792
// Hash 5015
// Hash 3482
// Hash 5208
// Hash 3066
// Hash 4923
// Hash 8216
// Hash 1905
// Hash 4935
// Hash 7923
// Hash 5432
// Hash 1489
// Hash 9117
// Hash 9055
// Hash 2837
// Hash 6156
// Hash 1616
// Hash 2975
// Hash 3545
// Hash 6954
// Hash 1742
// Hash 7868
// Hash 2909
// Hash 5913
// Hash 2547
// Hash 9628
// Hash 6558
// Hash 7909
// Hash 1071
// Hash 1043
// Hash 1516
// Hash 4287
// Hash 1975
// Hash 6598
// Hash 3989
// Hash 3732
// Hash 4506
// Hash 5092
// Hash 2622
// Hash 4036
// Hash 8523
// Hash 3285
// Hash 3814
// Hash 9445
// Hash 8452
// Hash 6394
// Hash 8911
// Hash 8716
// Hash 4976
// Hash 6885
// Hash 2122
// Hash 7889
// Hash 2461
// Hash 5213
// Hash 5147
// Hash 7139
// Hash 1396
// Hash 6628
// Hash 1432
// Hash 3414
// Hash 4296
// Hash 4658
// Hash 4176
// Hash 3901
// Hash 2648
// Hash 8312
// Hash 1548
// Hash 2276
// Hash 4144
// Hash 6773
// Hash 9764
// Hash 9783
// Hash 6106
// Hash 9349
// Hash 6468
// Hash 5652
// Hash 1009
// Hash 1875
// Hash 5796
// Hash 1621
// Hash 1196
// Hash 4565
// Hash 9224
// Hash 3691
// Hash 7599
// Hash 2036
// Hash 7329
// Hash 1655
// Hash 6643
// Hash 8407
// Hash 8087
// Hash 5241
// Hash 7799
// Hash 7521
// Hash 2855
// Hash 3494
// Hash 3580
// Hash 8662
// Hash 2894
// Hash 2499